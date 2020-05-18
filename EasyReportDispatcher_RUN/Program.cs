using System;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;
using Bdo.Objects;
using EasyReportDispatcher_Lib_Common.src.enums;
using EasyReportDispatcher_Lib_DAL.src.report;
using EasyReportDispatcher_Lib_BIZ.src.report;
using System.IO;
using Bdo.Logging;
using Amib.Threading;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace EasyReportDispacher_RUN
{
    class Program
    {

        private static BusinessSlot _Slot;
        private static bool _SendMail = true;
        private static int[] _ReportForzati = { };

        private static string _TaskLogDir;
        private static string _TaskLogFile;
        private static FileStreamLogger _TaskLogger;

        static void Main(string[] args)
        {
            printInfo();

            init();

            var rc = TaskExecute();

            _TaskLogger.Dispose();

            Environment.Exit(rc);
        }



        private const string SEP = @"======================================================================================================";


        private static void init()
        {
            WriteLog(@"Inizializzazione");

            //Slot
            _Slot = new BusinessSlot(@"Default");

            //Directory log
            _TaskLogDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ERD");

            if (!string.IsNullOrWhiteSpace(EasyReportDispatcher_RUN.Properties.Settings.Default.TaskLogDir))
                _TaskLogDir = EasyReportDispatcher_RUN.Properties.Settings.Default.TaskLogDir;

            //Assicura creazione directory
            Directory.CreateDirectory(_TaskLogDir);

            //Nome file di log 
            _TaskLogFile = Path.Combine(_TaskLogDir, string.Format(@"erd_{0:yyyy_MM_dd}.log", DateTime.Now));

            //Pulizia log
            if (EasyReportDispatcher_RUN.Properties.Settings.Default.TaskLogKeepNum > 0)
                keepLogFiles();

            //Avvia logger
            _TaskLogger = new FileStreamLogger(_TaskLogFile);
        }

        private static void keepLogFiles()
        {
            WriteLog(@"Pulizia logs");
            
            var dirInfo = new DirectoryInfo(_TaskLogDir);
            var delFiles = dirInfo.GetFiles(@".log").OrderByDescending(f => f.LastWriteTime).Skip(EasyReportDispatcher_RUN.Properties.Settings.Default.TaskLogKeepNum - 1);

            WriteLog(@"  > Eliminazione {0} logs", delFiles.Count());

            foreach (var item in delFiles)
            {
                item.Delete();
            }

        }


        protected static int TaskExecute()
        {

            var esito = 0;

            var bSendEmail = _SendMail;
            var smPool = new SmartThreadPool();


            WriteLog("Invio email attivo: {0}", bSendEmail.ToString());

            using (_Slot)
            {
                WriteLog(@"Caricamento estrazioni da valutare");

                var reports = _Slot.CreateList<ReportEstrazioneLista>()
                    .SearchByColumn(new FilterEQUAL(nameof(ReportEstrazione.Attivo), 1)
                    .And(new FilterLESSEQ(nameof(ReportEstrazione.DataInizio), DateTime.Today)
                    .And(new FilterGREATEREQ(nameof(ReportEstrazione.DataFine), DateTime.Today))));

                WriteLog("Numero estrazioni da valutare: {0}", reports.Count);

                WriteLog(@"Avvio valutazione estrazioni");
                //Usa i biz
                var reportsBiz = new ReportEstrazioneBIZ_Lista(reports);

                //Se presenti dei report da forzare li filtra dall'elenco
                IEnumerable<ReportEstrazioneBIZ> reportExec;
                if (_ReportForzati.Length > 0)
                {
                    //Include solo i report forzati
                    reportExec = reportsBiz.Where(r => _ReportForzati.Contains(r.DataObj.Id)).ToList();
                }
                else
                {
                    //Include solo i report che rientrano nella schedulazione
                    reportExec = reportsBiz.Where(r => r.CanRunSchedulato).ToList();
                }

                var iCount = reportExec.Count();

                WriteLog("Numero estrazioni da eseguire: {0}", iCount);

                //new

                //Raggruppa per connessione
                var connGroup = reportExec.GroupBy(r => r.DataObj.ConnessioneId);

                WriteLog("Numero estrazioni parallele: {0}", connGroup.Count());

                //Carica un threadgroup per ogni connessione
                foreach (var gp in connGroup)
                {
                    var cfg = new WIGStartInfo() { StartSuspended = true };
                    var wig = smPool.CreateWorkItemsGroup(1);

                    foreach (var est in gp)
                    {
                        
                        wig.QueueWorkItem(execSingleReport, 
                            new { EstrazioneId=est.DataObj.Id, SendEmail = bSendEmail },
                            r =>  esito += ((!r.IsCompleted || r.Exception != null || (bool)r.Result==false) ? 1 : 0)
                            );
                    }
                }
            }

            smPool.Start();
            smPool.WaitForIdle();

            if (EasyReportDispatcher_RUN.Properties.Settings.Default.TaskLogMail)
                sendRunLog(esito);

            return esito;
        }


        private static void sendRunLog(int esito)
        {
            if (string.IsNullOrWhiteSpace(EasyReportDispatcher_RUN.Properties.Settings.Default.TaskLogMailTO))
                return;

            using (var smtp = new System.Net.Mail.SmtpClient())
            {
                var msg = new System.Net.Mail.MailMessage();
                msg.To.Add(EasyReportDispatcher_RUN.Properties.Settings.Default.TaskLogMailTO);
                msg.CC.Add(EasyReportDispatcher_RUN.Properties.Settings.Default.TaskLogMailCC);
                msg.Subject = esito == 0 ? "OK - Report Dispatcher" : "ERR - Report Dispatcher";
                msg.IsBodyHtml = true;
                msg.Body = "In allegato il log dell'esecuzione.";
                msg.Attachments.Add(new System.Net.Mail.Attachment(_TaskLogFile));

                smtp.Send(msg);
            }

        }

        private static object execSingleReport(dynamic state)
        {
            var retObj = true;
            var repDefId = state.EstrazioneId;
            var bSendEmail = state.SendEmail;

            using (var slot = new BusinessSlot(@"Default"))
            {
                WriteLog(SEP);

                ReportEstrazioneBIZ repBiz = slot.BizNewWithLoadByPK<ReportEstrazioneBIZ>(repDefId);
                //RaiseOnProgress(index, iCount);
                WriteLog("Avvio report {0}", repBiz.DataObj.Nome);
                try
                {
                    //Esegue
                    WriteLog("Esecuzione");
                    repBiz.Run(true);

                    //Invia email
                    if (bSendEmail)
                    {

                        WriteLog("Previsto invio mail {0}", repBiz.IsPrevistoInvioMail ? "SI" : "NO");

                        if (repBiz.IsPrevistoInvioMail)
                        {
                            var mails = repBiz.SendEmail(true);

                            foreach (var item in mails)
                            {
                                WriteLog("Mail inviata");
                                WriteLog(" >> MailTO: {0}", item.MailTO);
                                WriteLog(" >> MailCC: {0}", item.MailCC);
                                WriteLog(" >> MailBCC: {0}", item.MailBCC);
                            }


                        }
                    }

                }
                catch (Exception e)
                {
                    WriteLog("**Errore report {0}: {1}", repBiz.DataObj.Nome, e.Message);
                    WriteLog(e.StackTrace);
                    retObj = false;
                }
                finally
                {
                    WriteLog("Fine report {0}", repBiz.DataObj.Nome);
                }
            }

            return retObj;
        }

        private static void printInfo()
        {
            WriteLog("*****************************************");
            WriteLog("*                                       *");
            WriteLog("*        EASY REPORT DISPATCHER         *");
            WriteLog("*            SCHEDULE RUN v1.0          *");
            WriteLog("*                                       *");
            WriteLog("*                                       *");
            WriteLog("*****************************************");
            WriteLog("");
            WriteLog("");
        }

        private static object _logLoc = new object();
        private static void WriteLog(string logFmt, params object[] args)
        {
            lock(_logLoc)
            {
                Console.Write(string.Format(@"{0:yyyy-MM-dd HH:mm:ss}  -  ", DateTime.Now));
                Console.WriteLine(logFmt, args);

                //Altro
                if (_TaskLogger != null)
                {
                    _TaskLogger.LogMessage(string.Concat(@" ", logFmt), args);
                }
            }

        }

    }
}

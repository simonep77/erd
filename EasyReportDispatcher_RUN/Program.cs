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
using System.Threading;

namespace EasyReportDispacher_RUN
{
    class Program
    {

        private static BusinessSlot _Slot;
        private static bool _SendMail = true;
        private static string _TaskLogDir;
        private static string _TaskLogFile;
        private static FileStreamLogger _TaskLogger;
        private static Mutex _Mutex;

        static void Main(string[] args)
        {
            var rc = TaskExecute();

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

            WriteLog(@"Creazione directory log");

            //Assicura creazione directory
            Directory.CreateDirectory(_TaskLogDir);

            //Nome file di log 
            _TaskLogFile = Path.Combine(_TaskLogDir, string.Format(@"erd_{0:yyyyMMdd_HHmmss}.log", DateTime.Now));

            WriteLog(@"Avvio pulizia log");

            //Pulizia log
            if (EasyReportDispatcher_RUN.Properties.Settings.Default.TaskLogKeepNum > 0)
                keepLogFiles();

            WriteLog(@"Avvio nuovo file log");

            //Avvia logger
            _TaskLogger = new FileStreamLogger(_TaskLogFile);

            //Scrive testata
            printInfo();

            //Verifica esecuzione unica
            oneInstance();
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

            try
            {
                //inizializza
                init();

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

                    //Include solo i report che rientrano nella schedulazione
                    var reportExec = reportsBiz.Where(r => r.CanRunSchedulato).ToList();

                    WriteLog("Numero estrazioni da eseguire: {0}", reportExec.Count());

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
                                new { EstrazioneId = est.DataObj.Id, SendEmail = bSendEmail },
                                r => esito += ((!r.IsCompleted || r.Exception != null || (bool)r.Result == false) ? 1 : 0)
                                );
                        }
                    }
                }

                smPool.Start();
                smPool.WaitForIdle();

                WriteLog(@"Fine esecuzione");
            }
            catch(Exception ex)
            {
                esito = 1;
                WriteLog(@"Errore esecuzione: {0}", ex.Message);
                WriteLog(@"Stack: {0}", ex.StackTrace);

            }
            finally
            {
                if (_Mutex != null)
                    _Mutex.Dispose();
                //Chiude log per invio tramite email
                _TaskLogger.Dispose();
            }

            try
            {
                if (EasyReportDispatcher_RUN.Properties.Settings.Default.TaskLogMail)
                    sendRunLog(esito);

            }
            catch (Exception)
            {
                esito = 1;
            }
            
            return esito;
        }


        /// <summary>
        /// Crea mutex per esecuzione a singola istanza
        /// </summary>
        private static void oneInstance()
        {
            bool createdNew;

            _Mutex = new Mutex(true, "ERD_Mutex_Run", out createdNew);

            if (!createdNew)
                throw new ApplicationException("E' già in esecuzione un'altra istanza dell'applicazione");
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
                //Scrive nel log il debug User1
                slot.OnLogDebugSent += ((a, b, c) => { if (b == DebugLevel.User_1) WriteLog(c); });
                
                ReportEstrazioneBIZ repBiz = slot.BizNewWithLoadByPK<ReportEstrazioneBIZ>(repDefId);

                ThreadData.ReportId = repDefId;
                ThreadData.Reportname = repBiz.DataObj.Nome;
                
                WriteLog(SEP);

                //RaiseOnProgress(index, iCount);
                WriteLog("Avvio report");
                try
                {
                    //Esegue
                    WriteLog("Esecuzione");
                    repBiz.Run(true, bSendEmail, true);

                    //Invia email
                    if (!bSendEmail)
                        WriteLog("Blocco esplicito invio email");

                }
                catch (Exception e)
                {
                    WriteLog("**Errore report: {0}", e.Message);
                    WriteLog(e.StackTrace);
                    retObj = false;
                }
                finally
                {
                    WriteLog("Fine report");
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
            var dtNow = DateTime.Now;
            var thName = ThreadData.ReportId == 0 ? string.Empty : string.Concat(@"[", ThreadData.ReportId.ToString(),@"] ", ThreadData.Reportname, @" - ");

            lock(_logLoc)
            {
                Console.Write(string.Format($"{dtNow:yyyy-MM-dd HH:mm:ss} - {thName}"));
                Console.WriteLine(logFmt, args);

                //Altro
                if (_TaskLogger != null)
                {
                    _TaskLogger.LogMessage(string.Concat(@" ", thName, logFmt), args);
                }
            }

        }

        /// <summary>
        /// Classe con dati volatili relativi al singolo thread
        /// </summary>
        private static class ThreadData
        {
            [ThreadStatic]
            public static int ReportId;

            [ThreadStatic]
            public static string Reportname;
        }

    }
}

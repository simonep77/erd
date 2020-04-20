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

namespace EasyReportDispacher_RUN
{
    class Program
    {

        private static BusinessSlot _Slot;
        private static bool _SendMail = true;
        private static int[] _ReportForzati = { };

        private static string _TaskLogDir;
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

            //Pulizia log
            if (EasyReportDispatcher_RUN.Properties.Settings.Default.TaskLogKeepNum > 0)
                keepLogFiles();

            //Avvia logger
            _TaskLogger = new FileStreamLogger(Path.Combine(_TaskLogDir, string.Format(@"erd_{0:yyyy_MM_dd}.log", DateTime.Now)));
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
                var index = 1;

                WriteLog("Numero estrazioni da eseguire: {0}", iCount);


                foreach (var repBiz in reportExec)
                {
                    WriteLog(SEP);
                    //RaiseOnProgress(index, iCount);
                    WriteLog("Avvio report {0}", repBiz.DataObj.Nome);
                    try
                    {
                        //Esegue
                        repBiz.Run();

                        //Invia email
                        if (bSendEmail)
                        {

                            WriteLog("Previsto invio mail {0}", repBiz.IsPrevistoInvioMail ? "SI" : "NO");

                            if (repBiz.IsPrevistoInvioMail)
                            {
                                var mails = repBiz.SendEmail();

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
                        esito = 1;
                        WriteLog("**Errore report {0}: {1}", repBiz.DataObj.Nome, e.Message);
                        WriteLog(e.StackTrace);
                    }
                    finally
                    {
                        index++;
                        WriteLog("Fine report {0}", repBiz.DataObj.Nome);
                    }
                }
            }

            return esito;
        }


        private static void printInfo()
        {
            WriteLog("*****************************************");
            WriteLog("*                                       *");
            WriteLog("*        EASY REPORT DISPATCHER         *");
            WriteLog("*               v1.0                    *");
            WriteLog("*                                       *");
            WriteLog("*                                       *");
            WriteLog("*****************************************");
            WriteLog("");
            WriteLog("");
        }

        private static void WriteLog(string logFmt, params object[] args)
        {
            Console.Write(string.Format(@"{0:yyyy-MM-dd HH:mm:ss}  -  ", DateTime.Now));
            Console.WriteLine(logFmt, args);

            //Altro
            if (_TaskLogger != null)
            {
                _TaskLogger.LogMessage(logFmt, args);
            }
        }

    }
}

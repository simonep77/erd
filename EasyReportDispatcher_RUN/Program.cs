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
        private static string _Reportname;

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                WriteLog(@"Parametro con nome del report non fornito!");
                Environment.Exit(1);
            }

            _Reportname = args[0];

            var rc = TaskExecute();

            Environment.Exit(rc);
        }



        private const string SEP = @"======================================================================================================";




        protected static int TaskExecute()
        {

            var esito = 0;
            printInfo();
            try
            {
                var bSendEmail = _SendMail;
                

                WriteLog($"Invio email attivo: {bSendEmail}");

                using (_Slot = new BusinessSlot(@"Default"))
                {
                    _Slot.OnLogDebugSent += ((a, b, c) => { WriteLog(c); });

                    ReportEstrazioneBIZ repBiz = _Slot.BizNewWithLoadByKEY<ReportEstrazioneBIZ>(ReportEstrazione.KEY_NOME, _Reportname);

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
                        WriteLog($"**Errore report: {e.Message}");
                        WriteLog(e.StackTrace);
                    }
                    finally
                    {
                        WriteLog("Fine report");
                    }

                }

                WriteLog(@"Fine esecuzione");
            }
            catch(Exception ex)
            {
                esito = 1;
                WriteLog($"Errore esecuzione: { ex.Message}");
                WriteLog($"Stack: {ex.StackTrace}" );

            }
            finally
            {

                //Chiude log per invio tramite email
            }
            
            return esito;
        }



        private static void printInfo()
        {
            WriteLog("*****************************************");
            WriteLog("*              REPORT RUNNER            *");
            WriteLog("*****************************************");
            WriteLog("");
            WriteLog($"Report: {_Reportname}");
            WriteLog("");
        }

        private static void WriteLog(string logMessage)
        {
            Console.Write(string.Format($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - "));
            Console.WriteLine(logMessage);
        }

    }
}

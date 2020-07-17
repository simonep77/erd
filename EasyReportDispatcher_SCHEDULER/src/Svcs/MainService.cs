﻿using Bdo.Objects;
using EasyReportDispatcher_Lib_Common.src;
using EasyReportDispatcher_SCHEDULER.src.Common;
using LevelB.Vici.WinService.Service;
using LevelB.Vici.WinService.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EasyReportDispatcher_SCHEDULER.src.Svcs
{
    public class MainService : Service
    {
        private Mutex mMutex;

        public int RunMode { get; set; }

        public IntSvcScheduler InternalScheduler { get; } = new IntSvcScheduler();

        public MainService(): base(@"ERD-Scheduler")
        {
            this.ServiceInfo.DisplayName = @"Easy Report Dispatcher Scheduler";
            this.ServiceInfo.Description = @"Servizio residente per la gestione dei report schedulati";
            this.ServiceInfo.ServiceAccount = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.ServiceInfo.ServiceStartMode = System.ServiceProcess.ServiceStartMode.Automatic;
            this.ServiceInfo.DependsOn = new string[] { @"tcpip" };
        }

        protected override void OnStarted()
        {
            base.OnStarted();
        }

        protected override void OnStarting()
        {
            base.OnStarting();

            this.initEventLog();

            this.InternalScheduler.Start();
        }

        protected override void OnStateChanged(ServiceTask serviceTask, ServiceState serviceState)
        {
            base.OnStateChanged(serviceTask, serviceState);
        }

        protected override void OnStopped()
        {
            base.OnStopped();
        }

        protected override void OnStopping()
        {
            base.OnStopping();

            this.InternalScheduler.Stop();

        }

        protected override void OnTaskException(ServiceTask serviceTask, Exception e)
        {
            base.OnTaskException(serviceTask, e);

            this.WriteLog(System.Diagnostics.EventLogEntryType.Error, e.Message);
        }

        protected override void OnTaskStarted(ServiceTask serviceTask)
        {
            base.OnTaskStarted(serviceTask);
        }

        protected override void OnTaskStarting(ServiceTask serviceTask)
        {
            base.OnTaskStarting(serviceTask);
        }

        protected override void OnTaskStopped(ServiceTask serviceTask)
        {
            base.OnTaskStopped(serviceTask);
        }

        protected override void OnTaskStopping(ServiceTask serviceTask)
        {
            base.OnTaskStopping(serviceTask);
        }


        #region PUBLIC


        public void RunByMode(int mode)
        {
            this.RunMode = mode;

            try
            {
                this.checkOneInstance();

                switch (mode)
                {
                    case CostantiSched.RunMode.Service:
                        //Service
                        this.Run();
                        break;
                    case CostantiSched.RunMode.Console:
                        //Console
                        this.RunConsole();
                        break;
                    case CostantiSched.RunMode.Install:
                        this.Install();
                        break;
                    case CostantiSched.RunMode.Uninstall:
                        this.UnInstall();
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                this.WriteLog(EventLogEntryType.Error, ex.Message);
            }

            
        }

        /// <summary>
        /// Crea gli slot da utilizzare di volta in volta
        /// </summary>
        /// <returns></returns>
        public BusinessSlot CreateSlot()
        {
            return new BusinessSlot("Default");
        }

        #endregion


        public void WriteLog(EventLogEntryType logType, string logMessage)
        {

            if (this.RunMode == 0)
            {
                EventLog.WriteEntry(AppContextERD.LOG_EVENT_SOURCE, logMessage, logType);
            }
            else
            {
                var dtNow = DateTime.Now;

                Console.Write(string.Format($"{dtNow:yyyy-MM-dd HH:mm:ss} - {Enum.GetName(typeof(EventLogEntryType), logType)} - "));
                Console.WriteLine(logMessage);
            }

        }


        #region PRIVATE

        /// <summary>
        /// Crea mutex per esecuzione a singola istanza
        /// </summary>
        private void checkOneInstance()
        {
            bool createdNew;

            this.mMutex = new Mutex(true, "ERD_Mutex_Run", out createdNew);

            if (!createdNew)
                throw new ApplicationException("E' già in esecuzione un'altra istanza dell'applicazione");
        }


        private void initEventLog()
        {
            if (this.RunMode == 0)
            {
                if (!EventLog.SourceExists(AppContextERD.LOG_EVENT_SOURCE))
                {
                    EventLog.CreateEventSource(AppContextERD.LOG_EVENT_SOURCE, AppContextERD.LOG_EVENT_SOURCE_LOG);
                }
            }
        }

        #endregion
    }
}
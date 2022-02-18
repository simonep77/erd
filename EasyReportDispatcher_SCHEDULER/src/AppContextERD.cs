﻿using Bdo.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyReportDispatcher_SCHEDULER.src.Svcs;
using EasyReportDispatcher_SCHEDULER.Properties;
namespace EasyReportDispatcher_SCHEDULER.src
{
    public class AppContextERD
    {
        public const string SERVICE_NAME = @"ERD-Scheduler";
        public const string LOG_EVENT_SOURCE = @"ERD_Scheduler";
        public const string LOG_EVENT_SOURCE_LOG = @"ERD Scheduler Eventlog";

        public static readonly int SCHEDULE_FORCED_UPDATE_CHECK_SECONDS = Settings.Default.SecondiCheckModificheSchedulazioni;
        public static readonly int SCHEDULE_EXECUTION_PLAN_DAYS = 7;

        public static string UserDataDir { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ERD");
        public static string UserDataDirOutput { get; } = Path.Combine(UserDataDir, "Output");
        public static string UserDataDirTemplate { get; } = Path.Combine(UserDataDir, "Template");

        public static MainService Service { get; } = new MainService();




    }
}

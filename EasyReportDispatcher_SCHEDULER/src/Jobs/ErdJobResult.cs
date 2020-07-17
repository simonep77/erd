using Bdo.Objects;
using EasyReportDispatcher_Lib_DAL.src.query;
using EasyReportDispatcher_Lib_DAL.src.report;
using EasyReportDispatcher_SCHEDULER.src.Common;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EasyReportDispatcher_SCHEDULER.src.Jobs
{
    public class ErdJobResult
    {
        public bool IsOK;
        public string Message = string.Empty;
        public ReportEstrazioneOutput Output;
    }
}

using Bdo.Objects;
using EasyReportDispatcher_Lib_DAL.src.query;
using EasyReportDispatcher_Lib_DAL.src.report;
using EasyReportDispatcher_SCHEDULER.src.Common;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EasyReportDispatcher_SCHEDULER.src.Jobs
{
    class JobReportListener : IJobListener
    {
        public string Name => @"JobReportListener1";

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            //throw new NotImplementedException();
            return Task.Run(()=> { });
        }

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            return Task.Run(() => {
                AppContextERD.Service.WriteLog( System.Diagnostics.EventLogEntryType.Information,$" > Avvio job [report] n.{context.JobDetail.JobDataMap[CostantiSched.JobDataMap.Reports.ReportId].ToString().PadLeft(4,'0')} - {context.JobDetail.JobDataMap[CostantiSched.JobDataMap.Reports.ReportName]}");
            });

        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
        {
            return Task.Run(() => {
                var repId = context.JobDetail.JobDataMap[CostantiSched.JobDataMap.Reports.ReportId].ToString().PadLeft(4, '0');
                var repName = context.JobDetail.JobDataMap[CostantiSched.JobDataMap.Reports.ReportName];
                var ret = context.Result as ErdJobResult;
                var logType = EventLogEntryType.Information;
                var errText = string.Empty;

                if (ret == null)
                {
                    logType = EventLogEntryType.Warning;
                }
                else if (jobException != null)
                {
                    logType = EventLogEntryType.Error;
                    Exception ex = jobException;
                    while (ex != null)
                    {
                        errText += ex.Message + " - ";
                        ex = ex.InnerException;
                    }
                    
                }
                else
                {
                    if (!ret.IsOK)
                    {
                        logType = EventLogEntryType.Error;
                        errText = ret.Message;
                    }
                }

                AppContextERD.Service.WriteLog(logType, $" > Termine job [report] n.{repId} - {repName}" + (errText.Length == 0 ? string.Empty : string.Concat("\n", errText)));

            });
        }
    }
}

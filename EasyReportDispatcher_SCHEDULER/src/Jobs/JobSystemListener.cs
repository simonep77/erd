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
    class JobSystemListener : IJobListener
    {
        public string Name => @"JobSystemListener1";

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            //throw new NotImplementedException();
            return Task.Run(()=> { });
        }

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            return Task.Run(() => {
                AppContextERD.Service.WriteLog( System.Diagnostics.EventLogEntryType.Information,$" > Avvio job [sistema] {context.JobDetail.Key.Name}");
            });

        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
        {
            return Task.Run(() => {
                var logType = EventLogEntryType.Information;
                var errText = string.Empty;

                 if (jobException != null)
                {
                    logType = EventLogEntryType.Error;
                    errText = jobException.Message;
                }

                AppContextERD.Service.WriteLog(logType, $" > Termine job [sistema] {context.JobDetail.Key.Name}" + (errText.Length == 0 ? string.Empty : string.Concat("\n", errText)));

            });
        }
    }
}

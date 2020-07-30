using Bdo.Objects;
using EasyReportDispatcher_Lib_Common.src.enums;
using EasyReportDispatcher_Lib_DAL.src.query;
using EasyReportDispatcher_Lib_DAL.src.report;
using EasyReportDispatcher_SCHEDULER.src.Common;
using EasyReportDispatcher_SCHEDULER.src.Svcs;
using LevelB.Arch.Core.Extensions;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyReportDispatcher_SCHEDULER.src.Jobs
{
    [DisallowConcurrentExecution]
    class JobScheduleResultChecker : IJob
    {
        private string hashSchedules = string.Empty;

        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(async () =>
            {

                {
                    //
                    using (var slot = AppContextERD.Service.CreateSlot())
                    {
                        var runJobs = await context.Scheduler.GetCurrentlyExecutingJobs();

                        var runScheds = slot.CreateList<ReportSchedulazioneLista>()
                            .SearchByColumn(Filter.Eq(nameof(ReportSchedulazione.StatoId), eReport.StatoSchedulazione.Avviata));


                        var zombies = runScheds.Where(s =>
                        {
                            return !runJobs.Where(j => j.JobDetail.Key.Group == IntSvcScheduler.JOB_TASKS_GROUP).Where(j => (int)j.JobDetail.JobDataMap[CostantiSched.JobDataMap.Reports.ReportId] == s.Id).Any();
                        });

                        if (zombies.Any())
                        {
                            AppContextERD.Service.WriteLog(System.Diagnostics.EventLogEntryType.Information, $"Trovate {zombies.Count()} schedulazioni senza esito.");

                            //OK, elimina
                            foreach (var item in zombies)
                            {
                                item.StatoId = eReport.StatoSchedulazione.NonCompletata;
                                slot.SaveObject(item);
                            }
                        }
                            
                    }
                }

            });

        }
    }
}

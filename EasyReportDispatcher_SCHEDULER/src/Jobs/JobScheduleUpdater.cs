using Bdo.Objects;
using EasyReportDispatcher_Lib_DAL.src.query;
using EasyReportDispatcher_Lib_DAL.src.report;
using EasyReportDispatcher_SCHEDULER.src.Common;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyReportDispatcher_SCHEDULER.src.Jobs
{
    [DisallowConcurrentExecution]
    class JobScheduleUpdater : IJob
    {
        private string hashSchedules = string.Empty;

        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() => {

                {
                    //Preimposta reload con indicazione di forzatura
                    var bEseguiReload = (context.JobDetail.JobDataMap.Contains(CostantiSched.JobDataMap.System.ForceReloadSchedules) && Convert.ToBoolean(context.JobDetail.JobDataMap[CostantiSched.JobDataMap.System.ForceReloadSchedules]));

                    //Ricalcola hash schedulazioni
                    this.calculateHash();

                    //Verifica hash non impostato
                    bEseguiReload |= string.IsNullOrWhiteSpace(AppContextERD.Service.InternalScheduler.Schedule_Last_Hash);

                    //Verifica hash cambiato
                    bEseguiReload |= (this.hashSchedules != AppContextERD.Service.InternalScheduler.Schedule_Last_Hash);


                    //Se necessario reload procede
                    if (bEseguiReload)
                    {
                        this.updateSchedules();

                        AppContextERD.Service.InternalScheduler.Schedule_Last_Hash = hashSchedules;
                        AppContextERD.Service.InternalScheduler.Schedule_Last_Refresh = DateTime.Now;
                    }


                }

            });
        }

        private void calculateHash()
        {
            using (var slot = AppContextERD.Service.CreateSlot())
            {
                this.hashSchedules = QueryReports.CalculateSchedulesHash(slot);
            }
        }

        private void updateSchedules()
        {
            AppContextERD.Service.InternalScheduler.ReloadReportSchedules();
        }


    }
}

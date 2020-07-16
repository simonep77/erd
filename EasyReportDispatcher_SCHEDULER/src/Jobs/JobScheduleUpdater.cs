using Bdo.Objects;
using EasyReportDispatcher_Lib_DAL.src.report;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyReportDispatcher_SCHEDULER.src.Jobs
{
    class JobScheduleUpdater : IJob
    {
        private string hashSchedules = string.Empty;

        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() => {

                {
                    var bEseguiReload = false;

                    //Se passato piu' di x dall'ultimo refresh 
                    if (DateTime.Now.Subtract(AppContextERD.Service.InternalScheduler.Schedule_Last_Refresh).TotalMinutes > AppContextERD.SCHEDULE_FORCED_REFRESH_MINUTES)
                        bEseguiReload = true;

                    //Ricalcola hash schedulazioni
                    this.hashSchedules = calculateSchedulesHash();

                    //Verifica hash
                    if (!bEseguiReload)
                    {
                        if (this.hashSchedules != AppContextERD.Service.InternalScheduler.Schedule_Last_Hash)
                        {
                            bEseguiReload = true;
                            
                        }
                    }

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

        private string calculateSchedulesHash()
        {
            using (var slot = AppContextERD.Service.CreateSlot())
            {
                var sql = new StringBuilder(@"SET group_concat_max_len = 1024 * 1024;");
                sql.AppendLine();
                sql.AppendFormat(@"SELECT IFNULL(SHA1(GROUP_CONCAT(e.{0} ORDER BY e.Id SEPARATOR ';')), '') ", nameof(ReportEstrazione.CronString), nameof(ReportEstrazione.Id));
                sql.AppendLine();
                sql.AppendFormat(@"FROM {0} e ", slot.DbPrefixGetTableName<ReportEstrazione>());
                sql.AppendLine();
                sql.AppendFormat(@"WHERE e.{0}=1 ", nameof(ReportEstrazione.Attivo));

                slot.DB.SQL = sql.ToString();

                return slot.DB.ExecScalar().ToString();
            }

        }


        private void updateSchedules()
        {
            AppContextERD.Service.InternalScheduler.ReloadSchedules();
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bdo.Objects;
using EasyReportDispatcher_Lib_DAL.src.report;
using EasyReportDispatcher_SCHEDULER.src.Jobs;
using NCrontab;
using Quartz;
using Quartz.Core;
using Quartz.Impl;
using Quartz.Impl.Matchers;

namespace EasyReportDispatcher_SCHEDULER.src.Svcs
{
    public class IntSvcScheduler
    {
        private const string JOB_INTERNAL_GROUP = @"InternalJobs";
        private const string JOB_TASKS_GROUP = @"TaskSchedJobs";
        private const string TRIGGER_INTERNAL_GROUP = @"InternalTrig";
        private const string TRIGGER_TASKS_GROUP = @"TaskSchedTrig";

        private object mMainSync = new object();
        private IScheduler mScheduler;
        public string Schedule_Last_Hash { get; set; } = string.Empty;
        public DateTime Schedule_Last_Refresh { get; set; } = DateTime.MinValue;


        async public void Start()
        {
            AppContextERD.Service.WriteLog(System.Diagnostics.EventLogEntryType.Information, @"Schedulatore interno job inizializzazione...");
            //Crea lo scheduler principale
            this.mScheduler = await (new StdSchedulerFactory()).GetScheduler();
            await this.mScheduler.Start();

            //Crea e schedula i job di sistema

            // 1) Il job di verifica variazione schedulazioni
            var trg = TriggerBuilder
                .Create()
                .WithIdentity("TrigScheduleUpdate", TRIGGER_INTERNAL_GROUP)
                .WithSchedule(SimpleScheduleBuilder.RepeatSecondlyForever(AppContextERD.SCHEDULE_FORCED_UPDATE_CHECK_SECONDS))
                .Build();

            await this.mScheduler.ScheduleJob( new JobDetailImpl("JobScheduleUpdate", JOB_INTERNAL_GROUP, typeof(JobScheduleUpdater)) , trg);

            AppContextERD.Service.WriteLog(System.Diagnostics.EventLogEntryType.Information, @"Schedulatore interno job avviato");
        }

        async public void Stop()
        {
            AppContextERD.Service.WriteLog(System.Diagnostics.EventLogEntryType.Information, @"Schedulatore interno job in chiusura...");
            //Stoppa schedulatore
            await this.mScheduler.Shutdown(true);

            AppContextERD.Service.WriteLog(System.Diagnostics.EventLogEntryType.Information, @"Schedulatore interno job terminato");
        }


        async public void DeleteAllSchedules()
        {
            var matcher = GroupMatcher<JobKey>.GroupContains(JOB_TASKS_GROUP);
            var jobkeys = await this.mScheduler.GetJobKeys(matcher);

            await this.mScheduler.DeleteJobs(jobkeys);
        }

        async public void ReloadSchedules()
        {
            //
            await this.mScheduler.PauseAll();
            try
            {
                this.DeleteAllSchedules();

                using (var slot = AppContextERD.Service.CreateSlot())
                {

                    var reports = slot.CreateList<ReportEstrazioneLista>()
                        .SearchByColumn(Filter.Eq(nameof(ReportEstrazione.Attivo), 1)
                        .And(Filter.Neq(nameof(ReportEstrazione.CronString), string.Empty)
                        .And(Filter.Lte(nameof(ReportEstrazione.DataInizio), DateTime.Today)
                        .And(Filter.Gte(nameof(ReportEstrazione.DataFine), DateTime.Today)))));

                    foreach (var rep in reports)
                    {
                        var idxTrig = 0;


                        //Verifica espressione cron
                        //var cron4quartz = string.Concat("0 ", rep.CronString);
                        var c = CrontabSchedule.Parse(rep.CronString);

                        var tb = TriggerBuilder.Create();

                        var cs = c.GetNextOccurrences(DateTime.Now, DateTime.Now.AddDays(7).ToUniversalTime());

                        foreach (var dt in cs)
                        {
                            //Crea trigger
                            var trg = TriggerBuilder.Create().StartAt(dt.ToUniversalTime()).WithIdentity(string.Format($"Trig_Rep_{rep.Id}_seq{idxTrig}"), TRIGGER_TASKS_GROUP).Build();
                            //Crea job
                            var jobDet = new JobDetailImpl(string.Format($"Job_Rep_{rep.Id}_seq{idxTrig}"), JOB_TASKS_GROUP, typeof(JobScheduleUpdater));
                            jobDet.JobDataMap.Add(@"ReportId", rep.Id);
                            jobDet.JobDataMap.Add(@"ReportName", rep.Nome);

                            await this.mScheduler.ScheduleJob(jobDet, trg);

                            idxTrig++;
                        }



                        
                        //var trg = this.mTriggerBuilder.WithIdentity(string.Concat(@"Trig_Rep_", rep.Id.ToString()), TRIGGER_TASKS_GROUP).WithCronSchedule(cron4quartz).Build();


                        //await this.mScheduler.ScheduleJob(jobDet, trg);

                    }
                }

                this.printSchedules();
            }
            catch(Exception ex)
            {
                AppContextERD.Service.WriteLog(System.Diagnostics.EventLogEntryType.Error, ex.Message);
            }
            finally
            {
                await this.mScheduler.ResumeAll();
            }
            
        }

        async private void printSchedules2()
        {
            var matcher = GroupMatcher<JobKey>.GroupContains(JOB_TASKS_GROUP);
            var jobkeys = await this.mScheduler.GetJobKeys(matcher);

            var sb = new StringBuilder();
            sb.AppendLine(@"Elenco prossime schedulazioni: ");

            foreach (var key in jobkeys)
            {
                var job = await this.mScheduler.GetJobDetail(key);
                var trs = await this.mScheduler.GetTriggersOfJob(key);

                sb.AppendFormat($" > job: {job.JobDataMap["ReportName"]}");
                sb.AppendLine();

                foreach (var item in trs)
                {
                    sb.AppendFormat($"     * run: {item.StartTimeUtc.ToLocalTime():dd/MM/yyyy HH:mm}");
                    sb.AppendLine();
                }
            }

            AppContextERD.Service.WriteLog(System.Diagnostics.EventLogEntryType.Information, sb.ToString());

        }


        async private void printSchedules()
        {
            var matcher = GroupMatcher<JobKey>.GroupContains(JOB_TASKS_GROUP);
            var jobkeys = await this.mScheduler.GetJobKeys(matcher);

            var trigList = new List<ITrigger>();

            var sb = new StringBuilder();
            sb.AppendLine(@"Elenco prossime schedulazioni: ");

            foreach (var key in jobkeys)
            {
                trigList.AddRange(await this.mScheduler.GetTriggersOfJob(key));
            }

            var trsOrdered = trigList.OrderBy(t => t.StartTimeUtc);

            foreach (var tr in trsOrdered)
            {
                var job = await this.mScheduler.GetJobDetail(tr.JobKey);
                sb.AppendFormat($" > {tr.StartTimeUtc.ToLocalTime():dd/MM/yyyy HH:mm} -  {job.JobDataMap["ReportName"]}");
                sb.AppendLine();
            }

            AppContextERD.Service.WriteLog(System.Diagnostics.EventLogEntryType.Information, sb.ToString());

        }

    }
}

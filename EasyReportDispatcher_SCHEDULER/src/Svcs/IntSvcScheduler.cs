using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bdo.Objects;
using EasyReportDispatcher_Lib_Common.src.enums;
using EasyReportDispatcher_Lib_DAL.src.report;
using EasyReportDispatcher_SCHEDULER.src.Common;
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
            //Aggiunge listener per i task
            //this.mScheduler.ListenerManager.AddJobListener(new JobSystemListener(), GroupMatcher<JobKey>.GroupContains(JOB_INTERNAL_GROUP));
            this.mScheduler.ListenerManager.AddJobListener(new JobReportListener(), GroupMatcher<JobKey>.GroupContains(JOB_TASKS_GROUP));
            await this.mScheduler.Start();

            //Crea e schedula i job di sistema

            // 1) Il job di caricamento schedulazioni per garantire l'estensione del piano di esecuzione
            var trg = TriggerBuilder
                .Create()
                .WithIdentity(CostantiSched.Quartz.TriggerNames.System.ScheduleExtender, TRIGGER_INTERNAL_GROUP)
                .WithCronSchedule(AppContextERD.SCHEDULE_EXTEND_PLAN_CRONSTRING)
                .Build();

            await this.mScheduler.ScheduleJob(new JobDetailImpl(CostantiSched.Quartz.JobNames.System.ScheduleExtender, JOB_INTERNAL_GROUP, typeof(JobScheduleUpdater)), trg);

            // 2) Il job di check presenza modifiche alle schedulazioni (che parte comunque immediatamente)
            trg = TriggerBuilder
                .Create()
                .WithIdentity(CostantiSched.Quartz.TriggerNames.System.ScheduleUpdateCheck, TRIGGER_INTERNAL_GROUP)
                .WithSchedule(SimpleScheduleBuilder.RepeatSecondlyForever(AppContextERD.SCHEDULE_FORCED_UPDATE_CHECK_SECONDS))
                .Build();

            await this.mScheduler.ScheduleJob( new JobDetailImpl(CostantiSched.Quartz.JobNames.System.ScheduleUpdateCheck, JOB_INTERNAL_GROUP, typeof(JobScheduleUpdater)) , trg);

            AppContextERD.Service.WriteLog(System.Diagnostics.EventLogEntryType.Information, @"Schedulatore interno job avviato");
        }

        async public void Stop()
        {
            AppContextERD.Service.WriteLog(System.Diagnostics.EventLogEntryType.Information, @"Schedulatore interno job in chiusura...");
            //Stoppa schedulatore
            await this.mScheduler.PauseAll();
            //await this.mScheduler.Clear();
            await this.mScheduler.Shutdown(true);

            AppContextERD.Service.WriteLog(System.Diagnostics.EventLogEntryType.Information, @"Schedulatore interno job terminato");
        }


        async public void DeleteAllReportSchedules()
        {
            var matcher = GroupMatcher<JobKey>.GroupContains(JOB_TASKS_GROUP);
            var jobkeys = await this.mScheduler.GetJobKeys(matcher);

            await this.mScheduler.DeleteJobs(jobkeys);
        }

        private string CalcTrigName(int estrazId, DateTime date)
        {
            return $"T_{estrazId}_dt_{date:yyyyMMddHHmm}";
        }

        private string CalcJobName(int estrazId, DateTime date)
        {
            return $"J_{estrazId}_dt_{date:yyyyMMddHHmm}";
        }

        async public void ReloadReportSchedules_OLD()
        {
            //Mette in pausa tutte le schedulazioni
            await this.mScheduler.PauseAll();
            try
            {
                this.DeleteAllReportSchedules();
                int iNumSched = 0;

                using (var slot = AppContextERD.Service.CreateSlot())
                {

                    var reports = slot.CreateList<ReportEstrazioneLista>()
                        .SearchByColumn(Filter.Eq(nameof(ReportEstrazione.Attivo), 1)
                        .And(Filter.Neq(nameof(ReportEstrazione.CronString), string.Empty)
                        .And(Filter.Lte(nameof(ReportEstrazione.DataInizio), DateTime.Today)
                        .And(Filter.Gte(nameof(ReportEstrazione.DataFine), DateTime.Today)))));

                    iNumSched = reports.Count;

                    foreach (var rep in reports)
                    {
                        var idxTrig = 0;


                        //Viene utilizzata la notazione senza secondi NCrontab (Cron.guru)
                        var c = CrontabSchedule.Parse(rep.CronString);

                        var tb = TriggerBuilder.Create();

                        var cs = c.GetNextOccurrences(DateTime.Now, DateTime.Now.AddDays(7).ToUniversalTime());

                        foreach (var dt in cs)
                        {
                            //Crea trigger
                            var trg = TriggerBuilder.Create().StartAt(dt.ToUniversalTime()).WithIdentity(this.CalcTrigName(rep.Id, dt), TRIGGER_TASKS_GROUP).Build();
                            //Crea job
                            var jobDet = new JobDetailImpl(this.CalcJobName(rep.Id, dt), JOB_TASKS_GROUP, typeof(JobReport));
                            jobDet.JobDataMap.Add(CostantiSched.JobDataMap.Reports.ReportId, rep.Id);
                            jobDet.JobDataMap.Add(CostantiSched.JobDataMap.Reports.ReportName, rep.Nome);

                            await this.mScheduler.ScheduleJob(jobDet, trg);

                            idxTrig++;
                        }

                    }
                }

                if ( AppContextERD.Service.RunMode == CostantiSched.RunMode.Console)
                    this.printSchedules();
                else
                    AppContextERD.Service.WriteLog(System.Diagnostics.EventLogEntryType.Information, $"Caricate {iNumSched} schedulazioni");
            }
            catch (Exception ex)
            {
                AppContextERD.Service.WriteLog(System.Diagnostics.EventLogEntryType.Error, ex.Message);
            }
            finally
            {
            //Riavvia tutte le schedulazioni
                await this.mScheduler.ResumeAll();
            }
            
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
                sb.AppendFormat($" > {tr.StartTimeUtc.ToLocalTime():dd/MM/yyyy HH:mm} -  {job.JobDataMap[CostantiSched.JobDataMap.Reports.ReportName]}");
                sb.AppendLine();
            }

            AppContextERD.Service.WriteLog(System.Diagnostics.EventLogEntryType.Information, $"Schedulazioni caricate: {trsOrdered.Count()}");

        }



        async public void ReloadReportSchedules_NODB()
        {
            //Mette in pausa tutte le schedulazioni
            await this.mScheduler.PauseAll();
            try
            {


                var matcher = GroupMatcher<JobKey>.GroupContains(JOB_TASKS_GROUP);
                var jobkeys = await this.mScheduler.GetJobKeys(matcher);
                var trigDiz = new Dictionary<string, ITrigger>();

                //Carichiamo i trigger == job
                foreach (var key in jobkeys)
                {
                    foreach (var trg in await this.mScheduler.GetTriggersOfJob(key))
                    {
                        trigDiz.Add(trg.Key.Name, trg);
                    }
                }

                int iNumSched = 0;

                using (var slot = AppContextERD.Service.CreateSlot())
                {

                    var reports = slot.CreateList<ReportEstrazioneLista>()
                        .SearchByColumn(Filter.Eq(nameof(ReportEstrazione.Attivo), 1)
                        .And(Filter.Neq(nameof(ReportEstrazione.CronString), string.Empty)
                        .And(Filter.Lte(nameof(ReportEstrazione.DataInizio), DateTime.Today)
                        .And(Filter.Gte(nameof(ReportEstrazione.DataFine), DateTime.Today)))));

                    iNumSched = reports.Count;

                    //Verifica essistenza ed aggiunge schedulazioni
                    foreach (var rep in reports)
                    {

                        //Viene utilizzata la notazione senza secondi NCrontab (Cron.guru)
                        var c = CrontabSchedule.Parse(rep.CronString);

                        var cs = c.GetNextOccurrences(DateTime.Now, DateTime.Now.AddDays(AppContextERD.SCHEDULE_EXECUTION_PLAN_DAYS).ToUniversalTime());

                        foreach (var dt in cs)
                        {
                            //Crea nome trigger
                            var trName = this.CalcTrigName(rep.Id, dt);

                            //Verifica esistenza: se non esiste lo crea, se esiste lo rimuove da elenco
                            if (!trigDiz.ContainsKey(trName))
                            {
                                //Crea trigger
                                var trg = TriggerBuilder.Create().StartAt(dt.ToUniversalTime()).WithIdentity(trName, TRIGGER_TASKS_GROUP).Build();
                                //Crea job
                                var jobDet = new JobDetailImpl(this.CalcJobName(rep.Id, dt), JOB_TASKS_GROUP, typeof(JobReport));
                                jobDet.JobDataMap.Add(CostantiSched.JobDataMap.Reports.ReportId, rep.Id);
                                jobDet.JobDataMap.Add(CostantiSched.JobDataMap.Reports.ReportName, rep.Nome);

                                await this.mScheduler.ScheduleJob(jobDet, trg);
                            }
                            else
                            {
                                //Rimuove da elenco in quanto esistente
                                trigDiz.Remove(trName);
                            }
                        }

                    }
                    //Rimuove quelle non piu' esistenti/coerenti
                    foreach (var item in trigDiz)
                    {
                        await this.mScheduler.DeleteJob(item.Value.JobKey);
                    }
                    trigDiz.Clear();

                }

                if (AppContextERD.Service.RunMode == CostantiSched.RunMode.Console)
                    this.printSchedules();
                else
                    AppContextERD.Service.WriteLog(System.Diagnostics.EventLogEntryType.Information, $"Caricate {iNumSched} schedulazioni");
            }
            catch (Exception ex)
            {
                AppContextERD.Service.WriteLog(System.Diagnostics.EventLogEntryType.Error, ex.Message);
            }
            finally
            {
                //Riavvia tutte le schedulazioni
                await this.mScheduler.ResumeAll();
            }

        }

        private class TriggerStatus
        {
            public ITrigger Trigger;
            public ReportSchedulazione Schedulazione;
        }

        async public void ReloadReportSchedules()
        {
            //Mette in pausa tutte le schedulazioni
            await this.mScheduler.Standby();
            try
            {


                var matcher = GroupMatcher<JobKey>.GroupContains(JOB_TASKS_GROUP);
                var jobkeys = await this.mScheduler.GetJobKeys(matcher);
                var trigDiz = new Dictionary<string, TriggerStatus>();
                int iNumSched = 0;

                using (var slot = AppContextERD.Service.CreateSlot())
                {
                    //Carica le schedulazioni
                    var schedList = slot.CreateList<ReportSchedulazioneLista>()
                        .LoadFullObjects()
                        .SearchByColumn(Filter.Eq(nameof(ReportSchedulazione.StatoId), eReport.StatoSchedulazione.Pianificata));

                    //Carichiamo i trigger == job IN MEMORIA
                    foreach (var key in jobkeys)
                    {
                        foreach (var trg in await this.mScheduler.GetTriggersOfJob(key))
                        {
                            var sched = schedList.FindFirstByPropertyFilter(Filter.Eq(nameof(ReportSchedulazione.TriggerKey), trg.Key.Name));

                            if (sched == null)
                            {
                                //Strano....
                                AppContextERD.Service.WriteLog(System.Diagnostics.EventLogEntryType.Warning, $"Attenzione: il trigger {trg.Key.Name} non ha una schedulazione DB associata!");
                            }
                            else
                                trigDiz.Add(trg.Key.Name, new TriggerStatus() { Trigger=trg, Schedulazione=sched });
                        }
                    }


                    var reports = slot.CreateList<ReportEstrazioneLista>()
                        .LoadFullObjects()
                        .SearchByColumn(Filter.Eq(nameof(ReportEstrazione.Attivo), 1)
                        .And(Filter.Neq(nameof(ReportEstrazione.CronString), string.Empty)
                        .And(Filter.Lte(nameof(ReportEstrazione.DataInizio), DateTime.Today)
                        .And(Filter.Gte(nameof(ReportEstrazione.DataFine), DateTime.Today)))));

                    iNumSched = reports.Count;
                    var dtPlanStart = DateTime.Now;
                    var dtPlanEnd = dtPlanStart.AddDays(AppContextERD.SCHEDULE_EXECUTION_PLAN_DAYS);

                    //Verifica essistenza ed aggiunge schedulazioni
                    foreach (var rep in reports)
                    {

                        //Viene utilizzata la notazione senza secondi NCrontab (Cron.guru)
                        var c = CrontabSchedule.Parse(rep.CronString);

                        var cs = c.GetNextOccurrences(dtPlanStart, dtPlanEnd);

                        foreach (var dt in cs)
                        {
                            //Crea nome trigger
                            var trName = this.CalcTrigName(rep.Id, dt);

                            var sched = schedList.FindFirstByPropertyFilter(Filter.Eq(nameof(ReportSchedulazione.TriggerKey), trName));

                            TriggerStatus trStatus = null;

                            //Verifica esistenza: se non esiste lo crea, se esiste lo rimuove da elenco
                            if (!trigDiz.TryGetValue(trName, out trStatus))
                            {
                                //Crea trigger
                                var trg = TriggerBuilder.Create().StartAt(dt.ToUniversalTime()).WithIdentity(trName, TRIGGER_TASKS_GROUP).Build();
                                
                                //Crea job
                                var jobDet = new JobDetailImpl(this.CalcJobName(rep.Id, dt), JOB_TASKS_GROUP, typeof(JobReport));
                                jobDet.JobDataMap.Add(CostantiSched.JobDataMap.Reports.ReportId, rep.Id);
                                jobDet.JobDataMap.Add(CostantiSched.JobDataMap.Reports.ReportName, rep.Nome);

                                await this.mScheduler.ScheduleJob(jobDet, trg);

                                //Ricerca piano da DB
                                if (sched == null)
                                {
                                    //Crea schedulazione db
                                    sched = slot.CreateObject<ReportSchedulazione>();
                                    sched.EstrazioneId = rep.Id;
                                    sched.TriggerKey = trName;
                                    sched.DataEsecuzione = dt;
                                    sched.StatoId = eReport.StatoSchedulazione.Pianificata;

                                    slot.SaveObject(sched);
                                }

                            }
                            else
                            {
                                //Rimuove da elenco in quanto esistente dei trigger in memoria
                                trigDiz.Remove(trName);

                            }

                            //Se la schedulazione a db c'e' allora la rimuove da elenco di valutazione
                            if (sched != null)
                                schedList.Remove(sched);
                        }

                    }
                    //Cerca le schedulazioni non piu' riprogrammate
                    var schedListDel = schedList.FindAllByPropertyFilter(Filter.Gt(nameof(ReportSchedulazione.DataEsecuzione), dtPlanStart));
                    var schedListSalt = schedList.Diff(schedListDel);
                    //Elimina non piu' riprogrammate
                    slot.DeleteAll(schedListDel);
                    //Imposta le entry db non trovate come saltate
                    schedListSalt.SetPropertyMassive(nameof(ReportSchedulazione.StatoId), eReport.StatoSchedulazione.Saltata);
                    slot.SaveAll(schedListSalt);

                    //Rimuove quelle non piu' esistenti/coerenti
                    foreach (var item in trigDiz)
                    {
                        //if (item.Value.Schedulazione.ObjectState == EObjectState.Loaded)
                        //    slot.DeleteObject(item.Value.Schedulazione);
                        
                        await this.mScheduler.DeleteJob(item.Value.Trigger.JobKey);
                    }
                    trigDiz.Clear();

                }

                if (AppContextERD.Service.RunMode == CostantiSched.RunMode.Console)
                    this.printSchedules();
                else
                    AppContextERD.Service.WriteLog(System.Diagnostics.EventLogEntryType.Information, $"Caricate {iNumSched} schedulazioni");
            }
            catch (Exception ex)
            {
                AppContextERD.Service.WriteLog(System.Diagnostics.EventLogEntryType.Error, ex.Message);
            }
            finally
            {
                //Riavvia tutte le schedulazioni
                await this.mScheduler.Start();
            }

        }
    }
}

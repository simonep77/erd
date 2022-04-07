using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Bdo.Objects;
using EasyReportDispatcher_Lib_BIZ.src.report;
using EasyReportDispatcher_Lib_BIZ.src.utils;
using EasyReportDispatcher_Lib_Common.src.enums;
using EasyReportDispatcher_Lib_DAL.src.report;
using EasyReportDispatcher_SCHEDULER.src.Common;
using FluentScheduler;


namespace EasyReportDispatcher_SCHEDULER.src.Svcs
{
    public class IntSvcScheduler
    {

        public string Schedule_Last_Hash { get; set; } = string.Empty;
        public DateTime Schedule_Last_Refresh { get; set; } = DateTime.MinValue;


        public void Start()
        {
            AppContextERD.Service.WriteLog(System.Diagnostics.EventLogEntryType.Information, @"Schedulatore interno job inizializzazione...");
            //Crea lo scheduler principale
            JobManager.Initialize();
            JobManager.JobStart += info => AppContextERD.Service.WriteLog(System.Diagnostics.EventLogEntryType.Information, $" > Avvio job [report] n.{info.Name}");
            JobManager.JobEnd += info => AppContextERD.Service.WriteLog(System.Diagnostics.EventLogEntryType.Information, $" > Fine job [report] n.{info.Name}");
            JobManager.JobException += info => AppContextERD.Service.WriteLog(System.Diagnostics.EventLogEntryType.Error, $" > Errore job [report] n.{info.Name}: {info.Exception}");

            JobManager.Start();

            //Lancia il primo caricamento del piano
            this.runUpdateScheduleCheck(true);

            AppContextERD.Service.WriteLog(System.Diagnostics.EventLogEntryType.Information, @"Schedulatore interno job avviato");
        }

        public void Stop()
        {
            AppContextERD.Service.WriteLog(System.Diagnostics.EventLogEntryType.Information, @"Schedulatore interno job in chiusura...");
            //Stoppa schedulatore
            JobManager.StopAndBlock();

            AppContextERD.Service.WriteLog(System.Diagnostics.EventLogEntryType.Information, @"Schedulatore interno job terminato");
        }





        private void printSchedules()
        {

            var sb = new StringBuilder();
            sb.AppendLine(@"Elenco prossime schedulazioni: ");

            foreach (var key in JobManager.AllSchedules.OrderBy(x => x.NextRun))
            {
                sb.AppendFormat($" > {key.NextRun:dd/MM/yyyy HH:mm} -  {key.Name}");
                sb.AppendLine();
            }


            AppContextERD.Service.WriteLog(System.Diagnostics.EventLogEntryType.Information, $"Schedulazioni caricate: {JobManager.AllSchedules.Count()}");

        }



        public void ReloadReportSchedules()
        {
            //Mette in pausa tutte le schedulazioni
            JobManager.Stop();
            try
            {
                //Cerca i job non di systema
                JobManager.RemoveAllJobs();

                //Schedula check modifiche
                JobManager.AddJob(() =>
                {
                    this.runUpdateScheduleCheck(false);
                },
                    s => s.WithName("SYS_Check_Updates").ToRunEvery(AppContextERD.SCHEDULE_FORCED_UPDATE_CHECK_SECONDS).Seconds()
                );

                //Rischedula update del piano ogni notte
                JobManager.AddJob(() =>
                {
                    this.runUpdateScheduleCheck(true);
                },
                    s => s.WithName("SYS_Rebuild_Plan").ToRunEvery(1).Days().At(0, 7)
                );

                using (var slot = AppContextERD.Service.CreateSlot())
                {

                    //Carica tutte le estrazioni
                    var reports = slot.CreateList<ReportEstrazioneLista>()
                        .LoadFullObjects()
                        .SearchByColumn(Filter.Neq(nameof(ReportEstrazione.CronString), string.Empty)
                        .And(Filter.Lte(nameof(ReportEstrazione.DataInizio), DateTime.Today)
                        .And(Filter.Gte(nameof(ReportEstrazione.DataFine), DateTime.Today))));

                    var dtPlanStart = DateTime.Now;
                    var dtPlanEnd = dtPlanStart.AddDays(AppContextERD.SCHEDULE_EXECUTION_PLAN_DAYS);

                    //Verifica essistenza ed aggiunge schedulazioni
                    foreach (var rep in reports)
                    {
                        try
                        {
                            var repBiz = rep.ToBizObject<ReportEstrazioneBIZ>();

                            if (repBiz.DataObj.Attivo == 0)
                            {
                                //Per le non attive eliminiamo eventuali schedulazioni attive
                                slot.DeleteAll(repBiz.ListaSchedulazioniAttive);
                            }
                            else
                            {
                                //Per le nuove ricalcoliamo il piano
                                repBiz.RebuildPianoSchedulazione(dtPlanStart, dtPlanEnd);

                                foreach (var item in repBiz.ListaSchedulazioniAttive)
                                {
                                    var schedId = item.Id;

                                    JobManager.AddJob(() =>
                                    {
                                        this.runUserJob(schedId);
                                    },
                                    s => s.WithName(repBiz.DataObj.Nome).ToRunOnceAt(item.DataEsecuzione));
                                }
                            }

                        }
                        catch (Exception e)
                        {
                            AppContextERD.Service.WriteLog(EventLogEntryType.Error, $"Errore nel caricamento della schedulazione per {rep.Nome} ({rep.Id}): {e.Message}");
                            try
                            {

                                MailUT.SendMailFromDefaultConf(Properties.Settings.Default.NotificaErroriApplicazioneTO,
                                                                Properties.Settings.Default.NotificaErroriApplicazioneCC,
                                                                $"ERR - ERD Scheduler - {rep.Nome} ({rep.Id})",
                                                                $"Si è verificato il seguente errore:<br/>{e.Message}<br/><br/>{e.StackTrace}", null);
                            }
                            catch (Exception)
                            {
                                AppContextERD.Service.WriteLog(EventLogEntryType.Error, $"Errore nell'invio mail di notifica");
                            }
                        }

                    }

                }

                if (AppContextERD.Service.RunMode == CostantiSched.RunMode.Console)
                    this.printSchedules();
                else
                    AppContextERD.Service.WriteLog(System.Diagnostics.EventLogEntryType.Information, $"Caricate {JobManager.AllSchedules.Count()} schedulazioni");
            }
            finally
            {
                //Riavvia tutte le schedulazioni
                JobManager.Start();
            }

        }


        private void runUpdateScheduleCheck(bool force)
        {

            var bEseguiReload = force;

            //Ricalcola hash schedulazioni
            var newhash = this.calculateHash();

            //Verifica hash non impostato
            bEseguiReload |= string.IsNullOrWhiteSpace(AppContextERD.Service.InternalScheduler.Schedule_Last_Hash);

            //Verifica hash cambiato
            bEseguiReload |= (newhash != AppContextERD.Service.InternalScheduler.Schedule_Last_Hash);


            //Se necessario reload procede
            if (bEseguiReload)
            {
                this.ReloadReportSchedules();

                AppContextERD.Service.InternalScheduler.Schedule_Last_Hash = newhash;
                AppContextERD.Service.InternalScheduler.Schedule_Last_Refresh = DateTime.Now;
            }

        }

        private string calculateHash()
        {
            using (var slot = AppContextERD.Service.CreateSlot())
            {
                return EasyReportDispatcher_Lib_DAL.src.query.QueryReports.CalculateSchedulesHash(slot);
            }
        }





        private void runUserJob(long schedId)
        {
            var bSendEmail = true;
            var sb = new StringBuilder();

            try
            {
                using (var jslot = AppContextERD.Service.CreateSlot())
                {
                    //Ricerca schedulazione db
                    var sched = jslot.LoadObjNullByPK<ReportSchedulazione>(schedId);

                    if (sched != null)
                    {
                        sched.StatoId = eReport.StatoSchedulazione.Avviata;
                        jslot.SaveObject(sched);
                    }

                    //Aggiorna piano schedulazione

                    //Scrive nel log il debug User1
                    jslot.OnLogDebugSent += ((a, b, c) => { if (b == DebugLevel.User_1) sb.AppendLine(c); });

                    var repBiz = sched.Estrazione.ToBizObject<ReportEstrazioneBIZ>();

                    try
                    {
                        repBiz.Run(true, bSendEmail, true);
                    }
                    catch (Exception)
                    {
                    };

                    //Termina schedulazione
                    if (sched != null)
                    {
                        if (repBiz.LastResult.Id > 0)
                            sched.OutputId = repBiz.LastResult.Id;


                        sched.StatoId = eReport.StatoSchedulazione.Eseguita;
                        jslot.SaveObject(sched);
                    }
                }
            }
            finally
            {
                //Forza deallocazione memoria non più utilizzata
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }


        }

    }
}

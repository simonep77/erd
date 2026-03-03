using ERD.Scheduler;
using System.Text;
using FluentScheduler;
using ERD.Service.BIZ;
using ERD.Service.DAL;
using Business.Data.Objects.Common.Utils;
using ERD.Service.DAL.Query;
using ERD.Service.Common.Enums;
using ERD.Service.BIZ.Utils;


namespace ERD.Scheduler
{
    public class IntSvcScheduler
    {

        public string Schedule_Last_Hash { get; set; } = string.Empty;
        public DateTime Schedule_Last_Refresh { get; set; } = DateTime.MinValue;


        public void Start()
        {
            AppContextERD.WriteLog("INFO", @"Schedulatore interno job inizializzazione...");
            //Crea lo scheduler principale
            JobManager.Initialize();
            JobManager.JobStart += info => AppContextERD.WriteLog("INFO", $" > Avvio job [report] n.{info.Name}");
            JobManager.JobEnd += info => AppContextERD.WriteLog("INFO", $" > Fine job [report] n.{info.Name}");
            JobManager.JobException += info => AppContextERD.WriteLog("ERROR", $" > Errore job [report] n.{info.Name}: {info.Exception}");

            JobManager.Start();

            //Lancia il primo caricamento del piano
            this.runUpdateScheduleCheck(true);

            AppContextERD.WriteLog("INFO", @"Schedulatore interno job avviato");
        }

        public void Stop()
        {
            AppContextERD.WriteLog("INFO", @"Schedulatore interno job in chiusura...");
            //Stoppa schedulatore
            JobManager.StopAndBlock();

            AppContextERD.WriteLog("INFO", @"Schedulatore interno job terminato");
        }





        private void printSchedules()
        {

            var sb = new StringBuilder();
            sb.AppendLine($"Schedulazioni caricate: {JobManager.AllSchedules.Count()}");

            foreach (var key in JobManager.AllSchedules.OrderBy(x => x.NextRun))
            {
                sb.AppendLine($" > {key.Name} @ {key.NextRun:dd/MM/yyyy HH:mm}");
            }

            AppContextERD.WriteLog("INFO", sb.ToString());

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
                    s => s.WithName("SYS_Check_Updates").ToRunEvery(AppContextERD.SCHEDULE_CHECK_SECONDS).Seconds()
                );

                //Rischedula update del piano ogni notte
                JobManager.AddJob(() =>
                {
                    this.runUpdateScheduleCheck(true);
                },
                    s => s.WithName("SYS_Rebuild_Plan").ToRunEvery(1).Days().At(0, 7)
                );

                using (var slot = AppContextERD.CreateSlot())
                {

                    //Carica tutte le estrazioni
                    var reports = slot.CreateList<ReportEstrazioneLista>()
                                        .SearchByLinq(x => x.CronString != "" && x.DataInizio <= DateTime.Today && x.DataFine >= DateTime.Today)
                                        .ToBizObjectList<ReportEstrazioneBIZ>();

                    var dtPlanStart = DateTime.Now;
                    var dtPlanEnd = dtPlanStart.AddDays(AppContextERD.SCHEDULE_PLAN_DAYS);

                    //Verifica essistenza ed aggiunge schedulazioni
                    foreach (var repBiz in reports)
                    {
                        try
                        {
                            if (repBiz.DataObj.Attivo == 0)
                            {
                                //Per le non attive eliminiamo eventuali schedulazioni attive
                                repBiz.EliminaSchedulazioniAttive();
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
                            AppContextERD.WriteLog("ERROR", $"Errore nel caricamento della schedulazione per {repBiz.DataObj.Nome} ({repBiz.DataObj.Id}): {e.Message}");
                            try
                            {

                                MailUT.SendMail(host: AppContextERD.Conf["SmtpNotifiche:Host"],
                                                                port: int.Parse(AppContextERD.Conf["SmtpNotifiche:Port"]),
                                                                useauth: bool.Parse(AppContextERD.Conf["SmtpNotifiche:UseAuthentication"]),
                                                                ssl: bool.Parse(AppContextERD.Conf["SmtpNotifiche:EnableSsl"]),
                                                                user: AppContextERD.Conf["SmtpNotifiche:Username"],
                                                                pass: AppContextERD.Conf["SmtpNotifiche:Password"],
                                                                from: AppContextERD.Conf["SmtpNotifiche:From"],
                                                                to: AppContextERD.Conf["SmtpNotifiche:To"],
                                                                cc: AppContextERD.Conf["SmtpNotifiche:Cc"],
                                                                subj: $"ERR - ERD Scheduler - {repBiz.DataObj.Nome} ({repBiz.DataObj.Id})",
                                                                body: $"Si è verificato il seguente errore:<br/>{e.Message}<br/><br/>{e.StackTrace}", 
                                                                files: null);
                            }
                            catch (Exception)
                            {
                                AppContextERD.WriteLog("ERROR", $"Errore nell'invio mail di notifica");
                            }
                        }

                    }

                }

                this.printSchedules();

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
            bEseguiReload |= string.IsNullOrWhiteSpace(this.Schedule_Last_Hash);

            //Verifica hash cambiato
            bEseguiReload |= (newhash != this.Schedule_Last_Hash);


            //Se necessario reload procede
            if (bEseguiReload)
            {
                this.ReloadReportSchedules();

                this.Schedule_Last_Hash = newhash;
                this.Schedule_Last_Refresh = DateTime.Now;
            }

        }

        private string calculateHash()
        {
            using (var slot = AppContextERD.CreateSlot())
            {
                return QueryReports.CalculateSchedulesHash(slot);
            }
        }





        private void runUserJob(long schedId)
        {
            var bSendEmail = true;
            var sb = new StringBuilder();

            try
            {
                using (var jslot = AppContextERD.CreateSlot())
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
                    jslot.OnLogDebugSent += (a, b, c) => sb.AppendLine($"{b} - {c}");

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

                    //Se presente un log di debug lo scrive
                    if (sb.Length >0)
                        AppContextERD.WriteLog("SLOT_DEBUG", sb.ToString());
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

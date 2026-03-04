using Business.Data.Objects.Common.Utils;
using ERD.Scheduler;
using ERD.Service.BIZ;
using ERD.Service.BIZ.Utils;
using ERD.Service.Common.Enums;
using ERD.Service.DAL;
using FluentScheduler;
using MoreLinq;
using Org.BouncyCastle.Crypto.Digests;
using System.IO;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;


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

                    //Carica tutte le estrazioni per rivalutarle
                    var reports = slot.CreateList<ReportEstrazioneLista>()
                                        .SearchAllObjects()
                                        .ToBizObjectList<ReportEstrazioneBIZ>();

                    var dtPlanStart = DateTime.Now;
                    var dtPlanEnd = dtPlanStart.AddDays(AppContextERD.SCHEDULE_PLAN_DAYS);

                    //Verifica essistenza ed aggiunge schedulazioni
                    reports.ForEach(r => {
                        try
                        {
                            //Ricalcola piano schedulazione. All'interno se piano non attivo elimina tutto
                            r.RebuildPianoSchedulazione(dtPlanStart, dtPlanEnd);

                            r.ListaSchedulazioniAttive.ForEach(s => {
                                var schedId = s.Id;

                                JobManager.AddJob(() => this.runUserJob(schedId),
                                                    j => j.WithName(r.DataObj.Nome).ToRunOnceAt(s.DataEsecuzione));


                            });
                        }
                        catch (Exception e)
                        {
                            AppContextERD.WriteLog("ERROR", $"Errore nel caricamento della schedulazione per {r.DataObj.Nome} ({r.DataObj.Id}): {e.Message}");
                            AppContextERD.NotificaMailErrore($"{r.DataObj.Nome} ({r.DataObj.Id})", e.Message, e.StackTrace);
                        }
                    });
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
            try
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
            catch (Exception e)
            {
                AppContextERD.WriteLog("ERROR", $"Errore nel refresh della schedulazione: {e.Message}");
                AppContextERD.NotificaMailErrore($"Refresh della schedulazione: {e.Message}", e.Message, e.StackTrace);
            }
        }


        private string calculateHash()
        {
            using (var slot = AppContextERD.CreateSlot())
            {
                return Convert.ToHexString(SHA1.HashData(Encoding.UTF8.GetBytes(string.Join(",", 
                                slot.CreateList<ReportEstrazioneLista>()
                                    .OrderByLinq(x => x.Id)
                                    .SearchByLinq(x => x.Attivo == 1 && x.CronString != "" && DateTime.Today.Between(x.DataInizio, x.DataFine))
                                    .Select(x => $"[{x.Id}]_[{x.CronString}]")))));

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
            catch (Exception e)
            {
                AppContextERD.WriteLog("ERROR", $"Errore nell'esecuzione della schedulazione {schedId}: {e.Message}");
                AppContextERD.NotificaMailErrore($"Schedulazione {schedId}", e.Message, e.StackTrace);
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

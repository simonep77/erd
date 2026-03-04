using Business.Data.Objects.Common.Utils;
using DocumentFormat.OpenXml.EMMA;
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
        private List<Schedule> SystemSchedules { get; set; } = new List<Schedule>();
        private List<Schedule> UserSchedules { get; set; } = new List<Schedule>();


        public void Start()
        {
            AppContextERD.WriteLog("INFO", @"Schedulatore interno job inizializzazione...");

            //Schedula check modifiche
            this.SystemSchedules.Add(new Schedule(() => this.runUpdateScheduleCheck(false), s => s.Every(AppContextERD.SCHEDULE_CHECK_SECONDS).Seconds()));
            //Rischedula update del piano ogni notte
            this.SystemSchedules.Add(new Schedule(() => this.runUpdateScheduleCheck(true), s => s.Everyday().At(AppContextERD.SCHEDULE_REBUILD_TIME.Hour, AppContextERD.SCHEDULE_REBUILD_TIME.Minute)));
            //Lancia il primo caricamento del piano
            this.runUpdateScheduleCheck(true);

            AppContextERD.WriteLog("INFO", @"Schedulatore interno job avviato");
        }

        public void Stop()
        {
            AppContextERD.WriteLog("INFO", @"Schedulatore interno job in chiusura...");

            //Stoppa schedulazioni di sistema, le resetta e le pulisce
            this.SystemSchedules.Stop();
            this.SystemSchedules.ResetScheduling();
            this.SystemSchedules.Clear();


            //Stoppa tutte le schedulazioni utente, le resetta e le pulisce
            this.UserSchedules.Stop();
            this.UserSchedules.ResetScheduling();
            this.UserSchedules.Clear();

            AppContextERD.WriteLog("INFO", @"Schedulatore interno job terminato");
        }



        public void ReloadReportSchedules()
        {
            //Mette in pausa tutte le schedulazioni
            this.SystemSchedules.Stop();
            this.UserSchedules.Stop();
            try
            {
                this.UserSchedules.ResetScheduling();
                this.UserSchedules.Clear();

                using (var slot = AppContextERD.CreateSlot())
                {
                    //Carica tutte le estrazioni per rivalutarle
                    var reports = slot.CreateList<ReportEstrazioneLista>()
                                        .SearchAllObjects()
                                        .ToBizObjectList<ReportEstrazioneBIZ>();

                    var dtPlanStart = DateTime.Now;
                    var dtPlanEnd = dtPlanStart.AddDays(AppContextERD.SCHEDULE_PLAN_DAYS);

                    //Verifica essistenza ed aggiunge schedulazioni
                    reports.ForEach(r =>
                    {
                        try
                        {
                            //Ricalcola piano schedulazione. All'interno se piano non attivo elimina tutto
                            r.RebuildPianoSchedulazione(dtPlanStart, dtPlanEnd);

                            r.ListaSchedulazioniAttive.ForEach(s =>
                            {
                                var schedId = s.Id;
                                this.UserSchedules.Add(new Schedule(() => this.runUserJob(schedId), x => x.OnceAt(s.DataEsecuzione)));

                                AppContextERD.WriteLog(@"INFO", $"   > Schedulazione il {s.DataEsecuzione:dd/MM/yyyy HH:mm} - {s.Id.ToString().PadLeft(6, '0')} - {r.DataObj.Nome}");
                            });
                        }
                        catch (Exception e)
                        {
                            AppContextERD.WriteLog("ERROR", $"Errore nel caricamento della schedulazione per {r.DataObj.Nome} ({r.DataObj.Id}): {e.Message}");
                            AppContextERD.NotificaMailErrore($"{r.DataObj.Nome} ({r.DataObj.Id})", e.Message, e.StackTrace);
                        }
                    });
                }

                AppContextERD.WriteLog(@"INFO", $"Totale schedulazioni caricate: {this.UserSchedules.Count}");
            }
            finally
            {
                //Riavvia tutte le schedulazioni
                this.SystemSchedules.Start();
                this.UserSchedules.Start();
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

            AppContextERD.WriteLog("INFO", $" > Avvio schedulazione {schedId}");
            try
            {
                using (var jslot = AppContextERD.CreateSlot())
                {
                    //Ricerca schedulazione db
                    var sched = jslot.LoadObjByPK<ReportSchedulazione>(schedId);

                    AppContextERD.WriteLog("INFO", $" > Schedulazione {schedId}: [report] {sched.Estrazione.Nome}");

                    //Aggiorna piano schedulazione
                    sched.StatoId = eReport.StatoSchedulazione.Avviata;
                    jslot.SaveObject(sched);

                    //Scrive nel log il debug User1
                    jslot.OnLogDebugSent += (a, b, c) => sb.AppendLine($"{b} - {c}");

                    var repBiz = sched.Estrazione.ToBizObject<ReportEstrazioneBIZ>();

                    try
                    {
                        repBiz.Run(true, bSendEmail, true);
                    }
                    catch (Exception) { }

                    //Aggiorna piano schedulazione
                    if (repBiz.LastResult.Id > 0)
                        sched.OutputId = repBiz.LastResult.Id;
                    sched.StatoId = eReport.StatoSchedulazione.Eseguita;
                    jslot.SaveObject(sched);
                
                    //Se presente un log di debug lo scrive
                    if (sb.Length > 0)
                        AppContextERD.WriteLog("SLOT_DEBUG", sb.ToString());
                }
            }
            catch (Exception e)
            {
                AppContextERD.WriteLog("INFO", $" > Errore schedulazione {schedId}: {e.Message}");
                AppContextERD.NotificaMailErrore($"Schedulazione {schedId}", e.Message, e.StackTrace);
            }
            finally
            {
                AppContextERD.WriteLog("INFO", $" > Fine schedulazione {schedId}");
                //Forza deallocazione memoria non più utilizzata
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }


        }

    }
}

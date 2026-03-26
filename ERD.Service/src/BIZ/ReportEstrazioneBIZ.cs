using Business.Data.Objects.Core;
using Business.Data.Objects.Database;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.EMMA;
using ERD.Service.BIZ.Utils;
using ERD.Service.Common;
using ERD.Service.Common.Enums;
using ERD.Service.DAL;
using ERD.Service.Models;
using Hfs.Client;
using ICSharpCode.SharpZipLib.Zip;
using MoreLinq;
using NCrontab.Advanced;
using System.Data;
using System.Net;
using System.Text;
using System.Text.Json;

namespace ERD.Service.BIZ
{
    public class ReportEstrazioneBIZ : BusinessObject<ReportEstrazione>
    {

        public ReportEstrazioneBIZ(ReportEstrazione obj) : base(obj) { }


        #region PROPERTY
        private DataTable? mTabResultSQL;
        public ReportEstrazioneOutput LastResult { get; set; }

        public ReportEstrazioneOutputLista ListaOutput => this.LazyGet(nameof(ListaOutput), () => this.Slot.CreateList<ReportEstrazioneOutputLista>()
                                                                                                                                 .OrderByLinq(x => x.Id)
                                                                                                                                 .SearchByLinq(x => x.EstrazioneId == this.DataObj.Id));

        /// <summary>
        /// Lista destinatari email
        /// </summary>
        public ReportEstrazioneDestinatarioEmailLista ListaDesinatariEmail => this.LazyGet(nameof(ListaDesinatariEmail), () => this.Slot.CreateList<ReportEstrazioneDestinatarioEmailLista>()
                                                                                                                                        .SearchByLinq(x => x.EstrazioneId == this.DataObj.Id)
        );


        public ReportEstrazioneSqlHistoryLista ListaSqlHistory => this.LazyGet(nameof(ListaSqlHistory), () => this.Slot.CreateList<ReportEstrazioneSqlHistoryLista>()
                                                                                                                        .OrderByLinq(x => x.Id)
                                                                                                                        .SearchByLinq(x => x.EstrazioneId == this.DataObj.Id));


        public ReportSchedulazioneLista ListaSchedulazioni => this.LazyGet(nameof(ListaSchedulazioni), () => this.Slot.CreateList<ReportSchedulazioneLista>()
                                                                                                                        .OrderByLinq(x => x.Id)
                                                                                                                        .SearchByLinq(x => x.EstrazioneId == this.DataObj.Id));


        public ReportSchedulazioneLista ListaSchedulazioniAttive => this.LazyGet(nameof(ListaSchedulazioniAttive), () => this.Slot.CreateList<ReportSchedulazioneLista>()
                                                                                                                        .OrderByLinq(x => x.Id)
                                                                                                                        .SearchByLinq(x => x.EstrazioneId == this.DataObj.Id && x.StatoId == eReport.StatoSchedulazione.Pianificata));




        /// <summary>
        /// Indica se previsto invio email
        /// </summary>
        public bool IsPrevistoInvioMail => this.LazyGet(nameof(IsPrevistoInvioMail), () => this.DataObj.InvioMailAttivo > 0 && this.ListaDesinatariEmail.Where(d => d.Attivo > 0).Any());

        /// <summary>
        /// Indica se prevista la copia su path
        /// </summary>
        public bool IsPrevistoCopyToPath => this.LazyGet(nameof(IsPrevistoCopyToPath), () => !string.IsNullOrWhiteSpace(this.DataObj.CopyToPath));

        /// <summary>
        /// Indica se attiva la schedulazione
        /// </summary>
        public bool IsSchedulazioneAttiva => this.LazyGet(nameof(IsSchedulazioneAttiva), () => this.DataObj.Attivo > 0 && !string.IsNullOrWhiteSpace(this.DataObj.CronString));

        /// <summary>
        /// Indica se presenti altre estrazioni da accorpare a questa
        /// </summary>
        public bool IsAccorpato => this.LazyGet(nameof(IsAccorpato), () => !string.IsNullOrWhiteSpace(this.DataObj.EstrazioniAccorpateIds));


        public IEnumerable<ReportEstrazione> ListaEstrazioniDaAccorpare => this.LazyGet(nameof(ListaEstrazioniDaAccorpare), () =>
        {
            if (this.IsAccorpato)
            {
                return this.DataObj.EstrazioniAccorpateIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => this.Slot.LoadObjNullByPK<ReportEstrazione>(Convert.ToInt32(x)))
                                .ToList();

            }

            return new List<ReportEstrazione>();
        });


        /// <summary>
        /// indica se ha un template custom
        /// </summary>
        public bool IsTemplateCustom => this.DataObj.TemplateId > 0;


        /// <summary>
        /// Template XLSX da utilizzare al posto di quello a DB
        /// </summary>
        public byte[] ForcedTemplate { get; set; } = null;


        #endregion

        #region PUBLIC

        public void Salva()
        {
            var bChangedSql = this.DataObj.GetCurrentChanges().Where(p => p == nameof(ReportEstrazione.SqlText)).Any();
            //Salva dati
            this.Slot.SaveObject(this.DataObj);

            //Storicizza SQL
            if (bChangedSql)
                this.handleSqlHistory();

        }

        /// <summary>
        /// Ritorna la prossima schedulazione determniata sulla base della data di riferimento
        /// </summary>
        /// <param name="dtRif"></param>
        /// <returns></returns>
        public DateTime GetNextSchedule(DateTime dtRif)
        {
            var cronExp = CrontabSchedule.Parse(this.DataObj.CronString);
            var dtInit = dtRif;
            var nextRun = cronExp.GetNextOccurrence(dtInit);

            return nextRun;
        }



        /// <summary>
        /// Clona una estrazione e tutti gli elementi dipendenti salvando tutto su db
        /// </summary>
        /// <returns></returns>
        public ReportEstrazioneBIZ ClonaEstrazione(bool salvaIncludi)
        {
            var estBiz = this.Slot.CloneObjectForNew(this.DataObj).ToBizObject<ReportEstrazioneBIZ>();
            estBiz.DataObj.Nome = string.Concat("Clone di ", this.DataObj.Nome);

            if (!salvaIncludi)
                return estBiz;

            //Salva
            estBiz.Salva();

            //Clona destinatari email
            this.ListaDesinatariEmail.ForEach(x =>
            {
                var dest = this.Slot.CloneObjectForNew(x);
                dest.EstrazioneId = estBiz.DataObj.Id;
                this.Slot.SaveObject(dest);
                estBiz.ListaDesinatariEmail.AddOrUpdate(dest);
            });

            //Clona template
            if (this.DataObj.TemplateId > 0)
            {
                var tpl = this.Slot.CloneObjectForNew(this.DataObj.Template);
                this.Slot.SaveObject(tpl);

                estBiz.DataObj.TemplateId = tpl.Id;
            }

            return estBiz;
        }



        public void EliminaLogicamente()
        {
            this.DataObj.Attivo = -1;
            this.Slot.SaveObject(this.DataObj);

        }


        public void Run(bool saveResult, bool sendEmail, bool copyTo)
        {
            this.Slot.LogDebug(DebugLevel.Debug_1, "Avvio Run()");

            this.Slot.LogDebug(DebugLevel.Debug_1, "Begin salvataggio output");
            this.LastResult = this.Slot.CreateObject<ReportEstrazioneOutput>();
            this.LastResult.EstrazioneId = this.DataObj.Id;
            this.LastResult.DataOraInizio = DateTime.Now;
            this.LastResult.StatoId = eReport.StatoEstrazione.Avviata;
            this.LastResult.TipoFileId = this.DataObj.TipoFileId;

            if (saveResult)
                this.Slot.SaveObject(this.LastResult);
            this.Slot.LogDebug(DebugLevel.Debug_1, "End salvataggio output");

            try
            {
                //Esegue query
                this.Slot.LogDebug(DebugLevel.Debug_1, "Begin runSQL()");
                this.mTabResultSQL = this.RunSQL();
                this.Slot.LogDebug(DebugLevel.Debug_1, "End runSQL()");

                //se accorpamento dati (allora neanche esefue render finale)
                this.runAccorpaSoloDati();

                //Render
                this.Slot.LogDebug(DebugLevel.Debug_1, "Begin renderOutput()");
                this.renderOutput();
                this.Slot.LogDebug(DebugLevel.Debug_1, "End renderOutput()");

                //Se accorpamento postumo solo excel...
                this.runAccorpaAltreEstrazioni();

                //Esegue copia
                if (copyTo)
                    this.runCopyTo();

                //Esegue sendmail
                if (sendEmail)
                {
                    //Manda se tipo 1 (sempre) o se tipo 2 (condizionato) in presenza di righe
                    if (this.DataObj.InvioMailAttivo == 1 || (this.DataObj.InvioMailAttivo == 2 && this.mTabResultSQL.Rows.Count > 0))
                        this.SendEmail(saveResult);
                }

                //Esito OK
                this.LastResult.StatoId = eReport.StatoEstrazione.TerminataConSuccesso;

            }
            catch (Exception e)
            {
                this.LastResult.StatoId = eReport.StatoEstrazione.TerminataConErrori;
                this.LastResult.EstrazioneEsito = e.Message;
            }
            finally
            {

            }


            //Fine: aggiornamento output
            this.Slot.LogDebug(DebugLevel.Debug_1, "Begin aggiornamento output");
            this.LastResult.DataOraFine = DateTime.Now;

            if (saveResult)
            {
                this.Slot.SaveObject(this.LastResult);
                this.ListaOutput.AddOrUpdate(this.LastResult);
            }


            this.Slot.LogDebug(DebugLevel.Debug_1, "End aggiornamento output");

            //Se errori esce
            if (this.LastResult.StatoId == eReport.StatoEstrazione.TerminataConErrori)
                throw new ApplicationException(@"L'esecuzione e' terminata con errori: " + this.LastResult.EstrazioneEsito);


            this.Slot.LogDebug(DebugLevel.Debug_1, "Begin pulizia output");
            this.cleanOutput();
            this.Slot.LogDebug(DebugLevel.Debug_1, "End pulizia output");

        }


        private void runAccorpaSoloDati()
        {
            if (!this.IsAccorpato)
                return;

            //Indicatore di accorpamento
            if (this.DataObj.AccorpaSoloDati < 1)
                return;
            //Solo excel si puo' accorpare


            this.Slot.LogDebug(DebugLevel.Debug_1, "Begin accorpamento dati");


            var ids = this.DataObj.EstrazioniAccorpateIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(id => Convert.ToInt32(id));


            //Salva stato simulate per consentire ripristino senza sovrascrittura (es. gia' in simulate)

            foreach (var id in ids)
            {
                this.Slot.LogDebug(DebugLevel.Debug_1, $"Begin merge dati id {id}");
                try
                {
                    //Esegue altra estrazione
                    var repBiz = this.Slot.BizNewWithLoadByPK<ReportEstrazioneBIZ>(id);
                    //Se eliminato logicamente non lo considera
                    if (repBiz.DataObj.Attivo == -1)
                    {
                        this.Slot.LogDebug(DebugLevel.Debug_1, "Estrazione eliminata logicamente. Skip.");
                        return;
                    }
                    //Lancia Query
                    //Esegue merge
                    this.mTabResultSQL.Merge(repBiz.RunSQL());
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    this.Slot.LogDebug(DebugLevel.Debug_1, $"End merge dati id {id}");
                }

            }




            this.Slot.LogDebug(DebugLevel.Debug_1, "End accorpamento dati");
        }

        /// <summary>
        /// Ritorna il nome file da utilizzare per gli output dell estrazione
        /// </summary>
        /// <returns></returns>
        public string getNomeFileIstantaneo()
        {
            var nomeFile = string.Empty;

            if (!string.IsNullOrWhiteSpace(this.DataObj.NomeFileMask))
            {
                nomeFile = string.Format(this.DataObj.NomeFileMask, this.LastResult.DataOraInizio);
            }
            else
            {
                var fileBase = this.DataObj.Nome.Replace(' ', '_');

                nomeFile = String.Format(@"{0}_{1:yyyy_MM_dd}{2}", fileBase, this.LastResult.DataOraInizio, this.DataObj.TipoFile.Estensione);
            }

            return nomeFile;
        }

        private void runAccorpaAltreEstrazioni()
        {
            if (!this.IsAccorpato)
                return;

            //Se richiesto solo accorpamento dati esce
            if (this.DataObj.AccorpaSoloDati > 0)
                return;

            //Solo excel si puo' accorpare
            if (this.DataObj.TipoFileId != eReport.TipoFile.Excel)
                throw new ApplicationException(@"E' possibile accorpare solo estrazioni di tipo Excel");

            this.Slot.LogDebug(DebugLevel.Debug_1, "Begin accorpamento");


            var ids = this.DataObj.EstrazioniAccorpateIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(id => Convert.ToInt32(id));
            var estraz = new List<byte[]>();

            estraz.Add(this.LastResult.DataBlob);

            //Salva stato simulate per consentire ripristino senza sovrascrittura (es. gia' in simulate)

            foreach (var id in ids)
            {
                this.Slot.LogDebug(DebugLevel.Debug_1, $"Begin merge excel id {id}");
                try
                {
                    //Esegue altra estrazione
                    var repBiz = this.Slot.BizNewWithLoadByPK<ReportEstrazioneBIZ>(id);
                    //Verifica se eliminata
                    if (repBiz.DataObj.Attivo == -1)
                    {
                        this.Slot.LogDebug(DebugLevel.Debug_1, "Estrazione eliminata logicamente. Skip.");
                        return;
                    }
                    //Tutte le estrazioni devono essere dello stesso tipo

                    //Solo excel si puo' accorpare
                    if (repBiz.DataObj.TipoFileId != this.DataObj.TipoFileId)
                        throw new ApplicationException($"L'estrazione {repBiz.DataObj.Id} - {repBiz.DataObj.Nome} deve essere dello stesso tipo di quella principale {this.DataObj.Id}");

                    repBiz.Run(false, false, false);
                    //Aggiunge ad elenco
                    estraz.Add(repBiz.LastResult.DataBlob);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    this.Slot.LogDebug(DebugLevel.Debug_1, $"Begin merge excel id {id}");
                }

            }



            if (estraz.Count <= 1)
                return;

            //Accorpa ed imposta su questo risultato
            this.LastResult.DataBlob = ExcelUT.EseguiAccorpamento(estraz);
            this.LastResult.DataLen = this.LastResult.DataBlob.Length;


            this.Slot.LogDebug(DebugLevel.Debug_1, "End accorpamento");

        }



        /// <summary>
        /// Esegue copia in directory specifica
        /// </summary>
        private void runCopyTo()
        {
            if (string.IsNullOrWhiteSpace(this.DataObj.CopyToPath))
                return;

            this.Slot.LogDebug(DebugLevel.Debug_1, "Begin Copy To");

            var pathText = this.DataObj.CopyToPath.Trim();

            //Se json allora notazione hfs
            if (pathText.StartsWith(@"{", StringComparison.Ordinal))
            {
                //HFS
                var obj = JsonSerializer.Deserialize<CopyToPathReportModel>(pathText)
                    ?? throw new ArgumentException($"Il valore della stringa Json in {nameof(ReportEstrazione.CopyToPath)} non è valido");

                string vpath = string.Format(obj.Path.ToString(), this.LastResult.DataOraInizio);

                ////vai   aa
                using (var hfs = new HfsClient(obj.Uri))
                {
                    hfs.FileWriteFromBuffer(vpath, this.LastResult.DataBlob);
                }
            }
            else
            {
                //Fisico o UNC
                var finalPath = string.Format(this.DataObj.CopyToPath, this.LastResult.DataOraInizio);

                //Si assicura la presenza della cartella
                var dir = Path.GetDirectoryName(finalPath);

                Directory.CreateDirectory(dir);

                //Ok, scrive il file
                File.WriteAllBytes(finalPath, this.LastResult.DataBlob);
            }

            this.Slot.LogDebug(DebugLevel.Debug_1, "End Copy To");

        }


        /// <summary>
        /// Ritorna lo stream ed il nome file per il dispatch del file via mail o su cartelle
        /// </summary>
        /// <returns></returns>
        private dynamic getBlobForDispatch(ReportEstrazioneDestinatarioEmail dest)
        {
            var ms = new MemoryStream();
            var filenameMail = this.LastResult.NomeFile;

            if (!string.IsNullOrEmpty(dest.Password))
            {
                filenameMail = Path.ChangeExtension(this.LastResult.NomeFile, @".zip");
                using (ZipOutputStream s = new ZipOutputStream(ms))
                {
                    s.IsStreamOwner = false; //non chiude lo stream sottostante
                    s.SetLevel(5); // 0 - store only to 9 - means best compression
                    s.Password = dest.Password;

                    var entry = new ZipEntry(Path.GetFileName(this.LastResult.NomeFile));
                    entry.DateTime = DateTime.Now;
                    s.PutNextEntry(entry);

                    using (var input = new MemoryStream(this.LastResult.DataBlob))
                    {
                        input.Position = 0;
                        input.CopyTo(s);
                    }
                    //s.Finish();
                    //s.Close();
                }

            }
            else
                ms.Write(this.LastResult.DataBlob, 0, this.LastResult.DataBlob.Length);

            ms.Position = 0;

            return new { NomeFile = filenameMail, Stream = ms };
        }

        public List<ReportEstrazioneDestinatarioEmail> SendEmail(bool saveResult)
        {
            var retList = new List<ReportEstrazioneDestinatarioEmail>();
            //Se non impostata email esce
            if (!this.IsPrevistoInvioMail)
                return retList;

            this.Slot.LogDebug(DebugLevel.Debug_1, "Begin invio mail");
            this.LastResult.MailEsito = string.Empty;
            this.LastResult.MailDataInvio = DateTime.MinValue;
            var sbErr = new StringBuilder();


            foreach (var item in this.ListaDesinatariEmail.Where(e => e.Attivo > 0))
            {

                try
                {

                    //Invia
                    using (var smtp = new System.Net.Mail.SmtpClient())
                    {
                        smtp.Host = item.SmtpConfig.Smtp;
                        smtp.Port = item.SmtpConfig.Port;
                        smtp.EnableSsl = (item.SmtpConfig.UseSSL > 0);
                        if (item.SmtpConfig.Auth > 0)
                        {
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = new NetworkCredential(item.SmtpConfig.UserName, item.SmtpConfig.Password);
                        }

                        using (var msg = new System.Net.Mail.MailMessage(item.MailFROM.Trim(), item.MailTO.Trim()))
                        {

                            if (!string.IsNullOrWhiteSpace(item.MailCC))
                                msg.CC.Add(item.MailCC.Trim());

                            if (!string.IsNullOrWhiteSpace(item.MailBCC))
                                msg.Bcc.Add(item.MailBCC.Trim());

                            msg.IsBodyHtml = true;
                            msg.Subject = String.Format(item.MailSUBJ, this.LastResult.DataOraInizio);
                            msg.Body = String.Format(item.MailBODY, this.LastResult.DataOraInizio);

                            if (string.IsNullOrWhiteSpace(this.DataObj.CopyToPath))
                            {
                                var fileout = this.getBlobForDispatch(item);

                                msg.Attachments.Add(new System.Net.Mail.Attachment(fileout.Stream, fileout.NomeFile));

                            }
                            else
                            {
                                if (this.DataObj.CopyToPath.Trim().StartsWith(@"{"))
                                {
                                    msg.Body += $"<br/><br/>Il file e' stato depositato su una cartella remota.";
                                }
                                else
                                {
                                    var finalPath = string.Format(this.DataObj.CopyToPath, this.LastResult.DataOraInizio);

                                    msg.Body += $"<br/><br/>Il file e' stato depositato in:<br/><a href='file://{finalPath.Replace(@"\", @"/")}'>{finalPath}</a>";
                                }
                            }


                            smtp.Send(msg);

                            //Aggiunge a email inviate
                            retList.Add(item);

                            //Scrive nel log dello slot
                            this.Slot.LogDebug(DebugLevel.User_1, @"Mail inviata");
                            this.Slot.LogDebug(DebugLevel.User_1, $" >> MailTO: {item.MailTO}");
                            this.Slot.LogDebug(DebugLevel.User_1, $" >> MailCC: {item.MailCC}");
                            this.Slot.LogDebug(DebugLevel.User_1, $" >> MailBCC: {item.MailBCC}");

                            //Fine: aggiornamento
                            this.LastResult.MailDataInvio = DateTime.Now;
                        }

                    }


                }
                catch (Exception ex)
                {
                    sbErr.AppendLine($"Errore invio email a {item.MailTO}: {ex.Message}");
                }

            }



            //Salva esito mail
            if (saveResult)
                this.Slot.SaveObject(this.LastResult);

            //Ripropaga email
            if (sbErr.Length > 0)
                throw new ApplicationException($"Errore nell'invio mail: {sbErr}");

            this.Slot.LogDebug(DebugLevel.Debug_1, "End invio mail");

            return retList;

        }


        private void cleanOutput()
        {

            var iRemove = this.ListaOutput.Count - this.DataObj.NumOutputStorico;

            //Verifica se necessario farlo
            if (this.DataObj.NumOutputStorico == 0 || iRemove <= 0)
                return;

            //Elimina
            for (int i = 0; i < iRemove; i++)
            {
                var item = this.ListaOutput[0];
                this.ListaOutput.RemoveAt(0);
                this.Slot.DeleteObject(item);
            }
        }


        public DataTable RunSQL()
        {
            var minAttesa = 30;
            var mutexKey = $"ERD_SEM_CONN_{this.DataObj.ConnessioneId}";

            this.Slot.LogDebug(DebugLevel.Debug_1, $"Creazione mutex {mutexKey}");

            using (var mtx = new Mutex(false, mutexKey))
            {
                this.Slot.LogDebug(DebugLevel.Debug_1, $"Attesa mutex {minAttesa} minuti");
                if (!mtx.WaitOne(minAttesa * 60 * 1000))
                {
                    throw new ApplicationException($"Impossibile eseguire la query entro i {minAttesa} minuti di attesa della connessione {this.DataObj.ConnessioneId} - {this.DataObj.Connessione.Nome}");
                }
                try
                {
                    this.Slot.LogDebug(DebugLevel.Debug_1, "Creazione connessione DB");
                    using (var db = Business.Data.Objects.Database.DataBaseFactory.CreaDataBase(this.DataObj.Connessione.BdoDbConnectioType, this.DataObj.Connessione.ConnectionString))
                    {
                        db.AutoCloseConnection = true;
                        db.ExecutionTimeout = 100000;

                        //Imposta sql
                        db.SQL = this.DataObj.SqlText;

                        //Aggiunge una serie di parametri di sistema:
                        this.loadErdSqlParameters(db);

                        //Esegue
                        this.Slot.LogDebug(DebugLevel.Debug_1, "Begin esecuzione");
                        return db.Select();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    this.Slot.LogDebug(DebugLevel.Debug_1, "End esecuzione (release mutex)");
                    mtx.ReleaseMutex();
                }

            }
        }

        /// <summary>
        /// Calcola le schedulazioni nell'intervallo date specificato
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public IEnumerable<DateTime> CalcSchedules(DateTime start, DateTime end)
        {
            //Viene utilizzata la notazione senza secondi NCrontab (Cron.guru)
            var c = CrontabSchedule.Parse(this.DataObj.CronString);

            return c.GetNextOccurrences(start, end);
        }


        private void loadErdSqlParameters(IDataBase db)
        {

            db.AddParameter(Costanti.Sql_Params.REPORT_ID, this.DataObj.Id);

            var dtNow = DateTime.Now;
            var dtAppo = dtNow.Date;
            //Date Varie
            db.AddParameter(Costanti.Sql_Params.DATE_TODAY, dtAppo);
            db.AddParameter(Costanti.Sql_Params.DATE_YESTERDAY, dtAppo.AddDays(-1));

            dtAppo = dtNow.Date.AddDays(dtNow.DayOfWeek == DayOfWeek.Sunday ? 6 : ((int)dtNow.DayOfWeek - 1));
            db.AddParameter(Costanti.Sql_Params.DATE_INIT_THIS_WEEK, dtAppo);
            db.AddParameter(Costanti.Sql_Params.DATE_END_THIS_WEEK, dtAppo.AddDays(6));

            //@ERD_LAST_ELAB: ultima elaborazione
            var lastOutput = this.ListaOutput.Where(o => o.Id != this.LastResult?.Id).OrderBy(o => o.Id).LastOrDefault();

            dtAppo = lastOutput?.DataOraInizio ?? new DateTime(1900, 1, 1);

            db.AddParameter(Costanti.Sql_Params.LAST_ELAB_DATE, dtAppo);


        }


        private void renderOutput()
        {
            switch (this.DataObj.TipoFileId)
            {
                case eReport.TipoFile.Csv:
                    this.renderCsv();
                    break;

                case eReport.TipoFile.Excel:

                    if (this.DataObj.TemplateId == 0 && this.ForcedTemplate == null)
                        this.renderExcel();
                    else
                        this.renderExcelTemplate();
                    break;

                default:
                    break;
            }
        }

        private void renderCsv()
        {
            this.Slot.LogDebug(DebugLevel.Debug_1, "Begin render csv");
            var sb = new StringBuilder();

            foreach (DataColumn col in this.mTabResultSQL.Columns)
            {
                sb.Append(col.ColumnName);
                sb.Append(';');
            }
            sb.Remove(sb.Length - 1, 1);
            sb.AppendLine();

            foreach (DataRow row in this.mTabResultSQL.Rows)
            {
                foreach (DataColumn col in this.mTabResultSQL.Columns)
                {
                    sb.Append(row[col.ColumnName]);
                    sb.Append(';');
                }
                sb.Remove(sb.Length - 1, 1);
                sb.AppendLine();
            }

            //Imposta Output
            this.LastResult.NomeFile = this.getNomeFileIstantaneo();
            this.LastResult.DataLen = sb.Length;
            this.LastResult.DataBlob = Encoding.UTF8.GetBytes(sb.ToString());
            this.Slot.LogDebug(DebugLevel.Debug_1, "End render csv");

        }

        private void renderExcel()
        {
            this.Slot.LogDebug(DebugLevel.Debug_1, "Begin render excel flat");

            var sheetname = !string.IsNullOrEmpty(this.DataObj.SheetName) ? this.DataObj.SheetName : this.DataObj.Nome.PadRight(30, ' ').Substring(0, 30).Trim();

            var excel = ExcelUT.EseguiRenderDataTableExcel(this.mTabResultSQL!, this.DataObj.Nome, this.DataObj.Titolo, sheetname, null);
            this.LastResult.NomeFile = this.getNomeFileIstantaneo();
            this.LastResult.DataLen = excel.DatiMemory.Length;
            this.LastResult.DataBlob = excel.DatiMemory;

            this.Slot.LogDebug(DebugLevel.Debug_1, "End render excel flat");
        }


        private void renderExcelTemplate()
        {
            this.Slot.LogDebug(DebugLevel.Debug_1, "Begin render excel template");

            var sheetname = !string.IsNullOrEmpty(this.DataObj.SheetName) ? this.DataObj.SheetName : this.DataObj.Nome.PadRight(30, ' ').Substring(0, 30).Trim();

            if (this.DataObj.TemplateId == 0 && this.ForcedTemplate == null)
                throw new ArgumentException("Deve essere specificato un Id template nella definizione dell'estrazione oppure impostato un template esterno (ForcedTemplate)");

            if (this.ForcedTemplate == null && (this.DataObj.Template.TemplateBlob == null || this.DataObj.Template.TemplateBlob.Length == 0))
                throw new ArgumentException("Deve essere impostato un file template Excel [ClosedXML.Reports] a livello di estrazione valorizzando TemplateId impostando ForcedTemplate");

            var tplBlob = this.ForcedTemplate ?? this.DataObj.Template.TemplateBlob;

            using (var msIn = new MemoryStream(tplBlob))
            {
                //var ms = new MemoryStream(File.ReadAllBytes(@"C:\DATI_SIMONE\Desktop\aaa.xlsx"));

                var tpl = new ClosedXML.Report.XLTemplate(msIn);

                //ms.Dispose();

                tpl.AddVariable("Estrazione", this);
                tpl.AddVariable("Dati", this.mTabResultSQL.Rows.Cast<DataRow>());

                var ret = tpl.Generate();
                if (ret.HasErrors)
                    throw new ApplicationException("Errore nel rendering del template Excel: " + string.Join(" - ", ret.ParsingErrors.Select(s => s.Message)));

                //Scrive
                using (var msOut = new MemoryStream())
                {
                    tpl.SaveAs(msOut);

                    //Imposta blob output
                    this.LastResult.NomeFile = this.getNomeFileIstantaneo();
                    this.LastResult.DataBlob = msOut.ToArray();
                    this.LastResult.DataLen = this.LastResult.DataBlob.Length;

                }
            }


            this.Slot.LogDebug(DebugLevel.Debug_1, "End render excel template");
        }


        private void handleSqlHistory()
        {

            var h = this.Slot.CreateObject<ReportEstrazioneSqlHistory>();
            h.EstrazioneId = this.DataObj.Id;
            h.UtenteId = this.DataObj.UtenteIdAggiornamento;
            h.SqlText = this.DataObj.SqlText;

            this.Slot.SaveObject(h);
        }


        /// <summary>
        /// Indica se l'SQL contiene parametri di sistema che ne influiscono le esecuzioni successive
        /// </summary>
        /// <returns></returns>
        public bool IsSqlConParametriElaborazione()
        {

            if (this.DataObj.SqlText.Contains(Costanti.Sql_Params.LAST_ELAB_DATE))
                return true;

            return false;
        }

        protected override void deleteExecBefore()
        {
            this.Slot.DeleteAll(this.ListaDesinatariEmail);
            this.Slot.DeleteAll(this.ListaOutput);
            this.Slot.DeleteAll(this.ListaSchedulazioni);
            this.DeleteHistory(-1);
        }


        public void DeleteHistory(int keepNum)
        {

            if (keepNum <= 0)
            {
                this.Slot.DeleteAll(this.ListaSqlHistory);
                this.LazyReset(nameof(ListaSqlHistory));
            }
            else if (keepNum < this.ListaSqlHistory.Count)
            {
                this.ListaSqlHistory.Take(this.ListaSqlHistory.Count - keepNum).ToList().ForEach(x =>
                {
                    this.ListaSqlHistory.Remove(x);
                    this.Slot.DeleteObject(x);
                });
            }
        }


        /// <summary>
        /// Esegue la validazione dei dati
        /// </summary>
        public void ValidazioneDati()
        {
            if (this.DataObj.Attivo > 0 && !string.IsNullOrWhiteSpace(this.DataObj.CronString))
            {
                try
                {
                    var cron = CrontabSchedule.Parse(this.DataObj.CronString);
                }
                catch (Exception)
                {
                    throw new ApplicationException(@"Cronstring non valida");
                }
            }
        }

        /// <summary>
        /// Elimina le schedulazioni attive dal piano
        /// </summary>
        public void EliminaSchedulazioniAttive()
        {
            this.Slot.DeleteAll(this.ListaSchedulazioniAttive);
            this.ListaSchedulazioniAttive.Clear();
        }


        #endregion


        public void RebuildPianoSchedulazione(DateTime dtFrom, DateTime dtTo)
        {
            //Se non attiva la schedulazione allora rimuove tutte le schedulazioni attive
            if (this.DataObj.Attivo <= 0 || string.IsNullOrWhiteSpace(this.DataObj.CronString))
            {
                this.EliminaSchedulazioniAttive();
                return;
            }

            //Ricalcola
            var recalc = this.CalcSchedules(dtFrom, dtTo);

            recalc.ForEach(d =>
            {
                var scheds = this.ListaSchedulazioniAttive.Where(x => x.DataEsecuzione == d);

                if (!scheds.Any())
                {
                    //Crea nuova
                    var sched = this.Slot.CreateObject<ReportSchedulazione>();
                    sched.EstrazioneId = this.DataObj.Id;
                    sched.DataEsecuzione = d;
                    sched.StatoId = eReport.StatoSchedulazione.Pianificata;

                    this.Slot.SaveObject(sched);
                    sched.ExtraDataSet("IsOk", true);
                    this.ListaSchedulazioniAttive.Add(sched);
                }
                else
                    //Esiste già, la mantiene e la marca come da controllata (per eventuale update stato)
                    scheds.ForEach(x => x.ExtraDataSet("IsOk", true));
            });

            //Gestisce quelle non verificate
            this.ListaSchedulazioniAttive.Where(x => !x.ExtraDataGet<bool>("IsOk", false)).ToList().ForEach(x =>
            {
                if (x.DataEsecuzione <= dtFrom)
                {
                    x.StatoId = eReport.StatoSchedulazione.Saltata;
                    this.Slot.SaveObject(x);
                }
                else
                {
                    //Futura: la eliminiamo
                    this.Slot.DeleteObject(x);
                }
                this.ListaSchedulazioniAttive.Remove(x);
            });

        }

    } // class

} // namespace


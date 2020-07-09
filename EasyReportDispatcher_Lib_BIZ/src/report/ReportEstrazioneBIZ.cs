
using System;
using System.Linq;
using Bdo.Objects;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using EasyReportDispatcher_Lib_Common.src.enums;
using EasyReportDispatcher_Lib_DAL.src.report;
using EasyReportDispatcher_Lib_BIZ.src.utils;
using Ionic.Zip;
using Newtonsoft.Json;
using EasyReportDispatcher_Lib_Common.src;

namespace EasyReportDispatcher_Lib_BIZ.src.report
{
   public class ReportEstrazioneBIZ : BusinessObject<ReportEstrazione>
    {

       public ReportEstrazioneBIZ(ReportEstrazione obj) : base(obj) { }


        #region PROPERTY
        private DataTable mTabResultSQL;
        private ReportEstrazioneOutput mLastResult;

        public ReportEstrazioneOutput LastResult
        {
            get
            {
                return this.mLastResult;
            }
            set
            {
                this.mLastResult = value;
            }
        }

       private ReportEstrazioneOutputLista mListaOutput;
       public ReportEstrazioneOutputLista ListaOutput
        {
           get
           {
               if (this.mListaOutput == null)
               {
                   this.mListaOutput = this.Slot.CreateList<ReportEstrazioneOutputLista>()
                        .LoadFullObjects()
                        .OrderBy(nameof(ReportEstrazioneOutput.Id))
                        .SearchByColumn(new FilterEQUAL(nameof(ReportEstrazioneOutput.EstrazioneId), this.DataObj.Id));
               }
               return this.mListaOutput;
           }
       }

        private ReportEstrazioneDestinatarioEmailLista mListaDesinatariEmail;
        /// <summary>
        /// Lista destinatari email
        /// </summary>
        public ReportEstrazioneDestinatarioEmailLista ListaDesinatariEmail
        {
            get
            {
                if (this.mListaDesinatariEmail == null)
                {
                    this.mListaDesinatariEmail = this.Slot.CreateList<ReportEstrazioneDestinatarioEmailLista>()
                         .SearchByColumn(new FilterEQUAL(nameof(ReportEstrazioneDestinatarioEmail.EstrazioneId), this.DataObj.Id));
                }
                return this.mListaDesinatariEmail;
            }
        }


        /// <summary>
        /// Data la stringa cron di schedulazione indica se puo' girare oppure no. Funziona solo su base giornaliera
        /// </summary>
        public bool CanRunSchedulato
        { get
            {
                //Se non valorizzata stringa cron esce
                if (string.IsNullOrEmpty(this.DataObj.CronString))
                    return false;

                //Valuta lanciabilita'
                try
                {

                    return (this.GetNextSchedule(DateTime.Today).Date == DateTime.Today);
                }
                catch (Exception e)
                {
                    //Trap errori
                    this.Slot.LogDebug(DebugLevel.Error_1, "Errore su property 'CanRunSchedulato': {0}", e.Message);
                    this.Slot.LogDebug(DebugLevel.Error_1, e.StackTrace);
                }

                return false;
            }
        }

        

        /// <summary>
        /// Indica se previsto invio email
        /// </summary>
        public bool IsPrevistoInvioMail
        {
            get
            {
                return (this.DataObj.InvioMailAttivo > 0 && this.ListaDesinatariEmail.Where(d => d.Attivo > 0).Any());
            }
        }

        
        /// <summary>
        /// Indica se presenti altre estrazioni da accorpare a questa
        /// </summary>
        public bool IsAccorpato
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.DataObj.EstrazioniAccorpateIds);
            }
        }


        private ReportEstrazioneLista mListaEstrazioniDaAccorpare;
        public ReportEstrazioneLista ListaEstrazioniDaAccorpare
        {
            get
            {
                if (this.mListaEstrazioniDaAccorpare == null)
                {
                    this.mListaEstrazioniDaAccorpare = this.Slot.CreateList<ReportEstrazioneLista>();
                    if (this.IsAccorpato)
                    {
                        var dips = this.DataObj.EstrazioniAccorpateIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (var estid in dips)
                        {
                            var estDip = this.Slot.LoadObjNullByPK<ReportEstrazione>(estid);

                            this.mListaEstrazioniDaAccorpare.Add(estDip);

                        }

                    }

                }

                return this.mListaEstrazioniDaAccorpare;
            }
        }


        /// <summary>
        /// indica se ha un template custom
        /// </summary>
        public bool IsTemplateCustom
        {
            get
            {
                return (this.DataObj.TemplateId > 0);
            }
        }


        private byte[] mForcedTemplate;
        
        /// <summary>
        /// Template XLSX da utilizzare al posto di quello a DB
        /// </summary>
        public byte[] ForcedTemplate
        {
            get
            {
                return this.mForcedTemplate;
            }
            set
            {
                this.mForcedTemplate = value;
            }
        }


        #endregion

        #region PUBLIC


        /// <summary>
        /// Ritorna la prossima schedulazione determniata sulla base della data di riferimento
        /// </summary>
        /// <param name="dtRif"></param>
        /// <returns></returns>
        public DateTime GetNextSchedule(DateTime dtRif)
        {
            var cronExp = NCrontab.CrontabSchedule.Parse(this.DataObj.CronString);
            var dtInit = dtRif;
            var dtEnd = dtRif.AddYears(2);
            var nextRun = cronExp.GetNextOccurrence(dtInit, dtEnd);

            if (DateTime.Now >= nextRun)
            {
                //Verifica se oggi ha gia' girato schedulato
                var d1 = nextRun.Date;
                var d2 = d1.AddDays(1).AddSeconds(-1);
                var lastRun = this.ListaOutput.Where(o => { return (SDS.CommonUtils.DateUT.IsBetween(o.DataOraInizio, d1, d2) && o.StatoId != eReport.StatoEstrazione.TerminataConErrori); });


                if (lastRun.Any())
                    return this.GetNextSchedule(dtRif.AddDays(1));
            }

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
            this.Slot.SaveObject(estBiz.DataObj);

            //Clona destinatari email
            estBiz.mListaDesinatariEmail = this.Slot.CreateList<ReportEstrazioneDestinatarioEmailLista>();
            foreach (var item in this.ListaDesinatariEmail)
            {
                var dest = this.Slot.CloneObjectForNew(item);
                dest.EstrazioneId = estBiz.DataObj.Id;
                this.Slot.SaveObject(dest);

                estBiz.mListaDesinatariEmail.Add(item);
            }


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
            this.mLastResult = this.Slot.CreateObject<ReportEstrazioneOutput>();
            this.mLastResult.EstrazioneId = this.DataObj.Id;
            this.mLastResult.DataOraInizio = DateTime.Now;
            this.mLastResult.StatoId = eReport.StatoEstrazione.Avviata;
            this.mLastResult.TipoFileId = this.DataObj.TipoFileId;

            if (saveResult)
                this.Slot.SaveObject(this.mLastResult);
            this.Slot.LogDebug(DebugLevel.Debug_1, "End salvataggio output");

            try
            {
                //Esegue query
                this.Slot.LogDebug(DebugLevel.Debug_1, "Begin runSQL()");
                this.runSQL();
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

                //Esito OK
                this.mLastResult.StatoId = eReport.StatoEstrazione.TerminataConSuccesso;

            }
            catch (Exception e)
            {
                this.mLastResult.StatoId = eReport.StatoEstrazione.TerminataConErrori;
                this.mLastResult.EstrazioneEsito = e.Message;
            }
            finally
            {

            }

            //Fine: aggiornamento output
            this.Slot.LogDebug(DebugLevel.Debug_1, "Begin aggiornamento output");
            this.mLastResult.DataOraFine = DateTime.Now;
            
            if (saveResult)
            {
                this.Slot.SaveObject(this.mLastResult);
                this.ListaOutput.AddOrUpdate(this.mLastResult);
            }


            this.Slot.LogDebug(DebugLevel.Debug_1, "End aggiornamento output");

            //Se errori esce
            if (this.mLastResult.StatoId == eReport.StatoEstrazione.TerminataConErrori)
                throw new ApplicationException(@"L'esecuzione e' terminata con errori: " + this.mLastResult.EstrazioneEsito);

            //Serve?

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
                this.Slot.LogDebug(DebugLevel.Debug_1, "Begin merge dati id {0}", id);
                try
                {
                    //Esegue altra estrazione
                    var repBiz = this.Slot.BizNewWithLoadByPK<ReportEstrazioneBIZ>(id);
                    //Se eliminato logicamente non lo considera
                    if (repBiz.DataObj.Attivo == -1)
                    {
                        this.Slot.LogDebug(DebugLevel.Debug_1, "Estrazione eliminata logicamente. Skip.", id);
                        return;
                    }
                    //Lancia Query
                    repBiz.runSQL();
                    //Esegue merge
                    this.mTabResultSQL.Merge(repBiz.mTabResultSQL);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    this.Slot.LogDebug(DebugLevel.Debug_1, "End merge dati id {0}", id);
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

            if (string.IsNullOrWhiteSpace(this.DataObj.NomeFileMask))
            {
                nomeFile = string.Format(this.DataObj.NomeFileMask, this.LastResult.DataOraInizio);
            }
            else
            {
                var fileBase = this.DataObj.TipoFileId == eReport.TipoFile.Excel && !string.IsNullOrWhiteSpace(this.DataObj.Titolo) ? this.DataObj.Titolo.Replace(' ', '_') : this.DataObj.Nome.Replace(' ', '_');

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
                this.Slot.LogDebug(DebugLevel.Debug_1, "Begin merge excel id {0}", id);
                try
                {
                    //Esegue altra estrazione
                    var repBiz = this.Slot.BizNewWithLoadByPK<ReportEstrazioneBIZ>(id);
                    //Verifica se eliminata
                    if (repBiz.DataObj.Attivo == -1)
                    {
                        this.Slot.LogDebug(DebugLevel.Debug_1, "Estrazione eliminata logicamente. Skip.", id);
                        return;
                    }
                    //Tutte le estrazioni devono essere dello stesso tipo

                    //Solo excel si puo' accorpare
                    if (repBiz.DataObj.TipoFileId != this.DataObj.TipoFileId)
                        throw new ApplicationException(string.Format(@"L'estrazione {0} - {1} deve essere dello stesso tipo di quella principale {2}",
                            repBiz.DataObj.Id, repBiz.DataObj.Nome, this.DataObj.Id));

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
                    this.Slot.LogDebug(DebugLevel.Debug_1, "Begin merge excel id {0}", id);
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
                dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(pathText);

                string hfsuri = obj.Uri;
                string vpath = string.Format(obj.Path.ToString(), this.LastResult.DataOraInizio);

                //vai   aa
                using (var hfs = SDS.CommonUtils.Arch.FileSystem.FileSystemFactory.GetFileSystem(hfsuri))
                {
                    hfs.FileWriteFromBuffer(vpath, this.LastResult.DataBlob);
                }
            }
            else
            {
                //Fisico o UNC
                var finalPath = string.Format(this.DataObj.CopyToPath, this.LastResult.DataOraInizio);
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
            MemoryStream ms = null;
            var filenameMail = this.LastResult.NomeFile;

            if (!string.IsNullOrEmpty(dest.Password))
            {
                ms = new MemoryStream();
                using (var input = new MemoryStream(this.LastResult.DataBlob))
                {
                    filenameMail = Path.ChangeExtension(this.LastResult.NomeFile, @".zip");
                    using (var zip = new ZipFile())
                    {
                        zip.Encryption = EncryptionAlgorithm.WinZipAes256;
                        zip.Password = dest.Password;
                        zip.AddEntry(this.LastResult.NomeFile, input);
                        zip.Save(ms);
                    }
                }


            }
            else
                ms = new MemoryStream(this.LastResult.DataBlob);

            ms.Position = 0;

            return new { NomeFile= filenameMail, Stream = ms};
        }

        public List<ReportEstrazioneDestinatarioEmail> SendEmail(bool saveResult)
        {
            var retList = new List<ReportEstrazioneDestinatarioEmail>();
            //Se non impostata email esce
            if (!this.IsPrevistoInvioMail)
                return retList;

            this.Slot.LogDebug(DebugLevel.Debug_1, "Begin invio mail");
            this.mLastResult.MailEsito = string.Empty;
            this.mLastResult.MailDataInvio = DateTime.MinValue;

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

                        using (var msg = new System.Net.Mail.MailMessage())
                        {
                            msg.From = new System.Net.Mail.MailAddress(item.MailFROM);
                            msg.To.Add(item.MailTO);

                            if (!string.IsNullOrEmpty(item.MailCC))
                                msg.CC.Add(item.MailCC);

                            if (!string.IsNullOrEmpty(item.MailBCC))
                                msg.Bcc.Add(item.MailBCC);

                            msg.IsBodyHtml = true;
                            msg.Subject = item.MailSUBJ;
                            msg.Body = item.MailBODY;

                            if (string.IsNullOrWhiteSpace(this.DataObj.CopyToPath))
                            {
                                var fileout = this.getBlobForDispatch(item);

                                msg.Attachments.Add(new System.Net.Mail.Attachment(fileout.Stream, fileout.NomeFile));

                            }

                            smtp.Send(msg);

                            //Aggiunge a email inviate
                            retList.Add(item);

                            //Fine: aggiornamento
                            this.mLastResult.MailDataInvio = DateTime.Now;
                        }

                    }


                }
                catch (Exception ex)
                {
                    this.mLastResult.MailEsito += string.Format("Errore invio email a {0}: {1}\n", item.MailTO, ex.Message);
                }

            }



            //Salva esito mail
            if (saveResult)
                this.Slot.SaveObject(this.mLastResult);

            //Ripropaga email
            if (!string.IsNullOrEmpty(this.mLastResult.MailEsito))
                throw new ApplicationException("Errore nell'invio mail: {0}" + this.mLastResult.MailEsito);

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


        private void runSQL()
        {
            using (var db = Bdo.Database.DataBaseFactory.CreaDataBase(this.DataObj.Connessione.BdoDbConnectioType, this.DataObj.Connessione.ConnectionString))
            {
                db.ExecutionTimeout = 100000;
                //db.SQL = this.DataObj.SqlText.Replace('\n',' ');
                db.SQL = this.DataObj.SqlText;

                //Aggiunge una serie di parametri di sistema:
                //@ERD_LAST_ELAB: ultima elaborazione
                db.AddParameter(Costanti.Sql_Params.REPORT_ID, this.DataObj.Id);
                db.AddParameter(Costanti.Sql_Params.LAST_ELAB_DATE, this.ListaOutput.Count > 0 ? this.ListaOutput.GetLast().DataOraInizio : new DateTime(1900, 1, 1));

                this.mTabResultSQL = db.Select();
            }
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
            sb.Remove(sb.Length-1,1);
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
            this.mLastResult.NomeFile = this.getNomeFileIstantaneo();
            this.mLastResult.DataLen = sb.Length;
            this.mLastResult.DataBlob = Encoding.UTF8.GetBytes(sb.ToString());
            this.Slot.LogDebug(DebugLevel.Debug_1, "End render csv");

        }

        private void renderExcel()
        {
            this.Slot.LogDebug(DebugLevel.Debug_1, "Begin render excel flat");

            var sheetname = !string.IsNullOrEmpty(this.DataObj.SheetName) ? this.DataObj.SheetName : this.DataObj.Nome.PadRight(30, ' ').Substring(0, 30).Trim();

            var excel = ExcelUT.EseguiRenderDataTableExcel(this.mTabResultSQL, this.DataObj.Nome, this.DataObj.Titolo, sheetname , null);
            this.mLastResult.NomeFile = string.Format(@"{0}_{1:yyyy_MM_dd}.xlsx", this.DataObj.Nome.Replace(' ', '_'), this.LastResult.DataOraInizio.Date);
            this.mLastResult.DataLen = excel.DatiMemory.Length;
            this.mLastResult.DataBlob = excel.DatiMemory;

            this.Slot.LogDebug(DebugLevel.Debug_1, "End render excel flat");
        }


        private void renderExcelTemplate()
        {
            this.Slot.LogDebug(DebugLevel.Debug_1, "Begin render excel template");

            var sheetname = !string.IsNullOrEmpty(this.DataObj.SheetName) ? this.DataObj.SheetName : this.DataObj.Nome.PadRight(30, ' ').Substring(0, 30).Trim();

            if (this.DataObj.TemplateId == 0 && this.ForcedTemplate == null)
                throw new ArgumentException("Deve essere specificato un Id template nella definizione dell'estrazione oopure impostato un template esterno (ForcedTemplate)");

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
                    this.mLastResult.NomeFile = this.getNomeFileIstantaneo();
                    this.mLastResult.DataBlob = msOut.ToArray();
                    this.mLastResult.DataLen = this.mLastResult.DataBlob.Length;

                }
            }


            this.Slot.LogDebug(DebugLevel.Debug_1, "End render excel template");
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

        #endregion


    } // class

} // namespace


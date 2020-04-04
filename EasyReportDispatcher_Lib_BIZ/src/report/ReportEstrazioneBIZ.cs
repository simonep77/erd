
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


        private ReportEstrazioneCopyToLista mListaCopyTo;
        /// <summary>
        /// Lista posizioni su cui copiare il file
        /// </summary>
        public ReportEstrazioneCopyToLista ListaCopyTo
        {
            get
            {
                if (this.mListaCopyTo == null)
                {
                    this.mListaCopyTo = this.Slot.CreateList<ReportEstrazioneCopyToLista>()
                         .SearchByColumn(new FilterEQUAL(nameof(ReportEstrazioneDestinatarioEmail.EstrazioneId), this.DataObj.Id));
                }
                return this.mListaCopyTo;
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
                    var cronExp = NCrontab.CrontabSchedule.Parse(this.DataObj.CronString);
                    var dtInit = DateTime.Today;
                    var dtEnd = DateTime.Today.AddYears(5);
                    var nextRun = cronExp.GetNextOccurrence(dtInit, dtEnd);

                    if (DateTime.Now >= nextRun)
                    {
                        //Verifica se oggi ha gia' girato schedulato
                        var lastRun = this.Slot.CreateList<ReportEstrazioneOutputLista>()
                            .SearchByColumn(new FilterEQUAL(nameof(ReportEstrazioneOutput.EstrazioneId), this.DataObj.Id)
                            .And(new FilterBETWEEN(nameof(ReportEstrazioneOutput.DataOraInizio), DateTime.Today, DateTime.Today.AddDays(1).AddSeconds(-1)))
                            .And(new FilterIN(nameof(ReportEstrazioneOutput.StatoId), eReport.StatoEstrazione.Avviata, eReport.StatoEstrazione.TerminataConSuccesso)));

                        return !lastRun.Any();
                    }
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
                return (this.DataObj.InvioMailAttivo > 0);
            }
        }

        
        /// <summary>
        /// Indica se presenti altre estrazioni da accorpare a questa
        /// </summary>
        public bool IsAccorpato
        {
            get
            {
                return !string.IsNullOrEmpty(this.DataObj.EstrazioniAccorpateIds);
            }
        }

       #endregion

            #region PUBLIC


        public void Run()
       {
            this.Slot.LogDebug(DebugLevel.Debug_1, "Avvio Run()");

            this.Slot.LogDebug(DebugLevel.Debug_1, "Begin salvataggio output");
            this.mLastResult = this.Slot.CreateObject<ReportEstrazioneOutput>();
            this.mLastResult.EstrazioneId = this.DataObj.Id;
            this.mLastResult.DataOraInizio = DateTime.Now;
            this.mLastResult.StatoId = eReport.StatoEstrazione.Avviata;
            this.mLastResult.TipoFileId = this.DataObj.TipoFileId;

            this.Slot.SaveObject(this.mLastResult);
            this.Slot.LogDebug(DebugLevel.Debug_1, "End salvataggio output");

            try
            {
                //Esegue query
                this.Slot.LogDebug(DebugLevel.Debug_1, "Begin runSQL()");
                this.runSQL();
                this.Slot.LogDebug(DebugLevel.Debug_1, "End runSQL()");

                //Render
                this.Slot.LogDebug(DebugLevel.Debug_1, "Begin renderOutput()");
                this.renderOutput();
                this.Slot.LogDebug(DebugLevel.Debug_1, "End renderOutput()");

                //Se estrazione excel e presenti altre estrazioni da accorpare le esegue e mette tutto insieme
                this.runAccorpaAltreEstrazioni();

                //Esegue copia

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
            this.Slot.SaveObject(this.mLastResult);
            this.Slot.LogDebug(DebugLevel.Debug_1, "End aggiornamento output");

            //Se errori esce
            if (this.mLastResult.StatoId == eReport.StatoEstrazione.TerminataConErrori)
                throw new ApplicationException(@"L'esecuzione e' terminata con errori: " + this.mLastResult.EstrazioneEsito);

            //Serve?
            this.ListaOutput.AddOrUpdate(this.mLastResult);

            this.Slot.LogDebug(DebugLevel.Debug_1, "Begin pulizia output");
            this.cleanOutput();
            this.Slot.LogDebug(DebugLevel.Debug_1, "End pulizia output");

        }

        private void runAccorpaAltreEstrazioni()
        {
            if (!this.IsAccorpato)
                return;

            //Solo excel si puo' accorpare
            if (this.DataObj.TipoFileId != eReport.TipoFile.Excel)
                throw new ApplicationException(@"E' possibile accorpare solo estrazioni di tipo Excel");

            this.Slot.LogDebug(DebugLevel.Debug_1, "Begin accorpamento");


            var ids = this.DataObj.EstrazioniAccorpateIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(id => Convert.ToInt32(id));
            var estraz = new List<byte[]>();

            estraz.Add(this.LastResult.DataBlob);

            try
            {
                this.Slot.Simulate = true;

                foreach (var id in ids)
                {
                    //Esegue altra estrazione
                    var repBiz = this.Slot.BizNewWithLoadByPK<ReportEstrazioneBIZ>(id);
                    //Tutte le estrazioni devono essere dello stesso tipo
                    
                    //Solo excel si puo' accorpare
                    if (repBiz.DataObj.TipoFileId != this.DataObj.TipoFileId)
                        throw new ApplicationException(string.Format(@"L'estrazione {0} - {1} deve essere dello stesso tipo di quella principale {2}", 
                            repBiz.DataObj.Id, repBiz.DataObj.Nome, this.DataObj.Id));

                    repBiz.Run();
                    //Aggiunge ad elenco
                    estraz.Add(repBiz.LastResult.DataBlob);
                }

            }
            finally
            {
                this.Slot.Simulate = false;
            }



            if (estraz.Count <= 1)
                return;

            //Accorpa ed imposta su questo risultato
            this.LastResult.DataBlob = ExcelUT.EseguiAccorpamento(estraz);
            this.LastResult.DataLen = this.LastResult.DataBlob.Length;


            this.Slot.LogDebug(DebugLevel.Debug_1, "End accorpamento");

        }

        /// <summary>
        /// Data una destinazione di copia e l'output ritorna un path completo
        /// </summary>
        /// <param name="copyto"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        private string getCopyToDestFile(ReportEstrazioneCopyTo copyto, ReportEstrazioneOutput output)
        {
            var destDir = string.Format(@"$" + copyto.Path).TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;
            return Path.Combine(destDir, output.NomeFile);
        }


        /// <summary>
        /// Esegue copia in directory specifica
        /// </summary>
        private void runCopyTo()
        {

            this.Slot.LogDebug(DebugLevel.Debug_1, "Begin Copy To");


            foreach (var item in this.ListaCopyTo)
            {
                var output = this.LastResult;
                var tempFile = Path.Combine(Path.GetTempPath(), output.NomeFile);
                
                var destFile = this.getCopyToDestFile(item, output);

                var destDir = destFile.Substring(0, destFile.LastIndexOf(output.NomeFile));

                File.WriteAllBytes(tempFile, output.DataBlob);
                try
                {
                    RunUT.XCopyTo(tempFile, destDir, item.User, item.Pass, item.Domain);
                }
                finally
                {
                    File.Delete(tempFile);
                }
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
                filenameMail = Path.ChangeExtension(this.LastResult.NomeFile, @".zip");
                using (var zip = new ZipFile())
                {
                    zip.Password = dest.Password;
                    zip.AddEntry(this.LastResult.NomeFile, ms);
                    zip.Save(ms);
                }

            }
            else
                ms = new MemoryStream(this.LastResult.DataBlob);

            ms.Position = 0;

            return new { NomeFile= filenameMail, Stream = ms};
        }

        public List<ReportEstrazioneDestinatarioEmail> SendEmail()
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

                            if (item.CopyToId > 0)
                            {
                                var filePath = this.getCopyToDestFile(this.ListaCopyTo.First(ct => ct.Id == item.CopyToId), this.ListaOutput.GetLast()).Replace('\\', '/');

                                msg.Body += string.Format($"<br/><br/>LINK al file: <a href='file:///{filePath}' target='_blank' >clicca qui</a>");
                                msg.Body += string.Format($"<br/>Se il link non funzionasse copiare il seguente percorso ed utilizzarlo dal proprio PC: <b>{filePath}</b>");

                            }
                            else
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
                db.SQL = this.DataObj.SqlText.Replace('\n',' ');
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
                    this.renderExcel();
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
            this.mLastResult.DataLen = sb.Length;
            this.mLastResult.DataBlob = Encoding.UTF8.GetBytes(sb.ToString());
            this.Slot.LogDebug(DebugLevel.Debug_1, "End render csv");

        }

        private void renderExcel()
        {
            this.Slot.LogDebug(DebugLevel.Debug_1, "Begin render excel");

            var sheetname = !string.IsNullOrEmpty(this.DataObj.SheetName) ? this.DataObj.SheetName : this.DataObj.Nome.PadRight(30, ' ').Substring(0, 30).Trim();

            var excel = ExcelUT.EseguiRenderDataTableExcel(this.mTabResultSQL, this.DataObj.Nome, this.DataObj.Titolo, sheetname , null);
            this.mLastResult.NomeFile = string.Format(@"{0}_{1:yyyy_MM_dd}.xlsx", this.DataObj.Nome.Replace(' ', '_'), this.LastResult.DataOraInizio.Date);
            this.mLastResult.DataLen = excel.DatiMemory.Length;
            this.mLastResult.DataBlob = excel.DatiMemory;

            this.Slot.LogDebug(DebugLevel.Debug_1, "End render excel");
        }

        #endregion


    } // class

} // namespace


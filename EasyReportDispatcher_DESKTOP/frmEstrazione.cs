using Bdo.Objects;
using EasyReportDispatcher_DESKTOP.src;
using EasyReportDispatcher_Lib_BIZ.src.report;
using EasyReportDispatcher_Lib_Common.src.enums;
using EasyReportDispatcher_Lib_DAL.src.report;
using ScintillaNET;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EasyReportDispatcher_DESKTOP
{
    public partial class frmEstrazione : ErdForm
    {

        private ReportEstrazioneBIZ mEstrazioneBiz;
        private DialogResult mResult = DialogResult.Cancel;

        public frmEstrazione(ReportEstrazioneBIZ est)
        {
            InitializeComponent();

            this.mEstrazioneBiz = est;

            this.loadData();
        }


        private void loadData()
        {

            this.Text = string.Format(@"Estrazione ID: {0}", this.mEstrazioneBiz.DataObj.ObjectState == Bdo.Objects.EObjectState.New ? "<new>" : this.mEstrazioneBiz.DataObj.Id.ToString());  this.mEstrazioneBiz.DataObj.Id.ToString();

            this.txtNome.Text = this.mEstrazioneBiz.DataObj.Nome;
            this.txtGruppo.Text = this.mEstrazioneBiz.DataObj.Gruppo;
            this.txtNote.Text = this.mEstrazioneBiz.DataObj.Note;
            this.txtCronString.Text = this.mEstrazioneBiz.DataObj.CronString;
            this.txtEstrazioniAcc.Text = this.mEstrazioneBiz.DataObj.EstrazioniAccorpateIds;
            this.chkAccorpaDati.Checked = (this.mEstrazioneBiz.DataObj.AccorpaSoloDati > 0);
            this.chkAttivo.Checked = (this.mEstrazioneBiz.DataObj.Attivo == 1);
            this.cmbInvioEmail.SelectedIndex = this.mEstrazioneBiz.DataObj.InvioMailAttivo;

            this.txtSQL.Text = this.mEstrazioneBiz.DataObj.SqlText;
            this.txtExcelTitolo.Text = this.mEstrazioneBiz.DataObj.Titolo;
            this.txtExcelSheet.Text = this.mEstrazioneBiz.DataObj.SheetName;
            this.txtNomeFileMask.Text = this.mEstrazioneBiz.DataObj.NomeFileMask;
            
            this.rbTemplateBase.Checked = (this.mEstrazioneBiz.DataObj.TipoFileId == eReport.TipoFile.Excel && this.mEstrazioneBiz.DataObj.TemplateId == 0);
            this.rbTemplateCustom.Checked = (this.mEstrazioneBiz.DataObj.TipoFileId == eReport.TipoFile.Excel && this.mEstrazioneBiz.DataObj.TemplateId > 0);

            if (this.mEstrazioneBiz.DataObj.TemplateId > 0)
            {
                if (this.mEstrazioneBiz.DataObj.ObjectState == Bdo.Objects.EObjectState.New)
                {
                    this.txtNomeTemplate.Text = string.Concat("Clone di", this.mEstrazioneBiz.DataObj.Template.Nome);
                    this.lbTemplatePath.Text = string.Concat("Clone di Template Id: ", this.mEstrazioneBiz.DataObj.TemplateId.ToString());
                }
                else
                {
                    this.txtNomeTemplate.Text = this.mEstrazioneBiz.DataObj.Template.Nome;
                    this.lbTemplatePath.Text = string.Concat("Template Id: ", this.mEstrazioneBiz.DataObj.TemplateId.ToString());
                }
            }
            else
            {
                this.txtNomeTemplate.Text = @"<Nuovo>";
                this.lbTemplatePath.Text = string.Empty;

                //
            }

            //Pulsantiera

  

            this.txtCopyToPath.Text = this.mEstrazioneBiz.DataObj.CopyToPath;
            this.txtNumOutput.Value = this.mEstrazioneBiz.DataObj.NumOutputStorico;


            //Load

            //Tipi file
            UI_Utils.Combo_Load(this.cmbTipoFile, AppContextERD.Slot.CreateList<ReportTipoFileLista>().CacheResult().SearchAllObjects(), 
                nameof(ReportTipoFile.Nome), nameof(ReportTipoFile.Id), this.mEstrazioneBiz.DataObj.TipoFileId > 0? this.mEstrazioneBiz.DataObj.TipoFile : null);
            //Connessioni
            UI_Utils.Combo_Load(this.cmbConnessioni, AppContextERD.Slot.CreateList<ReportConnessioneLista>().SearchAllObjects(),
                nameof(ReportConnessione.Nome), nameof(ReportConnessione.Id), this.mEstrazioneBiz.DataObj.ConnessioneId > 0 ? this.mEstrazioneBiz.DataObj.Connessione : null);
            //Destinatari email
            UI_Utils.Combo_Load(this.cmbDestEmail, this.mEstrazioneBiz.ListaDesinatariEmail,
                nameof(ReportEstrazioneDestinatarioEmail.MailTO), nameof(ReportEstrazioneDestinatarioEmail.Id), this.mEstrazioneBiz.ListaDesinatariEmail.GetFirst());
            //Copy to


            //Lancia un po' di eventi
            this.txtEstrazioniAcc_TextChanged(null, null);
            this.rbTemplateBase_CheckedChanged(null, null);
            this.rbTemplateCustom_CheckedChanged(null, null);
            this.cmbTipoFile_SelectedIndexChanged(null, null);
            this.cmbDestEmail_SelectedIndexChanged(null, null);

        }

        #region LOADING


        #endregion

        private void frmEstrazione_Load(object sender, EventArgs e)
        {
            this.setScintillaStyle();
        }


        private void setScintillaStyle()
        {
            var editor = this.txtSQL;

            editor.StyleResetDefault();
            editor.Styles[Style.Default].Size = Convert.ToInt32(this.Font.Size);
            editor.Styles[Style.Default].Font = this.Font.FontFamily.Name;
            editor.StyleClearAll();
            
            editor.Lexer = Lexer.Sql;


            editor.Styles[Style.Sql.Word].ForeColor = Color.Blue;
            editor.Styles[Style.Sql.Word2].ForeColor = Color.Magenta;
            editor.Styles[Style.Sql.Default].ForeColor = Color.Black;
            editor.Styles[Style.Sql.Character].ForeColor = Color.Maroon;
            editor.Styles[Style.Sql.Comment].ForeColor = Color.Green;
            editor.Styles[Style.Sql.CommentDoc].ForeColor = Color.Green;
            editor.Styles[Style.Sql.CommentLine].ForeColor = Color.Green;
            editor.Styles[Style.Sql.Identifier].ForeColor = Color.Red;
            editor.Styles[Style.Sql.Number].ForeColor = Color.Black;
            editor.Styles[Style.Sql.Operator].ForeColor = Color.Black;
            editor.Styles[Style.Sql.QOperator].ForeColor = Color.Black;
            editor.Styles[Style.Sql.QuotedIdentifier].ForeColor = Color.Maroon;

            // Set the Styles
            editor.Styles[Style.LineNumber].ForeColor = Color.FromArgb(255, 128, 128, 128);  //Dark Gray
            editor.Styles[Style.LineNumber].BackColor = Color.FromArgb(255, 228, 228, 228);  //Light Gray
            editor.Styles[Style.Sql.Comment].ForeColor = Color.Green;
            editor.Styles[Style.Sql.CommentLine].ForeColor = Color.Green;
            editor.Styles[Style.Sql.CommentLineDoc].ForeColor = Color.Green;
            editor.Styles[Style.Sql.Number].ForeColor = Color.Maroon;
            editor.Styles[Style.Sql.Word].ForeColor = Color.Blue;
            editor.Styles[Style.Sql.Word2].ForeColor = Color.Fuchsia;
            editor.Styles[Style.Sql.User1].ForeColor = Color.Gray;
            editor.Styles[Style.Sql.User2].ForeColor = Color.FromArgb(255, 00, 128, 192);    //Medium Blue-Green
            editor.Styles[Style.Sql.String].ForeColor = Color.Red;
            editor.Styles[Style.Sql.Character].ForeColor = Color.Red;
            editor.Styles[Style.Sql.Operator].ForeColor = Color.Black;

            // Set keyword lists
            // Word = 0
            editor.SetKeywords(0, @"add alter as authorization backup begin bigint binary bit break browse bulk by cascade case catch check checkpoint close clustered column commit compute constraint containstable continue create current cursor cursor database date datetime datetime2 datetimeoffset dbcc deallocate decimal declare default delete deny desc disk distinct distributed double drop dump else end errlvl escape except exec execute exit external fetch file fillfactor float for foreign freetext freetexttable from full function goto grant group having hierarchyid holdlock identity identity_insert identitycol if image index insert int intersect into key kill lineno load merge money national nchar nocheck nocount nolock nonclustered ntext numeric nvarchar of off offsets on open opendatasource openquery openrowset openxml option order over percent plan precision primary print proc procedure public raiserror read readtext real reconfigure references replication restore restrict return revert revoke rollback rowcount rowguidcol rule save schema securityaudit select set setuser shutdown smalldatetime smallint smallmoney sql_variant statistics table table tablesample text textsize then time timestamp tinyint to top tran transaction trigger truncate try union unique uniqueidentifier update updatetext use user values varbinary varchar varying view waitfor when where while with writetext xml go ");
            // Word2 = 1
            editor.SetKeywords(1, @"ascii cast char charindex ceiling coalesce collate contains convert current_date current_time current_timestamp current_user floor isnull max min nullif object_id session_user substring system_user tsequal ");
            // User1 = 4
            editor.SetKeywords(4, @"all and any between cross exists in inner is join left like not null or outer pivot right some unpivot ( ) * ");


        }

        private void cmbTipoFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            var iTipo = this.cmbTipoFile.SelectedItem == null ? 0 : Convert.ToInt32(this.cmbTipoFile.SelectedValue);


            this.gbExcel.Visible = (iTipo == (int)EasyReportDispatcher_Lib_Common.src.enums.eReport.TipoFile.Excel);

        }

        private void rbTemplateBase_CheckedChanged(object sender, EventArgs e)
        {
            this.panExcelBase.Enabled = this.rbTemplateBase.Checked;
        }

        private void rbTemplateCustom_CheckedChanged(object sender, EventArgs e)
        {
            this.panExcelCustom.Enabled = this.rbTemplateCustom.Checked;

        }

        private void btnCloneTemplate_Click(object sender, EventArgs e)
        {

            using (var frm = new OpenFileDialog())
            {
                frm.Filter = @"File Excel|*.xlsx";
                frm.DefaultExt = ".xlsx";

                if (frm.ShowDialog() != DialogResult.OK)
                    return;

                this.lbTemplatePath.Text = Path.GetFileName(frm.FileName);
                this.lbTemplatePath.Tag = frm.FileName;
            }
            

        }

        private void btnEliminaTemplate_Click(object sender, EventArgs e)
        {
            if (this.mEstrazioneBiz.DataObj.TemplateId > 0)
            {

            }
            else
            {

            }

            this.lbTemplatePath.Text = @"nessun file";
            this.lbTemplatePath.Tag = null; ;
        }

        private void validazioneDati()
        {

        }

        private void btnSalva_Click(object sender, EventArgs e)
        {
            try
            {
                this.validazioneDati();

                AppContextERD.Slot.DbBeginTransAll();
                try
                {
                    //Template custom
                    if (this.rbTemplateCustom.Checked)
                    {
                        var tpl = this.mEstrazioneBiz.DataObj.Template;

                        if (tpl == null)
                        {
                            tpl = AppContextERD.Slot.CreateObject<ReportTemplate>();
                        }
                        else if (this.mEstrazioneBiz.DataObj.ObjectState == Bdo.Objects.EObjectState.New)
                        {
                            tpl = AppContextERD.Slot.CloneObjectForNew(tpl);
                        }

                        tpl.Nome = this.txtNomeTemplate.Text;

                        if (this.lbTemplatePath.Tag != null)
                        {
                            tpl.TemplateBlob = File.ReadAllBytes(this.lbTemplatePath.Tag.ToString());
                        }

                        AppContextERD.Slot.SaveObject(tpl);

                        this.mEstrazioneBiz.DataObj.TemplateId = tpl.Id;
                    }


                    //Estrazione
                    this.mEstrazioneBiz.DataObj.Nome = this.txtNome.Text;
                    this.mEstrazioneBiz.DataObj.Note = this.txtNote.Text;
                    this.mEstrazioneBiz.DataObj.CronString = this.txtCronString.Text;
                    this.mEstrazioneBiz.DataObj.SqlText = this.txtSQL.Text;
                    this.mEstrazioneBiz.DataObj.Titolo = this.txtExcelTitolo.Text;
                    this.mEstrazioneBiz.DataObj.SheetName = this.txtExcelSheet.Text;

                    if (this.mEstrazioneBiz.DataObj.ObjectState == Bdo.Objects.EObjectState.New)
                    {
                        this.mEstrazioneBiz.DataObj.DataInizio = new DateTime(2001, 1, 1);
                        this.mEstrazioneBiz.DataObj.DataFine = new DateTime(9999, 12, 31);
                        this.mEstrazioneBiz.DataObj.UtenteIdInserimento = AppContextERD.Utente.Id;
                    }
                    this.mEstrazioneBiz.DataObj.Attivo = (sbyte)((this.chkAttivo.Checked) ? 1 : 0);
                    this.mEstrazioneBiz.DataObj.InvioMailAttivo = (sbyte)this.cmbInvioEmail.SelectedIndex;
                    this.mEstrazioneBiz.DataObj.Nome = this.txtNome.Text;
                    this.mEstrazioneBiz.DataObj.TipoFileId = (this.cmbTipoFile.SelectedItem as ReportTipoFile).Id;
                    this.mEstrazioneBiz.DataObj.ConnessioneId = (this.cmbConnessioni.SelectedItem as ReportConnessione).Id;
                    this.mEstrazioneBiz.DataObj.NumOutputStorico = Convert.ToSByte(this.txtNumOutput.Value);
                    this.mEstrazioneBiz.DataObj.EstrazioniAccorpateIds = this.txtEstrazioniAcc.Text.Trim(',');
                    this.mEstrazioneBiz.DataObj.CopyToPath = this.txtCopyToPath.Text.Trim();
                    this.mEstrazioneBiz.DataObj.Gruppo = this.txtGruppo.Text.Trim();
                    this.mEstrazioneBiz.DataObj.NomeFileMask = this.txtNomeFileMask.Text;

                    if (this.mEstrazioneBiz.DataObj.EstrazioniAccorpateIds.Length > 0)
                        this.mEstrazioneBiz.DataObj.AccorpaSoloDati = (sbyte)(this.chkAccorpaDati.Checked ? 1 : 0);

                    this.mEstrazioneBiz.DataObj.UtenteIdAggiornamento = AppContextERD.Utente.Id;

                    this.mEstrazioneBiz.Salva();

                    AppContextERD.Slot.DbCommitAll();

                }
                catch (Exception)
                {
                    AppContextERD.Slot.DbRollBackAll();

                    throw;
                }
                

                this.loadData();

                this.mResult = DialogResult.OK;

                UI_Utils.ShowInfo(@"Dati salvati");

            }
            catch (Exception ex)
            {
                UI_Utils.ShowError(ex.Message);

            }


        }

        private void frmEstrazione_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = this.mResult;
        }

        private void txtEstrazioniAcc_TextChanged(object sender, EventArgs e)
        {

            this.chkAccorpaDati.Enabled = (this.txtEstrazioniAcc.Text.Length > 0);

            if (this.txtEstrazioniAcc.Text.Length == 0)
                this.chkAccorpaDati.Checked = false;
        }

        private void btnInsModConnessione_Click(object sender, EventArgs e)
        {

            ReportConnessione conn = this.cmbConnessioni.SelectedItem as ReportConnessione;

            if ( object.ReferenceEquals(sender, this.btnInserisciConnessione) )
            {
                if (conn == null)
                {
                    conn = AppContextERD.Slot.CreateObject<ReportConnessione>();
                    conn.Nome = @"<Nuovo>";
                }
                else
                {
                    conn = AppContextERD.Slot.CloneObjectForNew(conn);
                    conn.Nome = "Copia di " + conn.Nome;
                }
            }
            

            using (var fc = new frmConnessione(conn))
            {
                if ( fc.ShowDialog() == DialogResult.OK )
                {
                    ((ReportConnessioneLista)this.cmbConnessioni.DataSource).Add(conn);
                    this.cmbConnessioni.SelectedItem = conn;
                }
            }

        }

        private void btnElimina_Click(object sender, EventArgs e)
        {
            ReportConnessione conn = this.cmbConnessioni.SelectedItem as ReportConnessione;

            if (conn == null)
                return;

            if (MessageBox.Show(string.Format("Confermi l'eliminazione della connessione '{0}' ", conn.Nome, conn.Id), "Conferma", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {

                var lst = AppContextERD.Slot.CreateList<ReportEstrazioneLista>().SearchByColumn(Filter.Eq(nameof(ReportEstrazione.ConnessioneId), conn.Id));

                if (lst.Count > 0)
                {
                    var ids = string.Join(",", lst.Select(est => est.Id.ToString()));

                    UI_Utils.ShowError(@"Non e' possibile eliminare la connessione in quanto collegata alle seguenti estarzioni: {0}", ids);

                    return;
                }

                AppContextERD.Slot.DeleteObject(conn);

                //Rimuove da datasource
                ((ReportConnessioneLista)this.cmbConnessioni.DataSource).Remove(conn);

                UI_Utils.ShowInfo("Connessione eliminata.");

            }
            catch (Exception ex)
            {
                UI_Utils.ShowError(ex.Message);
            }

        }

        private void chkAttivo_CheckedChanged(object sender, EventArgs e)
        {
            this.txtCronString.Enabled = this.chkAttivo.Checked;
        }


        private void btnEmailDestAdd_Click(object sender, EventArgs e)
        {
            bool isNew = (object.ReferenceEquals(sender, this.btnEmailDestAdd));

            ReportEstrazioneDestinatarioEmail dest = AppContextERD.Slot.CreateObject<ReportEstrazioneDestinatarioEmail>();

            if (isNew)
            {
                dest = AppContextERD.Slot.CreateObject<ReportEstrazioneDestinatarioEmail>();
                dest.EstrazioneId = this.mEstrazioneBiz.DataObj.Id;
            }
            else
                dest = this.cmbDestEmail.SelectedItem as ReportEstrazioneDestinatarioEmail;


            using (var frm = new frmDestMail(dest))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    
                    var lst = this.cmbDestEmail.DataSource as ReportEstrazioneDestinatarioEmailLista;
                    lst.AddOrUpdate(dest);

                    UI_Utils.Combo_Load(this.cmbDestEmail, lst,
                        nameof(ReportEstrazioneDestinatarioEmail.MailTO), nameof(ReportEstrazioneDestinatarioEmail.Id), dest);
                }
            }
        }

        private void btnEmailDestDel_Click(object sender, EventArgs e)
        {
            if (this.cmbDestEmail.SelectedItem == null)
                return;

            ReportEstrazioneDestinatarioEmail dest = this.cmbDestEmail.SelectedItem as ReportEstrazioneDestinatarioEmail;

            if (MessageBox.Show(string.Format("Confermi l'eliminazione del destinatario '{0}' ", dest.MailTO, dest.Id), "Conferma", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                AppContextERD.Slot.DeleteObject(dest);

                ReportEstrazioneDestinatarioEmailLista lst = this.cmbDestEmail.DataSource as ReportEstrazioneDestinatarioEmailLista;

                lst.Remove(dest);

                UI_Utils.Combo_Load(this.cmbDestEmail, lst,
                        nameof(ReportEstrazioneDestinatarioEmail.MailTO), nameof(ReportEstrazioneDestinatarioEmail.Id), null);

                UI_Utils.ShowInfo("Destinatario eliminato.");

            }
            catch (Exception ex)
            {

                UI_Utils.ShowError(ex.Message);

            }

        }

        private void cmbDestEmail_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cmbInvioEmail_SelectedIndexChanged(sender, e);
        }

        private void btnInfoCopy_Click(object sender, EventArgs e)
        {
            var text = "* Per percorso fisico o di rete (UNC) basta immettere il percorso.\n\n* Per percorso HFS immettere un testo JSON del tipo \n{{ \n'Uri': 'hfs://user:pass@server', \n'Path': '/Mia/dir/file_{{0:yyyy_MM_dd}}.csv' \n}}\nNel PATH è possibile utilizzare la data di elaborazione come stringa di formattazione in stile .NET";
            UI_Utils.ShowInfo(text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var text = "E' possibile utilizzare nella query i seguenti parametri pre-valorizzati:\n\n * @ERD_LAST_ELAB_DATE: è la data/ora dell'ultima esecuzione terminata con successo registrata su DB\n\n * @ERD_REPORT_ID: è l'ID del report in esecuzione";
            UI_Utils.ShowInfo(text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UI_Utils.ShowInfo("Campo non obbligatorio.\n\nSe valorizzato deve contenere anche l'estensione ed è possibile utilizzare i fermaposto di formattazione data .NET sull'indice 0.\n\n Es. nome_file_{0:yyyy_MM_dd_HH_mm_ss}.xlsx");
        }

        private void btnEseguiQuery_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                var tab = this.mEstrazioneBiz.RunSQL();

                using (var f = new frmShowQResult(tab))
                {
                    f.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                UI_Utils.ShowError(ex.Message);
            }
            finally
            {
                this.Cursor = this.DefaultCursor;
            }



        }

        private void cmbInvioEmail_SelectedIndexChanged(object sender, EventArgs e)
        {
            var color = SystemColors.Window;

            switch (this.cmbInvioEmail.SelectedIndex)
            {
                case 0:
                    color = Color.LightGray;
                    break;
                case 1:
                    color = Color.LightGreen;
                    break;
                case 2:
                    color = Color.Orange;
                    break;
                default:
                    break;
            }
            this.tip.SetToolTip(this.cmbInvioEmail, this.cmbInvioEmail.SelectedItem.ToString());
            this.cmbInvioEmail.BackColor = color;
            var bMailEn = this.cmbInvioEmail.SelectedIndex > 0;
            var bSelDest =  (this.cmbDestEmail.SelectedIndex >= 0);
            this.cmbDestEmail.Enabled = bMailEn;
            this.btnEmailDestAdd.Enabled = bMailEn;
            this.btnEmailDestEdit.Enabled = bMailEn && bSelDest;
            this.btnEmailDestDel.Enabled = bMailEn && bSelDest;
        }
    }
}

using Bdo.Objects;
using EasyReportDispatcher_DESKTOP.src;
using EasyReportDispatcher_Lib_BIZ.src.report;
using EasyReportDispatcher_Lib_Common.src.enums;
using EasyReportDispatcher_Lib_DAL.src.report;
using ScintillaNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyReportDispatcher_DESKTOP
{
    public partial class frmEstrazione : Form
    {

        private ReportEstrazioneBIZ mEstrazioneBiz;
        private DialogResult mResult = DialogResult.Cancel;

        public frmEstrazione(ReportEstrazione est)
        {
            InitializeComponent();

            this.mEstrazioneBiz = est.ToBizObject<ReportEstrazioneBIZ>();

            this.addBindings();
        }


        private void addBindings()
        {

            this.Text = string.Format(@"Estrazione: {0}", this.mEstrazioneBiz.DataObj.ObjectState == Bdo.Objects.EObjectState.New ? "<nuovo>" : this.mEstrazioneBiz.DataObj.Id.ToString());  this.mEstrazioneBiz.DataObj.Id.ToString();

            this.txtNome.Text = this.mEstrazioneBiz.DataObj.Nome;
            this.txtNote.Text = this.mEstrazioneBiz.DataObj.Note;
            this.txtCronString.Text = this.mEstrazioneBiz.DataObj.CronString;
            this.txtEstrazioniAcc.Text = this.mEstrazioneBiz.DataObj.EstrazioniAccorpateIds;
            this.chkAccorpaDati.Checked = (this.mEstrazioneBiz.DataObj.AccorpaSoloDati > 0);
            this.chkAttivo.Checked = (this.mEstrazioneBiz.DataObj.Attivo == 1);
            this.chbInvioEmail.Checked = (this.mEstrazioneBiz.DataObj.InvioMailAttivo == 1);

            this.txtSQL.Text = this.mEstrazioneBiz.DataObj.SqlText;
            this.txtExcelTitolo.Text = this.mEstrazioneBiz.DataObj.Titolo;
            this.txtExcelSheet.Text = this.mEstrazioneBiz.DataObj.SheetName;

            
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
            }

  

           

            this.btnEmailDest.Enabled = (this.mEstrazioneBiz.DataObj.ObjectState == Bdo.Objects.EObjectState.Loaded);

            this.txtNumOutput.Value = this.mEstrazioneBiz.DataObj.NumOutputStorico;


            //Load
            this.loadTipiFile();
            this.loadConnessioni();

            //Lancia un po' di eventi
            this.txtEstrazioniAcc_TextChanged(null, null);
            this.rbTemplateBase_CheckedChanged(null, null);
            this.rbTemplateCustom_CheckedChanged(null, null);

        }

        #region LOADING

        private void loadTipiFile()
        {
            var tipi = AppContextERD.Slot.CreateList<ReportTipoFileLista>().CacheResult().SearchAllObjects();

            this.cmbTipoFile.DisplayMember = nameof(ReportTipoFile.Nome);
            this.cmbTipoFile.ValueMember = nameof(ReportTipoFile.Id);
            this.cmbTipoFile.DataSource = tipi;

            if (this.mEstrazioneBiz.DataObj.TipoFileId > 0)
                this.cmbTipoFile.SelectedItem = this.mEstrazioneBiz.DataObj.TipoFile;
        }

        private void loadConnessioni()
        {
            var conn = AppContextERD.Slot.CreateList<ReportConnessioneLista>().SearchAllObjects();

            this.cmbConnessioni.DisplayMember = nameof(ReportTipoFile.Nome);
            this.cmbConnessioni.ValueMember = nameof(ReportTipoFile.Id);
            this.cmbConnessioni.DataSource = conn;

            if (this.mEstrazioneBiz.DataObj.ConnessioneId > 0)
                this.cmbConnessioni.SelectedItem = this.mEstrazioneBiz.DataObj.Connessione;
        }

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

                }
                this.mEstrazioneBiz.DataObj.Attivo = (sbyte)((this.chkAttivo.Checked) ? 1 : 0);
                this.mEstrazioneBiz.DataObj.InvioMailAttivo =(sbyte)((this.chbInvioEmail.Checked) ? 1 : 0);
                this.mEstrazioneBiz.DataObj.Nome = this.txtNome.Text;
                this.mEstrazioneBiz.DataObj.TipoFileId = (this.cmbTipoFile.SelectedItem as ReportTipoFile).Id;
                this.mEstrazioneBiz.DataObj.ConnessioneId = (this.cmbConnessioni.SelectedItem as ReportConnessione).Id;
                this.mEstrazioneBiz.DataObj.NumOutputStorico = Convert.ToSByte(this.txtNumOutput.Value);
                this.mEstrazioneBiz.DataObj.EstrazioniAccorpateIds = this.txtEstrazioniAcc.Text.Trim(',');

                if (this.mEstrazioneBiz.DataObj.EstrazioniAccorpateIds.Length > 0)
                    this.mEstrazioneBiz.DataObj.AccorpaSoloDati = (sbyte)(this.chkAccorpaDati.Checked ? 1 : 0);

                AppContextERD.Slot.SaveObject(this.mEstrazioneBiz.DataObj);

                this.addBindings();

                this.mResult = DialogResult.OK;

                if (MessageBox.Show("Estrazione salvata. Vuoi rimanere nella pagina di modifica?", "Conferma", MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes)
                    this.DialogResult = this.mResult;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
    }
}

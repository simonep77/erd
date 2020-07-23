using Bdo.Objects;
using EasyReportDispatcher_DESKTOP.src;
using EasyReportDispatcher_Lib_BIZ.src.report;
using EasyReportDispatcher_Lib_Common.src;
using EasyReportDispatcher_Lib_Common.src.enums;
using EasyReportDispatcher_Lib_DAL.src.report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyReportDispatcher_DESKTOP
{
    public partial class frmPianoSched : ErdForm
    {
        public frmPianoSched()
        {

            InitializeComponent();

            this.Text = $"Piano schedulazione estrazioni";

            this.loadCombo();

            this.loadData();
        }


        private void loadCombo()
        {
            var lst = AppContextERD.Slot.CreateList<ReportSchedulazioneStatoLista>()
                .LoadFullObjects()
                .OrderBy(nameof(ReportSchedulazioneStato.Id), OrderVersus.Asc)
                .SearchAllObjects();

            this.cmbStatoSched.Items.Clear();
            this.cmbStatoSched.ValueMember = @"Id";
            this.cmbStatoSched.DisplayMember = @"Nome";
            this.cmbStatoSched.Items.Clear();
            this.cmbStatoSched.Items.Add(new { Id = 0, Nome = " - TUTTI - " });
            foreach (var item in lst)
            {
                this.cmbStatoSched.Items.Add(new { Id = item.Id, Nome = item.Nome });

            }

            this.cmbStatoSched.SelectedIndex = 0;
        }
        private void loadData()
        {

            this.Cursor = Cursors.WaitCursor;
            this.lbExecCount.Text = "Totale schedulazioni: -";
            this.lvStorico.BeginUpdate();
            this.lvStorico.Items.Clear();
            
            try
            {
                var flt = Filter.Gte(nameof(ReportSchedulazione.DataEsecuzione), this.dtpDataInizio.Value.Date)
                    .And(Filter.Lte(nameof(ReportSchedulazione.DataEsecuzione), this.dtpDataFine.Value.Date.AddDays(1).AddSeconds(-1)));

                if (this.cmbStatoSched.SelectedIndex > 0)
                {
                    dynamic sel = this.cmbStatoSched.SelectedItem;
                    flt.And(Filter.Eq(nameof(ReportSchedulazione.StatoId), sel.Id));
                }

                var lst = AppContextERD.Slot.CreateList<ReportSchedulazioneLista>()
                    .LoadFullObjects()
                    .OrderBy(nameof(ReportSchedulazione.DataEsecuzione), OrderVersus.Asc)
                    .SearchByColumn(flt);

                foreach (var item in lst)
                {
                    var lvItem = new ListViewItem(item.DataEsecuzione.ToString("dd/MM/yyyy HH:mm"));
                    lvItem.SubItems.Add(item.Estrazione.Gruppo);
                    lvItem.SubItems.Add(item.Estrazione.Nome);
                    lvItem.SubItems.Add(item.Stato.Nome);
                    lvItem.SubItems.Add(item.StatoId == eReport.StatoSchedulazione.Eseguita ? item.Output?.DataOraFine.Subtract(item.Output.DataOraInizio).ToString() ?? string.Empty : string.Empty);
                    lvItem.SubItems.Add(item.Output?.Stato.Nome ?? string.Empty);
                    lvItem.SubItems.Add(item.Output?.EstrazioneEsito ?? string.Empty);


                    this.lvStorico.Items.Add(lvItem);
                }

                this.lbExecCount.Text = $"Totale schedulazioni: {lst.Count}";
            }
            finally
            {
                this.lvStorico.EndUpdate();
                this.Cursor = this.DefaultCursor;
            }


        }

        private void lvStorico_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var sel = this.lvStorico.SelectedItems.Count > 0;

            //this.tsElimina.Enabled = sel;
            //this.btnOpenFile.Enabled = sel;
            //this.btnEliminaTutti.Enabled = this.mEstBiz.ListaOutput.Count > 0;
        }

        private void actDeleteOne(object sender, EventArgs e)
        {
            //var selItem = this.lvStorico.SelectedItems[0];
            //var output = selItem.Tag as ReportEstrazioneOutput;

            //if (MessageBox.Show(string.Format($"Confermi l'eleminazione dell'esecuzione con id {output.Id}?"), "Conferma", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            //    return;

            //if(this.mEstBiz.IsSqlConParametriElaborazione() && output.Equals(this.mEstBiz.ListaOutput.GetLast()))
            //{
            //    if (!UI_Utils.ShowConfirmYesNo("Attenzione! L'SQL dell'estrazione contiene una dipendenza [parametro {0}] dall'output che si vuole eliminare.\n\nConfermi l'eliminazione che influenzerà l'esecuzione successiva?", Costanti.Sql_Params.LAST_ELAB_DATE))
            //        return;
            //}

            try
            {
               
            }
            catch (Exception ex)
            {
                UI_Utils.ShowError(ex.Message);
            }
        }


        private void actDeleteAll(object sender, EventArgs e)
        {
            //if (this.lvStorico.Items.Count == 0)
            //    return;

            //if (MessageBox.Show("Confermi l'eleminazione di tutti i dati di esecuzione?", "Conferma", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            //    return;

            //if (this.mEstBiz.IsSqlConParametriElaborazione())
            //{
            //    if (!UI_Utils.ShowConfirmYesNo("Attenzione! L'SQL dell'estrazione contiene una dipendenza [parametro {0}] dagli output storici.\n\nConfermi l'eliminazione che può influenzare le esecuzioni successive?", Costanti.Sql_Params.LAST_ELAB_DATE))
            //        return;

            //}

            try
            {
                //this.mEstBiz.GetSlot().DeleteAll(this.mEstBiz.ListaOutput);
                //this.mEstBiz.ListaOutput.Clear();

                //this.loadData();

            }
            catch (Exception ex)
            {
                UI_Utils.ShowError(ex.Message);
            }
        }

        private void actOpenFile(object sender, EventArgs e)
        {
            //if (this.lvStorico.SelectedItems.Count == 0)
            //    return;

            //this.Cursor = Cursors.WaitCursor;
            //Application.DoEvents();
            //try
            //{
            //    var selItem = this.lvStorico.SelectedItems[0];
            //    var output = selItem.Tag as ReportEstrazioneOutput;

            //    var outFilePath = AppContextERD.GetLocalFileNameWithTime(output.NomeFile);
            //    File.WriteAllBytes(outFilePath, output.DataBlob);

            //    Process.Start(outFilePath);
            //}
            //catch (Exception ex)
            //{
            //    UI_Utils.ShowError(ex.Message);
            //}
            //finally
            //{
            //    this.Cursor = this.DefaultCursor;
            //}
            

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            this.loadData();
        }
    }
}

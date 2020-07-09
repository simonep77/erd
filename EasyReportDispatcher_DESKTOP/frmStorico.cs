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
    public partial class frmStorico : ErdForm
    {
        private ReportEstrazioneBIZ mEstBiz;
        public frmStorico(ReportEstrazioneBIZ est)
        {
            this.mEstBiz = est;

            InitializeComponent();

            this.Text = string.Format($"Storico Estrazione ID: {this.mEstBiz.DataObj.Id} - {this.mEstBiz.DataObj.Nome}");

            this.loadData();
        }
        private void loadData()
        {
            this.lvStorico.Items.Clear();

            foreach (var output in this.mEstBiz.ListaOutput)
            {
                var item = new ListViewItem(output.Id.ToString());
                item.Name = output.Id.ToString();
                item.Tag = output;

                item.SubItems.Add(output.DataOraInizio.ToString(@"dd/MM/yyyy HH:mm:ss"));
                item.SubItems.Add(output.DataOraFine.Subtract(output.DataOraInizio).ToString());
                

                if (output.StatoId != eReport.StatoEstrazione.TerminataConSuccesso)
                {
                    item.UseItemStyleForSubItems = false;
                    var subi = item.SubItems.Add("ERR");
                    subi.ForeColor = Color.Red;
                    item.SubItems.Add(output.EstrazioneEsito);
                }
                else
                {
                    item.SubItems.Add("OK");
                    item.SubItems.Add(output.NomeFile);

                }


                this.lvStorico.Items.Add(item);
            }

            this.lbExecCount.Text = string.Format($"Esecuzioni: {this.mEstBiz.ListaOutput.Count}");

            this.lvStorico_SelectedIndexChanged(this.lvStorico, null);
        }

        private void lvStorico_SelectedIndexChanged(object sender, EventArgs e)
        {
            var sel = this.lvStorico.SelectedItems.Count > 0;

            this.tsElimina.Enabled = sel;
            this.btnOpenFile.Enabled = sel;
            this.btnEliminaTutti.Enabled = this.mEstBiz.ListaOutput.Count > 0;
        }

        private void actDeleteOne(object sender, EventArgs e)
        {
            var selItem = this.lvStorico.SelectedItems[0];
            var output = selItem.Tag as ReportEstrazioneOutput;

            if (MessageBox.Show(string.Format($"Confermi l'eleminazione dell'esecuzione con id {output.Id}?"), "Conferma", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            if(this.mEstBiz.IsSqlConParametriElaborazione() && output.Equals(this.mEstBiz.ListaOutput.GetLast()))
            {
                if (!UI_Utils.ShowConfirmYesNo("Attenzione! L'SQL dell'estrazione contiene una dipendenza [parametro {0}] dall'output che si vuole eliminare.\n\nConfermi l'eliminazione che influenzerà l'esecuzione successiva?", Costanti.Sql_Params.LAST_ELAB_DATE))
                    return;
            }

            try
            {
                this.mEstBiz.GetSlot().DeleteObject(output);
                this.mEstBiz.ListaOutput.Remove(output);
                this.lvStorico.Items.Remove(selItem);
            }
            catch (Exception ex)
            {
                UI_Utils.ShowError(ex.Message);
            }
        }


        private void actDeleteAll(object sender, EventArgs e)
        {
            if (this.lvStorico.Items.Count == 0)
                return;

            if (MessageBox.Show("Confermi l'eleminazione di tutti i dati di esecuzione?", "Conferma", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            if (this.mEstBiz.IsSqlConParametriElaborazione())
            {
                if (!UI_Utils.ShowConfirmYesNo("Attenzione! L'SQL dell'estrazione contiene una dipendenza [parametro {0}] dagli output storici.\n\nConfermi l'eliminazione che può influenzare le esecuzioni successive?", Costanti.Sql_Params.LAST_ELAB_DATE))
                    return;

            }

            try
            {
                this.mEstBiz.GetSlot().DeleteAll(this.mEstBiz.ListaOutput);
                this.mEstBiz.ListaOutput.Clear();

                this.loadData();

            }
            catch (Exception ex)
            {
                UI_Utils.ShowError(ex.Message);
            }
        }

        private void actOpenFile(object sender, EventArgs e)
        {
            if (this.lvStorico.SelectedItems.Count == 0)
                return;

            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            try
            {
                var selItem = this.lvStorico.SelectedItems[0];
                var output = selItem.Tag as ReportEstrazioneOutput;

                var outFilePath = AppContextERD.GetLocalFileNameWithTime(output.NomeFile);
                File.WriteAllBytes(outFilePath, output.DataBlob);

                Process.Start(outFilePath);
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
    }
}

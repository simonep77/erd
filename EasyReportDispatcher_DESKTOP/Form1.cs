using Bdo.Objects;
using EasyReportDispatcher_DESKTOP.src;
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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            this.btnDisconnetti_Click(this.btnDisconnetti, null);
        }

        private void btnConnetti_Click(object sender, EventArgs e)
        {

            if (AppContext.Slot == null)
                AppContext.Slot = new BusinessSlot(@"MYSQLDataBase", @"Server=remotemysql.com;UserId=ourKl13l8f;Password=IXehc1qbkZ;Database=ourKl13l8f;");

            Directory.CreateDirectory(AppContext.UserDataDir);
            Directory.CreateDirectory(AppContext.UserDataDirOutput);

            this.loadEstrazioni();

            this.tsConnessione.Text = "Connesso";
            this.btnConnetti.Enabled = false;
            this.btnDisconnetti.Enabled = true;
        }

        private void loadEstrazioni()
        {
            this.clearAll();

            var lst = AppContext.Slot.CreateList<ReportEstrazioneLista>().SearchAllObjects();

            foreach (var est in lst)
            {
                var item = new ListViewItem();

                item.Tag = est;
                item.Text = est.Id.ToString();
                item.SubItems.Add(est.Nome);
                item.SubItems.Add(est.Connessione.Nome);
                item.SubItems.Add(est.Attivo == 1 ? "SI" : "NO");
                item.SubItems.Add(est.TemplateId > 0 ? "SI" : "NO");
                item.SubItems.Add(File.Exists(this.getLocalTemplate(est)) ? "SI" : "NO");

                this.lvEstrazioni.Items.Add(item);
            }

            this.tsNumEstrazioni.Text = string.Format("N.Estrazioni: {0}", this.lvEstrazioni.Items.Count);
        }

        private void btnDisconnetti_Click(object sender, EventArgs e)
        {
            this.tsConnessione.Text = "Non connesso";
            this.clearAll();
            this.btnConnetti.Enabled = true;
            this.btnDisconnetti.Enabled = false;

            if (AppContext.Slot != null)
                AppContext.Slot.Dispose();

            AppContext.Slot = null;
        }

        private void clearAll()
        {
            this.lvEstrazioni.Items.Clear();
            this.handleSelezioneEstrazione();
            this.tsNumEstrazioni.Text = string.Format("N.Estrazioni: {0}", "ND");

        }

        private ReportEstrazione getSelectedEstrazione()
        {
            if (this.lvEstrazioni.SelectedItems.Count == 0)
                return null;

            return this.lvEstrazioni.SelectedItems[0].Tag as ReportEstrazione;

        }

        private void handleSelezioneEstrazione()
        {
            if (this.lvEstrazioni.SelectedItems.Count == 0)
            {
                this.btnOpenTemplate.Enabled = false;
                this.btnEsegui.Enabled = false;
                this.btnELiminaTplLocale.Enabled = false;
                this.btnSalvaTemplate.Enabled = false;
            }
            else
            {
                ReportEstrazione est = this.getSelectedEstrazione();
                this.btnOpenTemplate.Enabled = (est.TemplateId > 0);
                this.btnEsegui.Enabled = true;
                this.btnELiminaTplLocale.Enabled = File.Exists(this.getLocalTemplate(est));
                this.btnSalvaTemplate.Enabled = File.Exists(this.getLocalTemplate(est));
            }

        }

        private void handleOpenTemplate()
        {
            ReportEstrazione est = this.getSelectedEstrazione();

            var localtemplate = this.getLocalTemplate(est);
            var useLocal = false;

            if (File.Exists(localtemplate))
                if (MessageBox.Show(@"Esiste una copia locale del template, vuoi riutilizzarla o scaricare la versione sul server?", "Conferma", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    useLocal = true;

            if(!useLocal)
            {
                File.WriteAllBytes(localtemplate, est.Template.TemplateBlob);
                this.lvEstrazioni.SelectedItems[0].SubItems[this.colTemplateLocale.DisplayIndex].Text = "SI";
            }

            this.handleSelezioneEstrazione();

            Process.Start(localtemplate);

        }




        private string getLocalTemplate(ReportEstrazione est)
        {
            return Path.Combine(AppContext.UserDataDir, string.Format(@"ReportEstrazione_{0}.xlsx", est.Id));
        }


        private void lvEstrazioni_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.handleSelezioneEstrazione();
        }

        private void btnOpenTemplate_Click(object sender, EventArgs e)
        {
            this.handleOpenTemplate();
        }

        private void btnEsegui_Click(object sender, EventArgs e)
        {
            var est = this.getSelectedEstrazione();
            using (var frm = new frmEsegui(est, getLocalTemplate(est)))
            {
                frm.ShowDialog();
            }
        }

        private void btnELiminaTplLocale_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(@"Vuoi eliminare la copia locale del template?", "Conferma", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            var est = this.getSelectedEstrazione();
            var localTemplate = this.getLocalTemplate(est);
            File.Delete(localTemplate);
            this.lvEstrazioni.SelectedItems[0].SubItems[this.colTemplateLocale.DisplayIndex].Text = "NO";
            this.handleSelezioneEstrazione();
        }

        private void btnSalvaTemplate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(@"Vuoi salvare la copia locale del template sul DB?", "Conferma", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            try
            {
                var est = this.getSelectedEstrazione();
                var localTemplate = this.getLocalTemplate(est);

                est.Template.TemplateBlob = File.ReadAllBytes(localTemplate);

                this.handleSelezioneEstrazione();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}

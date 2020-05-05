using Bdo.Objects;
using EasyReportDispatcher_DESKTOP.src;
using EasyReportDispatcher_Lib_BIZ.src.report;
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

            try
            {
                Directory.CreateDirectory(AppContextERD.UserDataDir);
                Directory.CreateDirectory(AppContextERD.UserDataDirOutput);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConnetti_Click(object sender, EventArgs e)
        {

            if (AppContextERD.Slot == null)
            {
                AppContextERD.Slot = new BusinessSlot(@"MYSQLDataBase", @"Server=remotemysql.com;UserId=ourKl13l8f;Password=IXehc1qbkZ;Database=ourKl13l8f;");
                AppContextERD.Slot.DB.AutoCloseConnection = true;
            }

            this.Cursor = Cursors.WaitCursor;
            try
            {
                this.loadEstrazioni();

                this.tsConnessione.Text = "Connesso";
                this.btnConnetti.Enabled = false;
                this.btnDisconnetti.Enabled = true;
                this.btnAddEstrazione.Enabled = true;

                this.handleSelezioneEstrazione();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            
        }

        private void loadEstrazioni2()
        {
            this.clearAll();

            var lst = AppContextERD.Slot.CreateList<ReportEstrazioneLista>()
                .OrderBy(nameof(ReportEstrazione.Id), OrderVersus.Desc)
                .SearchByColumn(Filter.Gt(nameof(ReportEstrazione.Attivo), -1));

            foreach (var est in lst)
            {
                var item = new ListViewItem();

                this.setListItem(item, est);

                this.lvEstrazioni.Items.Add(item);
            }

            this.updateEstCount();
        }



        private void loadEstrazioni()
        {
            this.clearAll();

            var lst = AppContextERD.Slot.CreateList<ReportEstrazioneLista>()
                .SearchByColumn(Filter.Gt(nameof(ReportEstrazione.Attivo), -1));

            var gps = from est in lst
                      where est.EstrazioniAccorpateIds.Length > 0
                      select est;

            foreach (var est in gps)
            {
                //Crea gruppo
                var g = new ListViewGroup("Est_" + est.Id.ToString(), est.Nome);
                g.Tag = est;
                this.lvEstrazioni.Groups.Add(g);

                var item = new ListViewItem();

                this.setListItem(item, est);

                g.Items.Add(item);
                this.lvEstrazioni.Items.Add(item);


                //Cerca tutte le estrazioni da includere
                var subs = from id in est.EstrazioniAccorpateIds.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries)
                           from e in lst
                          where id == e.Id.ToString()
                          select e;

                foreach (var est2 in subs)
                {
                    var item2 = new ListViewItem();

                    this.setListItem(item2, est2);

                    g.Items.Add(item2);
                    this.lvEstrazioni.Items.Add(item2);
                }
            }

            var gDef= new ListViewGroup("Default", "Non accorpate");
            this.lvEstrazioni.Groups.Add(gDef);

            foreach (var est in lst)
            {

                foreach (var g in this.lvEstrazioni.Groups.Cast<ListViewGroup>())
                {
                    foreach (var it in g.Items.Cast<ListViewItem>())
                    {
                        if (it.Tag.Equals(est))
                            goto endMainLoop; 
                    }
                }

                var item = new ListViewItem();

                this.setListItem(item, est);

                gDef.Items.Add(item);

                this.lvEstrazioni.Items.Add(item);

                endMainLoop:;
            }

            this.updateEstCount();
        }

        private void updateEstCount()
        {
            this.tsNumEstrazioni.Text = string.Format("N.Estrazioni: {0}", this.lvEstrazioni.Items.Count);

        }


        private void setListItem(ListViewItem item, ReportEstrazione est)
        {
            item.SubItems.Clear();

            item.Tag = est;
            item.Text = est.Id.ToString();
            item.SubItems.Add(est.Nome);
            item.SubItems.Add(est.Connessione.Nome);
            item.SubItems.Add(est.Attivo == 1 ? "SI" : "NO");
            item.SubItems.Add(est.InvioMailAttivo == 1 ? "SI" : "NO");
            item.SubItems.Add(est.EstrazioniAccorpateIds.Length > 0 ? (est.AccorpaSoloDati > 0 ? "DATI: " : "FILE: ") + est.EstrazioniAccorpateIds : "");
            item.SubItems.Add(est.TemplateId > 0 ? "SI" : "NO");
            item.SubItems.Add(File.Exists(this.getLocalTemplate(est)) ? "SI" : "NO");
            
        }


        private void btnDisconnetti_Click(object sender, EventArgs e)
        {
            this.tsConnessione.Text = "Non connesso";
            this.clearAll();
            this.btnConnetti.Enabled = true;
            this.btnDisconnetti.Enabled = false;
            this.btnAddEstrazione.Enabled = false;

            if (AppContextERD.Slot != null)
                AppContextERD.Slot.Dispose();

            AppContextERD.Slot = null;
        }

        private void clearAll()
        {
            this.lvEstrazioni.Groups.Clear();
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
                this.btnEditEstrazione.Enabled = false;
                this.btnClonaEstrazione.Enabled = false;
                this.btnDelEstrazione.Enabled = false;
            }
            else
            {
                ReportEstrazione est = this.getSelectedEstrazione();
                this.btnOpenTemplate.Enabled = (est.TemplateId > 0);
                this.btnEsegui.Enabled = true;
                this.btnELiminaTplLocale.Enabled = File.Exists(this.getLocalTemplate(est));
                this.btnSalvaTemplate.Enabled = File.Exists(this.getLocalTemplate(est));
                this.btnEditEstrazione.Enabled = true;
                this.btnClonaEstrazione.Enabled = true;
                this.btnDelEstrazione.Enabled = true;
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

                this.Cursor = Cursors.WaitCursor;
            try
            {
                if (!useLocal)
                {
                    File.WriteAllBytes(localtemplate, est.Template.TemplateBlob);

                    this.setListItem(this.lvEstrazioni.SelectedItems[0], est);
                }

                this.handleSelezioneEstrazione();

                Process.Start(localtemplate);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }



        }




        private string getLocalTemplate(ReportEstrazione est)
        {
            return Path.Combine(AppContextERD.UserDataDir, string.Format(@"ReportEstrazione_{0}.xlsx", est.Id));
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

            this.setListItem(this.lvEstrazioni.SelectedItems[0], est);


            this.handleSelezioneEstrazione();
        }

        private void btnSalvaTemplate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(@"Vuoi salvare la copia locale del template sul DB?", "Conferma", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            

            this.Cursor = Cursors.WaitCursor;
            try
            {
                var est = this.getSelectedEstrazione();




                //Verifica se il template e' collegato a diverse estra
                var lst = AppContextERD.Slot.CreateList<ReportEstrazioneLista>().SearchByColumn(Filter.Eq(nameof(ReportEstrazione.TemplateId), est.TemplateId));

                if (lst.Count > 1 )
                {
                    if (MessageBox.Show(@"Attenzione: il template che si vuole aggiornare e' utilizzato da altri report oltre al corrente, vuoi modificarlo comunque?", "Conferma", MessageBoxButtons.YesNo) != DialogResult.Yes)
                        return;
                }

                var localTemplate = this.getLocalTemplate(est);

                est.Template.TemplateBlob = File.ReadAllBytes(localTemplate);

                //Salva template
                AppContextERD.Slot.SaveObject(est.Template);

                MessageBox.Show("Salvataggio effettuato.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.handleSelezioneEstrazione();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

        private void btnEditEstrazione_Click(object sender, EventArgs e)
        {
            var est = this.getSelectedEstrazione();
            using (var frm = new frmEstrazione(est))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    this.setListItem(this.lvEstrazioni.SelectedItems[0], est);
                }
                else
                    AppContextERD.Slot.RefreshObject(est, true);
            }
        }

        private void btnAddEstrazione_Click(object sender, EventArgs e)
        {
            var est = AppContextERD.Slot.CreateObject<ReportEstrazione>();

            using (var frm = new frmEstrazione(est))
            {
                if (frm.ShowDialog() != DialogResult.OK)
                    return;

                this.addEstrazioneToList(est);


            }
        }

        private void btnClonaEstrazione_Click(object sender, EventArgs e)
        {
            var est = this.getSelectedEstrazione();
            var estNew = AppContextERD.Slot.CloneObjectForNew<ReportEstrazione>(est);

            estNew.Nome = "Clone di " + estNew.Nome;

            using (var frm = new frmEstrazione(estNew))
            {
                if (frm.ShowDialog() != DialogResult.OK)
                    return;

                this.addEstrazioneToList(estNew);
            }
        }

        private void addEstrazioneToList(ReportEstrazione est)
        {
            var item = new ListViewItem();
            this.setListItem(item, est);
            this.lvEstrazioni.Items.Insert(0, item);
            this.updateEstCount();
            item.Selected = true;
        }

        private void btnDelEstrazione_Click(object sender, EventArgs e)
        {
            var est = this.getSelectedEstrazione();

            if (MessageBox.Show(string.Format($"Vuoi eliminare l'estrazione {est.Id}?"), "Conferma", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            var estBiz = est.ToBizObject<ReportEstrazioneBIZ>();
            estBiz.EliminaLogicamente();

            MessageBox.Show("ELiminazione (logica) effettuata.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.lvEstrazioni.Items.Remove(this.lvEstrazioni.SelectedItems[0]);
            this.updateEstCount();
        }


    }
}

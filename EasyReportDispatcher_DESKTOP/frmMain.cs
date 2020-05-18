using Bdo.Objects;
using EasyReportDispatcher_DESKTOP.src;
using EasyReportDispatcher_Lib_BIZ.src.report;
using EasyReportDispatcher_Lib_DAL.src.report;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyReportDispatcher_DESKTOP
{
    public partial class frmMain : Form
    {

        private ReportEstrazioneBIZ mCurrentEstrazione;
        private List<ListViewItem> mLvItems = new List<ListViewItem>(100);

        public frmMain()
        {
            InitializeComponent();

            this.actDisconnetti(this, null);

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

        private void actConnetti(object sender, EventArgs e)
        {
            this.tsConnessione.Text = "Connessione in corso...";
            Application.DoEvents();

            if (AppContextERD.Slot == null)
            {
                AppContextERD.Slot = new BusinessSlot(Properties.Settings.Default.ClasseDataBase, Properties.Settings.Default.StringaConnessione);
                AppContextERD.Slot.DB.AutoCloseConnection = true;
            }

            this.Cursor = Cursors.WaitCursor;
            try
            {
                this.loadEstrazioni();

                this.tsConnessione.Text = "Connesso";

                this.tsNumEstrazioni.Visible = true;

                this.tsEstrazioneNew.Enabled = true;

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

        private void loadEstrazioni()
        {
            this.clearAll();

            //Application.DoEvents();

            var lst = AppContextERD.Slot.CreateList<ReportEstrazioneLista>()
                .OrderBy(nameof(ReportEstrazione.Id), OrderVersus.Desc)
                .SearchByColumn(Filter.Gt(nameof(ReportEstrazione.Attivo), -1));

            foreach (var est in lst)
            {
                this.addEstrazioneToList(est.ToBizObject<ReportEstrazioneBIZ>(), false);
                //Application.DoEvents();
            }

            //Indenta le dipendenti
            //foreach (var item in this.mLvItems)
            //{
            //    var est = item.Tag as ReportEstrazioneBIZ;
            //}

            this.updateEstCount();
        }

        private void addEstrazioneToList(ReportEstrazioneBIZ est, bool select)
        {
            var item = new ListViewItem();
            this.setListItem(item, est);
            this.mLvItems.Add(item);
            this.lvEstrazioni.Items.Add(item);
            this.updateEstCount();

            if (select)
            {
                item.Selected = true;
                item.EnsureVisible();
            }
        }



        private void loadEstrazioni2()
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

                this.setListItem(item, est.ToBizObject<ReportEstrazioneBIZ>());

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

                    this.setListItem(item2, est2.ToBizObject<ReportEstrazioneBIZ>());

                    g.Items.Add(item2);
                    this.lvEstrazioni.Items.Add(item2);
                }
            }

            var gDef= new ListViewGroup("Default", "STANDARD");
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

                this.setListItem(item, est.ToBizObject<ReportEstrazioneBIZ>());

                gDef.Items.Add(item);

                this.lvEstrazioni.Items.Add(item);

                endMainLoop:;
            }

            this.updateEstCount();
        }

        private void updateEstCount()
        {
            this.tsNumEstrazioni.Text = string.Format("Estrazioni: {0}", this.lvEstrazioni.Items.Count);

        }


        private void setListItem(ListViewItem item, ReportEstrazioneBIZ est)
        {
            item.SubItems.Clear();

            item.Tag = est;
            item.Text = est.DataObj.Id.ToString();
            item.SubItems.Add(est.DataObj.Nome);
            item.SubItems.Add(est.DataObj.Connessione.Nome);
            item.SubItems.Add(est.DataObj.Attivo == 1 ? "SI" : "NO");

            if (est.DataObj.Attivo > 0 && est.DataObj.CronString != null)
            {
                var nextRun = est.GetNextSchedule(DateTime.Now);
                item.SubItems.Add(nextRun.ToString($"{nextRun:dd/MM/yyyy HH:mm:ss}"));
            }
            else
                item.SubItems.Add(string.Empty);

            item.SubItems.Add(est.DataObj.InvioMailAttivo == 1 ? "SI" : "NO");
            item.SubItems.Add(est.DataObj.EstrazioniAccorpateIds.Length > 0 ? (est.DataObj.AccorpaSoloDati > 0 ? "DATI: " : "FILE: ") + est.DataObj.EstrazioniAccorpateIds : "");
            item.SubItems.Add(est.DataObj.TemplateId > 0 ? "SI" : "NO");
            item.SubItems.Add(File.Exists(this.getLocalTemplate(est)) ? "SI" : "NO");

            if (est.DataObj.EstrazioniAccorpateIds.Length > 0)
            {
                item.ImageKey = @"application_double.png";
            }
            else
            {
                item.ImageKey = @"table.png";
            }

        }


        private void actDisconnetti(object sender, EventArgs e)
        {
            this.tsConnessione.Text = "Non connesso";
            this.clearAll();

            this.tsEstrazioneNew.Enabled = false;

            if (AppContextERD.Slot != null)
                AppContextERD.Slot.Dispose();

            AppContextERD.Slot = null;
        }

        private void clearAll()
        {
            this.mLvItems.Clear();
            //this.lvEstrazioni.Groups.Clear();
            this.lvEstrazioni.Items.Clear();
            this.handleSelezioneEstrazione();
            this.tsNumEstrazioni.Visible = false;
            this.tsNumEstrazioni.Text = @"Estrazioni: -";
            this.txtFiltro.Text = string.Empty;
            //this.lbFiltroNum.Visible = false;
            //this.lbFiltroNum.Text = string.Empty;

        }

        private ReportEstrazioneBIZ getSelectedEstrazioneBiz()
        {
            if (this.mCurrentEstrazione == null)
                return null;

            return this.mCurrentEstrazione;

        }

        private void handleSelezioneEstrazione()
        {
            var selected = (this.mCurrentEstrazione != null);


            this.tsTemplateLocOpen.Enabled = selected && (this.mCurrentEstrazione.IsTemplateCustom);
            this.tsTemplateLocElimina.Enabled = selected && File.Exists(this.getLocalTemplate(this.mCurrentEstrazione));
            this.tsTemplateLocSave.Enabled = selected && File.Exists(this.getLocalTemplate(this.mCurrentEstrazione));
            
            this.tsEstrazioneEdit.Enabled = selected;
            this.tsEstrazioneClona.Enabled = selected;
            this.tsEstrazioneDel.Enabled = selected;
            this.tsEstrazioneEsegui.Enabled = selected;

        }

        private void handleOpenTemplate()
        {
            var est = this.getSelectedEstrazioneBiz();

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
                    File.WriteAllBytes(localtemplate, est.DataObj.Template.TemplateBlob);

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




        private string getLocalTemplate(ReportEstrazioneBIZ est)
        {
            return Path.Combine(AppContextERD.UserDataDir, string.Format(@"ReportEstrazione_{0}.xlsx", est.DataObj.Id));
        }


        private void lvEstrazioni_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.mCurrentEstrazione = (this.lvEstrazioni.SelectedItems.Count == 0) ? null : (ReportEstrazioneBIZ)this.lvEstrazioni.SelectedItems[0].Tag;

            this.handleSelezioneEstrazione();
        }

        private void btnOpenTemplate_Click(object sender, EventArgs e)
        {
            this.handleOpenTemplate();
        }

        private void btnEsegui_Click(object sender, EventArgs e)
        {
            var est = this.getSelectedEstrazioneBiz();
            using (var frm = new frmEsegui(est, getLocalTemplate(est)))
            {
                frm.ShowDialog();
            }
        }

        private void btnELiminaTplLocale_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(@"Vuoi eliminare la copia locale del template?", "Conferma", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            var est = this.getSelectedEstrazioneBiz();
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
                var est = this.getSelectedEstrazioneBiz();




                //Verifica se il template e' collegato a diverse estra
                var lst = AppContextERD.Slot.CreateList<ReportEstrazioneLista>().SearchByColumn(Filter.Eq(nameof(ReportEstrazione.TemplateId), est.DataObj.TemplateId));

                if (lst.Count > 1 )
                {
                    if (MessageBox.Show(@"Attenzione: il template che si vuole aggiornare e' utilizzato da altri report oltre al corrente, vuoi modificarlo comunque?", "Conferma", MessageBoxButtons.YesNo) != DialogResult.Yes)
                        return;
                }

                var localTemplate = this.getLocalTemplate(est);

                est.DataObj.Template.TemplateBlob = File.ReadAllBytes(localTemplate);

                //Salva template
                AppContextERD.Slot.SaveObject(est.DataObj.Template);

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
            var est = this.getSelectedEstrazioneBiz();

            using (var frm = new frmEstrazione(est))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    this.setListItem(this.lvEstrazioni.SelectedItems[0], est);
                }
                else
                    AppContextERD.Slot.RefreshObject(est.DataObj, true);
            }
        }

        private void btnAddEstrazione_Click(object sender, EventArgs e)
        {
            var est = AppContextERD.Slot.CreateObject<ReportEstrazione>().ToBizObject<ReportEstrazioneBIZ>();

            using (var frm = new frmEstrazione(est))
            {
                if (frm.ShowDialog() != DialogResult.OK)
                    return;

                this.addEstrazioneToList(est, true);


            }
        }

        private void btnClonaEstrazione_Click(object sender, EventArgs e)
        {
            var estBiz = this.getSelectedEstrazioneBiz();

            ReportEstrazioneBIZ estNew;

            var cloneSave = MessageBox.Show("Vuoi clonare anche anche tutti gli oggetti dipendenti (destinatari email, copia dei file, ..) è necessario salvare immediatamente la versione clonata. Confermi?\n\n Premendo [NO] verrà creata una copia non salvata e senza entità dipendenti", @"Conferma", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            estNew = estBiz.ClonaEstrazione(cloneSave == DialogResult.Yes);



            using (var frm = new frmEstrazione(estNew))
            {
                frm.ShowDialog();

                if (estNew.DataObj.ObjectState != EObjectState.New)
                    this.addEstrazioneToList(estNew, true);

            }
        }


        private void btnDelEstrazione_Click(object sender, EventArgs e)
        {
            var est = this.getSelectedEstrazioneBiz();

            if (MessageBox.Show(string.Format($"Vuoi eliminare (logicamente) l'estrazione {est.DataObj.Id}?"), "Conferma", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            est.EliminaLogicamente();

            MessageBox.Show("Eliminazione (logica) effettuata.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.lvEstrazioni.Items.Remove(this.lvEstrazioni.SelectedItems[0]);
            this.updateEstCount();
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtFiltro.Text))
            {
                this.lbFiltroNum.Visible = false;

                if (this.lvEstrazioni.Items.Count != this.mLvItems.Count)
                {
                    this.lvEstrazioni.Items.Clear();
                    this.lvEstrazioni.Items.AddRange(this.mLvItems.ToArray());
                }

                return;
            }
            //Esce se vuoto
            if (this.lvEstrazioni.Items.Count == 0)
                return;
            //Svuota
            this.lvEstrazioni.Items.Clear();
            //Carica
            foreach (var item in this.mLvItems)
            {
                var searchText = this.txtFiltro.Text.ToUpper();
                if (item.SubItems[colNome.DisplayIndex
                    ].Text.ToUpper().Contains(searchText))
                    this.lvEstrazioni.Items.Add(item);
            }
            //Visualizza ris
            this.lbFiltroNum.Visible = true;
            this.lbFiltroNum.Text = string.Format("Visibili {0} su {1}", this.lvEstrazioni.Items.Count, this.mLvItems.Count);
        }

        private void lvEstrazioni_DoubleClick(object sender, EventArgs e)
        {
            if (this.mCurrentEstrazione != null)
                this.btnEsegui_Click(sender, e);
        }

        private void actOutputDir(object sender, EventArgs e)
        {
            Process.Start(AppContextERD.UserDataDirOutput);
        }

        private void actRicarica(object sender, EventArgs e)
        {
            this.actDisconnetti(sender, e);
            this.actConnetti(sender, e);
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            using (var frm = new frmConfig())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (Properties.Settings.Default.StringaConnessione != AppContextERD.Slot.DB.ConnectionString)
                    {
                        UI_Utils.ShowInfo("Puntamento DB modificato. Verrà riavviato il caricamento delle estrazioni.");

                        this.actDisconnetti(null, null);

                        AppContextERD.Slot = null;

                        this.actConnetti(null, null);
                    }

                }
            }
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            this.actConnetti(sender, e);
        }
    }
}

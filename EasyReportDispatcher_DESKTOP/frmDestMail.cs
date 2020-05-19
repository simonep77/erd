using Bdo.Objects;
using EasyReportDispatcher_DESKTOP.src;
using EasyReportDispatcher_Lib_DAL.src.report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyReportDispatcher_DESKTOP
{
    public partial class frmDestMail : ErdForm
    {
        private ReportEstrazioneDestinatarioEmail mDest;
        public frmDestMail(ReportEstrazioneDestinatarioEmail dest)
        {
            InitializeComponent();

            this.mDest = dest;

            this.loadData();

            UI_Utils.Combo_Load(this.cmbSmtp, AppContextERD.Slot.CreateList<ReportSmtpConfigLista>().SearchAllObjects(),
                nameof(ReportSmtpConfig.Nome), nameof(ReportTipoFile.Id), (this.mDest.SmtpConfigId > 0 ? this.mDest.SmtpConfig : null));
        }


        private void loadData()
        {
            this.Text = string.Format("Destinatari Mail ID: {0}", this.mDest.ObjectState == Bdo.Objects.EObjectState.New ? @"<new>" : this.mDest.Id.ToString());
            this.txtMailSubj.Text =  this.mDest.MailSUBJ;
            this.txtMailCorpo.Text =  this.mDest.MailBODY;
            this.txtPassword.Text =  this.mDest.Password;
            this.txtMailFROM.Text =  this.mDest.MailFROM;
            this.txtMailTO.Text =  this.mDest.MailTO;
            this.txtMailCC.Text =  this.mDest.MailCC;
            this.txtMailBCC.Text =  this.mDest.MailBCC;
        }

        private void chkMostra_CheckedChanged(object sender, EventArgs e)
        {
            this.txtPassword.PasswordChar = this.chkMostra.Checked ? '\0' : '*';
        }

        private void btnSalva_Click(object sender, EventArgs e)
        {
            try
            {
                //Verifiche

                //Salvataggio
                this.mDest.MailSUBJ = this.txtMailSubj.Text;
                this.mDest.MailBODY = this.txtMailCorpo.Text;
                this.mDest.Password = this.txtPassword.Text;
                this.mDest.MailFROM = this.txtMailFROM.Text;
                this.mDest.MailTO = this.txtMailTO.Text;
                this.mDest.MailCC = this.txtMailCC.Text;
                this.mDest.MailBCC = this.txtMailBCC.Text;

                AppContextERD.Slot.SaveObject(this.mDest);

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                UI_Utils.ShowError(ex.Message);
            }
            
        }

        private void actAddEditSmtp(object sender, EventArgs e)
        {
            bool isNew = (object.ReferenceEquals(sender, this.btnSmtpAdd));

            ReportSmtpConfig dest = AppContextERD.Slot.CreateObject<ReportSmtpConfig>();

            if (isNew)
                dest = AppContextERD.Slot.CreateObject<ReportSmtpConfig>();
            else
                dest = this.cmbSmtp.SelectedItem as ReportSmtpConfig;


            using (var frm = new frmSmtp(dest))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {

                    var lst = this.cmbSmtp.DataSource as ReportSmtpConfigLista;
                    lst.AddOrUpdate(dest);

                    UI_Utils.Combo_Load(this.cmbSmtp, lst,
                        nameof(ReportSmtpConfig.Nome), nameof(ReportSmtpConfig.Id), dest);
                }
            }
        }

        private void btnSmtpDel_Click(object sender, EventArgs e)
        {
            if (this.cmbSmtp.SelectedItem == null)
                return;

            ReportSmtpConfig dest = this.cmbSmtp.SelectedItem as ReportSmtpConfig;

            if (MessageBox.Show(string.Format("Confermi l'eliminazione della configurazione SMTP '{0}' ", dest.Nome, dest.Id), "Conferma", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                //Se la config e' utilizzata anche da altri destinatari 
                var lstdest = AppContextERD.Slot.CreateList<ReportEstrazioneDestinatarioEmailLista>().SearchByColumn(Filter.Eq(nameof(ReportEstrazioneDestinatarioEmail.SmtpConfigId), dest.Id));

                if (lstdest.Count != 0)
                    throw new ApplicationException("Attenzione: la configurazione e' in uso. Cambiarla prima dove viene utilizzata.");

                AppContextERD.Slot.DeleteObject(dest);

                ReportSmtpConfigLista lst = this.cmbSmtp.DataSource as ReportSmtpConfigLista;

                lst.Remove(dest);

                UI_Utils.Combo_Load(this.cmbSmtp, lst,
                        nameof(ReportSmtpConfig.Nome), nameof(ReportSmtpConfig.Id), null);

                UI_Utils.ShowInfo("Configurazione SMTP eliminata.");

            }
            catch (Exception ex)
            {
                UI_Utils.ShowError(ex.Message);

            }
        }
    }
}

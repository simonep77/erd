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
    public partial class frmDestMail : Form
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
            this.Text = string.Format("Destinatari Mail {0}", this.mDest.ObjectState == Bdo.Objects.EObjectState.New ? @"<new>" : this.mDest.Id.ToString());
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
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

    }
}

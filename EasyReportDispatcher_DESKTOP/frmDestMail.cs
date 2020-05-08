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
    }
}

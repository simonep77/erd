using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyReportDispatcher_DESKTOP.src;
using EasyReportDispatcher_Lib_DAL.src.report;

namespace EasyReportDispatcher_DESKTOP
{
    public partial class frmSmtp : Form
    {
        private ReportSmtpConfig mConf;

        public frmSmtp( ReportSmtpConfig conf )
        {
            InitializeComponent();

            this.mConf = conf;

            this.loadData();
        }

        private void loadData()
        {

            this.Text = string.Format(@"Configuraziuone SMTP: {0}", this.mConf.ObjectState == Bdo.Objects.EObjectState.New ? "<new>" : this.mConf.Id.ToString());

            this.txtNome.Text = this.mConf.Nome;
            this.txtHost.Text = this.mConf.Smtp;
            this.txtPort.Value = this.mConf.Port;
            this.chbSSL.Checked = (this.mConf.UseSSL > 0);
            this.chbAuth.Checked = (this.mConf.Auth > 0);
            this.txtUsername.Text = this.mConf.UserName;
            this.txtPassword.Text = this.mConf.Password;

            this.txtUsername.Enabled = this.chbAuth.Checked;
            this.txtPassword.Enabled = this.chbAuth.Checked;
        }

        private void btnSalva_Click(object sender, EventArgs e)
        {
            
            try
            {
                this.mConf.Nome = this.txtNome.Text;
                this.mConf.Smtp = this.txtHost.Text;
                this.mConf.Port = Convert.ToInt32(this.txtPort.Value);
                this.mConf.UseSSL = this.chbSSL.Checked ? (sbyte)1 : (sbyte)0;
                this.mConf.Auth = this.chbAuth.Checked ? (sbyte)1 : (sbyte)0;
                this.mConf.UserName = this.txtUsername.Text;
                this.mConf.Password = this.txtPassword.Text;

                AppContextERD.Slot.SaveObject(this.mConf);

                this.loadData();

                UI_Utils.ShowInfo("Configurazione salvata.");

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                UI_Utils.ShowError(ex.Message);
            }


        }
    }
}

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
    public partial class frmConnessione : Form
    {
        private ReportConnessione mConn;
        public frmConnessione(ReportConnessione conn)
        {
            InitializeComponent();

            this.mConn = conn;

            this.initData();

            this.fillData();
        }

        private void fillData()
        {
            this.Text = string.Format(@"Connessione ID: {0}", (this.mConn.ObjectState == Bdo.Objects.EObjectState.New) ? "<nuovo>" : this.mConn.Id.ToString());
            this.txtNome.Text = this.mConn.Nome;
            this.txtConnStr.Text = this.mConn.ConnectionString;

            int idx = 0;
            foreach (var item in this.cmbTipoDb.DataSource as IList<dynamic>)
            {
                if (item.Class.ToUpper() == this.mConn.BdoDbConnectioType.ToUpper())
                {
                    this.cmbTipoDb.SelectedIndex = idx;
                    break;
                }
                idx++;

            }
        }

        private void initData()
        {
            var lst = new[] { new { Testo="MYSQL", Class="MYSQLDataBase" }, new { Testo = "MS SQL SERVER", Class = "MSSQLDataBase2005" } };

            this.cmbTipoDb.DisplayMember = "Testo";
            this.cmbTipoDb.ValueMember = "Class";
            this.cmbTipoDb.DataSource = lst;

        }

        private void btnSlava_Click(object sender, EventArgs e)
        {
            //validazione

            //salva
            try
            {
                this.mConn.Nome = this.txtNome.Text;
                this.mConn.ConnectionString = this.txtConnStr.Text;
                this.mConn.BdoDbConnectioType = this.cmbTipoDb.SelectedValue.ToString();

                AppContextERD.Slot.SaveObject(this.mConn);

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                UI_Utils.ShowError(ex.Message);
            }
        }
    }
}

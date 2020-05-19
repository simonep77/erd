using EasyReportDispatcher_DESKTOP.src;
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
    public partial class frmConfig : ErdForm
    {
        public frmConfig()
        {
            InitializeComponent();
        }

        private void btnSalva_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            this.DialogResult = DialogResult.OK;
        }
    }
}

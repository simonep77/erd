using EasyReportDispatcher_Lib_BIZ.src.report;
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
    public partial class frmStorico : Form
    {
        private ReportEstrazioneBIZ mEstBiz;
        public frmStorico(ReportEstrazioneBIZ est)
        {
            this.mEstBiz = est;

            InitializeComponent();

        }
        private void loadData()
        {
            foreach (var output in this.mEstBiz.ListaOutput)
            {
                var item = new ListViewItem();
                item.Name = output.Id.ToString();
                item.SubItems.Add(output.DataOraInizio.ToString(@"dd/MM/yyyy HH:mm:ss"));
            }
        }
    }
}

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
    public partial class frmShowQResult : Form
    {
        public frmShowQResult(DataTable tab)
        {
            InitializeComponent();


            var myBindingSource = new BindingSource();

            myBindingSource.DataSource = tab;


           
            this.dgv.DataSource = myBindingSource;
            
            this.nav.BindingSource = myBindingSource;




        }

    }
}

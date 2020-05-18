using EasyReportDispatcher_Lib_BIZ.src.report;
using EasyReportDispatcher_Lib_Common.src.enums;
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

            this.Text = string.Format($"Storico Estrazione ID: {this.mEstBiz.DataObj.Id} - {this.mEstBiz.DataObj.Nome}");

            this.loadData();
        }
        private void loadData()
        {
            this.lvStorico.Items.Clear();

            foreach (var output in this.mEstBiz.ListaOutput)
            {
                var item = new ListViewItem(output.Id.ToString());
                item.Name = output.Id.ToString();
                item.SubItems.Add(output.DataOraInizio.ToString(@"dd/MM/yyyy HH:mm:ss"));
                item.SubItems.Add(output.DataOraFine.Subtract(output.DataOraInizio).ToString());
                

                if (output.StatoId != eReport.StatoEstrazione.TerminataConSuccesso)
                {
                    item.UseItemStyleForSubItems = false;
                    var subi = item.SubItems.Add("ERR");
                    subi.ForeColor = Color.Red;
                    item.SubItems.Add(output.EstrazioneEsito);
                }
                else
                {
                    item.SubItems.Add("OK");
                    item.SubItems.Add(output.NomeFile);

                }


                this.lvStorico.Items.Add(item);
            }

            this.lbExecCount.Text = string.Format($"Esecuzioni: {this.mEstBiz.ListaOutput.Count}");

            this.lvStorico_SelectedIndexChanged(this.lvStorico, null);
        }

        private void lvStorico_SelectedIndexChanged(object sender, EventArgs e)
        {
            var sel = this.lvStorico.SelectedItems.Count > 0;

            this.tsElimina.Enabled = sel;
            this.btnEliminaTutti.Enabled = this.mEstBiz.ListaOutput.Count > 0;
        }
    }
}

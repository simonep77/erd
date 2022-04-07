using EasyReportDispatcher_DESKTOP.src;
using EasyReportDispatcher_Lib_BIZ.src.report;
using EasyReportDispatcher_Lib_DAL.src.report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyReportDispatcher_DESKTOP
{
    public partial class frmEsegui : ErdForm
    {
        private ReportEstrazioneBIZ mEstrazioneBiz;
        private string mLocalTemplatePath;
        public frmEsegui(ReportEstrazioneBIZ est, string localTplPath)
        {
            this.mEstrazioneBiz = est;
            this.mLocalTemplatePath = localTplPath;

            InitializeComponent();

            this.loadData();
        }


        private void loadData()
        {
            this.chkUsaTemplateLocale.Enabled = File.Exists(this.mLocalTemplatePath);
            this.chbInvioEmail.Enabled = this.mEstrazioneBiz.IsPrevistoInvioMail;
            this.chbApriExcel.Checked = this.chkUsaTemplateLocale.Enabled || !this.chkSaveOutput.Enabled || !this.chbInvioEmail.Enabled;
            this.chkCopyTo.Enabled = !string.IsNullOrWhiteSpace(this.mEstrazioneBiz.DataObj.CopyToPath);
        }

        private void btnEsegui_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.btnEsegui.Text = "Esecuzione...";
            this.btnEsegui.Enabled = false;
            Application.DoEvents();
            try
            {
                if (this.chkUsaTemplateLocale.Checked)
                    this.mEstrazioneBiz.ForcedTemplate = File.ReadAllBytes(this.mLocalTemplatePath);

                //Se solo query non esegue il Run standard
                if (!this.chbSoloQuery.Checked)
                    this.mEstrazioneBiz.Run(this.chkSaveOutput.Checked, this.chbInvioEmail.Checked, this.chkCopyTo.Checked);

                if (this.chbApriExcel.Checked)
                {
                    //Apre
                    Directory.CreateDirectory(AppContextERD.UserDataDirOutput);

                    var outFilePath = AppContextERD.GetLocalFileNameWithTime(this.mEstrazioneBiz.LastResult.NomeFile);
                    File.WriteAllBytes(outFilePath, this.mEstrazioneBiz.LastResult.DataBlob);

                    Process.Start(outFilePath);

                }
                else if (this.chbSoloQuery.Checked)
                {
                        var tab = this.mEstrazioneBiz.RunSQL();

                        using (var f = new frmShowQResult(tab))
                        {
                            f.ShowDialog();
                        }

                }
                else
                    UI_Utils.ShowInfo("Esecuzione conclusa con successo");

                this.Close();
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.AppendLine(@"Si e' verificato un errore:");
                sb.AppendLine();
                sb.AppendLine(ex.Message);
                sb.AppendLine();
                sb.AppendLine(@"Stack:");
                sb.AppendLine(ex.StackTrace);
                MessageBox.Show(sb.ToString(), "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                this.Cursor = this.DefaultCursor;
                this.btnEsegui.Text = "Esegui";
                this.btnEsegui.Enabled = true;
            }

        }

        private void chbSoloQuery_CheckedChanged(object sender, EventArgs e)
        {
            var chks = this.Controls.Cast<Control>().Where(c => c is CheckBox && !c.Equals(this.chbSoloQuery)).ToList();
            
            if (this.chbSoloQuery.Checked)
            {
                chks.ForEach(c => { ((CheckBox)c).Checked = false; c.Enabled = false; });
            }
            else
            {
                chks.ForEach(c => { c.Enabled = true; });
                this.loadData();

            }
        }
    }
}

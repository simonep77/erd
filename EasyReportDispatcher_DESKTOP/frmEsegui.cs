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

        }

        private void btnEsegui_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (this.chkUsaTemplateLocale.Checked)
                    this.mEstrazioneBiz.ForcedTemplate = File.ReadAllBytes(this.mLocalTemplatePath);

                this.mEstrazioneBiz.Run(this.chkSaveOutput.Checked);

                if (this.chbInvioEmail.Checked)
                    this.mEstrazioneBiz.SendEmail(this.chkSaveOutput.Checked);

                //Apre
                Directory.CreateDirectory(AppContextERD.UserDataDirOutput);

                var outFilePath = Path.Combine(AppContextERD.UserDataDirOutput, this.mEstrazioneBiz.LastResult.NomeFile);
                File.WriteAllBytes(outFilePath, this.mEstrazioneBiz.LastResult.DataBlob);

                Process.Start(outFilePath);

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
                this.Cursor = this.DefaultCursor;
            }

        }
    }
}

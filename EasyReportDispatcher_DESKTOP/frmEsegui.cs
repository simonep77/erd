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
    public partial class frmEsegui : Form
    {
        private ReportEstrazione mEstrazione;
        private string mLocalTemplatePath;
        public frmEsegui(ReportEstrazione est, string localTplPath)
        {
            this.mEstrazione = est;
            this.mLocalTemplatePath = localTplPath;

            InitializeComponent();

            this.chkUsaTemplateLocale.Enabled = File.Exists(localTplPath);
        }

        private void btnEsegui_Click(object sender, EventArgs e)
        {
            var estBiz = this.mEstrazione.ToBizObject<ReportEstrazioneBIZ>();

            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (this.chkUsaTemplateLocale.Checked)
                    estBiz.ForcedTemplate = File.ReadAllBytes(this.mLocalTemplatePath);

                estBiz.Run(this.chkSaveOutput.Checked);

                Directory.CreateDirectory(AppContextERD.UserDataDirOutput);

                var outFilePath = Path.Combine(AppContextERD.UserDataDirOutput, estBiz.LastResult.NomeFile);
                File.WriteAllBytes(outFilePath, estBiz.LastResult.DataBlob);

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

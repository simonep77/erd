using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyReportDispatcher_DESKTOP.src
{
    class UI_Utils
    {

        /// <summary>
        /// Mostra popup errore
        /// </summary>
        /// <param name="msgFmt"></param>
        /// <param name="args"></param>
        public static void ShowError(string msgFmt, params object[] args)
        {
            MessageBox.Show(string.Format(msgFmt, args), "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        /// <summary>
        /// Mostra popup info
        /// </summary>
        /// <param name="msgFmt"></param>
        /// <param name="args"></param>
        public static void ShowInfo(string msgFmt, params object[] args)
        {
            MessageBox.Show(string.Format(msgFmt, args), "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

    }
}

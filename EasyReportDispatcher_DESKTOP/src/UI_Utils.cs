using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bdo.Objects.Base;

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


        /// <summary>
        /// Caricamento basico combo
        /// </summary>
        /// <param name="combo"></param>
        /// <param name="list"></param>
        /// <param name="dMember"></param>
        /// <param name="vMember"></param>
        /// <param name="item"></param>
        public static void Combo_Load(ComboBox combo, DataListBase list, string dMember, string vMember, DataObjectBase item)
        {
            combo.DataSource = null;
            combo.DisplayMember = dMember;
            combo.ValueMember = vMember;
            combo.DataSource = list;

            if (item != null)
                combo.SelectedItem = item;
        }

    }
}

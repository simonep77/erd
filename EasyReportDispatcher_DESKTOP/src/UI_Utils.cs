using System;
using System.Collections.Generic;
using System.Drawing;
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


        public static void ShowSpinner(Control ctrl)
        {
            //return;

            var pb = new PictureBox();
            pb.Name = ctrl.Name + "_spinner";
            pb.Image = Properties.Resources.spinner;
            pb.Width = Properties.Resources.spinner.Width;
            pb.Height = Properties.Resources.spinner.Height;
            
            ctrl.Controls.Add(pb);

            pb.Left = ctrl.Width / 2 - pb.Width/2;
            pb.Top = ctrl.Height / 2 - pb.Height/2;
            //ctrl.Enabled = false;
            Application.DoEvents();
        }

        public static void HideSpinner(Control ctrl)
        {
            var pb = ctrl.Controls[ctrl.Name + "_spinner"];

            if (pb != null)
            {
                pb.Hide();

                ctrl.Controls.Remove(pb);
                pb.Dispose();

            }
            //ctrl.Enabled = true;
            Application.DoEvents();
        }


        /// <summary>
        /// Registra il form per la chiusura tramite ESC
        /// </summary>
        /// <param name="frm"></param>
        public static void registerExitESC(Form frm)
        {
            Button cancelBTN = new Button();
            cancelBTN.Size = new Size(0, 0);
            cancelBTN.TabStop = false;
            frm.Controls.Add(cancelBTN);
            frm.CancelButton = cancelBTN;
        }


    }
}

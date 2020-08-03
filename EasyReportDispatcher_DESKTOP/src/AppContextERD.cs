using Bdo.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyReportDispatcher_DESKTOP.src
{
    public class AppContextERD
    {

        public static BusinessSlot Slot { get; set; }

        public static string UserDataDir { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ERD");
        public static string UserDataDirOutput { get; } = Path.Combine(UserDataDir, "Output");
        public static string UserDataDirTemplate { get; } = Path.Combine(UserDataDir, "Template");

        public static EasyReportDispatcher_Lib_DAL.src.report.ReportUtente Utente { get; set; }


        public static void InitDirectories()
        {

            Directory.CreateDirectory(UserDataDir);
            Directory.CreateDirectory(UserDataDirOutput);
            Directory.CreateDirectory(UserDataDirTemplate);
        }

        /// <summary>
        /// Crea uno slot per l'accesso ai dati
        /// </summary>
        /// <returns></returns>
        public static BusinessSlot CreateSlot()
        {
            var slot = new BusinessSlot(Properties.Settings.Default.ClasseDataBase, Properties.Settings.Default.StringaConnessione);
            slot.LiveTrackingEnabled = true;
            slot.ChangeTrackingEnabled = true;
            slot.DB.AutoCloseConnection = true;

            return slot;
        }

        /// <summary>
        /// Ritorna path locale per salvataggio con timestamp orario suffisso
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetLocalFileNameWithTime(string fileName)
        {
            return Path.Combine(UserDataDirOutput, Path.GetFileNameWithoutExtension(fileName) + "_ts" + DateTime.Now.ToString("HHmmss") + Path.GetExtension(fileName));
        }

    }
}

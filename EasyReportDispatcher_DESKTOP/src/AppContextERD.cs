using Bdo.Objects;
using Microsoft.Win32;
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

        private static string mConfigConnectionString;
        public static string ConfigConnectionString
        {
            get
            {
                if (mConfigConnectionString == null)
                    mConfigConnectionString = ReadConfig(nameof(Properties.Settings.Default.StringaConnessione));

                return mConfigConnectionString;
            }
            set
            {
                mConfigConnectionString = SaveConfig(nameof(Properties.Settings.Default.StringaConnessione), value);
            }
        }

        private static string mConfigClasseDataBase;
        public static string ConfigClasseDataBase
        {
            get
            {
                if (mConfigClasseDataBase == null)
                    mConfigClasseDataBase = ReadConfig(nameof(Properties.Settings.Default.ClasseDataBase));

                return mConfigClasseDataBase;
            }
            set
            {
                mConfigClasseDataBase = SaveConfig(nameof(Properties.Settings.Default.ClasseDataBase), value);
            }
        }

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
            var slot = new BusinessSlot(AppContextERD.ConfigClasseDataBase, AppContextERD.ConfigConnectionString);
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

        public static string ReadConfig(string key)
        {
            var regKey = Registry.LocalMachine.CreateSubKey("Software\\Easy Report\\Desktop");

            return regKey.GetValue(key, Properties.Settings.Default[key]).ToString();
        }

        public static string SaveConfig(string key, string value)
        {
            var regKey = Registry.LocalMachine.CreateSubKey("Software\\Easy Report\\Desktop");

            regKey.SetValue(key, value);

            return value;
        }

    }
}

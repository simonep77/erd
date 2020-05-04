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

    }
}

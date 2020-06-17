using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyReportDispatcher_Lib_Common.src
{
    public static class Costanti
    {
        /// <summary>
        /// Parametri SQL esposti dalla piattaforma
        /// </summary>
        public static class Sql_Params
        {
            /// <summary>
            /// ID dell'estrazione in esecuzione
            /// </summary>
            public const string REPORT_ID = @"@ERD_REPORTI_ID";


            /// <summary>
            /// Parametro contenente valore di data/ora dell'ultioma elaborazione eseguita con successo
            /// </summary>
            public const string LAST_ELAB_DATE = @"@ERD_LAST_ELAB_DATE";

        }



    }
}

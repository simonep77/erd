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


            public const string DATE_TODAY = @"@ERD_DATE_TODAY";
            public const string DATE_YESTERDAY = @"@ERD_DATE_YESTERDAY";

            public const string DATE_INIT_THIS_WEEK = @"@ERD_DATE_INIT_THIS_WEEK";
            public const string DATE_END_THIS_WEEK = @"@ERD_DATE_END_THIS_WEEK";

            public const string DATE_INIT_PREV_WEEK = @"@ERD_DATE_INIT_PREV_WEEK";
            public const string DATE_END_PREV_WEEK = @"@ERD_DATE_END_PREV_WEEK";

            public const string DATE_INIT_THIS_MONTH = @"@ERD_DATE_INIT_THIS_MONTH";
            public const string DATE_END_THIS_MONTH = @"@ERD_DATE_END_THIS_MONTH";

            public const string DATE_INIT_PREV_MONTH = @"@ERD_DATE_INIT_PREV_MONTH";
            public const string DATE_END_PREV_MONTH = @"@ERD_DATE_END_PREV_MONTH";

            public const string DATE_INIT_THIS_YEAR = @"@ERD_DATE_INIT_THIS_YEAR";
            public const string DATE_END_THIS_YEAR = @"@ERD_DATE_END_THIS_YEAR";

            public const string DATE_INIT_PREV_YEAR = @"@ERD_DATE_INIT_PREV_YEAR";
            public const string DATE_END_PREV_YEAR = @"@ERD_DATE_END_PREV_YEAR";


            /// <summary>
            /// Parametro contenente valore di data/ora dell'ultioma elaborazione eseguita con successo
            /// </summary>
            public const string LAST_ELAB_DATE = @"@ERD_LAST_ELAB_DATE";

        }



    }
}

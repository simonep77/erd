using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyReportDispatcher_Lib_Common.src.enums
{
    /// <summary>
    /// Costanti per Card
    /// </summary>
    public class eReport
    {

        public class StatoEstrazione
        {
            public const sbyte Avviata = 1;
            public const sbyte TerminataConSuccesso = 2;
            public const sbyte TerminataConErrori = 3;
        }

        /// <summary>
        /// Stato della schedulazione
        /// </summary>
        public class StatoSchedulazione
        {
            public const sbyte Pianificata = 1;
            public const sbyte Avviata = 2;
            public const sbyte Eseguita = 3;
            public const sbyte Saltata = 4;
            public const sbyte NonCompletata = 5;
        }

        public class TipoFile
        {
           public const int  Csv = 1;
           public const int  Excel = 2;
        }


        /// <summary>
        /// Tipo di notifica legata all'estrazione
        /// </summary>
        public class TipoNotifica
        {
            public const int Nessuna = 0;
            public const int EmailConAllegato = 1;
            public const int EmailConLink = 2;
        }

    }
}

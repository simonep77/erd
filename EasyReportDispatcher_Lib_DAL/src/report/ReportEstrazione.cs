using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bdo.Attributes;
using Bdo.Objects;

namespace EasyReportDispatcher_Lib_DAL.src.report
{
    [Table("report_estrazioni")]
    public abstract class ReportEstrazione : DataObject<ReportEstrazione>
    {
        [PrimaryKey, AutoIncrement]
        public abstract int Id { get; }

        [MaxLength(100)]
        public abstract string Nome { get; set; }

        public abstract sbyte Attivo { get; set; }

        [AcceptNull()]
        public abstract String Titolo { get; set; }

        [AcceptNull()]
        public abstract string Gruppo { get; set; }

        [AcceptNull()]
        public abstract String Note { get; set; }

        public abstract int ConnessioneId { get; set; }

        [PropertyMap(nameof(ConnessioneId))]
        public abstract ReportConnessione Connessione { get; }
        public abstract sbyte TipoFileId { get; set; }
       
        [PropertyMap(nameof(TipoFileId))]
        public abstract ReportTipoFile TipoFile { get; }


        public abstract sbyte InvioMailAttivo { get; set; }

        public abstract string SqlText { get; set; }

        [AcceptNull()]
        public abstract string SheetName { get; set; }

        public abstract string CronString { get; set; }

        public abstract DateTime DataInizio { get; set; }
        public abstract DateTime DataFine { get; set; }
        
        [DefaultValue("20")]
        public abstract sbyte NumOutputStorico { get; set; }

        [AcceptNull()]
        public abstract string EstrazioniAccorpateIds { get; set; }

        public abstract sbyte AccorpaSoloDati { get; set; }

        [AcceptNull]
        public abstract int TemplateId { get; set; }

        [PropertyMap(nameof(TemplateId))]
        public abstract ReportTemplate Template { get; }

        [AcceptNull()]
        public abstract string CopyToPath { get; set; }

        [AcceptNull(), MaxLength(150)]
        public abstract string NomeFileMask { get; set; }

        public abstract int UtenteIdInserimento { get; set; }

        [PropertyMap(nameof(UtenteIdInserimento))]
        public abstract ReportUtente UtenteInserimento { get; }

        public abstract int UtenteIdAggiornamento { get; set; }

        [PropertyMap(nameof(UtenteIdAggiornamento))]
        public abstract ReportUtente UtenteAggiornamento { get; }

        [AutoInsertTimestamp]
        public abstract DateTime DataInserimento { get; }

        [AutoUpdateTimestamp]
        public abstract DateTime DataAggiornamento { get; }

    }
}

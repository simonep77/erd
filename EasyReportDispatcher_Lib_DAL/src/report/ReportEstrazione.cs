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
        [PrimaryKey]
        public abstract int Id { get; }

        [MaxLength(100)]
        public abstract string Nome { get; }

        public abstract sbyte Attivo { get; }

        [AcceptNull()]
        public abstract String Titolo { get; }
        [AcceptNull()]
        public abstract String Note { get; }

        public abstract Int16 ConnessioneId { get; }

        [PropertyMap(nameof(ConnessioneId))]
        public abstract ReportConnessione Connessione { get; }
        public abstract sbyte TipoFileId { get; }
        public abstract sbyte InvioMailAttivo { get; }

        public abstract string SqlText { get; }

        public abstract string SheetName { get; }

        public abstract string CronString { get; }

        public abstract DateTime DataInizio { get; }
        public abstract DateTime DataFine { get; }
        public abstract sbyte NumOutputStorico { get; }


        [AcceptNull()]
        public abstract string EstrazioniAccorpateIds { get; }

        [AcceptNull]
        public abstract int TemplateId { get; set; }

        [PropertyMap(nameof(TemplateId))]
        public abstract ReportTemplate Template { get; }

    }
}

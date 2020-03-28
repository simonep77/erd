using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bdo.Attributes;
using Bdo.Objects;

namespace EasyReportDispatcher_Lib_DAL.src.report
{
    [Table("report_estrazioni_output")]
    public abstract class ReportEstrazioneOutput : DataObject<ReportEstrazioneOutput>
    {
        [PrimaryKey, AutoIncrement]
        public abstract long Id { get; }

        public abstract int EstrazioneId { get; set; }

        [DefaultValue("1")]
        public abstract sbyte StatoId { get; set; }

        [AcceptNull()]
        public abstract string EstrazioneEsito { get; set; }

        public abstract DateTime DataOraInizio { get; set; }

        [AcceptNull()]
        public abstract DateTime DataOraFine { get; set; }

        public abstract sbyte TipoFileId { get; set; }

        [AcceptNull()]
        public abstract int DataLen { get; set; }

        [AcceptNull(), LoadOnAccess]
        public abstract byte[] DataBlob { get; set; }

        [AcceptNull()]
        public abstract string MailEsito { get; set; }

        [AcceptNull()]
        public abstract DateTime MailDataInvio { get; set; }

        [AutoInsertTimestamp()]
        public abstract DateTime DataInserimento { get; }

        [AutoUpdateTimestamp()]
        public abstract DateTime DataAggiornamento { get; }

    }
}

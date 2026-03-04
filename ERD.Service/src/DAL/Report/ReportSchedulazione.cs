using Business.Data.Objects.Core;
using Business.Data.Objects.Core.Attributes;

namespace ERD.Service.DAL
{
    [Table("report_piano_schedulazione")]
    public abstract class ReportSchedulazione : DataObject<ReportSchedulazione>
    {
        [PrimaryKey, AutoIncrement()]
        public abstract long Id { get; }

        public abstract int EstrazioneId { get; set; }
        
        [PropertyMap(nameof(EstrazioneId))]
        public abstract ReportEstrazione Estrazione { get; }

        public abstract DateTime DataEsecuzione { get; set; }

        public abstract sbyte StatoId { get; set; }

        [PropertyMap(nameof(StatoId))]
        public abstract ReportSchedulazioneStato Stato { get; }

        [AcceptNull]
        public abstract long OutputId { get; set; }
        
        [PropertyMap(nameof(OutputId))]
        public abstract ReportEstrazioneOutput Output { get; }


        [AutoInsertTimestamp]
        public abstract DateTime DataInserimento { get; }

        [AutoUpdateTimestamp]
        public abstract DateTime DataAggiornamento { get; }

    }
}

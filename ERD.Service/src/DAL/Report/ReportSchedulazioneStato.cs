using Business.Data.Objects.Core;
using Business.Data.Objects.Core.Attributes;

namespace ERD.Service.DAL
{
    [Table("report_piano_schedulazione_stati"), GlobalCache()]
    public abstract class ReportSchedulazioneStato : DataObject<ReportSchedulazioneStato>
    {
        [PrimaryKey]
        public abstract sbyte Id { get; }

        [MaxLength(100)]
        public abstract string Nome { get; }

    }
}

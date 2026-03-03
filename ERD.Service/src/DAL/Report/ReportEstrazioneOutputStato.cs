using Business.Data.Objects.Core;
using Business.Data.Objects.Core.Attributes;

namespace ERD.Service.DAL
{
    [Table("report_estrazioni_output_stati"), GlobalCache()]
    public abstract class ReportEstrazioneOutputStato : DataObject<ReportEstrazioneOutputStato>
    {
        [PrimaryKey]
        public abstract sbyte Id { get; }

        [MaxLength(100)]
        public abstract string Nome { get; }

    }
}

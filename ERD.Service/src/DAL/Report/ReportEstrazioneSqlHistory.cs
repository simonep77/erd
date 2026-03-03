using Business.Data.Objects.Core;
using Business.Data.Objects.Core.Attributes;

namespace ERD.Service.DAL
{
    [Table("report_estrazioni_sqlhistory")]
    public abstract class ReportEstrazioneSqlHistory : DataObject<ReportEstrazioneSqlHistory>
    {
        [PrimaryKey, AutoIncrement]
        public abstract long Id { get; }

        public abstract int EstrazioneId { get; set; }

        public abstract int UtenteId { get; set; }
        
        public abstract string SqlText { get; set; }
        
        [AutoInsertTimestamp]
        public abstract DateTime DataInserimento { get; }
    }
}

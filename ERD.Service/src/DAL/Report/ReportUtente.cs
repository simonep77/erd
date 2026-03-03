using Business.Data.Objects.Core;
using Business.Data.Objects.Core.Attributes;

namespace ERD.Service.DAL
{
    [Table("report_utenti")]
    public abstract class ReportUtente : DataObject<ReportUtente>
    {
        [PrimaryKey, AutoIncrement()]
        public abstract int Id { get; }

        [MaxLength(50)]
        public abstract string Username { get; set; }

        [MaxLength(100)]
        public abstract String Dominio { get; set; }

        [MaxLength(200)]
        public abstract String Nominativo { get; set; }

        [AcceptNull, MaxLength(200)]
        public abstract String Email { get; set; }

        [AutoInsertTimestamp]
        public abstract DateTime DataInserimento { get; }

    }
}

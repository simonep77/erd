using Business.Data.Objects.Core;
using Business.Data.Objects.Core.Attributes;

namespace ERD.Service.DAL
{
    [Table("report_connessioni")]
    public abstract class ReportConnessione : DataObject<ReportConnessione>
    {
        [PrimaryKey, AutoIncrement()]
        public abstract int Id { get; }

        [MaxLength(100)]
        public abstract string Nome { get; set; }

        [MaxLength(300)]
        public abstract String ConnectionString { get; set; }

        [MaxLength(150)]
        public abstract String BdoDbConnectioType { get; set; }

    }
}

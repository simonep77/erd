using Business.Data.Objects.Core;
using Business.Data.Objects.Core.Attributes;

namespace ERD.Service.DAL
{
    [Table("report_tipi_notifiche"), GlobalCache()]
    public abstract class ReportTipoNotifica : DataObject<ReportTipoNotifica>
    {
        [PrimaryKey]
        public abstract long Id { get; }

        [MaxLength(100)]
        public abstract string Nome { get; }


    }
}

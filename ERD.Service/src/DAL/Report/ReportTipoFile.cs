using Business.Data.Objects.Core;
using Business.Data.Objects.Core.Attributes;

namespace ERD.Service.DAL
{
    [Table("report_tipi_file"), GlobalCache()]
    public abstract class ReportTipoFile : DataObject<ReportTipoFile>
    {
        [PrimaryKey]
        public abstract sbyte Id { get; }

        [MaxLength(100)]
        public abstract string Nome { get; }

        [MaxLength(10)]
        public abstract String Estensione { get; }

    }
}

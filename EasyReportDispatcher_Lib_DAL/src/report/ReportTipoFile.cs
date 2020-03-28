using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bdo.Attributes;
using Bdo.Objects;

namespace EasyReportDispatcher_Lib_DAL.src.report
{
    [Table("report_tipi_file"), GlobalCache()]
    public abstract class ReportTipoFile : DataObject<ReportTipoFile>
    {
        [PrimaryKey]
        public abstract long Id { get; }

        [MaxLength(100)]
        public abstract string Nome { get; }

        [MaxLength(10)]
        public abstract String Estensione { get; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bdo.Attributes;
using Bdo.Objects;

namespace EasyReportDispatcher_Lib_DAL.src.report
{
    [Table("report_connessioni")]
    public abstract class ReportConnessione : DataObject<ReportConnessione>
    {
        [PrimaryKey]
        public abstract long Id { get; }

        [MaxLength(100)]
        public abstract string Nome { get; }

        [MaxLength(300)]
        public abstract String ConnectionString { get; }

        [MaxLength(150)]
        public abstract String BdoDbConnectioType { get; }

    }
}

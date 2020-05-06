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

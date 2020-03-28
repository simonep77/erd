using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bdo.Attributes;
using Bdo.Objects;

namespace EasyReportDispatcher_Lib_DAL.src.report
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

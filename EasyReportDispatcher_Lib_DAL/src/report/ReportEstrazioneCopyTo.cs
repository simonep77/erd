using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bdo.Attributes;
using Bdo.Objects;

namespace EasyReportDispatcher_Lib_DAL.src.report
{
    [Table("report_estrazioni_copyto")]
    public abstract class ReportEstrazioneCopyTo : DataObject<ReportEstrazioneCopyTo>
    {
        [PrimaryKey, AutoIncrement]
        public abstract long Id { get; }

        public abstract int EstrazioneId { get; set; }

        public abstract string Path { get; set; }

        [AcceptNull()]
        public abstract string User { get; set; }

        [AcceptNull()]
        public abstract string Pass { get; set; }

        [AcceptNull()]
        public abstract string Domain { get; set; }

      

    }
}

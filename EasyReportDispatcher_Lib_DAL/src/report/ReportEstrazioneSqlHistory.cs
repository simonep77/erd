using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bdo.Attributes;
using Bdo.Objects;

namespace EasyReportDispatcher_Lib_DAL.src.report
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

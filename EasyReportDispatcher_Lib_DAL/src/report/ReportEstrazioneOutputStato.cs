using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bdo.Attributes;
using Bdo.Objects;

namespace EasyReportDispatcher_Lib_DAL.src.report
{
    [Table("report_estrazioni_output_stati"), GlobalCache()]
    public abstract class ReportEstrazioneOutputStato : DataObject<ReportEstrazioneOutputStato>
    {
        [PrimaryKey]
        public abstract sbyte Id { get; }

        [MaxLength(100)]
        public abstract string Nome { get; }

    }
}

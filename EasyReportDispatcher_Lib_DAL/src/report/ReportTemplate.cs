using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bdo.Attributes;
using Bdo.Objects;

namespace EasyReportDispatcher_Lib_DAL.src.report
{
    [Table("report_templates")]
    public abstract class ReportTemplate : DataObject<ReportTemplate>
    {
        [PrimaryKey, AutoIncrement]
        public abstract int Id { get; }

        public abstract string Nome { get; set; }

        public abstract byte[] TemplateBlob { get; set; }

        [AcceptNull]
        public abstract string Note { get; set; }



    }
}

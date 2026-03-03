using Business.Data.Objects.Core;
using Business.Data.Objects.Core.Attributes;

namespace ERD.Service.DAL
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

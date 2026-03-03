using Business.Data.Objects.Core;
using Business.Data.Objects.Core.Attributes;

namespace ERD.Service.DAL
{
    [Table("report_smtp_configs")]
    public abstract class ReportSmtpConfig : DataObject<ReportSmtpConfig>
    {
        [PrimaryKey, AutoIncrement]
        public abstract int Id { get; }

        public abstract String Nome { get; set; }

        [MaxLength(255)]
        public abstract String Smtp { get; set; }

        [DefaultValue(@"25")]
        public abstract int Port { get; set; }

        public abstract SByte UseSSL { get; set; }

        public abstract SByte Auth { get; set; }

        public abstract String UserName { get; set; }

        public abstract String Password { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bdo.Attributes;
using Bdo.Objects;

namespace EasyReportDispatcher_Lib_DAL.src.report
{
    [Table("report_smtp_configs"), GlobalCache()]
    public abstract class ReportSmtpConfig : DataObject<ReportSmtpConfig>
    {
        [PrimaryKey]
        public abstract long Id { get; }

        public abstract String Nome { get;}

        [MaxLength(255)]
        public abstract String Smtp { get;  }

        public abstract int Port { get;  }

        public abstract SByte UseSSL { get;  }

        public abstract SByte Auth { get; }

        public abstract String UserName { get; }

        public abstract String Password { get; }

    }
}

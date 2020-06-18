using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bdo.Attributes;
using Bdo.Objects;

namespace EasyReportDispatcher_Lib_DAL.src.report
{
    [Table("report_utenti")]
    public abstract class ReportUtente : DataObject<ReportUtente>
    {
        public const string KEY_USER_DOM = @"KEY_USER_DOM";
        
        [PrimaryKey, AutoIncrement()]
        public abstract int Id { get; }

        [MaxLength(50), SearchKey(KEY_USER_DOM)]
        public abstract string Username { get; set; }

        [MaxLength(100), SearchKey(KEY_USER_DOM)]
        public abstract String Dominio { get; set; }

        [MaxLength(200)]
        public abstract String Nominativo { get; set; }

        [AcceptNull, MaxLength(200)]
        public abstract String Email { get; set; }

        [AutoInsertTimestamp]
        public abstract DateTime DataInserimento { get; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bdo.Attributes;
using Bdo.Objects;

namespace EasyReportDispatcher_Lib_DAL.src.report
{
    [Table("report_destinatari_email")]
    public abstract class ReportEstrazioneDestinatarioEmail : DataObject<ReportEstrazioneDestinatarioEmail>
    {
        [PrimaryKey, AutoIncrement]
        public abstract int Id { get; }

        public abstract int EstrazioneId { get; set; }

        [PropertyMap(nameof(EstrazioneId))]
        public abstract ReportEstrazione Estrazione { get; }

        public abstract int SmtpConfigId { get; set; }

        [PropertyMap(nameof(SmtpConfigId))]
        public abstract ReportSmtpConfig SmtpConfig { get; }

        public abstract sbyte Attivo { get; set; }

        [AcceptNull()]
        public abstract string MailFROM { get; set; }
        
        public abstract string MailTO { get; set; }

        [AcceptNull()]
        public abstract string MailCC { get; set; }

        [AcceptNull()]
        public abstract string MailBCC { get; set; }

        public abstract string MailSUBJ { get; set; }

        public abstract string MailBODY { get; set; }

        [AcceptNull()]
        public abstract string Password { get; set; }

    }
}

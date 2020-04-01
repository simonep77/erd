using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bdo.Attributes;
using Bdo.Objects;

namespace EasyReportDispatcher_Lib_DAL.src.report
{
    [Table("report_estrazioni_destinatari_email")]
    public abstract class ReportEstrazioneDestinatarioEmail : DataObject<ReportEstrazioneDestinatarioEmail>
    {
        [PrimaryKey]
        public abstract int Id { get; }

        public abstract int EstrazioneId { get; }

        [PropertyMap(nameof(EstrazioneId))]
        public abstract ReportEstrazione Estrazione { get; }

        public abstract int SmtpConfigId { get; }

        [PropertyMap(nameof(SmtpConfigId))]
        public abstract ReportSmtpConfig SmtpConfig { get; }

        public abstract sbyte Attivo { get; }

        [AcceptNull()]
        public abstract string MailFROM { get; }
        
        public abstract string MailTO { get; }

        [AcceptNull()]
        public abstract string MailCC { get; }

        [AcceptNull()]
        public abstract string MailBCC { get; }

        public abstract string MailSUBJ { get; }

        public abstract string MailBODY { get; }

        [AcceptNull()]
        public abstract string Password { get; }

        [AcceptNull()]
        public abstract int CopyToId { get; }
    }
}

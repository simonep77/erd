using Business.Data.Objects.Core;
using Business.Data.Objects.Core.Attributes;

namespace ERD.Service.DAL
{
    [Table("report_connessioni")]
    public abstract class ReportConnessione : DataObject<ReportConnessione>
    {
        [PrimaryKey, AutoIncrement()]
        public abstract int Id { get; }

        [MaxLength(100)]
        public abstract string Nome { get; set; }

        [MaxLength(300)]
        public abstract String ConnectionString { get; set; }

        [MaxLength(150)]
        public abstract String BdoDbConnectioType { get; set; }

    }

    public abstract class ReportConnessioneLista : DataList<ReportConnessioneLista, ReportConnessione>
    {
    }


    [Table("report_estrazioni")]
    public abstract class ReportEstrazione : DataObject<ReportEstrazione>
    {
        [PrimaryKey, AutoIncrement]
        public abstract int Id { get; }

        [MaxLength(100)]
        public abstract string Nome { get; set; }

        public abstract sbyte Attivo { get; set; }

        [AcceptNull()]
        public abstract String Titolo { get; set; }

        [AcceptNull()]
        public abstract string Gruppo { get; set; }

        [AcceptNull()]
        public abstract String Note { get; set; }

        public abstract int ConnessioneId { get; set; }

        [PropertyMap(nameof(ConnessioneId))]
        public abstract ReportConnessione Connessione { get; }
        public abstract sbyte TipoFileId { get; set; }

        [PropertyMap(nameof(TipoFileId))]
        public abstract ReportTipoFile TipoFile { get; }


        public abstract sbyte InvioMailAttivo { get; set; }

        public abstract string SqlText { get; set; }

        [AcceptNull()]
        public abstract string SheetName { get; set; }

        public abstract string CronString { get; set; }

        [DefaultValue("01/01/2001")]
        public abstract DateTime DataInizio { get; set; }

        [DefaultValue("31/12/9999")]
        public abstract DateTime DataFine { get; set; }

        [DefaultValue("20")]
        public abstract sbyte NumOutputStorico { get; set; }

        [AcceptNull()]
        public abstract string EstrazioniAccorpateIds { get; set; }

        public abstract sbyte AccorpaSoloDati { get; set; }

        [AcceptNull]
        public abstract int TemplateId { get; set; }

        [PropertyMap(nameof(TemplateId))]
        public abstract ReportTemplate Template { get; }

        [AcceptNull()]
        public abstract string CopyToPath { get; set; }

        [AcceptNull(), MaxLength(150), Trim()]
        public abstract string NomeFileMask { get; set; }

        public abstract int UtenteIdInserimento { get; set; }

        [PropertyMap(nameof(UtenteIdInserimento))]
        public abstract ReportUtente UtenteInserimento { get; }

        public abstract int UtenteIdAggiornamento { get; set; }

        [PropertyMap(nameof(UtenteIdAggiornamento))]
        public abstract ReportUtente UtenteAggiornamento { get; }

        [AutoInsertTimestamp]
        public abstract DateTime DataInserimento { get; }

        [AutoUpdateTimestamp]
        public abstract DateTime DataAggiornamento { get; }

    }


    public abstract class ReportEstrazioneLista : DataList<ReportEstrazioneLista, ReportEstrazione>
    {
    }


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


    public abstract class ReportEstrazioneDestinatarioEmailLista : DataList<ReportEstrazioneDestinatarioEmailLista, ReportEstrazioneDestinatarioEmail>
    {
    }


    [Table("report_estrazioni_output")]
    public abstract class ReportEstrazioneOutput : DataObject<ReportEstrazioneOutput>
    {
        [PrimaryKey, AutoIncrement]
        public abstract long Id { get; }

        public abstract int EstrazioneId { get; set; }

        [DefaultValue("1")]
        public abstract sbyte StatoId { get; set; }

        [PropertyMap(nameof(StatoId))]
        public abstract ReportEstrazioneOutputStato Stato { get; }

        [AcceptNull()]
        public abstract string EstrazioneEsito { get; set; }

        public abstract DateTime DataOraInizio { get; set; }

        [AcceptNull()]
        public abstract DateTime DataOraFine { get; set; }

        public abstract sbyte TipoFileId { get; set; }

        public abstract string NomeFile { get; set; }

        [AcceptNull()]
        public abstract int DataLen { get; set; }

        [AcceptNull(), LoadOnAccess]
        public abstract byte[] DataBlob { get; set; }

        [AcceptNull()]
        public abstract string MailEsito { get; set; }

        [AcceptNull()]
        public abstract DateTime MailDataInvio { get; set; }

        [AutoInsertTimestamp()]
        public abstract DateTime DataInserimento { get; }

        [AutoUpdateTimestamp()]
        public abstract DateTime DataAggiornamento { get; }

    }


    public abstract class ReportEstrazioneOutputLista : DataList<ReportEstrazioneOutputLista, ReportEstrazioneOutput>
    {
    }


    [Table("report_estrazioni_output_stati"), GlobalCache()]
    public abstract class ReportEstrazioneOutputStato : DataObject<ReportEstrazioneOutputStato>
    {
        [PrimaryKey]
        public abstract sbyte Id { get; }

        [MaxLength(100)]
        public abstract string Nome { get; }

    }



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


    public abstract class ReportEstrazioneSqlHistoryLista : DataList<ReportEstrazioneSqlHistoryLista, ReportEstrazioneSqlHistory>
    {
    }


    [Table("report_piano_schedulazione")]
    public abstract class ReportSchedulazione : DataObject<ReportSchedulazione>
    {
        [PrimaryKey, AutoIncrement()]
        public abstract long Id { get; }

        public abstract int EstrazioneId { get; set; }

        [PropertyMap(nameof(EstrazioneId))]
        public abstract ReportEstrazione Estrazione { get; }

        public abstract DateTime DataEsecuzione { get; set; }

        public abstract sbyte StatoId { get; set; }

        [PropertyMap(nameof(StatoId))]
        public abstract ReportSchedulazioneStato Stato { get; }

        [AcceptNull]
        public abstract long OutputId { get; set; }

        [PropertyMap(nameof(OutputId))]
        public abstract ReportEstrazioneOutput Output { get; }


        [AutoInsertTimestamp]
        public abstract DateTime DataInserimento { get; }

        [AutoUpdateTimestamp]
        public abstract DateTime DataAggiornamento { get; }

    }


    public abstract class ReportSchedulazioneLista : DataList<ReportSchedulazioneLista, ReportSchedulazione>
    {
    }


    [Table("report_piano_schedulazione_stati"), GlobalCache()]
    public abstract class ReportSchedulazioneStato : DataObject<ReportSchedulazioneStato>
    {
        [PrimaryKey]
        public abstract sbyte Id { get; }

        [MaxLength(100)]
        public abstract string Nome { get; }

    }


    public abstract class ReportSchedulazioneStatoLista : DataList<ReportSchedulazioneStatoLista, ReportSchedulazioneStato>
    {
    }


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


    public abstract class ReportSmtpConfigLista : DataList<ReportSmtpConfigLista, ReportSmtpConfig>
    {
    }


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


    public abstract class ReportTemplateLista : DataList<ReportTemplateLista, ReportTemplate>
    {
    }


    [Table("report_tipi_file"), GlobalCache()]
    public abstract class ReportTipoFile : DataObject<ReportTipoFile>
    {
        [PrimaryKey]
        public abstract sbyte Id { get; }

        [MaxLength(100)]
        public abstract string Nome { get; }

        [MaxLength(10)]
        public abstract String Estensione { get; }

    }


    public abstract class ReportTipoFileLista : DataList<ReportTipoFileLista, ReportTipoFile>
    {
    }


    [Table("report_tipi_notifiche"), GlobalCache()]
    public abstract class ReportTipoNotifica : DataObject<ReportTipoNotifica>
    {
        [PrimaryKey]
        public abstract long Id { get; }

        [MaxLength(100)]
        public abstract string Nome { get; }
    }


    [Table("report_utenti")]
    public abstract class ReportUtente : DataObject<ReportUtente>
    {
        [PrimaryKey, AutoIncrement()]
        public abstract int Id { get; }

        [MaxLength(50)]
        public abstract string Username { get; set; }

        [MaxLength(100)]
        public abstract String Dominio { get; set; }

        [MaxLength(200)]
        public abstract String Nominativo { get; set; }

        [AcceptNull, MaxLength(200)]
        public abstract String Email { get; set; }

        [AutoInsertTimestamp]
        public abstract DateTime DataInserimento { get; }

    }
}

using Business.Data.Objects.Core;
using Business.Data.Objects.Core.Common.Utils;
using ERD.Service.BIZ.Utils;
using Microsoft.Extensions.Configuration;

namespace ERD.Scheduler
{
    public class AppContextERD
    {
        private static LazyStore _LazyStore = new LazyStore();
        public static IntSvcScheduler Scheduler { get; set; }
        public static IConfiguration Conf { get; set; }

        public static int SCHEDULE_PLAN_DAYS => _LazyStore.Get(nameof(SCHEDULE_PLAN_DAYS), () => Conf["Scheduler:ExecutionPlanDays"] != null ? int.Parse(Conf["Scheduler:ExecutionPlanDays"]) : 7);
        public static int SCHEDULE_CHECK_SECONDS => _LazyStore.Get(nameof(SCHEDULE_CHECK_SECONDS), () => Conf["Scheduler:CheckEverySeconds"] != null ? int.Parse(Conf["Scheduler:CheckEverySeconds"]) : 120);
        public static TimeOnly SCHEDULE_REBUILD_TIME => _LazyStore.Get(nameof(SCHEDULE_REBUILD_TIME), () => Conf["Scheduler:RebuildTime"] != null ? TimeOnly.Parse(Conf["Scheduler:CheckEverySeconds"]) : new TimeOnly(0, 7));


        public static void WriteLog(string kind, string logMessage)
        {
            Console.WriteLine(string.Format($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {kind} - {logMessage}"));
        }

        public static BusinessSlot CreateSlot()
        {
            var cs = Conf["Database:ConnectionString"] ?? throw new Exception("Connection string non trovata in configurazione");
            var pv = Conf["Database:Provider"] ?? @"MYSQLDataBase";
            var bs = new BusinessSlot(pv, cs);
            bs.LiveTrackingEnabled = false;
            bs.ChangeTrackingEnabled = true;
            bs.DB.AutoCloseConnection = true;

            return bs;
        }


        /// <summary>
        /// Invia notifica errore via mail
        /// </summary>
        /// <param name="subj"></param>
        /// <param name="errore"></param>
        /// <param name="extra"></param>
        public static void NotificaMailErrore(string subj, string errore, string extra)
        {
            try
            {
                MailUT.SendMail(host: AppContextERD.Conf["SmtpNotifiche:Host"],
                                port: int.Parse(AppContextERD.Conf["SmtpNotifiche:Port"]),
                                useauth: bool.Parse(AppContextERD.Conf["SmtpNotifiche:UseAuthentication"]),
                                ssl: bool.Parse(AppContextERD.Conf["SmtpNotifiche:EnableSsl"]),
                                user: AppContextERD.Conf["SmtpNotifiche:Username"],
                                pass: AppContextERD.Conf["SmtpNotifiche:Password"],
                                from: AppContextERD.Conf["SmtpNotifiche:From"],
                                to: AppContextERD.Conf["SmtpNotifiche:To"],
                                cc: AppContextERD.Conf["SmtpNotifiche:Cc"],
                                subj: $"ERR - ERD Scheduler - {subj})",
                                body: $"Si è verificato il seguente errore:<br/>{errore}<br/><br/>{extra}",
                                files: null);
            }
            catch (Exception e)
            {
                AppContextERD.WriteLog("ERROR", $"Errore nell'invio mail di notifica errore: {e.Message}");
            }
        }

    }
}

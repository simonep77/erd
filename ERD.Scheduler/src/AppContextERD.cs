using Business.Data.Objects.Core;
using Microsoft.Extensions.Configuration;

namespace ERD.Scheduler
{
    public class AppContextERD
    {


        public static IntSvcScheduler Scheduler { get; set; }
        public static IConfiguration Conf { get; set; }

        public static int SCHEDULE_PLAN_DAYS => Conf["Scheduler:ExecutionPlanDays"] != null ? int.Parse(Conf["Scheduler:ExecutionPlanDays"]) : 7;
        public static int SCHEDULE_CHECK_SECONDS => Conf["Scheduler:CheckEverySeconds"] != null ? int.Parse(Conf["Scheduler:CheckEverySeconds"]) : 120;


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

    }
}

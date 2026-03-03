using System.Text;
using Business.Data.Objects.Core;

namespace ERD.Service.DAL.Query
{
    public class QueryReports
    {

        /// <summary>
        /// Calcola hash delle schedulazioni dei reports
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        public static string CalculateSchedulesHash(BusinessSlot slot)
        {
            var sql = new StringBuilder(@"SET group_concat_max_len = 1024 * 1024;");
            sql.AppendLine();
            sql.AppendFormat(@"SELECT IFNULL(SHA1(GROUP_CONCAT(e.{0} ORDER BY e.Id SEPARATOR ';')), '') ", nameof(ReportEstrazione.CronString), nameof(ReportEstrazione.Id));
            sql.AppendLine();
            sql.AppendFormat(@"FROM {0} e ", slot.DbPrefixGetTableName<ReportEstrazione>());
            sql.AppendLine();
            sql.AppendFormat(@"WHERE e.{0}=1 ", nameof(ReportEstrazione.Attivo));

            slot.DB.SQL = sql.ToString();

            return slot.DB.ExecScalar().ToString();

        }

    }
}

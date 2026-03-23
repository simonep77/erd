using Business.Data.Objects.Core;
using Business.Data.Objects.Core.Attributes;
using System.Data;

namespace ERD.Service.DAL
{
    public static class Query
    {
        /// <summary>
        /// Ritorna tutti i gruppi definiti
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        public static IEnumerable<string> GruppiReportGetAll(this BusinessSlot slot)
        {
            slot.DB.SQL = @"SELECT DISTINCT Gruppo FROM report_estrazioni WHERE Gruppo IS NOT NULL ORDER BY Gruppo";
            var tab = slot.DB.Select();
            return tab.Rows.Cast<DataRow>().Select(r => r["Gruppo"].ToString()!).ToList();  
        }

    }

}

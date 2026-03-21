using Business.Data.Objects.Common;
using ERD.Service.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ERD.Web.Pages
{
    public class Index : ErdPageBase
    {

        public ReportEstrazioneLista Lista {  get; set; }
        public DataPager Pager { get; set; }

        public void OnGet()
        {

            this.Lista = this.Slot.CreateList<ReportEstrazioneLista>(1, 20)
                .SearchAllObjects();
            this.Pager = this.Lista.Pager;
        }
    }
}

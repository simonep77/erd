using Business.Data.Objects.Common;
using ERD.Service.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ERD.Web.Pages
{
    public class Index : ErdPageBase
    {
        [BindProperty(SupportsGet = true)]
        public int p { get; set; } = 1;
        [BindProperty(SupportsGet = true)]
        public int o { get; set; } = 20;

        public ReportEstrazioneLista Lista {  get; set; }

        public DataPager Pager { get; set; }

        public void OnGet()
        {

            this.Lista = this.Slot.CreateList<ReportEstrazioneLista>(this.p, this.o)
                .SearchAllObjects();
            this.Pager = this.Lista.Pager;
        }
    }
}

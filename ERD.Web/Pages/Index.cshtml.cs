using Business.Data.Objects.Common;
using Business.Data.Objects.Common.Utils;
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
        [BindProperty(SupportsGet = true)]
        public string g { get; set; } = string.Empty;
        [BindProperty(SupportsGet = true)]
        public string n { get; set; } = string.Empty;
        [BindProperty(SupportsGet = true)]
        public string s { get; set; } = string.Empty;

        public ReportEstrazioneLista Lista {  get; set; }

        public DataPager Pager { get; set; }

        public IEnumerable<string> ListaGruppi => this.Slot.LazyStore.Get(nameof(ListaGruppi), () => this.Slot.GruppiReportGetAll());

        public void OnGet()
        {
            this.s = this.s?.Trim();
            this.g = this.g?.Trim();
            this.n = this.n?.Trim();

            this.Lista = this.Slot.CreateList<ReportEstrazioneLista>(this.p, this.o)
                .OrderByLinqDesc(x => x.Id).SearchByLinq(x => (g.IsNull() || g == "" || x.Gruppo == g) && (n.IsNull() || n == "" || x.Nome.Like($"%{n}%")) && (s.IsNull() || s == "" || (x.Attivo == 1 && x.CronString != "")));
            this.Pager = this.Lista.Pager;
        }
    }
}

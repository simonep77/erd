using Business.Data.Objects.Common;
using Business.Data.Objects.Common.Utils;
using DocumentFormat.OpenXml.Office2010.Excel;
using ERD.Service.BIZ;
using ERD.Service.DAL;
using Hfs.Client;
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

        public IEnumerable<ReportEstrazioneBIZ> Lista {  get; set; }

        public DataPager Pager { get; set; }

        public IEnumerable<string> ListaGruppi => this.Slot.LazyStore.Get(nameof(ListaGruppi), () => this.Slot.GruppiReportGetAll());

        public void OnGet()
        {
            this.s = this.s?.Trim();
            this.g = this.g?.Trim();
            this.n = this.n?.Trim();

            var l = this.Slot.CreateList<ReportEstrazioneLista>(this.p, this.o)
                .OrderByLinqDesc(x => x.Id)
                .SearchByLinq(x => (g.IsNull() || g == "" || x.Gruppo == g) && (n.IsNull() || n == "" || x.Nome.Like($"%{n}%")) && (s.IsNull() || s == "" || (x.Attivo == 1 && x.CronString != "")))
                .ToBizObjectPagedList<ReportEstrazioneBIZ>();
            this.Pager = l.Pager;
            this.Lista = l;
        }



        public ActionResult OnGetEsegui(int id)
        {
            var reBiz = this.Slot.BizNewWithLoadOrNewByPK<ReportEstrazioneBIZ>(id);

            return this.Partial("~/Pages/Report/_Partial_Esegui.cshtml", reBiz);
        }


        public ActionResult OnGetEseguiTable(int id)
        {
            var reBiz = this.Slot.BizNewWithLoadOrNewByPK<ReportEstrazioneBIZ>(id);
            var tab = reBiz.RunSQL();
            return this.Partial("~/Pages/Report/_Partial_Result_Table.cshtml", tab);
        }


        public ActionResult OnGetEseguiFile(int id, bool storico, bool invia, bool copia)
        {
            var reBiz = this.Slot.BizNewWithLoadOrNewByPK<ReportEstrazioneBIZ>(id);
            reBiz.Run(storico, invia, copia);
          
            return new ObjectResult(new
            {
                NomeFile = reBiz.LastResult.NomeFile,
                HistoryId = reBiz.LastResult.Id,
                MimeType = MimeHelper.GetMimeFromFilename(reBiz.LastResult.NomeFile),
                BlobFile = reBiz.LastResult.DataBlob,
                TipoFile = reBiz.LastResult.TipoFileId
            });
        }


    }
}

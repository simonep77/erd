using ERD.Service.BIZ;
using ERD.Service.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ERD.Web.Pages
{
    public class AddOrEdit : ErdPageBase
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public ReportEstrazioneBIZ ReportBiz => this.Slot.LazyStore.Get(nameof(ReportBiz), () => this.Slot.BizNewWithLoadOrNewByPK<ReportEstrazioneBIZ>(Id));

        // Liste per le tendine (popolate in OnGet, non servono in OnPost)
        public List<SelectListItem> Connessioni { get; set; } = new();
        public List<SelectListItem> TipiFile { get; set; } = new();
        public List<SelectListItem> Templates { get; set; } = new();

        public bool IsEdit => this.Id > 0;


        public void OnGet()
        {
            this.PopolaListe();
        }



        // ════════════════════════════════════════════════════════════════
        // ON POST  →  handler "Save", chiamato via AJAX jQuery
        // URL:  POST /ReportEstrazioni/Edit?handler=Save
        // Body: JSON  (Content-Type: application/json)
        // ════════════════════════════════════════════════════════════════
        public IActionResult OnPostSave([FromBody] InputModel input)
        {
            // Valida il model
            if (!TryValidateModel(input))
            {
                var errors = ModelState
                    .Where(kv => kv.Value?.Errors.Count > 0)
                    .ToDictionary(
                        kv => kv.Key.Replace("Input.", ""),
                        kv => kv.Value!.Errors.Select(e => e.ErrorMessage).First()
                    );

                return new JsonResult(new
                {
                    success = false,
                    message = "Correggi i campi evidenziati.",
                    errors
                });
            }

            try
            {
                // ── MODIFICA ──────────────────────────────────────
                this.ReportBiz.DataObj.Nome = input.Nome;
                this.ReportBiz.DataObj.Attivo = input.Attivo ? (sbyte)1 : (sbyte)0;
                this.ReportBiz.DataObj.Titolo = input.Titolo;
                this.ReportBiz.DataObj.Gruppo = input.Gruppo;
                this.ReportBiz.DataObj.Note = input.Note;
                this.ReportBiz.DataObj.ConnessioneId = input.ConnessioneId;
                this.ReportBiz.DataObj.TipoFileId = input.TipoFileId;
                this.ReportBiz.DataObj.TemplateId = input.TemplateId ?? 0;
                this.ReportBiz.DataObj.InvioMailAttivo = input.InvioMailAttivo ? (sbyte)1 : (sbyte)0;
                this.ReportBiz.DataObj.SqlText = input.SqlText;
                this.ReportBiz.DataObj.SheetName = input.SheetName;
                this.ReportBiz.DataObj.CronString = input.CronString;
                this.ReportBiz.DataObj.DataInizio = input.DataInizio;
                this.ReportBiz.DataObj.DataFine = input.DataFine;
                this.ReportBiz.DataObj.NumOutputStorico = input.NumOutputStorico;
                this.ReportBiz.DataObj.EstrazioniAccorpateIds = input.EstrazioniAccorpateIds;
                this.ReportBiz.DataObj.AccorpaSoloDati = input.AccorpaSoloDati ? (sbyte)1 : (sbyte)0;
                this.ReportBiz.DataObj.CopyToPath = input.CopyToPath;
                this.ReportBiz.DataObj.NomeFileMask = input.NomeFileMask;
                this.ReportBiz.DataObj.UtenteIdAggiornamento = UtenteCorrenteId();

                this.ReportBiz.Salva();

                return new JsonResult(new
                {
                    success = true,
                    message = IsEdit
                        ? "Estrazione aggiornata con successo."
                        : "Estrazione creata con successo.",
                    id = this.ReportBiz.DataObj.Id
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = "Errore: " + ex.Message });
            }
        }

        // ════════════════════════════════════════════════════════════════
        // Metodi privati di supporto
        // ════════════════════════════════════════════════════════════════

       

        private void PopolaListe()
        {

            Connessioni = new() { new SelectListItem("— seleziona —", "") };
            TipiFile = new() { new SelectListItem("— seleziona —", "") };
            Templates = new() { new SelectListItem("(nessuno)", "") };

            this.Connessioni.AddRange(this.Slot.CreateList<ReportConnessioneLista>().SearchAllObjects().Select(x => new SelectListItem(x.Nome, x.Id.ToString())));
            this.TipiFile.AddRange(this.Slot.CreateList<ReportTipoFileLista>().SearchAllObjects().Select(x => new SelectListItem(x.Nome, x.Id.ToString())));
            this.Templates.AddRange(this.Slot.CreateList<ReportTemplateLista>().SearchAllObjects().Select(x => new SelectListItem(x.Nome, x.Id.ToString())));
        }

        private int UtenteCorrenteId()
        {
            // return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            return 1; // placeholder
        }


        public ActionResult OnGetConnessione(int idconn)
        {
            var conn = this.Slot.LoadObjOrNewByPK<ReportConnessione>(idconn);
            return this.Partial("~/Pages/Report/_Partial_Connessione.cshtml", conn);

        }

        public JsonResult OnPostConnessioneSave([FromBody] ConnectionModel input)
        {
            var conn = this.Slot.LoadObjOrNewByPK<ReportConnessione>(input.Id);

            conn.Nome = input.Nome;
            conn.BdoDbConnectioType = input.DbProvider;
            conn.ConnectionString = input.ConnectionString;

            this.Slot.SaveObject(conn);

            input.Id = conn.Id;

            return new JsonResult(new
            {
                success = true,
                connessione = input
            });
        }

        public JsonResult OnPostConnessioneDelete(int idconn)
        {
            var conn = this.Slot.LoadObjOrNewByPK<ReportConnessione>(idconn);

            this.Slot.DeleteObject(conn);

            return new JsonResult(new
            {
                success = true,
            });
        }



        #region MODELS

        // ════════════════════════════════════════════════════════════════
        // INPUT MODEL  (bind dal form via AJAX)
        // ════════════════════════════════════════════════════════════════
        public class InputModel
        {
            public int Id { get; set; }

            // ── Anagrafica ────────────────────────────────────────────
            [Required(ErrorMessage = "Il nome è obbligatorio.")]
            [MaxLength(100, ErrorMessage = "Massimo 100 caratteri.")]
            [Display(Name = "Nome")]
            public string Nome { get; set; } = string.Empty;

            [Display(Name = "Attivo")]
            public bool Attivo { get; set; } = true;

            [Display(Name = "Titolo")]
            public string? Titolo { get; set; }

            [Display(Name = "Gruppo")]
            [Required(ErrorMessage = "Il gruppo è obbligatorio.")]
            public string? Gruppo { get; set; }

            [Display(Name = "Note")]
            public string? Note { get; set; }

            // ── Lookup ────────────────────────────────────────────────
            [Required(ErrorMessage = "Selezionare una connessione.")]
            [Display(Name = "Connessione")]
            [Range(1, 999)]
            public int ConnessioneId { get; set; }

            [Range(1, 999)]
            [Required(ErrorMessage = "Selezionare un tipo file.")]
            [Display(Name = "Tipo file")]
            public sbyte TipoFileId { get; set; }

            [Display(Name = "Template")]
            public int? TemplateId { get; set; }

            // ── Mail ──────────────────────────────────────────────────
            [Display(Name = "Invio mail attivo")]
            public bool InvioMailAttivo { get; set; }

            // ── SQL ───────────────────────────────────────────────────
            [Required(ErrorMessage = "Il testo SQL è obbligatorio.")]
            [Display(Name = "SQL")]
            public string SqlText { get; set; } = string.Empty;

            [Display(Name = "Nome foglio")]
            public string? SheetName { get; set; }

            // ── Schedulazione ─────────────────────────────────────────
            [Display(Name = "CRON")]
            public string CronString { get; set; } = string.Empty;

            [Required(ErrorMessage = "La data inizio è obbligatoria.")]
            [Display(Name = "Data inizio")]
            [DataType(DataType.Date)]
            public DateTime DataInizio { get; set; } = DateTime.Today;

            [Required(ErrorMessage = "La data fine è obbligatoria.")]
            [Display(Name = "Data fine")]
            [DataType(DataType.Date)]
            public DateTime DataFine { get; set; } = DateTime.Today.AddYears(10);

            // ── Output ────────────────────────────────────────────────
            [Range(1, 999)]
            [Display(Name = "Output in storico")]
            [Required]
            public sbyte NumOutputStorico { get; set; } = 20;

            [Display(Name = "IDs estrazioni da accorpare")]
            public string? EstrazioniAccorpateIds { get; set; }

            [Display(Name = "Accorpa solo dati")]
            public bool AccorpaSoloDati { get; set; }

            [Display(Name = "Copia su percorso")]
            public string? CopyToPath { get; set; }

            [MaxLength(150)]
            [Display(Name = "Maschera nome file")]
            public string? NomeFileMask { get; set; }
        }


        public class ConnectionModel
        {
            public int Id { get; set; }

            // ── Anagrafica ────────────────────────────────────────────
            [Required(ErrorMessage = "Il nome è obbligatorio.")]
            [MaxLength(100, ErrorMessage = "Massimo 100 caratteri.")]
            [Display(Name = "Nome")]
            public string Nome { get; set; } = string.Empty;

            [Required(ErrorMessage = "Il tipo di database è obbligatorio.")]
            [MaxLength(100, ErrorMessage = "Massimo 100 caratteri.")]
            [Display(Name = "DbProvider")]
            public string DbProvider { get; set; } = string.Empty;

            [Required(ErrorMessage = "La stringa di connessione è obbligatoria.")]
            [MaxLength(1000, ErrorMessage = "Massimo 1000 caratteri.")]
            [Display(Name = "ConnectionString")]
            public string ConnectionString { get; set; } = string.Empty;

        }


        #endregion



        }
}

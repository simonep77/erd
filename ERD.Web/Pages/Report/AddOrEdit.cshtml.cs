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
        public ReportEstrazioneBIZ ReportBiz { get; set; }

        [BindProperty()]
        public InputModel Input { get; set; } = new();

        // Liste per le tendine (popolate in OnGet, non servono in OnPost)
        public List<SelectListItem> Connessioni { get; set; } = new();
        public List<SelectListItem> TipiFile { get; set; } = new();
        public List<SelectListItem> Templates { get; set; } = new();

        public bool IsEdit => this.Id > 0;


        public void OnGet()
        {
            this.ReportBiz = this.Slot.BizNewWithLoadOrNewByPK<ReportEstrazioneBIZ>(Id);

            this.PopolaListe();

            if (this.IsEdit)
                this.CaricaPerModifica();
        }



        // ════════════════════════════════════════════════════════════════
        // ON POST  →  handler "Save", chiamato via AJAX jQuery
        // URL:  POST /ReportEstrazioni/Edit?handler=Save
        // Body: JSON  (Content-Type: application/json)
        // ════════════════════════════════════════════════════════════════
        public IActionResult OnPostSave([FromBody] InputModel input)
        {
            Input = input;
            ModelState.Clear();
            // Valida il model
            if (!TryValidateModel(Input, nameof(Input)))
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
                int savedId;
                this.ReportBiz = this.Slot.BizNewWithLoadOrNewByPK<ReportEstrazioneBIZ>(Id);

                // ── MODIFICA ──────────────────────────────────────
                this.ReportBiz.DataObj.Nome = Input.Nome;
                this.ReportBiz.DataObj.Attivo = Input.Attivo ? (sbyte)1 : (sbyte)0;
                this.ReportBiz.DataObj.Titolo = Input.Titolo;
                this.ReportBiz.DataObj.Gruppo = Input.Gruppo;
                this.ReportBiz.DataObj.Note = Input.Note;
                this.ReportBiz.DataObj.ConnessioneId = Input.ConnessioneId;
                this.ReportBiz.DataObj.TipoFileId = Input.TipoFileId;
                this.ReportBiz.DataObj.TemplateId = Input.TemplateId ?? 0;
                this.ReportBiz.DataObj.InvioMailAttivo = Input.InvioMailAttivo ? (sbyte)1 : (sbyte)0;
                this.ReportBiz.DataObj.SqlText = Input.SqlText;
                this.ReportBiz.DataObj.SheetName = Input.SheetName;
                this.ReportBiz.DataObj.CronString = Input.CronString;
                this.ReportBiz.DataObj.DataInizio = Input.DataInizio;
                this.ReportBiz.DataObj.DataFine = Input.DataFine;
                this.ReportBiz.DataObj.NumOutputStorico = Input.NumOutputStorico;
                this.ReportBiz.DataObj.EstrazioniAccorpateIds = Input.EstrazioniAccorpateIds;
                this.ReportBiz.DataObj.AccorpaSoloDati = Input.AccorpaSoloDati ? (sbyte)1 : (sbyte)0;
                this.ReportBiz.DataObj.CopyToPath = Input.CopyToPath;
                this.ReportBiz.DataObj.NomeFileMask = Input.NomeFileMask;
                this.ReportBiz.DataObj.UtenteIdAggiornamento = UtenteCorrenteId();



                this.ReportBiz.Save();
                savedId = this.ReportBiz.DataObj.Id;

                return new JsonResult(new
                {
                    success = true,
                    message = IsEdit
                        ? "Estrazione aggiornata con successo."
                        : "Estrazione creata con successo.",
                    id = savedId
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

        private void CaricaPerModifica()
        {
            this.Input = new InputModel
            {
                Id = this.ReportBiz.DataObj.Id,
                Nome = this.ReportBiz.DataObj.Nome,
                Attivo = this.ReportBiz.DataObj.Attivo == 1,
                Titolo = this.ReportBiz.DataObj.Titolo,
                Gruppo = this.ReportBiz.DataObj.Gruppo,
                Note = this.ReportBiz.DataObj.Note,
                ConnessioneId = this.ReportBiz.DataObj.ConnessioneId,
                TipoFileId = this.ReportBiz.DataObj.TipoFileId,
                TemplateId = this.ReportBiz.DataObj.TemplateId == 0 ? null : this.ReportBiz.DataObj.TemplateId,
                InvioMailAttivo = this.ReportBiz.DataObj.InvioMailAttivo == 1,
                SqlText = this.ReportBiz.DataObj.SqlText,
                SheetName = this.ReportBiz.DataObj.SheetName,
                CronString = this.ReportBiz.DataObj.CronString,
                DataInizio = this.ReportBiz.DataObj.DataInizio,
                DataFine = this.ReportBiz.DataObj.DataFine,
                NumOutputStorico = this.ReportBiz.DataObj.NumOutputStorico,
                EstrazioniAccorpateIds = this.ReportBiz.DataObj.EstrazioniAccorpateIds,
                AccorpaSoloDati = this.ReportBiz.DataObj.AccorpaSoloDati == 1,
                CopyToPath = this.ReportBiz.DataObj.CopyToPath,
                NomeFileMask = this.ReportBiz.DataObj.NomeFileMask,
            };
            Input.Id = this.Id; // placeholder
        }

        private void PopolaListe()
        {
            // Connessioni = ReportConnessione.GetAll()
            //     .Select(c => new SelectListItem(c.Nome, c.Id.ToString())).ToList();
            // TipiFile = ReportTipoFile.GetAll()
            //     .Select(t => new SelectListItem(t.Descrizione, t.Id.ToString())).ToList();
            // Templates = ReportTemplate.GetAll()
            //     .Select(t => new SelectListItem(t.Nome, t.Id.ToString())).ToList();

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






        #region MyRegion

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


        #endregion



    }
}

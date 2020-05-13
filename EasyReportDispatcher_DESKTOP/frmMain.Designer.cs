namespace EasyReportDispatcher_DESKTOP
{
    partial class frmMain
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsConnessione = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsNumEstrazioni = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.panMain = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panCenter = new System.Windows.Forms.Panel();
            this.lvEstrazioni = new System.Windows.Forms.ListView();
            this.colID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNome = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colConnessione = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAttivo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colInvioEmail = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAccorpamento = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTemplate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTemplateLocale = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ctxMenuEst = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsTitolo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsTemplateLocOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsTemplateLocElimina = new System.Windows.Forms.ToolStripMenuItem();
            this.tsTemplateLocSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsEstrazioneEsegui = new System.Windows.Forms.ToolStripMenuItem();
            this.panSearch = new System.Windows.Forms.Panel();
            this.lbFiltroNum = new System.Windows.Forms.Label();
            this.txtFiltro = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tsEstrazioneNew = new System.Windows.Forms.ToolStripMenuItem();
            this.tsEstrazioneClona = new System.Windows.Forms.ToolStripMenuItem();
            this.tsEstrazioneEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsEstrazioneDel = new System.Windows.Forms.ToolStripMenuItem();
            this.btnConnetti = new System.Windows.Forms.ToolStripButton();
            this.btnDisconnetti = new System.Windows.Forms.ToolStripButton();
            this.btnConfig = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panMain.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panCenter.SuspendLayout();
            this.ctxMenuEst.SuspendLayout();
            this.panSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsConnessione,
            this.tsNumEstrazioni});
            this.statusStrip1.Location = new System.Drawing.Point(0, 636);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1224, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsConnessione
            // 
            this.tsConnessione.AutoSize = false;
            this.tsConnessione.Name = "tsConnessione";
            this.tsConnessione.Size = new System.Drawing.Size(200, 17);
            this.tsConnessione.Text = "Non connesso";
            this.tsConnessione.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsNumEstrazioni
            // 
            this.tsNumEstrazioni.Name = "tsNumEstrazioni";
            this.tsNumEstrazioni.Size = new System.Drawing.Size(92, 17);
            this.tsNumEstrazioni.Text = "N.Estrazioni: ND";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnConnetti,
            this.btnDisconnetti,
            this.btnConfig});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1224, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // panMain
            // 
            this.panMain.Controls.Add(this.groupBox1);
            this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panMain.Location = new System.Drawing.Point(0, 25);
            this.panMain.Name = "panMain";
            this.panMain.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.panMain.Size = new System.Drawing.Size(1224, 611);
            this.panMain.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panCenter);
            this.groupBox1.Controls.Add(this.panSearch);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox1.Size = new System.Drawing.Size(1224, 601);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Elenco Estrazioni";
            // 
            // panCenter
            // 
            this.panCenter.Controls.Add(this.lvEstrazioni);
            this.panCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panCenter.Location = new System.Drawing.Point(10, 71);
            this.panCenter.Name = "panCenter";
            this.panCenter.Padding = new System.Windows.Forms.Padding(5);
            this.panCenter.Size = new System.Drawing.Size(1204, 520);
            this.panCenter.TabIndex = 2;
            // 
            // lvEstrazioni
            // 
            this.lvEstrazioni.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colID,
            this.colNome,
            this.colConnessione,
            this.colAttivo,
            this.colInvioEmail,
            this.colAccorpamento,
            this.colTemplate,
            this.colTemplateLocale});
            this.lvEstrazioni.ContextMenuStrip = this.ctxMenuEst;
            this.lvEstrazioni.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvEstrazioni.FullRowSelect = true;
            this.lvEstrazioni.GridLines = true;
            this.lvEstrazioni.HideSelection = false;
            this.lvEstrazioni.Location = new System.Drawing.Point(5, 5);
            this.lvEstrazioni.Margin = new System.Windows.Forms.Padding(20);
            this.lvEstrazioni.MultiSelect = false;
            this.lvEstrazioni.Name = "lvEstrazioni";
            this.lvEstrazioni.Scrollable = false;
            this.lvEstrazioni.ShowGroups = false;
            this.lvEstrazioni.Size = new System.Drawing.Size(1194, 510);
            this.lvEstrazioni.TabIndex = 0;
            this.lvEstrazioni.UseCompatibleStateImageBehavior = false;
            this.lvEstrazioni.View = System.Windows.Forms.View.Details;
            this.lvEstrazioni.SelectedIndexChanged += new System.EventHandler(this.lvEstrazioni_SelectedIndexChanged);
            this.lvEstrazioni.DoubleClick += new System.EventHandler(this.lvEstrazioni_DoubleClick);
            // 
            // colID
            // 
            this.colID.Text = "ID";
            // 
            // colNome
            // 
            this.colNome.Text = "Nome";
            this.colNome.Width = 250;
            // 
            // colConnessione
            // 
            this.colConnessione.Text = "Connessione";
            this.colConnessione.Width = 200;
            // 
            // colAttivo
            // 
            this.colAttivo.Text = "Attivo";
            // 
            // colInvioEmail
            // 
            this.colInvioEmail.Text = "Invio Email";
            this.colInvioEmail.Width = 120;
            // 
            // colAccorpamento
            // 
            this.colAccorpamento.Text = "Accorpamento";
            this.colAccorpamento.Width = 120;
            // 
            // colTemplate
            // 
            this.colTemplate.Text = "Template";
            this.colTemplate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colTemplate.Width = 100;
            // 
            // colTemplateLocale
            // 
            this.colTemplateLocale.Text = "Template Locale";
            this.colTemplateLocale.Width = 120;
            // 
            // ctxMenuEst
            // 
            this.ctxMenuEst.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ctxMenuEst.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsTitolo,
            this.toolStripSeparator3,
            this.tsEstrazioneNew,
            this.tsEstrazioneClona,
            this.tsEstrazioneEdit,
            this.tsEstrazioneDel,
            this.toolStripMenuItem1,
            this.toolStripMenuItem3,
            this.toolStripSeparator4,
            this.tsTemplateLocOpen,
            this.tsTemplateLocElimina,
            this.tsTemplateLocSave,
            this.toolStripMenuItem2,
            this.toolStripMenuItem4,
            this.toolStripSeparator5,
            this.tsEstrazioneEsegui});
            this.ctxMenuEst.Name = "ctxMenuEst";
            this.ctxMenuEst.Size = new System.Drawing.Size(245, 298);
            this.ctxMenuEst.Text = "ESTRAZIONI";
            // 
            // tsTitolo
            // 
            this.tsTitolo.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tsTitolo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsTitolo.Enabled = false;
            this.tsTitolo.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsTitolo.Name = "tsTitolo";
            this.tsTitolo.Size = new System.Drawing.Size(244, 24);
            this.tsTitolo.Text = "Gestione Estrazioni";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(241, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(241, 6);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.toolStripMenuItem3.Enabled = false;
            this.toolStripMenuItem3.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(244, 24);
            this.toolStripMenuItem3.Text = "Gestione Template";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(241, 6);
            // 
            // tsTemplateLocOpen
            // 
            this.tsTemplateLocOpen.Name = "tsTemplateLocOpen";
            this.tsTemplateLocOpen.Size = new System.Drawing.Size(244, 24);
            this.tsTemplateLocOpen.Text = "Apri Template (copia locale)";
            this.tsTemplateLocOpen.ToolTipText = "Scarica in locale il custom template per poterlo visualizzare e/o modifcare";
            this.tsTemplateLocOpen.Click += new System.EventHandler(this.btnOpenTemplate_Click);
            // 
            // tsTemplateLocElimina
            // 
            this.tsTemplateLocElimina.Name = "tsTemplateLocElimina";
            this.tsTemplateLocElimina.Size = new System.Drawing.Size(244, 24);
            this.tsTemplateLocElimina.Text = "EliminaTemplate Locale";
            this.tsTemplateLocElimina.ToolTipText = "Elimina la copia locale del template";
            this.tsTemplateLocElimina.Click += new System.EventHandler(this.btnELiminaTplLocale_Click);
            // 
            // tsTemplateLocSave
            // 
            this.tsTemplateLocSave.Name = "tsTemplateLocSave";
            this.tsTemplateLocSave.Size = new System.Drawing.Size(244, 24);
            this.tsTemplateLocSave.Text = "Salva Template Locale";
            this.tsTemplateLocSave.ToolTipText = "Salva la copia locale del template sul DB allineandolo";
            this.tsTemplateLocSave.Click += new System.EventHandler(this.btnSalvaTemplate_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(241, 6);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.toolStripMenuItem4.Enabled = false;
            this.toolStripMenuItem4.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(244, 24);
            this.toolStripMenuItem4.Text = "Gestione Operativa";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(241, 6);
            // 
            // tsEstrazioneEsegui
            // 
            this.tsEstrazioneEsegui.Name = "tsEstrazioneEsegui";
            this.tsEstrazioneEsegui.Size = new System.Drawing.Size(244, 24);
            this.tsEstrazioneEsegui.Text = "Esegui";
            this.tsEstrazioneEsegui.ToolTipText = "Esegue l\'estrazione";
            this.tsEstrazioneEsegui.Click += new System.EventHandler(this.btnEsegui_Click);
            // 
            // panSearch
            // 
            this.panSearch.Controls.Add(this.lbFiltroNum);
            this.panSearch.Controls.Add(this.txtFiltro);
            this.panSearch.Controls.Add(this.label1);
            this.panSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panSearch.Location = new System.Drawing.Point(10, 30);
            this.panSearch.Name = "panSearch";
            this.panSearch.Size = new System.Drawing.Size(1204, 41);
            this.panSearch.TabIndex = 1;
            // 
            // lbFiltroNum
            // 
            this.lbFiltroNum.AutoSize = true;
            this.lbFiltroNum.Location = new System.Drawing.Point(389, 9);
            this.lbFiltroNum.Name = "lbFiltroNum";
            this.lbFiltroNum.Size = new System.Drawing.Size(94, 19);
            this.lbFiltroNum.TabIndex = 2;
            this.lbFiltroNum.Text = "Visibili x su y";
            this.lbFiltroNum.Visible = false;
            // 
            // txtFiltro
            // 
            this.txtFiltro.Location = new System.Drawing.Point(69, 6);
            this.txtFiltro.Name = "txtFiltro";
            this.txtFiltro.Size = new System.Drawing.Size(301, 27);
            this.txtFiltro.TabIndex = 1;
            this.txtFiltro.TextChanged += new System.EventHandler(this.txtFiltro_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filtra";
            // 
            // tsEstrazioneNew
            // 
            this.tsEstrazioneNew.Image = global::EasyReportDispatcher_DESKTOP.Properties.Resources.add;
            this.tsEstrazioneNew.Name = "tsEstrazioneNew";
            this.tsEstrazioneNew.Size = new System.Drawing.Size(244, 24);
            this.tsEstrazioneNew.Text = "Nuova";
            this.tsEstrazioneNew.ToolTipText = "Inserisce una nuova estrazione";
            this.tsEstrazioneNew.Click += new System.EventHandler(this.btnAddEstrazione_Click);
            // 
            // tsEstrazioneClona
            // 
            this.tsEstrazioneClona.Image = global::EasyReportDispatcher_DESKTOP.Properties.Resources.table_multiple;
            this.tsEstrazioneClona.Name = "tsEstrazioneClona";
            this.tsEstrazioneClona.Size = new System.Drawing.Size(244, 24);
            this.tsEstrazioneClona.Text = "Clona";
            this.tsEstrazioneClona.ToolTipText = "Crea una nuova estrazione copiando il contenuto di una esistente";
            this.tsEstrazioneClona.Click += new System.EventHandler(this.btnClonaEstrazione_Click);
            // 
            // tsEstrazioneEdit
            // 
            this.tsEstrazioneEdit.Image = global::EasyReportDispatcher_DESKTOP.Properties.Resources.table_edit;
            this.tsEstrazioneEdit.Name = "tsEstrazioneEdit";
            this.tsEstrazioneEdit.Size = new System.Drawing.Size(244, 24);
            this.tsEstrazioneEdit.Text = "Modifica";
            this.tsEstrazioneEdit.ToolTipText = "Modifica un\'estrazione";
            this.tsEstrazioneEdit.Click += new System.EventHandler(this.btnEditEstrazione_Click);
            // 
            // tsEstrazioneDel
            // 
            this.tsEstrazioneDel.Image = global::EasyReportDispatcher_DESKTOP.Properties.Resources.delete;
            this.tsEstrazioneDel.Name = "tsEstrazioneDel";
            this.tsEstrazioneDel.Size = new System.Drawing.Size(244, 24);
            this.tsEstrazioneDel.Text = "Elimina";
            this.tsEstrazioneDel.ToolTipText = "Elimina logicamente un\'estrazione. Nessuna azione distruttiva.";
            this.tsEstrazioneDel.Click += new System.EventHandler(this.btnDelEstrazione_Click);
            // 
            // btnConnetti
            // 
            this.btnConnetti.Image = global::EasyReportDispatcher_DESKTOP.Properties.Resources.login;
            this.btnConnetti.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConnetti.Name = "btnConnetti";
            this.btnConnetti.Size = new System.Drawing.Size(73, 22);
            this.btnConnetti.Text = "Connetti";
            this.btnConnetti.Click += new System.EventHandler(this.btnConnetti_Click);
            // 
            // btnDisconnetti
            // 
            this.btnDisconnetti.Image = global::EasyReportDispatcher_DESKTOP.Properties.Resources.logout;
            this.btnDisconnetti.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDisconnetti.Name = "btnDisconnetti";
            this.btnDisconnetti.Size = new System.Drawing.Size(87, 22);
            this.btnDisconnetti.Text = "Disconnetti";
            this.btnDisconnetti.Click += new System.EventHandler(this.btnDisconnetti_Click);
            // 
            // btnConfig
            // 
            this.btnConfig.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnConfig.Image = global::EasyReportDispatcher_DESKTOP.Properties.Resources.wrench_orange;
            this.btnConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(108, 22);
            this.btnConfig.Text = "Configurazione";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1224, 658);
            this.Controls.Add(this.panMain);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMain";
            this.Text = "Easy Report Dispatcher - DESKTOP";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panMain.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panCenter.ResumeLayout(false);
            this.ctxMenuEst.ResumeLayout(false);
            this.panSearch.ResumeLayout(false);
            this.panSearch.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnConnetti;
        private System.Windows.Forms.ToolStripButton btnDisconnetti;
        private System.Windows.Forms.Panel panMain;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lvEstrazioni;
        private System.Windows.Forms.ColumnHeader colID;
        private System.Windows.Forms.ColumnHeader colNome;
        private System.Windows.Forms.ColumnHeader colConnessione;
        private System.Windows.Forms.ColumnHeader colAttivo;
        private System.Windows.Forms.ToolStripStatusLabel tsConnessione;
        private System.Windows.Forms.ToolStripStatusLabel tsNumEstrazioni;
        private System.Windows.Forms.ColumnHeader colTemplate;
        private System.Windows.Forms.ColumnHeader colTemplateLocale;
        private System.Windows.Forms.ColumnHeader colInvioEmail;
        private System.Windows.Forms.ColumnHeader colAccorpamento;
        private System.Windows.Forms.ContextMenuStrip ctxMenuEst;
        private System.Windows.Forms.ToolStripMenuItem tsEstrazioneNew;
        private System.Windows.Forms.ToolStripMenuItem tsEstrazioneClona;
        private System.Windows.Forms.ToolStripMenuItem tsEstrazioneEdit;
        private System.Windows.Forms.ToolStripMenuItem tsEstrazioneDel;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsTemplateLocOpen;
        private System.Windows.Forms.ToolStripMenuItem tsTemplateLocElimina;
        private System.Windows.Forms.ToolStripMenuItem tsTemplateLocSave;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem tsEstrazioneEsegui;
        private System.Windows.Forms.ToolStripMenuItem tsTitolo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.Panel panSearch;
        private System.Windows.Forms.TextBox txtFiltro;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbFiltroNum;
        private System.Windows.Forms.Panel panCenter;
        private System.Windows.Forms.ToolStripButton btnConfig;
    }
}


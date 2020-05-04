﻿namespace EasyReportDispatcher_DESKTOP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.preferenzeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsConnessione = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsNumEstrazioni = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnConnetti = new System.Windows.Forms.ToolStripButton();
            this.btnDisconnetti = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEsegui = new System.Windows.Forms.ToolStripButton();
            this.panMain = new System.Windows.Forms.Panel();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.btnAddEstrazione = new System.Windows.Forms.ToolStripButton();
            this.btnClonaEstrazione = new System.Windows.Forms.ToolStripButton();
            this.btnEditEstrazione = new System.Windows.Forms.ToolStripButton();
            this.btnDelEstrazione = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvEstrazioni = new System.Windows.Forms.ListView();
            this.colID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNome = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colConnessione = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAttivo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colInvioEmail = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTemplate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTemplateLocale = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnOpenTemplate = new System.Windows.Forms.ToolStripButton();
            this.btnELiminaTplLocale = new System.Windows.Forms.ToolStripButton();
            this.btnSalvaTemplate = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panMain.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preferenzeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1224, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // preferenzeToolStripMenuItem
            // 
            this.preferenzeToolStripMenuItem.Name = "preferenzeToolStripMenuItem";
            this.preferenzeToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.preferenzeToolStripMenuItem.Text = "Preferenze";
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
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnConnetti,
            this.btnDisconnetti,
            this.toolStripSeparator2,
            this.toolStripSeparator1,
            this.btnEsegui});
            this.toolStrip1.Location = new System.Drawing.Point(0, 611);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1224, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnConnetti
            // 
            this.btnConnetti.Image = ((System.Drawing.Image)(resources.GetObject("btnConnetti.Image")));
            this.btnConnetti.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConnetti.Name = "btnConnetti";
            this.btnConnetti.Size = new System.Drawing.Size(73, 22);
            this.btnConnetti.Text = "Connetti";
            this.btnConnetti.Click += new System.EventHandler(this.btnConnetti_Click);
            // 
            // btnDisconnetti
            // 
            this.btnDisconnetti.Image = ((System.Drawing.Image)(resources.GetObject("btnDisconnetti.Image")));
            this.btnDisconnetti.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDisconnetti.Name = "btnDisconnetti";
            this.btnDisconnetti.Size = new System.Drawing.Size(87, 22);
            this.btnDisconnetti.Text = "Disconnetti";
            this.btnDisconnetti.Click += new System.EventHandler(this.btnDisconnetti_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnEsegui
            // 
            this.btnEsegui.Image = ((System.Drawing.Image)(resources.GetObject("btnEsegui.Image")));
            this.btnEsegui.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEsegui.Name = "btnEsegui";
            this.btnEsegui.Size = new System.Drawing.Size(61, 22);
            this.btnEsegui.Text = "Esegui";
            this.btnEsegui.ToolTipText = "Esegui Estrazione";
            this.btnEsegui.Click += new System.EventHandler(this.btnEsegui_Click);
            // 
            // panMain
            // 
            this.panMain.Controls.Add(this.toolStrip3);
            this.panMain.Controls.Add(this.toolStrip2);
            this.panMain.Controls.Add(this.groupBox1);
            this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panMain.Location = new System.Drawing.Point(0, 24);
            this.panMain.Name = "panMain";
            this.panMain.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.panMain.Size = new System.Drawing.Size(1224, 587);
            this.panMain.TabIndex = 3;
            // 
            // toolStrip3
            // 
            this.toolStrip3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.btnOpenTemplate,
            this.btnELiminaTplLocale,
            this.btnSalvaTemplate});
            this.toolStrip3.Location = new System.Drawing.Point(0, 537);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(1224, 25);
            this.toolStrip3.TabIndex = 2;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(107, 22);
            this.toolStripLabel2.Text = "Gestione Template:";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.btnAddEstrazione,
            this.btnClonaEstrazione,
            this.btnEditEstrazione,
            this.btnDelEstrazione});
            this.toolStrip2.Location = new System.Drawing.Point(0, 562);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1224, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(109, 22);
            this.toolStripLabel1.Text = "Gestione Estrazioni:";
            // 
            // btnAddEstrazione
            // 
            this.btnAddEstrazione.Image = ((System.Drawing.Image)(resources.GetObject("btnAddEstrazione.Image")));
            this.btnAddEstrazione.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddEstrazione.Name = "btnAddEstrazione";
            this.btnAddEstrazione.Size = new System.Drawing.Size(118, 22);
            this.btnAddEstrazione.Text = "Nuova Estrazione";
            this.btnAddEstrazione.Click += new System.EventHandler(this.btnAddEstrazione_Click);
            // 
            // btnClonaEstrazione
            // 
            this.btnClonaEstrazione.Image = ((System.Drawing.Image)(resources.GetObject("btnClonaEstrazione.Image")));
            this.btnClonaEstrazione.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClonaEstrazione.Name = "btnClonaEstrazione";
            this.btnClonaEstrazione.Size = new System.Drawing.Size(114, 22);
            this.btnClonaEstrazione.Text = "Clona Estrazione";
            this.btnClonaEstrazione.Click += new System.EventHandler(this.btnClonaEstrazione_Click);
            // 
            // btnEditEstrazione
            // 
            this.btnEditEstrazione.Image = ((System.Drawing.Image)(resources.GetObject("btnEditEstrazione.Image")));
            this.btnEditEstrazione.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditEstrazione.Name = "btnEditEstrazione";
            this.btnEditEstrazione.Size = new System.Drawing.Size(130, 22);
            this.btnEditEstrazione.Text = "Modifica Estrazione";
            this.btnEditEstrazione.Click += new System.EventHandler(this.btnEditEstrazione_Click);
            // 
            // btnDelEstrazione
            // 
            this.btnDelEstrazione.Image = ((System.Drawing.Image)(resources.GetObject("btnDelEstrazione.Image")));
            this.btnDelEstrazione.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelEstrazione.Name = "btnDelEstrazione";
            this.btnDelEstrazione.Size = new System.Drawing.Size(122, 22);
            this.btnDelEstrazione.Text = "Elimina Estrazione";
            this.btnDelEstrazione.Click += new System.EventHandler(this.btnDelEstrazione_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvEstrazioni);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox1.Size = new System.Drawing.Size(1224, 357);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Elenco Estrazioni";
            // 
            // lvEstrazioni
            // 
            this.lvEstrazioni.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colID,
            this.colNome,
            this.colConnessione,
            this.colAttivo,
            this.colInvioEmail,
            this.colTemplate,
            this.colTemplateLocale});
            this.lvEstrazioni.Dock = System.Windows.Forms.DockStyle.Top;
            this.lvEstrazioni.FullRowSelect = true;
            this.lvEstrazioni.GridLines = true;
            this.lvEstrazioni.HideSelection = false;
            this.lvEstrazioni.Location = new System.Drawing.Point(10, 30);
            this.lvEstrazioni.Margin = new System.Windows.Forms.Padding(20);
            this.lvEstrazioni.MultiSelect = false;
            this.lvEstrazioni.Name = "lvEstrazioni";
            this.lvEstrazioni.Scrollable = false;
            this.lvEstrazioni.ShowGroups = false;
            this.lvEstrazioni.Size = new System.Drawing.Size(1204, 302);
            this.lvEstrazioni.TabIndex = 0;
            this.lvEstrazioni.UseCompatibleStateImageBehavior = false;
            this.lvEstrazioni.View = System.Windows.Forms.View.Details;
            this.lvEstrazioni.SelectedIndexChanged += new System.EventHandler(this.lvEstrazioni_SelectedIndexChanged);
            // 
            // colID
            // 
            this.colID.Text = "ID";
            // 
            // colNome
            // 
            this.colNome.Text = "Nome";
            this.colNome.Width = 150;
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
            // btnOpenTemplate
            // 
            this.btnOpenTemplate.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenTemplate.Image")));
            this.btnOpenTemplate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenTemplate.Name = "btnOpenTemplate";
            this.btnOpenTemplate.Size = new System.Drawing.Size(99, 22);
            this.btnOpenTemplate.Text = "Apri template";
            this.btnOpenTemplate.Click += new System.EventHandler(this.btnOpenTemplate_Click);
            // 
            // btnELiminaTplLocale
            // 
            this.btnELiminaTplLocale.Image = ((System.Drawing.Image)(resources.GetObject("btnELiminaTplLocale.Image")));
            this.btnELiminaTplLocale.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnELiminaTplLocale.Name = "btnELiminaTplLocale";
            this.btnELiminaTplLocale.Size = new System.Drawing.Size(154, 22);
            this.btnELiminaTplLocale.Text = "Elimina Template Locale";
            this.btnELiminaTplLocale.Click += new System.EventHandler(this.btnELiminaTplLocale_Click);
            // 
            // btnSalvaTemplate
            // 
            this.btnSalvaTemplate.Image = ((System.Drawing.Image)(resources.GetObject("btnSalvaTemplate.Image")));
            this.btnSalvaTemplate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalvaTemplate.Name = "btnSalvaTemplate";
            this.btnSalvaTemplate.Size = new System.Drawing.Size(142, 22);
            this.btnSalvaTemplate.Text = "Salva Template Locale";
            this.btnSalvaTemplate.Click += new System.EventHandler(this.btnSalvaTemplate_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1224, 658);
            this.Controls.Add(this.panMain);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMain";
            this.Text = "Easy Report Dispatcher - DESKTOP";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panMain.ResumeLayout(false);
            this.panMain.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem preferenzeToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnConnetti;
        private System.Windows.Forms.ToolStripButton btnDisconnetti;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
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
        private System.Windows.Forms.ToolStripButton btnEsegui;
        private System.Windows.Forms.ColumnHeader colTemplateLocale;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnAddEstrazione;
        private System.Windows.Forms.ToolStripButton btnEditEstrazione;
        private System.Windows.Forms.ColumnHeader colInvioEmail;
        private System.Windows.Forms.ToolStripButton btnClonaEstrazione;
        private System.Windows.Forms.ToolStripButton btnDelEstrazione;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton btnOpenTemplate;
        private System.Windows.Forms.ToolStripButton btnELiminaTplLocale;
        private System.Windows.Forms.ToolStripButton btnSalvaTemplate;
    }
}


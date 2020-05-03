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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.preferenzeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsConnessione = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsNumEstrazioni = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnConnetti = new System.Windows.Forms.ToolStripButton();
            this.btnDisconnetti = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnOpenTemplate = new System.Windows.Forms.ToolStripButton();
            this.panMain = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvEstrazioni = new System.Windows.Forms.ListView();
            this.colID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNome = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colConnessione = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAttivo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTemplate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnEsegui = new System.Windows.Forms.ToolStripButton();
            this.colTemplateLocale = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnELiminaTplLocale = new System.Windows.Forms.ToolStripButton();
            this.btnSalvaTemplate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panMain.SuspendLayout();
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
            this.btnOpenTemplate,
            this.btnELiminaTplLocale,
            this.btnSalvaTemplate,
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
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
            // panMain
            // 
            this.panMain.Controls.Add(this.groupBox1);
            this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panMain.Location = new System.Drawing.Point(0, 24);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(1224, 587);
            this.panMain.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvEstrazioni);
            this.groupBox1.Location = new System.Drawing.Point(72, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1068, 357);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Elenco Estrazioni";
            // 
            // lvEstrazioni
            // 
            this.lvEstrazioni.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvEstrazioni.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colID,
            this.colNome,
            this.colConnessione,
            this.colAttivo,
            this.colTemplate,
            this.colTemplateLocale});
            this.lvEstrazioni.FullRowSelect = true;
            this.lvEstrazioni.GridLines = true;
            this.lvEstrazioni.HideSelection = false;
            this.lvEstrazioni.Location = new System.Drawing.Point(23, 32);
            this.lvEstrazioni.Margin = new System.Windows.Forms.Padding(20);
            this.lvEstrazioni.MultiSelect = false;
            this.lvEstrazioni.Name = "lvEstrazioni";
            this.lvEstrazioni.Scrollable = false;
            this.lvEstrazioni.ShowGroups = false;
            this.lvEstrazioni.Size = new System.Drawing.Size(1022, 302);
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
            // colTemplate
            // 
            this.colTemplate.Text = "Template";
            this.colTemplate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colTemplate.Width = 100;
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
            // colTemplateLocale
            // 
            this.colTemplateLocale.Text = "Template Locale";
            this.colTemplateLocale.Width = 120;
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
        private System.Windows.Forms.ToolStripButton btnOpenTemplate;
        private System.Windows.Forms.ToolStripButton btnEsegui;
        private System.Windows.Forms.ColumnHeader colTemplateLocale;
        private System.Windows.Forms.ToolStripButton btnELiminaTplLocale;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnSalvaTemplate;
    }
}


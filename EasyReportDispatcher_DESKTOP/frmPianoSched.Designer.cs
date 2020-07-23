namespace EasyReportDispatcher_DESKTOP
{
    partial class frmPianoSched
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lvStorico = new System.Windows.Forms.ListView();
            this.colDataExec = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colGruppo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEstr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStato = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colElaps = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEsito = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTesto = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbExecCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panFiltri = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbStatoSched = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.dtpDataFine = new System.Windows.Forms.DateTimePicker();
            this.dtpDataInizio = new System.Windows.Forms.DateTimePicker();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panFiltri.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvStorico
            // 
            this.lvStorico.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvStorico.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDataExec,
            this.colGruppo,
            this.colEstr,
            this.colStato,
            this.colElaps,
            this.colEsito,
            this.colTesto});
            this.lvStorico.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvStorico.FullRowSelect = true;
            this.lvStorico.GridLines = true;
            this.lvStorico.HideSelection = false;
            this.lvStorico.Location = new System.Drawing.Point(5, 75);
            this.lvStorico.MultiSelect = false;
            this.lvStorico.Name = "lvStorico";
            this.lvStorico.Size = new System.Drawing.Size(1328, 529);
            this.lvStorico.TabIndex = 0;
            this.lvStorico.UseCompatibleStateImageBehavior = false;
            this.lvStorico.View = System.Windows.Forms.View.Details;
            this.lvStorico.SelectedIndexChanged += new System.EventHandler(this.lvStorico_SelectedIndexChanged);
            this.lvStorico.DoubleClick += new System.EventHandler(this.actOpenFile);
            // 
            // colDataExec
            // 
            this.colDataExec.Text = "Data Schedulazione";
            this.colDataExec.Width = 150;
            // 
            // colGruppo
            // 
            this.colGruppo.Text = "Gruppo";
            this.colGruppo.Width = 220;
            // 
            // colEstr
            // 
            this.colEstr.Text = "Estrazione";
            this.colEstr.Width = 350;
            // 
            // colStato
            // 
            this.colStato.Text = "Stato";
            this.colStato.Width = 180;
            // 
            // colElaps
            // 
            this.colElaps.Text = "Durata";
            this.colElaps.Width = 100;
            // 
            // colEsito
            // 
            this.colEsito.Text = "Esito";
            this.colEsito.Width = 130;
            // 
            // colTesto
            // 
            this.colTesto.Text = "Dettagli";
            this.colTesto.Width = 250;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbExecCount});
            this.statusStrip1.Location = new System.Drawing.Point(0, 609);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1338, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lbExecCount
            // 
            this.lbExecCount.Name = "lbExecCount";
            this.lbExecCount.Size = new System.Drawing.Size(74, 17);
            this.lbExecCount.Text = "Esecuzioni: 0";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lvStorico);
            this.panel1.Controls.Add(this.panFiltri);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(1338, 609);
            this.panel1.TabIndex = 2;
            // 
            // panFiltri
            // 
            this.panFiltri.Controls.Add(this.label3);
            this.panFiltri.Controls.Add(this.cmbStatoSched);
            this.panFiltri.Controls.Add(this.label2);
            this.panFiltri.Controls.Add(this.label1);
            this.panFiltri.Controls.Add(this.btnUpdate);
            this.panFiltri.Controls.Add(this.dtpDataFine);
            this.panFiltri.Controls.Add(this.dtpDataInizio);
            this.panFiltri.Dock = System.Windows.Forms.DockStyle.Top;
            this.panFiltri.Location = new System.Drawing.Point(5, 5);
            this.panFiltri.Name = "panFiltri";
            this.panFiltri.Size = new System.Drawing.Size(1328, 70);
            this.panFiltri.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(513, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 18);
            this.label3.TabIndex = 17;
            this.label3.Text = "Stato";
            // 
            // cmbStatoSched
            // 
            this.cmbStatoSched.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatoSched.FormattingEnabled = true;
            this.cmbStatoSched.Location = new System.Drawing.Point(559, 7);
            this.cmbStatoSched.Name = "cmbStatoSched";
            this.cmbStatoSched.Size = new System.Drawing.Size(213, 26);
            this.cmbStatoSched.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(262, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "a";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "Da";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Image = global::EasyReportDispatcher_DESKTOP.Properties.Resources.database_refresh;
            this.btnUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpdate.Location = new System.Drawing.Point(811, 7);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(104, 26);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "Aggiorna";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // dtpDataFine
            // 
            this.dtpDataFine.Location = new System.Drawing.Point(290, 7);
            this.dtpDataFine.Name = "dtpDataFine";
            this.dtpDataFine.Size = new System.Drawing.Size(200, 26);
            this.dtpDataFine.TabIndex = 1;
            // 
            // dtpDataInizio
            // 
            this.dtpDataInizio.Location = new System.Drawing.Point(47, 7);
            this.dtpDataInizio.Name = "dtpDataInizio";
            this.dtpDataInizio.Size = new System.Drawing.Size(200, 26);
            this.dtpDataInizio.TabIndex = 0;
            // 
            // frmPianoSched
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1338, 631);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmPianoSched";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmStorico";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panFiltri.ResumeLayout(false);
            this.panFiltri.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvStorico;
        private System.Windows.Forms.ColumnHeader colDataExec;
        private System.Windows.Forms.ColumnHeader colElaps;
        private System.Windows.Forms.ColumnHeader colEsito;
        private System.Windows.Forms.ColumnHeader colTesto;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lbExecCount;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ColumnHeader colStato;
        private System.Windows.Forms.Panel panFiltri;
        private System.Windows.Forms.DateTimePicker dtpDataFine;
        private System.Windows.Forms.DateTimePicker dtpDataInizio;
        private System.Windows.Forms.ColumnHeader colGruppo;
        private System.Windows.Forms.ColumnHeader colEstr;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbStatoSched;
    }
}
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
            this.colID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDataExec = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStato = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colElaps = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEsito = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTesto = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbExecCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panFiltri = new System.Windows.Forms.Panel();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.colGruppo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEstr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panFiltri.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvStorico
            // 
            this.lvStorico.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvStorico.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colID,
            this.colDataExec,
            this.colGruppo,
            this.colEstr,
            this.colStato,
            this.colElaps,
            this.colEsito,
            this.colTesto});
            this.lvStorico.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lvStorico.FullRowSelect = true;
            this.lvStorico.HideSelection = false;
            this.lvStorico.Location = new System.Drawing.Point(5, 136);
            this.lvStorico.MultiSelect = false;
            this.lvStorico.Name = "lvStorico";
            this.lvStorico.Size = new System.Drawing.Size(1127, 278);
            this.lvStorico.TabIndex = 0;
            this.lvStorico.UseCompatibleStateImageBehavior = false;
            this.lvStorico.View = System.Windows.Forms.View.Details;
            this.lvStorico.SelectedIndexChanged += new System.EventHandler(this.lvStorico_SelectedIndexChanged);
            this.lvStorico.DoubleClick += new System.EventHandler(this.actOpenFile);
            // 
            // colID
            // 
            this.colID.Text = "ID";
            this.colID.Width = 80;
            // 
            // colDataExec
            // 
            this.colDataExec.Text = "Data Esecuzione";
            this.colDataExec.Width = 150;
            // 
            // colStato
            // 
            this.colStato.Text = "Stato";
            this.colStato.Width = 140;
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 419);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1137, 22);
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
            this.panel1.Controls.Add(this.panFiltri);
            this.panel1.Controls.Add(this.lvStorico);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(1137, 419);
            this.panel1.TabIndex = 2;
            // 
            // panFiltri
            // 
            this.panFiltri.Controls.Add(this.dateTimePicker2);
            this.panFiltri.Controls.Add(this.dateTimePicker1);
            this.panFiltri.Dock = System.Windows.Forms.DockStyle.Top;
            this.panFiltri.Location = new System.Drawing.Point(5, 5);
            this.panFiltri.Name = "panFiltri";
            this.panFiltri.Size = new System.Drawing.Size(1127, 70);
            this.panFiltri.TabIndex = 1;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(299, 20);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 26);
            this.dateTimePicker2.TabIndex = 1;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(46, 20);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 26);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // colGruppo
            // 
            this.colGruppo.Width = 130;
            // 
            // colEstr
            // 
            this.colEstr.Text = "Estrazione";
            this.colEstr.Width = 200;
            // 
            // frmPianoSched
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1137, 441);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmPianoSched";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmStorico";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panFiltri.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvStorico;
        private System.Windows.Forms.ColumnHeader colID;
        private System.Windows.Forms.ColumnHeader colDataExec;
        private System.Windows.Forms.ColumnHeader colElaps;
        private System.Windows.Forms.ColumnHeader colEsito;
        private System.Windows.Forms.ColumnHeader colTesto;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lbExecCount;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ColumnHeader colStato;
        private System.Windows.Forms.Panel panFiltri;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ColumnHeader colGruppo;
        private System.Windows.Forms.ColumnHeader colEstr;
    }
}
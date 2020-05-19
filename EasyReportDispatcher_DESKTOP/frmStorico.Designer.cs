namespace EasyReportDispatcher_DESKTOP
{
    partial class frmStorico
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
            this.colElaps = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEsito = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNomeFIle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbExecCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsElimina = new System.Windows.Forms.ToolStripButton();
            this.btnEliminaTutti = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvStorico
            // 
            this.lvStorico.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvStorico.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colID,
            this.colDataExec,
            this.colElaps,
            this.colEsito,
            this.colNomeFIle});
            this.lvStorico.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvStorico.FullRowSelect = true;
            this.lvStorico.HideSelection = false;
            this.lvStorico.Location = new System.Drawing.Point(5, 5);
            this.lvStorico.MultiSelect = false;
            this.lvStorico.Name = "lvStorico";
            this.lvStorico.Size = new System.Drawing.Size(782, 384);
            this.lvStorico.TabIndex = 0;
            this.lvStorico.UseCompatibleStateImageBehavior = false;
            this.lvStorico.View = System.Windows.Forms.View.Details;
            this.lvStorico.SelectedIndexChanged += new System.EventHandler(this.lvStorico_SelectedIndexChanged);
            // 
            // colID
            // 
            this.colID.Text = "ID";
            this.colID.Width = 40;
            // 
            // colDataExec
            // 
            this.colDataExec.Text = "Data Esecuzione";
            this.colDataExec.Width = 150;
            // 
            // colElaps
            // 
            this.colElaps.Text = "Durata";
            this.colElaps.Width = 100;
            // 
            // colEsito
            // 
            this.colEsito.Text = "Esito";
            // 
            // colNomeFIle
            // 
            this.colNomeFIle.Text = "Nome File";
            this.colNomeFIle.Width = 250;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbExecCount});
            this.statusStrip1.Location = new System.Drawing.Point(0, 419);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(792, 22);
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
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(792, 394);
            this.panel1.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsElimina,
            this.btnEliminaTutti});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(792, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsElimina
            // 
            this.tsElimina.Image = global::EasyReportDispatcher_DESKTOP.Properties.Resources.delete;
            this.tsElimina.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsElimina.Name = "tsElimina";
            this.tsElimina.Size = new System.Drawing.Size(66, 22);
            this.tsElimina.Text = "Elimina";
            this.tsElimina.Click += new System.EventHandler(this.actDeleteOne);
            // 
            // btnEliminaTutti
            // 
            this.btnEliminaTutti.Image = global::EasyReportDispatcher_DESKTOP.Properties.Resources.cancel;
            this.btnEliminaTutti.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminaTutti.Name = "btnEliminaTutti";
            this.btnEliminaTutti.Size = new System.Drawing.Size(93, 22);
            this.btnEliminaTutti.Text = "Elimina Tutti";
            this.btnEliminaTutti.Click += new System.EventHandler(this.actDeleteAll);
            // 
            // frmStorico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 441);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmStorico";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmStorico";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvStorico;
        private System.Windows.Forms.ColumnHeader colID;
        private System.Windows.Forms.ColumnHeader colDataExec;
        private System.Windows.Forms.ColumnHeader colElaps;
        private System.Windows.Forms.ColumnHeader colEsito;
        private System.Windows.Forms.ColumnHeader colNomeFIle;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lbExecCount;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsElimina;
        private System.Windows.Forms.ToolStripButton btnEliminaTutti;
    }
}
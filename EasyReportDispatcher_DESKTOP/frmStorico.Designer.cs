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
            this.colNomeFIle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEsito = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.lvStorico.FullRowSelect = true;
            this.lvStorico.HideSelection = false;
            this.lvStorico.Location = new System.Drawing.Point(102, 43);
            this.lvStorico.Name = "lvStorico";
            this.lvStorico.Size = new System.Drawing.Size(528, 333);
            this.lvStorico.TabIndex = 0;
            this.lvStorico.UseCompatibleStateImageBehavior = false;
            this.lvStorico.View = System.Windows.Forms.View.Details;
            // 
            // colID
            // 
            this.colID.Text = "ID";
            // 
            // colDataExec
            // 
            this.colDataExec.Text = "Data Esecuzione";
            this.colDataExec.Width = 120;
            // 
            // colElaps
            // 
            this.colElaps.Text = "Durata";
            // 
            // colNomeFIle
            // 
            this.colNomeFIle.Text = "Nome File";
            this.colNomeFIle.Width = 150;
            // 
            // colEsito
            // 
            this.colEsito.Text = "Esito";
            // 
            // frmStorico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 441);
            this.Controls.Add(this.lvStorico);
            this.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmStorico";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmStorico";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvStorico;
        private System.Windows.Forms.ColumnHeader colID;
        private System.Windows.Forms.ColumnHeader colDataExec;
        private System.Windows.Forms.ColumnHeader colElaps;
        private System.Windows.Forms.ColumnHeader colEsito;
        private System.Windows.Forms.ColumnHeader colNomeFIle;
    }
}
﻿namespace EasyReportDispatcher_DESKTOP
{
    partial class frmConnessione
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.txtConnStr = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTipoDb = new System.Windows.Forms.ComboBox();
            this.btnSlava = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Descrizione";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(194, 52);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(286, 26);
            this.txtNome.TabIndex = 1;
            // 
            // txtConnStr
            // 
            this.txtConnStr.Location = new System.Drawing.Point(194, 84);
            this.txtConnStr.Multiline = true;
            this.txtConnStr.Name = "txtConnStr";
            this.txtConnStr.Size = new System.Drawing.Size(286, 120);
            this.txtConnStr.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Stringa di Connessione";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "Tipo Database";
            // 
            // cmbTipoDb
            // 
            this.cmbTipoDb.FormattingEnabled = true;
            this.cmbTipoDb.Location = new System.Drawing.Point(194, 20);
            this.cmbTipoDb.Name = "cmbTipoDb";
            this.cmbTipoDb.Size = new System.Drawing.Size(202, 26);
            this.cmbTipoDb.TabIndex = 5;
            // 
            // btnSlava
            // 
            this.btnSlava.Location = new System.Drawing.Point(262, 235);
            this.btnSlava.Name = "btnSlava";
            this.btnSlava.Size = new System.Drawing.Size(91, 29);
            this.btnSlava.TabIndex = 6;
            this.btnSlava.Text = "Salva";
            this.btnSlava.UseVisualStyleBackColor = true;
            this.btnSlava.Click += new System.EventHandler(this.btnSlava_Click);
            // 
            // frmConnessione
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 292);
            this.Controls.Add(this.btnSlava);
            this.Controls.Add(this.cmbTipoDb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtConnStr);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmConnessione";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Connessione";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.TextBox txtConnStr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbTipoDb;
        private System.Windows.Forms.Button btnSlava;
    }
}
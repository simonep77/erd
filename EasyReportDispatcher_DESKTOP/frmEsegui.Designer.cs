namespace EasyReportDispatcher_DESKTOP
{
    partial class frmEsegui
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
            this.chkSaveOutput = new System.Windows.Forms.CheckBox();
            this.btnEsegui = new System.Windows.Forms.Button();
            this.chkUsaTemplateLocale = new System.Windows.Forms.CheckBox();
            this.chbInvioEmail = new System.Windows.Forms.CheckBox();
            this.chbApriExcel = new System.Windows.Forms.CheckBox();
            this.chkCopyTo = new System.Windows.Forms.CheckBox();
            this.chbSoloQuery = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chkSaveOutput
            // 
            this.chkSaveOutput.AutoSize = true;
            this.chkSaveOutput.Location = new System.Drawing.Point(40, 41);
            this.chkSaveOutput.Margin = new System.Windows.Forms.Padding(4);
            this.chkSaveOutput.Name = "chkSaveOutput";
            this.chkSaveOutput.Size = new System.Drawing.Size(166, 23);
            this.chkSaveOutput.TabIndex = 0;
            this.chkSaveOutput.Text = "Salva Risultato su DB";
            this.chkSaveOutput.UseVisualStyleBackColor = true;
            // 
            // btnEsegui
            // 
            this.btnEsegui.Location = new System.Drawing.Point(113, 215);
            this.btnEsegui.Margin = new System.Windows.Forms.Padding(4);
            this.btnEsegui.Name = "btnEsegui";
            this.btnEsegui.Size = new System.Drawing.Size(100, 34);
            this.btnEsegui.TabIndex = 1;
            this.btnEsegui.Text = "Esegui";
            this.btnEsegui.UseVisualStyleBackColor = true;
            this.btnEsegui.Click += new System.EventHandler(this.btnEsegui_Click);
            // 
            // chkUsaTemplateLocale
            // 
            this.chkUsaTemplateLocale.AutoSize = true;
            this.chkUsaTemplateLocale.Location = new System.Drawing.Point(40, 72);
            this.chkUsaTemplateLocale.Margin = new System.Windows.Forms.Padding(4);
            this.chkUsaTemplateLocale.Name = "chkUsaTemplateLocale";
            this.chkUsaTemplateLocale.Size = new System.Drawing.Size(163, 23);
            this.chkUsaTemplateLocale.TabIndex = 2;
            this.chkUsaTemplateLocale.Text = "Usa Template Locale";
            this.chkUsaTemplateLocale.UseVisualStyleBackColor = true;
            // 
            // chbInvioEmail
            // 
            this.chbInvioEmail.AutoSize = true;
            this.chbInvioEmail.Location = new System.Drawing.Point(40, 133);
            this.chbInvioEmail.Margin = new System.Windows.Forms.Padding(4);
            this.chbInvioEmail.Name = "chbInvioEmail";
            this.chbInvioEmail.Size = new System.Drawing.Size(99, 23);
            this.chbInvioEmail.TabIndex = 4;
            this.chbInvioEmail.Text = "Invia Email";
            this.chbInvioEmail.UseVisualStyleBackColor = true;
            // 
            // chbApriExcel
            // 
            this.chbApriExcel.AutoSize = true;
            this.chbApriExcel.Location = new System.Drawing.Point(40, 164);
            this.chbApriExcel.Margin = new System.Windows.Forms.Padding(4);
            this.chbApriExcel.Name = "chbApriExcel";
            this.chbApriExcel.Size = new System.Drawing.Size(181, 23);
            this.chbApriExcel.TabIndex = 6;
            this.chbApriExcel.Text = "Scarica ed apri risultato";
            this.chbApriExcel.UseVisualStyleBackColor = true;
            // 
            // chkCopyTo
            // 
            this.chkCopyTo.AutoSize = true;
            this.chkCopyTo.Location = new System.Drawing.Point(40, 103);
            this.chkCopyTo.Margin = new System.Windows.Forms.Padding(4);
            this.chkCopyTo.Name = "chkCopyTo";
            this.chkCopyTo.Size = new System.Drawing.Size(173, 23);
            this.chkCopyTo.TabIndex = 8;
            this.chkCopyTo.Text = "Copia su path previsto";
            this.chkCopyTo.UseVisualStyleBackColor = true;
            // 
            // chbSoloQuery
            // 
            this.chbSoloQuery.AutoSize = true;
            this.chbSoloQuery.Location = new System.Drawing.Point(40, 11);
            this.chbSoloQuery.Margin = new System.Windows.Forms.Padding(4);
            this.chbSoloQuery.Name = "chbSoloQuery";
            this.chbSoloQuery.Size = new System.Drawing.Size(142, 23);
            this.chbSoloQuery.TabIndex = 10;
            this.chbSoloQuery.Text = "Esegui solo query";
            this.chbSoloQuery.UseVisualStyleBackColor = true;
            this.chbSoloQuery.CheckedChanged += new System.EventHandler(this.chbSoloQuery_CheckedChanged);
            // 
            // frmEsegui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 283);
            this.Controls.Add(this.chbSoloQuery);
            this.Controls.Add(this.chkCopyTo);
            this.Controls.Add(this.chbApriExcel);
            this.Controls.Add(this.chbInvioEmail);
            this.Controls.Add(this.chkUsaTemplateLocale);
            this.Controls.Add(this.btnEsegui);
            this.Controls.Add(this.chkSaveOutput);
            this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEsegui";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Esegui Estrazione";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkSaveOutput;
        private System.Windows.Forms.Button btnEsegui;
        private System.Windows.Forms.CheckBox chkUsaTemplateLocale;
        private System.Windows.Forms.CheckBox chbInvioEmail;
        private System.Windows.Forms.CheckBox chbApriExcel;
        private System.Windows.Forms.CheckBox chkCopyTo;
        private System.Windows.Forms.CheckBox chbSoloQuery;
    }
}
﻿namespace EasyReportDispatcher_DESKTOP
{
    partial class frmSelEstrazioni
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
            this.gbGenerali = new System.Windows.Forms.GroupBox();
            this.btnEmailDest = new System.Windows.Forms.Button();
            this.chbInvioEmail = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbTipoFile = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCronString = new System.Windows.Forms.TextBox();
            this.chkAttivo = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbExcel = new System.Windows.Forms.GroupBox();
            this.panExcelCustom = new System.Windows.Forms.Panel();
            this.btnEliminaTemplate = new System.Windows.Forms.Button();
            this.lbTemplatePath = new System.Windows.Forms.Label();
            this.txtNomeTemplate = new System.Windows.Forms.TextBox();
            this.btnScegliTemplate = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.panExcelBase = new System.Windows.Forms.Panel();
            this.txtExcelSheet = new System.Windows.Forms.TextBox();
            this.txtExcelTitolo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.rbTemplateCustom = new System.Windows.Forms.RadioButton();
            this.rbTemplateBase = new System.Windows.Forms.RadioButton();
            this.gbSql = new System.Windows.Forms.GroupBox();
            this.txtSQL = new ScintillaNET.Scintilla();
            this.btnSalva = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbConnessioni = new System.Windows.Forms.ComboBox();
            this.txtNumOutput = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtEstrazioniAcc = new System.Windows.Forms.TextBox();
            this.btnSelezionaEstrazioni = new System.Windows.Forms.Button();
            this.gbGenerali.SuspendLayout();
            this.gbExcel.SuspendLayout();
            this.panExcelCustom.SuspendLayout();
            this.panExcelBase.SuspendLayout();
            this.gbSql.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumOutput)).BeginInit();
            this.SuspendLayout();
            // 
            // gbGenerali
            // 
            this.gbGenerali.Controls.Add(this.btnSelezionaEstrazioni);
            this.gbGenerali.Controls.Add(this.label11);
            this.gbGenerali.Controls.Add(this.txtEstrazioniAcc);
            this.gbGenerali.Controls.Add(this.label10);
            this.gbGenerali.Controls.Add(this.txtNumOutput);
            this.gbGenerali.Controls.Add(this.cmbConnessioni);
            this.gbGenerali.Controls.Add(this.label9);
            this.gbGenerali.Controls.Add(this.btnEmailDest);
            this.gbGenerali.Controls.Add(this.chbInvioEmail);
            this.gbGenerali.Controls.Add(this.label7);
            this.gbGenerali.Controls.Add(this.cmbTipoFile);
            this.gbGenerali.Controls.Add(this.label5);
            this.gbGenerali.Controls.Add(this.txtCronString);
            this.gbGenerali.Controls.Add(this.chkAttivo);
            this.gbGenerali.Controls.Add(this.label3);
            this.gbGenerali.Controls.Add(this.txtNote);
            this.gbGenerali.Controls.Add(this.label2);
            this.gbGenerali.Controls.Add(this.lblID);
            this.gbGenerali.Controls.Add(this.txtNome);
            this.gbGenerali.Controls.Add(this.label1);
            this.gbGenerali.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbGenerali.Location = new System.Drawing.Point(20, 20);
            this.gbGenerali.Margin = new System.Windows.Forms.Padding(3, 3, 3, 20);
            this.gbGenerali.Name = "gbGenerali";
            this.gbGenerali.Size = new System.Drawing.Size(1027, 201);
            this.gbGenerali.TabIndex = 0;
            this.gbGenerali.TabStop = false;
            this.gbGenerali.Text = "Generali";
            // 
            // btnEmailDest
            // 
            this.btnEmailDest.Location = new System.Drawing.Point(833, 107);
            this.btnEmailDest.Name = "btnEmailDest";
            this.btnEmailDest.Size = new System.Drawing.Size(87, 23);
            this.btnEmailDest.TabIndex = 13;
            this.btnEmailDest.Text = "Destinatari";
            this.btnEmailDest.UseVisualStyleBackColor = true;
            // 
            // chbInvioEmail
            // 
            this.chbInvioEmail.Location = new System.Drawing.Point(426, 105);
            this.chbInvioEmail.Name = "chbInvioEmail";
            this.chbInvioEmail.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chbInvioEmail.Size = new System.Drawing.Size(215, 22);
            this.chbInvioEmail.TabIndex = 11;
            this.chbInvioEmail.Text = "Invio Email";
            this.chbInvioEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chbInvioEmail.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(425, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 18);
            this.label7.TabIndex = 10;
            this.label7.Text = "Tipo Estrazione";
            // 
            // cmbTipoFile
            // 
            this.cmbTipoFile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoFile.FormattingEnabled = true;
            this.cmbTipoFile.Location = new System.Drawing.Point(627, 74);
            this.cmbTipoFile.Name = "cmbTipoFile";
            this.cmbTipoFile.Size = new System.Drawing.Size(201, 26);
            this.cmbTipoFile.TabIndex = 9;
            this.cmbTipoFile.SelectedIndexChanged += new System.EventHandler(this.cmbTipoFile_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(425, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(185, 18);
            this.label5.TabIndex = 8;
            this.label5.Text = "Schedulazione (CRON string)";
            // 
            // txtCronString
            // 
            this.txtCronString.Location = new System.Drawing.Point(626, 41);
            this.txtCronString.Name = "txtCronString";
            this.txtCronString.Size = new System.Drawing.Size(201, 26);
            this.txtCronString.TabIndex = 7;
            // 
            // chkAttivo
            // 
            this.chkAttivo.Location = new System.Drawing.Point(426, 14);
            this.chkAttivo.Name = "chkAttivo";
            this.chkAttivo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkAttivo.Size = new System.Drawing.Size(215, 22);
            this.chkAttivo.TabIndex = 6;
            this.chkAttivo.Text = "Attivo (schedulazione)";
            this.chkAttivo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAttivo.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "Note";
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(151, 106);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(201, 57);
            this.txtNote.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "Nome";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(148, 13);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(49, 18);
            this.lblID.TabIndex = 2;
            this.lblID.Text = "<new>";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(151, 74);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(201, 26);
            this.txtNome.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID";
            // 
            // gbExcel
            // 
            this.gbExcel.Controls.Add(this.panExcelCustom);
            this.gbExcel.Controls.Add(this.panExcelBase);
            this.gbExcel.Controls.Add(this.rbTemplateCustom);
            this.gbExcel.Controls.Add(this.rbTemplateBase);
            this.gbExcel.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbExcel.Location = new System.Drawing.Point(20, 221);
            this.gbExcel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 20);
            this.gbExcel.Name = "gbExcel";
            this.gbExcel.Size = new System.Drawing.Size(1027, 133);
            this.gbExcel.TabIndex = 6;
            this.gbExcel.TabStop = false;
            this.gbExcel.Text = "Specifiche Excel";
            // 
            // panExcelCustom
            // 
            this.panExcelCustom.Controls.Add(this.btnEliminaTemplate);
            this.panExcelCustom.Controls.Add(this.lbTemplatePath);
            this.panExcelCustom.Controls.Add(this.txtNomeTemplate);
            this.panExcelCustom.Controls.Add(this.btnScegliTemplate);
            this.panExcelCustom.Controls.Add(this.label8);
            this.panExcelCustom.Location = new System.Drawing.Point(450, 44);
            this.panExcelCustom.Name = "panExcelCustom";
            this.panExcelCustom.Size = new System.Drawing.Size(567, 83);
            this.panExcelCustom.TabIndex = 11;
            // 
            // btnEliminaTemplate
            // 
            this.btnEliminaTemplate.Location = new System.Drawing.Point(431, 9);
            this.btnEliminaTemplate.Name = "btnEliminaTemplate";
            this.btnEliminaTemplate.Size = new System.Drawing.Size(29, 23);
            this.btnEliminaTemplate.TabIndex = 15;
            this.btnEliminaTemplate.Text = "X";
            this.btnEliminaTemplate.UseVisualStyleBackColor = true;
            this.btnEliminaTemplate.Click += new System.EventHandler(this.btnEliminaTemplate_Click);
            // 
            // lbTemplatePath
            // 
            this.lbTemplatePath.AutoSize = true;
            this.lbTemplatePath.Location = new System.Drawing.Point(177, 45);
            this.lbTemplatePath.Name = "lbTemplatePath";
            this.lbTemplatePath.Size = new System.Drawing.Size(75, 18);
            this.lbTemplatePath.TabIndex = 14;
            this.lbTemplatePath.Text = "nessun file";
            // 
            // txtNomeTemplate
            // 
            this.txtNomeTemplate.Location = new System.Drawing.Point(180, 8);
            this.txtNomeTemplate.Name = "txtNomeTemplate";
            this.txtNomeTemplate.Size = new System.Drawing.Size(201, 26);
            this.txtNomeTemplate.TabIndex = 13;
            this.txtNomeTemplate.Text = "<Nuovo>";
            // 
            // btnScegliTemplate
            // 
            this.btnScegliTemplate.Location = new System.Drawing.Point(396, 9);
            this.btnScegliTemplate.Name = "btnScegliTemplate";
            this.btnScegliTemplate.Size = new System.Drawing.Size(29, 23);
            this.btnScegliTemplate.TabIndex = 12;
            this.btnScegliTemplate.Text = "...";
            this.btnScegliTemplate.UseVisualStyleBackColor = true;
            this.btnScegliTemplate.Click += new System.EventHandler(this.btnCloneTemplate_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 18);
            this.label8.TabIndex = 11;
            this.label8.Text = "Nome Template";
            // 
            // panExcelBase
            // 
            this.panExcelBase.Controls.Add(this.txtExcelSheet);
            this.panExcelBase.Controls.Add(this.txtExcelTitolo);
            this.panExcelBase.Controls.Add(this.label4);
            this.panExcelBase.Controls.Add(this.label6);
            this.panExcelBase.Location = new System.Drawing.Point(32, 44);
            this.panExcelBase.Name = "panExcelBase";
            this.panExcelBase.Size = new System.Drawing.Size(369, 83);
            this.panExcelBase.TabIndex = 10;
            // 
            // txtExcelSheet
            // 
            this.txtExcelSheet.Location = new System.Drawing.Point(117, 45);
            this.txtExcelSheet.Name = "txtExcelSheet";
            this.txtExcelSheet.Size = new System.Drawing.Size(201, 26);
            this.txtExcelSheet.TabIndex = 6;
            // 
            // txtExcelTitolo
            // 
            this.txtExcelTitolo.Location = new System.Drawing.Point(117, 13);
            this.txtExcelTitolo.Name = "txtExcelTitolo";
            this.txtExcelTitolo.Size = new System.Drawing.Size(201, 26);
            this.txtExcelTitolo.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 18);
            this.label4.TabIndex = 5;
            this.label4.Text = "Titolo";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 18);
            this.label6.TabIndex = 7;
            this.label6.Text = "Nome Foglio";
            // 
            // rbTemplateCustom
            // 
            this.rbTemplateCustom.AutoSize = true;
            this.rbTemplateCustom.Location = new System.Drawing.Point(630, 23);
            this.rbTemplateCustom.Name = "rbTemplateCustom";
            this.rbTemplateCustom.Size = new System.Drawing.Size(156, 22);
            this.rbTemplateCustom.TabIndex = 9;
            this.rbTemplateCustom.TabStop = true;
            this.rbTemplateCustom.Text = "Usa template custom";
            this.rbTemplateCustom.UseVisualStyleBackColor = true;
            this.rbTemplateCustom.CheckedChanged += new System.EventHandler(this.rbTemplateCustom_CheckedChanged);
            // 
            // rbTemplateBase
            // 
            this.rbTemplateBase.AutoSize = true;
            this.rbTemplateBase.Location = new System.Drawing.Point(151, 23);
            this.rbTemplateBase.Name = "rbTemplateBase";
            this.rbTemplateBase.Size = new System.Drawing.Size(209, 22);
            this.rbTemplateBase.TabIndex = 8;
            this.rbTemplateBase.TabStop = true;
            this.rbTemplateBase.Text = "Usa template base (tabellare)";
            this.rbTemplateBase.UseVisualStyleBackColor = true;
            this.rbTemplateBase.CheckedChanged += new System.EventHandler(this.rbTemplateBase_CheckedChanged);
            // 
            // gbSql
            // 
            this.gbSql.Controls.Add(this.txtSQL);
            this.gbSql.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbSql.Location = new System.Drawing.Point(20, 354);
            this.gbSql.Name = "gbSql";
            this.gbSql.Padding = new System.Windows.Forms.Padding(10);
            this.gbSql.Size = new System.Drawing.Size(1027, 213);
            this.gbSql.TabIndex = 7;
            this.gbSql.TabStop = false;
            this.gbSql.Text = "SQL";
            // 
            // txtSQL
            // 
            this.txtSQL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSQL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSQL.Lexer = ScintillaNET.Lexer.Sql;
            this.txtSQL.Location = new System.Drawing.Point(10, 29);
            this.txtSQL.Name = "txtSQL";
            this.txtSQL.Size = new System.Drawing.Size(1007, 174);
            this.txtSQL.TabIndex = 0;
            // 
            // btnSalva
            // 
            this.btnSalva.Location = new System.Drawing.Point(413, 566);
            this.btnSalva.Name = "btnSalva";
            this.btnSalva.Size = new System.Drawing.Size(93, 34);
            this.btnSalva.TabIndex = 14;
            this.btnSalva.Text = "Salva";
            this.btnSalva.UseVisualStyleBackColor = true;
            this.btnSalva.Click += new System.EventHandler(this.btnSalva_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(56, 45);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 18);
            this.label9.TabIndex = 14;
            this.label9.Text = "Connessione";
            // 
            // cmbConnessioni
            // 
            this.cmbConnessioni.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConnessioni.FormattingEnabled = true;
            this.cmbConnessioni.Location = new System.Drawing.Point(151, 42);
            this.cmbConnessioni.Name = "cmbConnessioni";
            this.cmbConnessioni.Size = new System.Drawing.Size(201, 26);
            this.cmbConnessioni.TabIndex = 15;
            // 
            // txtNumOutput
            // 
            this.txtNumOutput.Location = new System.Drawing.Point(626, 133);
            this.txtNumOutput.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.txtNumOutput.Name = "txtNumOutput";
            this.txtNumOutput.Size = new System.Drawing.Size(72, 26);
            this.txtNumOutput.TabIndex = 16;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(425, 137);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 18);
            this.label10.TabIndex = 17;
            this.label10.Text = "Num. Output";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(425, 169);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(133, 18);
            this.label11.TabIndex = 19;
            this.label11.Text = "Estrazioni Accorpate";
            // 
            // txtEstrazioniAcc
            // 
            this.txtEstrazioniAcc.Location = new System.Drawing.Point(626, 165);
            this.txtEstrazioniAcc.Name = "txtEstrazioniAcc";
            this.txtEstrazioniAcc.ReadOnly = true;
            this.txtEstrazioniAcc.Size = new System.Drawing.Size(201, 26);
            this.txtEstrazioniAcc.TabIndex = 18;
            // 
            // btnSelezionaEstrazioni
            // 
            this.btnSelezionaEstrazioni.Location = new System.Drawing.Point(833, 165);
            this.btnSelezionaEstrazioni.Name = "btnSelezionaEstrazioni";
            this.btnSelezionaEstrazioni.Size = new System.Drawing.Size(87, 23);
            this.btnSelezionaEstrazioni.TabIndex = 20;
            this.btnSelezionaEstrazioni.Text = "Seleziona";
            this.btnSelezionaEstrazioni.UseVisualStyleBackColor = true;
            // 
            // frmEstrazione
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 623);
            this.Controls.Add(this.btnSalva);
            this.Controls.Add(this.gbSql);
            this.Controls.Add(this.gbExcel);
            this.Controls.Add(this.gbGenerali);
            this.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmEstrazione";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Estrazione";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEstrazione_FormClosing);
            this.Load += new System.EventHandler(this.frmEstrazione_Load);
            this.gbGenerali.ResumeLayout(false);
            this.gbGenerali.PerformLayout();
            this.gbExcel.ResumeLayout(false);
            this.gbExcel.PerformLayout();
            this.panExcelCustom.ResumeLayout(false);
            this.panExcelCustom.PerformLayout();
            this.panExcelBase.ResumeLayout(false);
            this.panExcelBase.PerformLayout();
            this.gbSql.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtNumOutput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbGenerali;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCronString;
        private System.Windows.Forms.CheckBox chkAttivo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbExcel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtExcelSheet;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtExcelTitolo;
        private System.Windows.Forms.CheckBox chbInvioEmail;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbTipoFile;
        private System.Windows.Forms.GroupBox gbSql;
        private ScintillaNET.Scintilla txtSQL;
        private System.Windows.Forms.RadioButton rbTemplateCustom;
        private System.Windows.Forms.RadioButton rbTemplateBase;
        private System.Windows.Forms.Panel panExcelBase;
        private System.Windows.Forms.Panel panExcelCustom;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnScegliTemplate;
        private System.Windows.Forms.Button btnEmailDest;
        private System.Windows.Forms.Button btnSalva;
        private System.Windows.Forms.TextBox txtNomeTemplate;
        private System.Windows.Forms.Label lbTemplatePath;
        private System.Windows.Forms.Button btnEliminaTemplate;
        private System.Windows.Forms.ComboBox cmbConnessioni;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown txtNumOutput;
        private System.Windows.Forms.Button btnSelezionaEstrazioni;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtEstrazioniAcc;
    }
}
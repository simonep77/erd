namespace EasyReportDispatcher_DESKTOP
{
    partial class frmEstrazione
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
            this.btnInfoCopy = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCopyToPath = new System.Windows.Forms.TextBox();
            this.btnEmailDestDel = new System.Windows.Forms.Button();
            this.btnEmailDestEdit = new System.Windows.Forms.Button();
            this.btnEmailDestAdd = new System.Windows.Forms.Button();
            this.cmbDestEmail = new System.Windows.Forms.ComboBox();
            this.btnElimina = new System.Windows.Forms.Button();
            this.btnEditConn = new System.Windows.Forms.Button();
            this.btnInserisciConnessione = new System.Windows.Forms.Button();
            this.chkAccorpaDati = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtEstrazioniAcc = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtNumOutput = new System.Windows.Forms.NumericUpDown();
            this.cmbConnessioni = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chbInvioEmail = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbTipoFile = new System.Windows.Forms.ComboBox();
            this.txtCronString = new System.Windows.Forms.TextBox();
            this.chkAttivo = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
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
            this.panTop = new System.Windows.Forms.Panel();
            this.panCenter = new System.Windows.Forms.Panel();
            this.panBottom = new System.Windows.Forms.Panel();
            this.gbGenerali.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumOutput)).BeginInit();
            this.gbExcel.SuspendLayout();
            this.panExcelCustom.SuspendLayout();
            this.panExcelBase.SuspendLayout();
            this.gbSql.SuspendLayout();
            this.panTop.SuspendLayout();
            this.panCenter.SuspendLayout();
            this.panBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbGenerali
            // 
            this.gbGenerali.Controls.Add(this.btnInfoCopy);
            this.gbGenerali.Controls.Add(this.label1);
            this.gbGenerali.Controls.Add(this.txtCopyToPath);
            this.gbGenerali.Controls.Add(this.btnEmailDestDel);
            this.gbGenerali.Controls.Add(this.btnEmailDestEdit);
            this.gbGenerali.Controls.Add(this.btnEmailDestAdd);
            this.gbGenerali.Controls.Add(this.cmbDestEmail);
            this.gbGenerali.Controls.Add(this.btnElimina);
            this.gbGenerali.Controls.Add(this.btnEditConn);
            this.gbGenerali.Controls.Add(this.btnInserisciConnessione);
            this.gbGenerali.Controls.Add(this.chkAccorpaDati);
            this.gbGenerali.Controls.Add(this.label11);
            this.gbGenerali.Controls.Add(this.txtEstrazioniAcc);
            this.gbGenerali.Controls.Add(this.label10);
            this.gbGenerali.Controls.Add(this.txtNumOutput);
            this.gbGenerali.Controls.Add(this.cmbConnessioni);
            this.gbGenerali.Controls.Add(this.label9);
            this.gbGenerali.Controls.Add(this.chbInvioEmail);
            this.gbGenerali.Controls.Add(this.label7);
            this.gbGenerali.Controls.Add(this.cmbTipoFile);
            this.gbGenerali.Controls.Add(this.txtCronString);
            this.gbGenerali.Controls.Add(this.chkAttivo);
            this.gbGenerali.Controls.Add(this.label3);
            this.gbGenerali.Controls.Add(this.txtNote);
            this.gbGenerali.Controls.Add(this.label2);
            this.gbGenerali.Controls.Add(this.txtNome);
            this.gbGenerali.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbGenerali.Location = new System.Drawing.Point(0, 0);
            this.gbGenerali.Margin = new System.Windows.Forms.Padding(3, 3, 3, 20);
            this.gbGenerali.Name = "gbGenerali";
            this.gbGenerali.Size = new System.Drawing.Size(1379, 230);
            this.gbGenerali.TabIndex = 0;
            this.gbGenerali.TabStop = false;
            this.gbGenerali.Text = "Generali";
            // 
            // btnInfoCopy
            // 
            this.btnInfoCopy.Image = global::EasyReportDispatcher_DESKTOP.Properties.Resources.information;
            this.btnInfoCopy.Location = new System.Drawing.Point(322, 153);
            this.btnInfoCopy.Name = "btnInfoCopy";
            this.btnInfoCopy.Size = new System.Drawing.Size(27, 23);
            this.btnInfoCopy.TabIndex = 31;
            this.btnInfoCopy.UseVisualStyleBackColor = true;
            this.btnInfoCopy.Click += new System.EventHandler(this.btnInfoCopy_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 18);
            this.label1.TabIndex = 30;
            this.label1.Text = "Copia su:";
            // 
            // txtCopyToPath
            // 
            this.txtCopyToPath.Location = new System.Drawing.Point(115, 152);
            this.txtCopyToPath.Multiline = true;
            this.txtCopyToPath.Name = "txtCopyToPath";
            this.txtCopyToPath.Size = new System.Drawing.Size(201, 57);
            this.txtCopyToPath.TabIndex = 29;
            // 
            // btnEmailDestDel
            // 
            this.btnEmailDestDel.Image = global::EasyReportDispatcher_DESKTOP.Properties.Resources.delete;
            this.btnEmailDestDel.Location = new System.Drawing.Point(894, 105);
            this.btnEmailDestDel.Name = "btnEmailDestDel";
            this.btnEmailDestDel.Size = new System.Drawing.Size(27, 23);
            this.btnEmailDestDel.TabIndex = 28;
            this.btnEmailDestDel.UseVisualStyleBackColor = true;
            this.btnEmailDestDel.Click += new System.EventHandler(this.btnEmailDestDel_Click);
            // 
            // btnEmailDestEdit
            // 
            this.btnEmailDestEdit.Image = global::EasyReportDispatcher_DESKTOP.Properties.Resources.table_edit;
            this.btnEmailDestEdit.Location = new System.Drawing.Point(864, 105);
            this.btnEmailDestEdit.Name = "btnEmailDestEdit";
            this.btnEmailDestEdit.Size = new System.Drawing.Size(27, 23);
            this.btnEmailDestEdit.TabIndex = 27;
            this.btnEmailDestEdit.UseVisualStyleBackColor = true;
            this.btnEmailDestEdit.Click += new System.EventHandler(this.btnEmailDestAdd_Click);
            // 
            // btnEmailDestAdd
            // 
            this.btnEmailDestAdd.Image = global::EasyReportDispatcher_DESKTOP.Properties.Resources.add;
            this.btnEmailDestAdd.Location = new System.Drawing.Point(834, 105);
            this.btnEmailDestAdd.Name = "btnEmailDestAdd";
            this.btnEmailDestAdd.Size = new System.Drawing.Size(27, 23);
            this.btnEmailDestAdd.TabIndex = 26;
            this.btnEmailDestAdd.UseVisualStyleBackColor = true;
            this.btnEmailDestAdd.Click += new System.EventHandler(this.btnEmailDestAdd_Click);
            // 
            // cmbDestEmail
            // 
            this.cmbDestEmail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDestEmail.FormattingEnabled = true;
            this.cmbDestEmail.Location = new System.Drawing.Point(627, 103);
            this.cmbDestEmail.Name = "cmbDestEmail";
            this.cmbDestEmail.Size = new System.Drawing.Size(201, 26);
            this.cmbDestEmail.TabIndex = 25;
            this.cmbDestEmail.SelectedIndexChanged += new System.EventHandler(this.cmbDestEmail_SelectedIndexChanged);
            // 
            // btnElimina
            // 
            this.btnElimina.Image = global::EasyReportDispatcher_DESKTOP.Properties.Resources.delete;
            this.btnElimina.Location = new System.Drawing.Point(382, 27);
            this.btnElimina.Name = "btnElimina";
            this.btnElimina.Size = new System.Drawing.Size(27, 23);
            this.btnElimina.TabIndex = 23;
            this.btnElimina.UseVisualStyleBackColor = true;
            this.btnElimina.Click += new System.EventHandler(this.btnElimina_Click);
            // 
            // btnEditConn
            // 
            this.btnEditConn.Image = global::EasyReportDispatcher_DESKTOP.Properties.Resources.table_edit;
            this.btnEditConn.Location = new System.Drawing.Point(352, 27);
            this.btnEditConn.Name = "btnEditConn";
            this.btnEditConn.Size = new System.Drawing.Size(27, 23);
            this.btnEditConn.TabIndex = 22;
            this.btnEditConn.UseVisualStyleBackColor = true;
            this.btnEditConn.Click += new System.EventHandler(this.btnInsModConnessione_Click);
            // 
            // btnInserisciConnessione
            // 
            this.btnInserisciConnessione.Image = global::EasyReportDispatcher_DESKTOP.Properties.Resources.add;
            this.btnInserisciConnessione.Location = new System.Drawing.Point(322, 27);
            this.btnInserisciConnessione.Name = "btnInserisciConnessione";
            this.btnInserisciConnessione.Size = new System.Drawing.Size(27, 23);
            this.btnInserisciConnessione.TabIndex = 21;
            this.btnInserisciConnessione.UseVisualStyleBackColor = true;
            this.btnInserisciConnessione.Click += new System.EventHandler(this.btnInsModConnessione_Click);
            // 
            // chkAccorpaDati
            // 
            this.chkAccorpaDati.AutoSize = true;
            this.chkAccorpaDati.Location = new System.Drawing.Point(833, 163);
            this.chkAccorpaDati.Name = "chkAccorpaDati";
            this.chkAccorpaDati.Size = new System.Drawing.Size(81, 22);
            this.chkAccorpaDati.TabIndex = 20;
            this.chkAccorpaDati.Text = "Solo Dati";
            this.chkAccorpaDati.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(425, 166);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(133, 18);
            this.label11.TabIndex = 19;
            this.label11.Text = "Estrazioni Accorpate";
            // 
            // txtEstrazioniAcc
            // 
            this.txtEstrazioniAcc.Location = new System.Drawing.Point(626, 161);
            this.txtEstrazioniAcc.Name = "txtEstrazioniAcc";
            this.txtEstrazioniAcc.Size = new System.Drawing.Size(201, 26);
            this.txtEstrazioniAcc.TabIndex = 18;
            this.txtEstrazioniAcc.TextChanged += new System.EventHandler(this.txtEstrazioniAcc_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(425, 136);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 18);
            this.label10.TabIndex = 17;
            this.label10.Text = "Num. Output";
            // 
            // txtNumOutput
            // 
            this.txtNumOutput.Location = new System.Drawing.Point(626, 132);
            this.txtNumOutput.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.txtNumOutput.Name = "txtNumOutput";
            this.txtNumOutput.Size = new System.Drawing.Size(72, 26);
            this.txtNumOutput.TabIndex = 16;
            // 
            // cmbConnessioni
            // 
            this.cmbConnessioni.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConnessioni.FormattingEnabled = true;
            this.cmbConnessioni.Location = new System.Drawing.Point(115, 25);
            this.cmbConnessioni.Name = "cmbConnessioni";
            this.cmbConnessioni.Size = new System.Drawing.Size(201, 26);
            this.cmbConnessioni.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 18);
            this.label9.TabIndex = 14;
            this.label9.Text = "Connessione";
            // 
            // chbInvioEmail
            // 
            this.chbInvioEmail.Location = new System.Drawing.Point(426, 105);
            this.chbInvioEmail.Name = "chbInvioEmail";
            this.chbInvioEmail.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chbInvioEmail.Size = new System.Drawing.Size(193, 22);
            this.chbInvioEmail.TabIndex = 11;
            this.chbInvioEmail.Text = "Invio Email";
            this.chbInvioEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chbInvioEmail.UseVisualStyleBackColor = true;
            this.chbInvioEmail.CheckedChanged += new System.EventHandler(this.chbInvioEmail_CheckedChanged);
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
            // txtCronString
            // 
            this.txtCronString.Location = new System.Drawing.Point(626, 41);
            this.txtCronString.Name = "txtCronString";
            this.txtCronString.Size = new System.Drawing.Size(201, 26);
            this.txtCronString.TabIndex = 7;
            // 
            // chkAttivo
            // 
            this.chkAttivo.Location = new System.Drawing.Point(426, 43);
            this.chkAttivo.Name = "chkAttivo";
            this.chkAttivo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkAttivo.Size = new System.Drawing.Size(193, 22);
            this.chkAttivo.TabIndex = 6;
            this.chkAttivo.Text = "Schedulazione (CRON)";
            this.chkAttivo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAttivo.UseVisualStyleBackColor = true;
            this.chkAttivo.CheckedChanged += new System.EventHandler(this.chkAttivo_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "Note";
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(115, 89);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(201, 57);
            this.txtNote.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "Nome";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(115, 57);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(201, 26);
            this.txtNome.TabIndex = 1;
            // 
            // gbExcel
            // 
            this.gbExcel.Controls.Add(this.panExcelCustom);
            this.gbExcel.Controls.Add(this.panExcelBase);
            this.gbExcel.Controls.Add(this.rbTemplateCustom);
            this.gbExcel.Controls.Add(this.rbTemplateBase);
            this.gbExcel.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbExcel.Location = new System.Drawing.Point(0, 230);
            this.gbExcel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 20);
            this.gbExcel.Name = "gbExcel";
            this.gbExcel.Size = new System.Drawing.Size(1379, 125);
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
            this.panExcelCustom.Location = new System.Drawing.Point(450, 32);
            this.panExcelCustom.Name = "panExcelCustom";
            this.panExcelCustom.Size = new System.Drawing.Size(567, 83);
            this.panExcelCustom.TabIndex = 11;
            // 
            // btnEliminaTemplate
            // 
            this.btnEliminaTemplate.Image = global::EasyReportDispatcher_DESKTOP.Properties.Resources.cancel;
            this.btnEliminaTemplate.Location = new System.Drawing.Point(414, 9);
            this.btnEliminaTemplate.Name = "btnEliminaTemplate";
            this.btnEliminaTemplate.Size = new System.Drawing.Size(27, 23);
            this.btnEliminaTemplate.TabIndex = 15;
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
            this.btnScegliTemplate.Image = global::EasyReportDispatcher_DESKTOP.Properties.Resources.attach;
            this.btnScegliTemplate.Location = new System.Drawing.Point(384, 9);
            this.btnScegliTemplate.Name = "btnScegliTemplate";
            this.btnScegliTemplate.Size = new System.Drawing.Size(27, 23);
            this.btnScegliTemplate.TabIndex = 12;
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
            this.panExcelBase.Location = new System.Drawing.Point(32, 32);
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
            this.rbTemplateCustom.Location = new System.Drawing.Point(630, 11);
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
            this.rbTemplateBase.Location = new System.Drawing.Point(151, 11);
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
            this.gbSql.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSql.Location = new System.Drawing.Point(0, 0);
            this.gbSql.Name = "gbSql";
            this.gbSql.Padding = new System.Windows.Forms.Padding(10);
            this.gbSql.Size = new System.Drawing.Size(1379, 291);
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
            this.txtSQL.Size = new System.Drawing.Size(1359, 252);
            this.txtSQL.TabIndex = 0;
            // 
            // btnSalva
            // 
            this.btnSalva.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSalva.Location = new System.Drawing.Point(605, 9);
            this.btnSalva.Name = "btnSalva";
            this.btnSalva.Size = new System.Drawing.Size(93, 34);
            this.btnSalva.TabIndex = 14;
            this.btnSalva.Text = "Salva";
            this.btnSalva.UseVisualStyleBackColor = true;
            this.btnSalva.Click += new System.EventHandler(this.btnSalva_Click);
            // 
            // panTop
            // 
            this.panTop.Controls.Add(this.gbExcel);
            this.panTop.Controls.Add(this.gbGenerali);
            this.panTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panTop.Location = new System.Drawing.Point(20, 20);
            this.panTop.Name = "panTop";
            this.panTop.Size = new System.Drawing.Size(1379, 360);
            this.panTop.TabIndex = 16;
            // 
            // panCenter
            // 
            this.panCenter.Controls.Add(this.gbSql);
            this.panCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panCenter.Location = new System.Drawing.Point(20, 380);
            this.panCenter.Name = "panCenter";
            this.panCenter.Size = new System.Drawing.Size(1379, 291);
            this.panCenter.TabIndex = 0;
            // 
            // panBottom
            // 
            this.panBottom.Controls.Add(this.btnSalva);
            this.panBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panBottom.Location = new System.Drawing.Point(20, 671);
            this.panBottom.Name = "panBottom";
            this.panBottom.Size = new System.Drawing.Size(1379, 51);
            this.panBottom.TabIndex = 18;
            // 
            // frmEstrazione
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1419, 742);
            this.Controls.Add(this.panCenter);
            this.Controls.Add(this.panTop);
            this.Controls.Add(this.panBottom);
            this.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmEstrazione";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Estrazione";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEstrazione_FormClosing);
            this.Load += new System.EventHandler(this.frmEstrazione_Load);
            this.gbGenerali.ResumeLayout(false);
            this.gbGenerali.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumOutput)).EndInit();
            this.gbExcel.ResumeLayout(false);
            this.gbExcel.PerformLayout();
            this.panExcelCustom.ResumeLayout(false);
            this.panExcelCustom.PerformLayout();
            this.panExcelBase.ResumeLayout(false);
            this.panExcelBase.PerformLayout();
            this.gbSql.ResumeLayout(false);
            this.panTop.ResumeLayout(false);
            this.panCenter.ResumeLayout(false);
            this.panBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbGenerali;
        private System.Windows.Forms.TextBox txtNome;
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
        private System.Windows.Forms.Button btnSalva;
        private System.Windows.Forms.TextBox txtNomeTemplate;
        private System.Windows.Forms.Label lbTemplatePath;
        private System.Windows.Forms.Button btnEliminaTemplate;
        private System.Windows.Forms.ComboBox cmbConnessioni;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown txtNumOutput;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtEstrazioniAcc;
        private System.Windows.Forms.CheckBox chkAccorpaDati;
        private System.Windows.Forms.Button btnInserisciConnessione;
        private System.Windows.Forms.Button btnEditConn;
        private System.Windows.Forms.Button btnElimina;
        private System.Windows.Forms.Button btnEmailDestDel;
        private System.Windows.Forms.Button btnEmailDestEdit;
        private System.Windows.Forms.Button btnEmailDestAdd;
        private System.Windows.Forms.ComboBox cmbDestEmail;
        private System.Windows.Forms.Panel panTop;
        private System.Windows.Forms.Panel panCenter;
        private System.Windows.Forms.Panel panBottom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCopyToPath;
        private System.Windows.Forms.Button btnInfoCopy;
    }
}
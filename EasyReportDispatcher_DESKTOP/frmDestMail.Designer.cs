namespace EasyReportDispatcher_DESKTOP
{
    partial class frmDestMail
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
            this.txtMailSubj = new System.Windows.Forms.TextBox();
            this.txtMailCorpo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkMostra = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtMailBCC = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMailCC = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMailTO = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMailFROM = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSalva = new System.Windows.Forms.Button();
            this.panSalva = new System.Windows.Forms.Panel();
            this.panIndirizzi = new System.Windows.Forms.Panel();
            this.panContenuto = new System.Windows.Forms.Panel();
            this.panTop = new System.Windows.Forms.Panel();
            this.btnSmtpDel = new System.Windows.Forms.Button();
            this.btnSmtpEdit = new System.Windows.Forms.Button();
            this.btnSmtpAdd = new System.Windows.Forms.Button();
            this.cmbSmtp = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panSalva.SuspendLayout();
            this.panIndirizzi.SuspendLayout();
            this.panContenuto.SuspendLayout();
            this.panTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Oggetto";
            // 
            // txtMailSubj
            // 
            this.txtMailSubj.Location = new System.Drawing.Point(147, 28);
            this.txtMailSubj.Name = "txtMailSubj";
            this.txtMailSubj.Size = new System.Drawing.Size(420, 26);
            this.txtMailSubj.TabIndex = 1;
            // 
            // txtMailCorpo
            // 
            this.txtMailCorpo.Location = new System.Drawing.Point(147, 60);
            this.txtMailCorpo.Multiline = true;
            this.txtMailCorpo.Name = "txtMailCorpo";
            this.txtMailCorpo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMailCorpo.Size = new System.Drawing.Size(420, 139);
            this.txtMailCorpo.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Corpo";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkMostra);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.txtMailCorpo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtMailSubj);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(675, 242);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Contenuto Mail";
            // 
            // chkMostra
            // 
            this.chkMostra.AutoSize = true;
            this.chkMostra.Location = new System.Drawing.Point(574, 208);
            this.chkMostra.Name = "chkMostra";
            this.chkMostra.Size = new System.Drawing.Size(70, 22);
            this.chkMostra.TabIndex = 6;
            this.chkMostra.Text = "Mostra";
            this.chkMostra.UseVisualStyleBackColor = true;
            this.chkMostra.CheckedChanged += new System.EventHandler(this.chkMostra_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 208);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "Password (file)";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(147, 205);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(420, 26);
            this.txtPassword.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtMailBCC);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtMailCC);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtMailTO);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtMailFROM);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(10, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(675, 262);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Indirizzi";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 230);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(517, 18);
            this.label8.TabIndex = 15;
            this.label8.Text = "Tranne il campo \"Mail FROM\" è possibile inserire più indirizzi separati da virgol" +
    "a \",\"";
            // 
            // txtMailBCC
            // 
            this.txtMailBCC.Location = new System.Drawing.Point(147, 167);
            this.txtMailBCC.Multiline = true;
            this.txtMailBCC.Name = "txtMailBCC";
            this.txtMailBCC.Size = new System.Drawing.Size(420, 51);
            this.txtMailBCC.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 170);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 18);
            this.label6.TabIndex = 13;
            this.label6.Text = "Mail BCC";
            // 
            // txtMailCC
            // 
            this.txtMailCC.Location = new System.Drawing.Point(147, 114);
            this.txtMailCC.Multiline = true;
            this.txtMailCC.Name = "txtMailCC";
            this.txtMailCC.Size = new System.Drawing.Size(420, 47);
            this.txtMailCC.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 117);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 18);
            this.label7.TabIndex = 11;
            this.label7.Text = "Mail CC";
            // 
            // txtMailTO
            // 
            this.txtMailTO.Location = new System.Drawing.Point(147, 54);
            this.txtMailTO.Multiline = true;
            this.txtMailTO.Name = "txtMailTO";
            this.txtMailTO.Size = new System.Drawing.Size(420, 54);
            this.txtMailTO.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 18);
            this.label5.TabIndex = 9;
            this.label5.Text = "Mail TO (obbl.)";
            // 
            // txtMailFROM
            // 
            this.txtMailFROM.Location = new System.Drawing.Point(147, 22);
            this.txtMailFROM.Name = "txtMailFROM";
            this.txtMailFROM.Size = new System.Drawing.Size(322, 26);
            this.txtMailFROM.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 18);
            this.label4.TabIndex = 7;
            this.label4.Text = "Mail FROM (obbl.)";
            // 
            // btnSalva
            // 
            this.btnSalva.Location = new System.Drawing.Point(296, 13);
            this.btnSalva.Name = "btnSalva";
            this.btnSalva.Size = new System.Drawing.Size(83, 32);
            this.btnSalva.TabIndex = 6;
            this.btnSalva.Text = "Salva";
            this.btnSalva.UseVisualStyleBackColor = true;
            this.btnSalva.Click += new System.EventHandler(this.btnSalva_Click);
            // 
            // panSalva
            // 
            this.panSalva.Controls.Add(this.btnSalva);
            this.panSalva.Dock = System.Windows.Forms.DockStyle.Top;
            this.panSalva.Location = new System.Drawing.Point(10, 622);
            this.panSalva.Name = "panSalva";
            this.panSalva.Padding = new System.Windows.Forms.Padding(10);
            this.panSalva.Size = new System.Drawing.Size(695, 56);
            this.panSalva.TabIndex = 6;
            // 
            // panIndirizzi
            // 
            this.panIndirizzi.Controls.Add(this.groupBox2);
            this.panIndirizzi.Dock = System.Windows.Forms.DockStyle.Top;
            this.panIndirizzi.Location = new System.Drawing.Point(10, 340);
            this.panIndirizzi.Name = "panIndirizzi";
            this.panIndirizzi.Padding = new System.Windows.Forms.Padding(10);
            this.panIndirizzi.Size = new System.Drawing.Size(695, 282);
            this.panIndirizzi.TabIndex = 7;
            // 
            // panContenuto
            // 
            this.panContenuto.Controls.Add(this.groupBox1);
            this.panContenuto.Dock = System.Windows.Forms.DockStyle.Top;
            this.panContenuto.Location = new System.Drawing.Point(10, 78);
            this.panContenuto.Name = "panContenuto";
            this.panContenuto.Padding = new System.Windows.Forms.Padding(10);
            this.panContenuto.Size = new System.Drawing.Size(695, 262);
            this.panContenuto.TabIndex = 8;
            // 
            // panTop
            // 
            this.panTop.Controls.Add(this.btnSmtpDel);
            this.panTop.Controls.Add(this.btnSmtpEdit);
            this.panTop.Controls.Add(this.btnSmtpAdd);
            this.panTop.Controls.Add(this.cmbSmtp);
            this.panTop.Controls.Add(this.label9);
            this.panTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panTop.Location = new System.Drawing.Point(10, 10);
            this.panTop.Name = "panTop";
            this.panTop.Padding = new System.Windows.Forms.Padding(10);
            this.panTop.Size = new System.Drawing.Size(695, 68);
            this.panTop.TabIndex = 0;
            // 
            // btnSmtpDel
            // 
            this.btnSmtpDel.Image = global::EasyReportDispatcher_DESKTOP.Properties.Resources.delete;
            this.btnSmtpDel.Location = new System.Drawing.Point(548, 23);
            this.btnSmtpDel.Name = "btnSmtpDel";
            this.btnSmtpDel.Size = new System.Drawing.Size(29, 26);
            this.btnSmtpDel.TabIndex = 9;
            this.btnSmtpDel.UseVisualStyleBackColor = true;
            this.btnSmtpDel.Click += new System.EventHandler(this.btnSmtpDel_Click);
            // 
            // btnSmtpEdit
            // 
            this.btnSmtpEdit.Image = global::EasyReportDispatcher_DESKTOP.Properties.Resources.table_edit;
            this.btnSmtpEdit.Location = new System.Drawing.Point(516, 23);
            this.btnSmtpEdit.Name = "btnSmtpEdit";
            this.btnSmtpEdit.Size = new System.Drawing.Size(29, 26);
            this.btnSmtpEdit.TabIndex = 8;
            this.btnSmtpEdit.UseVisualStyleBackColor = true;
            this.btnSmtpEdit.Click += new System.EventHandler(this.actAddEditSmtp);
            // 
            // btnSmtpAdd
            // 
            this.btnSmtpAdd.Image = global::EasyReportDispatcher_DESKTOP.Properties.Resources.add;
            this.btnSmtpAdd.Location = new System.Drawing.Point(485, 23);
            this.btnSmtpAdd.Name = "btnSmtpAdd";
            this.btnSmtpAdd.Size = new System.Drawing.Size(29, 26);
            this.btnSmtpAdd.TabIndex = 7;
            this.btnSmtpAdd.UseVisualStyleBackColor = true;
            this.btnSmtpAdd.Click += new System.EventHandler(this.actAddEditSmtp);
            // 
            // cmbSmtp
            // 
            this.cmbSmtp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSmtp.FormattingEnabled = true;
            this.cmbSmtp.Location = new System.Drawing.Point(157, 24);
            this.cmbSmtp.Name = "cmbSmtp";
            this.cmbSmtp.Size = new System.Drawing.Size(322, 26);
            this.cmbSmtp.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(43, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 18);
            this.label9.TabIndex = 1;
            this.label9.Text = "SMTP";
            // 
            // frmDestMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 688);
            this.Controls.Add(this.panSalva);
            this.Controls.Add(this.panIndirizzi);
            this.Controls.Add(this.panContenuto);
            this.Controls.Add(this.panTop);
            this.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmDestMail";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Destinatari Email <new>";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panSalva.ResumeLayout(false);
            this.panIndirizzi.ResumeLayout(false);
            this.panContenuto.ResumeLayout(false);
            this.panTop.ResumeLayout(false);
            this.panTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMailSubj;
        private System.Windows.Forms.TextBox txtMailCorpo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox chkMostra;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtMailBCC;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMailCC;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMailTO;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMailFROM;
        private System.Windows.Forms.Button btnSalva;
        private System.Windows.Forms.Panel panSalva;
        private System.Windows.Forms.Panel panIndirizzi;
        private System.Windows.Forms.Panel panContenuto;
        private System.Windows.Forms.Panel panTop;
        private System.Windows.Forms.ComboBox cmbSmtp;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSmtpDel;
        private System.Windows.Forms.Button btnSmtpEdit;
        private System.Windows.Forms.Button btnSmtpAdd;
    }
}
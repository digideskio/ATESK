namespace TevelATE
{
    partial class MainForm
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
            this.cmbUserType = new System.Windows.Forms.ComboBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEnterBTM = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.llShowUsers = new System.Windows.Forms.LinkLabel();
            this.llSettings = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.comboUserName = new System.Windows.Forms.ComboBox();
            this.llCreateTech = new System.Windows.Forms.LinkLabel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbUserType
            // 
            this.cmbUserType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUserType.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUserType.FormattingEnabled = true;
            this.cmbUserType.Items.AddRange(new object[] {
            "Operator",
            "Technician",
            "Admin"});
            this.cmbUserType.Location = new System.Drawing.Point(17, 30);
            this.cmbUserType.Name = "cmbUserType";
            this.cmbUserType.Size = new System.Drawing.Size(192, 33);
            this.cmbUserType.TabIndex = 0;
            this.cmbUserType.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(20, 184);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(195, 32);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "User:";
            // 
            // btnEnterBTM
            // 
            this.btnEnterBTM.ForeColor = System.Drawing.Color.Blue;
            this.btnEnterBTM.Location = new System.Drawing.Point(23, 245);
            this.btnEnterBTM.Name = "btnEnterBTM";
            this.btnEnterBTM.Size = new System.Drawing.Size(191, 37);
            this.btnEnterBTM.TabIndex = 2;
            this.btnEnterBTM.Text = "Enter BTM Application";
            this.btnEnterBTM.UseVisualStyleBackColor = true;
            this.btnEnterBTM.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "User name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Password:";
            // 
            // llShowUsers
            // 
            this.llShowUsers.AutoSize = true;
            this.llShowUsers.Enabled = false;
            this.llShowUsers.Location = new System.Drawing.Point(21, 302);
            this.llShowUsers.Name = "llShowUsers";
            this.llShowUsers.Size = new System.Drawing.Size(40, 13);
            this.llShowUsers.TabIndex = 7;
            this.llShowUsers.TabStop = true;
            this.llShowUsers.Text = "Users..";
            this.llShowUsers.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llShowUsers_LinkClicked);
            // 
            // llSettings
            // 
            this.llSettings.AutoSize = true;
            this.llSettings.Enabled = false;
            this.llSettings.Location = new System.Drawing.Point(187, 303);
            this.llSettings.Name = "llSettings";
            this.llSettings.Size = new System.Drawing.Size(45, 13);
            this.llSettings.TabIndex = 8;
            this.llSettings.TabStop = true;
            this.llSettings.Text = "Settings";
            this.llSettings.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(405, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Version 1.0.0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(405, 245);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "--";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.comboUserName);
            this.panel1.Controls.Add(this.llCreateTech);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cmbUserType);
            this.panel1.Controls.Add(this.txtPassword);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnEnterBTM);
            this.panel1.Controls.Add(this.llSettings);
            this.panel1.Controls.Add(this.llShowUsers);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(252, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(617, 369);
            this.panel1.TabIndex = 13;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::TevelATE.Properties.Resources.luster;
            this.pictureBox2.Location = new System.Drawing.Point(398, 92);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(160, 71);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 185;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TevelATE.Properties.Resources.EPSILOR;
            this.pictureBox1.Location = new System.Drawing.Point(374, 32);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(201, 54);
            this.pictureBox1.TabIndex = 184;
            this.pictureBox1.TabStop = false;
            // 
            // comboUserName
            // 
            this.comboUserName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboUserName.FormattingEnabled = true;
            this.comboUserName.Location = new System.Drawing.Point(17, 113);
            this.comboUserName.Name = "comboUserName";
            this.comboUserName.Size = new System.Drawing.Size(200, 33);
            this.comboUserName.TabIndex = 14;
            this.comboUserName.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // llCreateTech
            // 
            this.llCreateTech.AutoSize = true;
            this.llCreateTech.Enabled = false;
            this.llCreateTech.Location = new System.Drawing.Point(71, 302);
            this.llCreateTech.Name = "llCreateTech";
            this.llCreateTech.Size = new System.Drawing.Size(84, 13);
            this.llCreateTech.TabIndex = 13;
            this.llCreateTech.TabStop = true;
            this.llCreateTech.Text = "Create techinian";
            this.llCreateTech.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnEnterBTM;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1095, 607);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Luster Tech - BTM ATE";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbUserType;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEnterBTM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel llShowUsers;
        private System.Windows.Forms.LinkLabel llSettings;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel llCreateTech;
        private System.Windows.Forms.ComboBox comboUserName;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}


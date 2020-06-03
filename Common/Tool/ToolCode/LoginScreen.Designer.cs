namespace CommonicationMemory
{
    partial class LoginScreen
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginScreen));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtCatalog2 = new System.Windows.Forms.TextBox();
            this.gbSQLServerCredential = new System.Windows.Forms.GroupBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.lblLogin = new System.Windows.Forms.Label();
            this.rbtnSqlServerAuthentication = new System.Windows.Forms.RadioButton();
            this.rbtnWindowsAuthentication = new System.Windows.Forms.RadioButton();
            this.txtCatalog = new System.Windows.Forms.TextBox();
            this.txtSqlServer = new System.Windows.Forms.TextBox();
            this.lblCatalog = new System.Windows.Forms.Label();
            this.lblServer = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLinkMapEntity = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnConnectMySql = new System.Windows.Forms.Button();
            this.btnConnectOracle = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnConnection = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.gbSQLServerCredential.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.SteelBlue;
            this.pnlHeader.Controls.Add(this.pictureBox1);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(569, 33);
            this.pnlHeader.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CommonicationMemory.Properties.Resources.DbInfo;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(213, 28);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.tabControl2);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 33);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(569, 261);
            this.panel2.TabIndex = 0;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Location = new System.Drawing.Point(12, 6);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(543, 207);
            this.tabControl2.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtCatalog2);
            this.tabPage1.Controls.Add(this.gbSQLServerCredential);
            this.tabPage1.Controls.Add(this.rbtnSqlServerAuthentication);
            this.tabPage1.Controls.Add(this.rbtnWindowsAuthentication);
            this.tabPage1.Controls.Add(this.txtCatalog);
            this.tabPage1.Controls.Add(this.txtSqlServer);
            this.tabPage1.Controls.Add(this.lblCatalog);
            this.tabPage1.Controls.Add(this.lblServer);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(535, 181);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Login";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtCatalog2
            // 
            this.txtCatalog2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCatalog2.Location = new System.Drawing.Point(337, 35);
            this.txtCatalog2.Name = "txtCatalog2";
            this.txtCatalog2.Size = new System.Drawing.Size(172, 20);
            this.txtCatalog2.TabIndex = 14;
            // 
            // gbSQLServerCredential
            // 
            this.gbSQLServerCredential.Controls.Add(this.txtPort);
            this.gbSQLServerCredential.Controls.Add(this.label2);
            this.gbSQLServerCredential.Controls.Add(this.txtPassword);
            this.gbSQLServerCredential.Controls.Add(this.lblPassword);
            this.gbSQLServerCredential.Controls.Add(this.txtLogin);
            this.gbSQLServerCredential.Controls.Add(this.lblLogin);
            this.gbSQLServerCredential.Enabled = false;
            this.gbSQLServerCredential.Location = new System.Drawing.Point(11, 86);
            this.gbSQLServerCredential.Name = "gbSQLServerCredential";
            this.gbSQLServerCredential.Size = new System.Drawing.Size(505, 82);
            this.gbSQLServerCredential.TabIndex = 13;
            this.gbSQLServerCredential.TabStop = false;
            this.gbSQLServerCredential.Text = "SQL/MySQL Server Credentials";
            // 
            // txtPort
            // 
            this.txtPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPort.Location = new System.Drawing.Point(54, 45);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(172, 20);
            this.txtPort.TabIndex = 6;
            this.txtPort.Text = "1433";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Port";
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Location = new System.Drawing.Point(315, 19);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(172, 20);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.Text = "admin";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(256, 22);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "&Password";
            // 
            // txtLogin
            // 
            this.txtLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLogin.Location = new System.Drawing.Point(54, 19);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(172, 20);
            this.txtLogin.TabIndex = 1;
            this.txtLogin.Text = "lemon";
            // 
            // lblLogin
            // 
            this.lblLogin.AutoSize = true;
            this.lblLogin.Location = new System.Drawing.Point(15, 21);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(33, 13);
            this.lblLogin.TabIndex = 0;
            this.lblLogin.Text = "&Login";
            // 
            // rbtnSqlServerAuthentication
            // 
            this.rbtnSqlServerAuthentication.AutoSize = true;
            this.rbtnSqlServerAuthentication.Location = new System.Drawing.Point(177, 63);
            this.rbtnSqlServerAuthentication.Name = "rbtnSqlServerAuthentication";
            this.rbtnSqlServerAuthentication.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rbtnSqlServerAuthentication.Size = new System.Drawing.Size(185, 17);
            this.rbtnSqlServerAuthentication.TabIndex = 12;
            this.rbtnSqlServerAuthentication.Text = "&SQL/MySql Server Authentication";
            this.rbtnSqlServerAuthentication.UseVisualStyleBackColor = true;
            this.rbtnSqlServerAuthentication.CheckedChanged += new System.EventHandler(this.rbtnSqlServerAuthentication_CheckedChanged_1);
            // 
            // rbtnWindowsAuthentication
            // 
            this.rbtnWindowsAuthentication.AutoSize = true;
            this.rbtnWindowsAuthentication.Checked = true;
            this.rbtnWindowsAuthentication.Location = new System.Drawing.Point(18, 63);
            this.rbtnWindowsAuthentication.Name = "rbtnWindowsAuthentication";
            this.rbtnWindowsAuthentication.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rbtnWindowsAuthentication.Size = new System.Drawing.Size(140, 17);
            this.rbtnWindowsAuthentication.TabIndex = 11;
            this.rbtnWindowsAuthentication.TabStop = true;
            this.rbtnWindowsAuthentication.Text = "&Windows Authentication";
            this.rbtnWindowsAuthentication.UseVisualStyleBackColor = true;
            this.rbtnWindowsAuthentication.CheckedChanged += new System.EventHandler(this.rbtnSqlServerAuthentication_CheckedChanged);
            // 
            // txtCatalog
            // 
            this.txtCatalog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCatalog.Location = new System.Drawing.Point(337, 9);
            this.txtCatalog.Name = "txtCatalog";
            this.txtCatalog.Size = new System.Drawing.Size(172, 20);
            this.txtCatalog.TabIndex = 10;
            this.txtCatalog.Text = "AptechProjectS4";
            // 
            // txtSqlServer
            // 
            this.txtSqlServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSqlServer.Location = new System.Drawing.Point(76, 9);
            this.txtSqlServer.Name = "txtSqlServer";
            this.txtSqlServer.Size = new System.Drawing.Size(172, 20);
            this.txtSqlServer.TabIndex = 8;
            this.txtSqlServer.Text = "34.92.152.57";
            // 
            // lblCatalog
            // 
            this.lblCatalog.AutoSize = true;
            this.lblCatalog.Location = new System.Drawing.Point(285, 12);
            this.lblCatalog.Name = "lblCatalog";
            this.lblCatalog.Size = new System.Drawing.Size(43, 13);
            this.lblCatalog.TabIndex = 9;
            this.lblCatalog.Text = "&Catalog";
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(8, 12);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(62, 13);
            this.lblServer.TabIndex = 7;
            this.lblServer.Text = "S&QL Server";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.txtLinkMapEntity);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(535, 181);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Setting";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Link map entities name";
            // 
            // txtLinkMapEntity
            // 
            this.txtLinkMapEntity.Location = new System.Drawing.Point(127, 6);
            this.txtLinkMapEntity.Name = "txtLinkMapEntity";
            this.txtLinkMapEntity.Size = new System.Drawing.Size(394, 20);
            this.txtLinkMapEntity.TabIndex = 16;
            this.txtLinkMapEntity.Text = "C:\\ProjectCode\\NightVisionCore\\vision-foundation\\Entity\\Entities";
            this.txtLinkMapEntity.TextChanged += new System.EventHandler(this.TextBox1TextChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.Controls.Add(this.btnConnectMySql);
            this.panel1.Controls.Add(this.btnConnectOracle);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.btnConnection);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 229);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(569, 32);
            this.panel1.TabIndex = 9;
            // 
            // btnConnectMySql
            // 
            this.btnConnectMySql.BackColor = System.Drawing.Color.White;
            this.btnConnectMySql.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnectMySql.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConnectMySql.Location = new System.Drawing.Point(419, 5);
            this.btnConnectMySql.Name = "btnConnectMySql";
            this.btnConnectMySql.Size = new System.Drawing.Size(136, 23);
            this.btnConnectMySql.TabIndex = 10;
            this.btnConnectMySql.Text = "C&onnect MySql";
            this.btnConnectMySql.UseVisualStyleBackColor = false;
            this.btnConnectMySql.Click += new System.EventHandler(this.btnConnectMySql_Click);
            // 
            // btnConnectOracle
            // 
            this.btnConnectOracle.BackColor = System.Drawing.Color.White;
            this.btnConnectOracle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnectOracle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConnectOracle.Location = new System.Drawing.Point(270, 5);
            this.btnConnectOracle.Name = "btnConnectOracle";
            this.btnConnectOracle.Size = new System.Drawing.Size(136, 23);
            this.btnConnectOracle.TabIndex = 9;
            this.btnConnectOracle.Text = "C&onnect ORACLE SQL";
            this.btnConnectOracle.UseVisualStyleBackColor = false;
            this.btnConnectOracle.Click += new System.EventHandler(this.btnConnectOracle_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.White;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Location = new System.Drawing.Point(12, 5);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 23);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "&Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            // 
            // btnConnection
            // 
            this.btnConnection.BackColor = System.Drawing.Color.White;
            this.btnConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnection.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConnection.Location = new System.Drawing.Point(150, 5);
            this.btnConnection.Name = "btnConnection";
            this.btnConnection.Size = new System.Drawing.Size(114, 23);
            this.btnConnection.TabIndex = 8;
            this.btnConnection.Text = "C&onnect MS SQL";
            this.btnConnection.UseVisualStyleBackColor = false;
            this.btnConnection.Click += new System.EventHandler(this.btnConnection_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // LoginScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 294);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginScreen";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tier Generator";
            this.Load += new System.EventHandler(this.LoginScreen_Load);
            this.pnlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.gbSQLServerCredential.ResumeLayout(false);
            this.gbSQLServerCredential.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnConnection;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnConnectOracle;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox gbSQLServerCredential;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.RadioButton rbtnSqlServerAuthentication;
        private System.Windows.Forms.RadioButton rbtnWindowsAuthentication;
        private System.Windows.Forms.TextBox txtCatalog;
        private System.Windows.Forms.TextBox txtSqlServer;
        private System.Windows.Forms.Label lblCatalog;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLinkMapEntity;
        private System.Windows.Forms.TextBox txtCatalog2;
        private System.Windows.Forms.Button btnConnectMySql;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPort;
    }
}


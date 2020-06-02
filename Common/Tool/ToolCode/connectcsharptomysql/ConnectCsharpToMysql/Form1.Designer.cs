namespace ConnectCsharpToMysql
{
    partial class Form1
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
            this.bInsert = new System.Windows.Forms.Button();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.lblLogin = new System.Windows.Forms.Label();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnConnectQueryString = new System.Windows.Forms.Button();
            this.txtQueryString = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // bInsert
            // 
            this.bInsert.Location = new System.Drawing.Point(12, 175);
            this.bInsert.Name = "bInsert";
            this.bInsert.Size = new System.Drawing.Size(241, 23);
            this.bInsert.TabIndex = 0;
            this.bInsert.Text = "Connect with Config";
            this.bInsert.UseVisualStyleBackColor = true;
            this.bInsert.Click += new System.EventHandler(this.bInsert_Click);
            // 
            // txtHost
            // 
            this.txtHost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHost.Location = new System.Drawing.Point(81, 17);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(172, 20);
            this.txtHost.TabIndex = 3;
            this.txtHost.Text = "10.8.6.87";
            // 
            // lblLogin
            // 
            this.lblLogin.AutoSize = true;
            this.lblLogin.Location = new System.Drawing.Point(12, 19);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(29, 13);
            this.lblLogin.TabIndex = 2;
            this.lblLogin.Text = "Host";
            // 
            // txtDatabase
            // 
            this.txtDatabase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDatabase.Location = new System.Drawing.Point(81, 45);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(172, 20);
            this.txtDatabase.TabIndex = 5;
            this.txtDatabase.Text = "DatabaseWorking";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Database";
            // 
            // txtUserName
            // 
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserName.Location = new System.Drawing.Point(81, 75);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(172, 20);
            this.txtUserName.TabIndex = 7;
            this.txtUserName.Text = "lemon";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Login";
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Location = new System.Drawing.Point(81, 105);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(172, 20);
            this.txtPassword.TabIndex = 9;
            this.txtPassword.Text = "Quantedge@123";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Password";
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(81, 134);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(172, 20);
            this.textBox1.TabIndex = 11;
            this.textBox1.Text = "3306";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Port";
            // 
            // btnConnectQueryString
            // 
            this.btnConnectQueryString.Location = new System.Drawing.Point(12, 318);
            this.btnConnectQueryString.Name = "btnConnectQueryString";
            this.btnConnectQueryString.Size = new System.Drawing.Size(180, 23);
            this.btnConnectQueryString.TabIndex = 12;
            this.btnConnectQueryString.Text = "Connect with QueryString";
            this.btnConnectQueryString.UseVisualStyleBackColor = true;
            this.btnConnectQueryString.Click += new System.EventHandler(this.btnConnectQueryString_Click);
            // 
            // txtQueryString
            // 
            this.txtQueryString.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQueryString.Location = new System.Drawing.Point(15, 240);
            this.txtQueryString.Multiline = true;
            this.txtQueryString.Name = "txtQueryString";
            this.txtQueryString.Size = new System.Drawing.Size(431, 72);
            this.txtQueryString.TabIndex = 13;
            this.txtQueryString.Text = "SERVER=10.8.6.87;DATABASE=DatabaseWorking;UID=lemon;PASSWORD=Quantedge@123;CharSe" +
    "t=utf8;Port=3306;";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 372);
            this.Controls.Add(this.txtQueryString);
            this.Controls.Add(this.btnConnectQueryString);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDatabase);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtHost);
            this.Controls.Add(this.lblLogin);
            this.Controls.Add(this.bInsert);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connect Csharp To Mysql";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bInsert;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnConnectQueryString;
        private System.Windows.Forms.TextBox txtQueryString;
    }
}


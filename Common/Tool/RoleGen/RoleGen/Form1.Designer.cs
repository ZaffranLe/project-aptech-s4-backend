namespace RoleGen
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
            this.txtDisplay = new System.Windows.Forms.TextBox();
            this.btnCreateScript = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSheetName = new System.Windows.Forms.TextBox();
            this.txtUrlRole = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtDisplay
            // 
            this.txtDisplay.Location = new System.Drawing.Point(12, 120);
            this.txtDisplay.Multiline = true;
            this.txtDisplay.Name = "txtDisplay";
            this.txtDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDisplay.Size = new System.Drawing.Size(954, 317);
            this.txtDisplay.TabIndex = 54;
            // 
            // btnCreateScript
            // 
            this.btnCreateScript.Location = new System.Drawing.Point(12, 12);
            this.btnCreateScript.Name = "btnCreateScript";
            this.btnCreateScript.Size = new System.Drawing.Size(75, 23);
            this.btnCreateScript.TabIndex = 55;
            this.btnCreateScript.Text = "Tạo script";
            this.btnCreateScript.UseVisualStyleBackColor = true;
            this.btnCreateScript.Click += new System.EventHandler(this.btnCreateScript_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(891, 443);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 56;
            this.button1.Text = "Copy";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(207, 13);
            this.label3.TabIndex = 61;
            this.label3.Text = "Nhập đường dẫn đến ma trận phân quyền";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 66;
            this.label1.Text = "Nhập tên Sheet:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtSheetName
            // 
            this.txtSheetName.Location = new System.Drawing.Point(225, 73);
            this.txtSheetName.Name = "txtSheetName";
            this.txtSheetName.Size = new System.Drawing.Size(209, 20);
            this.txtSheetName.TabIndex = 67;
            this.txtSheetName.Text = "1";
            // 
            // txtUrlRole
            // 
            this.txtUrlRole.Location = new System.Drawing.Point(225, 39);
            this.txtUrlRole.Name = "txtUrlRole";
            this.txtUrlRole.Size = new System.Drawing.Size(390, 20);
            this.txtUrlRole.TabIndex = 60;
            this.txtUrlRole.Text = "C:\\Users\\qe\\Downloads\\Role Defination.xlsx";
            this.txtUrlRole.TextChanged += new System.EventHandler(this.txtUrlRole_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 484);
            this.Controls.Add(this.txtSheetName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtUrlRole);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnCreateScript);
            this.Controls.Add(this.txtDisplay);
            this.Name = "Form1";
            this.Text = "Tạo script ma trận role";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtDisplay;
        private System.Windows.Forms.Button btnCreateScript;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSheetName;
        private System.Windows.Forms.TextBox txtUrlRole;
    }
}


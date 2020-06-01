namespace BaseUtils
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
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.openDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveDialog = new System.Windows.Forms.SaveFileDialog();
            this.btnEncryptForder = new System.Windows.Forms.Button();
            this.btnDecryptForder = new System.Windows.Forms.Button();
            this.lblFile = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPathService = new System.Windows.Forms.TextBox();
            this.txtPathConfig = new System.Windows.Forms.TextBox();
            this.btnCopyFileConfig = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCopyFileDll = new System.Windows.Forms.Button();
            this.btnCopyAllFile = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnChangeNameService = new System.Windows.Forms.Button();
            this.txtLinkCompare = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCopyLinkCompareToService = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtThuMucBuildVisionTrading = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(146, 6);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(128, 23);
            this.btnEncrypt.TabIndex = 0;
            this.btnEncrypt.Text = "Encrypt File";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Location = new System.Drawing.Point(286, 6);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(117, 23);
            this.btnDecrypt.TabIndex = 1;
            this.btnDecrypt.Text = "Decrypt File";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // openDialog
            // 
            this.openDialog.FileName = "openFileDialog1";
            // 
            // saveDialog
            // 
            this.saveDialog.DefaultExt = "qes";
            // 
            // btnEncryptForder
            // 
            this.btnEncryptForder.Location = new System.Drawing.Point(145, 42);
            this.btnEncryptForder.Name = "btnEncryptForder";
            this.btnEncryptForder.Size = new System.Drawing.Size(128, 23);
            this.btnEncryptForder.TabIndex = 2;
            this.btnEncryptForder.Text = "Encrypt Forder";
            this.btnEncryptForder.UseVisualStyleBackColor = true;
            this.btnEncryptForder.Click += new System.EventHandler(this.btnEncryptForder_Click);
            // 
            // btnDecryptForder
            // 
            this.btnDecryptForder.Location = new System.Drawing.Point(286, 42);
            this.btnDecryptForder.Name = "btnDecryptForder";
            this.btnDecryptForder.Size = new System.Drawing.Size(117, 23);
            this.btnDecryptForder.TabIndex = 3;
            this.btnDecryptForder.Text = "Decrypt Forder";
            this.btnDecryptForder.UseVisualStyleBackColor = true;
            this.btnDecryptForder.Click += new System.EventHandler(this.btnDecryptForder_Click);
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(40, 11);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(83, 13);
            this.lblFile.TabIndex = 4;
            this.lblFile.Text = "Mã hóa từng file";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Mã hóa cả thư mục";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Thư mục chứa service";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Thư mục chứa file config";
            // 
            // txtPathService
            // 
            this.txtPathService.Location = new System.Drawing.Point(154, 152);
            this.txtPathService.Name = "txtPathService";
            this.txtPathService.Size = new System.Drawing.Size(604, 20);
            this.txtPathService.TabIndex = 25;
            this.txtPathService.Text = "C:\\ProjectCode\\VisionTrading\\NightyBuild\\LastestBuild\\Worker";
            // 
            // txtPathConfig
            // 
            this.txtPathConfig.Location = new System.Drawing.Point(154, 126);
            this.txtPathConfig.Name = "txtPathConfig";
            this.txtPathConfig.Size = new System.Drawing.Size(604, 20);
            this.txtPathConfig.TabIndex = 24;
            this.txtPathConfig.Text = "C:\\QuantEdge\\Config";
            // 
            // btnCopyFileConfig
            // 
            this.btnCopyFileConfig.Location = new System.Drawing.Point(154, 178);
            this.btnCopyFileConfig.Name = "btnCopyFileConfig";
            this.btnCopyFileConfig.Size = new System.Drawing.Size(229, 23);
            this.btnCopyFileConfig.TabIndex = 23;
            this.btnCopyFileConfig.Text = "Copy file .qes từ thư mục Config vào Service";
            this.btnCopyFileConfig.UseVisualStyleBackColor = true;
            this.btnCopyFileConfig.Click += new System.EventHandler(this.btnCopyFileConfig_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(293, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "Copy thư mục config từ thư mục gốc tới các thư mục Service";
            // 
            // btnCopyFileDll
            // 
            this.btnCopyFileDll.Location = new System.Drawing.Point(389, 178);
            this.btnCopyFileDll.Name = "btnCopyFileDll";
            this.btnCopyFileDll.Size = new System.Drawing.Size(219, 23);
            this.btnCopyFileDll.TabIndex = 29;
            this.btnCopyFileDll.Text = "Copy DLL từ thư mục Config vào Service";
            this.btnCopyFileDll.UseVisualStyleBackColor = true;
            this.btnCopyFileDll.Click += new System.EventHandler(this.btnCopyFileDll_Click);
            // 
            // btnCopyAllFile
            // 
            this.btnCopyAllFile.Location = new System.Drawing.Point(389, 207);
            this.btnCopyAllFile.Name = "btnCopyAllFile";
            this.btnCopyAllFile.Size = new System.Drawing.Size(219, 23);
            this.btnCopyAllFile.TabIndex = 30;
            this.btnCopyAllFile.Text = "Copy All File từ thư mục Config vào Service";
            this.btnCopyAllFile.UseVisualStyleBackColor = true;
            this.btnCopyAllFile.Click += new System.EventHandler(this.btnCopyAllFile_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 403);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Dung cho VisionTrading";
            // 
            // btnChangeNameService
            // 
            this.btnChangeNameService.Location = new System.Drawing.Point(152, 398);
            this.btnChangeNameService.Name = "btnChangeNameService";
            this.btnChangeNameService.Size = new System.Drawing.Size(219, 23);
            this.btnChangeNameService.TabIndex = 33;
            this.btnChangeNameService.Text = "1. Đổi tên Service";
            this.btnChangeNameService.UseVisualStyleBackColor = true;
            this.btnChangeNameService.Click += new System.EventHandler(this.btnChangeNameService_Click);
            // 
            // txtLinkCompare
            // 
            this.txtLinkCompare.Location = new System.Drawing.Point(154, 361);
            this.txtLinkCompare.Name = "txtLinkCompare";
            this.txtLinkCompare.Size = new System.Drawing.Size(604, 20);
            this.txtLinkCompare.TabIndex = 34;
            this.txtLinkCompare.Text = "C:\\ProjectCode\\VisionTrading\\UpdateProduction\\UpdateProduct_20180906";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 364);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 13);
            this.label6.TabIndex = 35;
            this.label6.Text = "Thư mục cập nhật Product";
            // 
            // btnCopyLinkCompareToService
            // 
            this.btnCopyLinkCompareToService.Location = new System.Drawing.Point(390, 398);
            this.btnCopyLinkCompareToService.Name = "btnCopyLinkCompareToService";
            this.btnCopyLinkCompareToService.Size = new System.Drawing.Size(302, 23);
            this.btnCopyLinkCompareToService.TabIndex = 37;
            this.btnCopyLinkCompareToService.Text = "2. Copy các file từ linkcompare đến thư mục service";
            this.btnCopyLinkCompareToService.UseVisualStyleBackColor = true;
            this.btnCopyLinkCompareToService.Click += new System.EventHandler(this.btnCopyLinkCompareToService_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 313);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(589, 13);
            this.label8.TabIndex = 38;
            this.label8.Text = "Bước 4: Sử dụng BeyonceCompare để merce và so sánh dữ liệu WorkerNew với WorkerPr" +
    "oduct cũ. Hoàn thành bản build.";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 340);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(141, 13);
            this.label10.TabIndex = 41;
            this.label10.Text = "Thư mục build VisionTrading";
            // 
            // txtThuMucBuildVisionTrading
            // 
            this.txtThuMucBuildVisionTrading.Location = new System.Drawing.Point(154, 337);
            this.txtThuMucBuildVisionTrading.Name = "txtThuMucBuildVisionTrading";
            this.txtThuMucBuildVisionTrading.Size = new System.Drawing.Size(604, 20);
            this.txtThuMucBuildVisionTrading.TabIndex = 40;
            this.txtThuMucBuildVisionTrading.Text = "C:\\ProjectCode\\VisionTrading\\NightyBuild\\LastestBuild\\Worker";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 253);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(307, 13);
            this.label7.TabIndex = 42;
            this.label7.Text = "Bước 1: Chọn đường dẫn build và đường dẫn cập nhật Product";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(24, 271);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(502, 13);
            this.label9.TabIndex = 43;
            this.label9.Text = "Bước 2: Click đổi tên Service để Tạo ra 1 thư mục WorkerNew nằm cạnh đường dẫn bu" +
    "ild VisionTrading";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(24, 292);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(535, 13);
            this.label11.TabIndex = 44;
            this.label11.Text = "Bước 3: Click copy các file từ thư mục cập nhật Product. Sẽ copy 1 số file cơ bản" +
    " vào thư mục mới tạo ở bước 2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 463);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtThuMucBuildVisionTrading);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnCopyLinkCompareToService);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtLinkCompare);
            this.Controls.Add(this.btnChangeNameService);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnCopyAllFile);
            this.Controls.Add(this.btnCopyFileDll);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPathService);
            this.Controls.Add(this.txtPathConfig);
            this.Controls.Add(this.btnCopyFileConfig);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.btnDecryptForder);
            this.Controls.Add(this.btnEncryptForder);
            this.Controls.Add(this.btnDecrypt);
            this.Controls.Add(this.btnEncrypt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Encrypt - Decrypt Config";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.OpenFileDialog openDialog;
        private System.Windows.Forms.SaveFileDialog saveDialog;
        private System.Windows.Forms.Button btnEncryptForder;
        private System.Windows.Forms.Button btnDecryptForder;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPathService;
        private System.Windows.Forms.TextBox txtPathConfig;
        private System.Windows.Forms.Button btnCopyFileConfig;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCopyFileDll;
        private System.Windows.Forms.Button btnCopyAllFile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnChangeNameService;
        private System.Windows.Forms.TextBox txtLinkCompare;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnCopyLinkCompareToService;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtThuMucBuildVisionTrading;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
    }
}
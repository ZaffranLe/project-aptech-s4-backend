using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Anotar.NLog;

namespace BaseUtils
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    var fileOpen = openDialog.FileName;
                    var fullText = File.ReadAllText(fileOpen);
                    fullText = EncryptUtils.Encrypt(fullText, System.Text.Encoding.Unicode);

                    //save de len file cu
                    var fileSave = openDialog.FileName;
                    File.WriteAllText(fileSave, fullText);
                }
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            try
            {
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    var fileOpen = openDialog.FileName;
                    var fullText = File.ReadAllText(fileOpen);
                    fullText = EncryptUtils.Decrypt(fullText, System.Text.Encoding.Unicode);

                    //save de len file cu
                    var fileSave = openDialog.FileName;
                    File.WriteAllText(fileSave, fullText);
                }
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
        }

        private void btnEncryptForder_Click(object sender, EventArgs e)
        {
            try
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();
                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        var forderPath = fbd.SelectedPath;
                        string[] listFilesConfigs = Directory.GetFiles(forderPath, "*.qes", SearchOption.AllDirectories);
                        foreach (var filesConfig in listFilesConfigs)
                        {
                            var fullText = File.ReadAllText(filesConfig);
                            if (!fullText.Trim().StartsWith("{")) continue; //Đảm bảo chỉ encrypt 1 lần

                            //var forderEncrypt = forderPath + "Encrypt\\";
                            //if (!Directory.Exists(forderEncrypt))
                            //    Directory.CreateDirectory(forderEncrypt);
                            //var fileName = GetFileName(filesConfig);
                            //var fileSave = forderEncrypt + fileName;

                            //Thay the luon file cu
                            var fileSave = filesConfig;
                            fullText = EncryptUtils.Encrypt(fullText, System.Text.Encoding.Unicode);

                            if (File.Exists(fileSave))
                                File.Delete(fileSave);
                            File.WriteAllText(fileSave, fullText);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
        }

        private void btnDecryptForder_Click(object sender, EventArgs e)
        {
            try
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();
                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        var forderPath = fbd.SelectedPath;
                        string[] listFilesConfigs = Directory.GetFiles(forderPath, "*.qes", SearchOption.AllDirectories);

                        DirectoryInfo parentForder = Directory.GetParent(forderPath);
                        if (parentForder.Parent != null)
                            forderPath = parentForder.Parent.FullName + "\\"; //Lùi lại 1 cấp forder
                        foreach (var filesConfig in listFilesConfigs)
                        {
                            var fullText = File.ReadAllText(filesConfig);
                            if (fullText.Trim().StartsWith("{")) continue; //Đảm bảo chỉ decrypt 1 lần

                            //var forderDecrypt = forderPath + "Decrypt\\";
                            //if (!Directory.Exists(forderDecrypt))
                            //    Directory.CreateDirectory(forderDecrypt);
                            //var fileName = GetFileName(filesConfig);
                            //var fileSave = forderDecrypt + fileName;

                            //Thay the luon file cu
                            var fileSave = filesConfig;
                            fullText = EncryptUtils.Decrypt(fullText, System.Text.Encoding.Unicode);

                            if (File.Exists(fileSave))
                                File.Delete(fileSave);
                            File.WriteAllText(fileSave, fullText);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
        }

        private string GetFileName(string path)
        {
            try
            {
                int lastIndex = path.LastIndexOf(@"\", StringComparison.Ordinal);
                string fileName = path.Substring(lastIndex + 1, path.Length - lastIndex - 1);
                return fileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"error" + ex);
            }
            return "";
        }

        private void btnCopyFileConfig_Click(object sender, EventArgs e)
        {
            try
            {
                //Đọc tất cả các file config chứa trong thư mục
                string pathFileConfig = txtPathConfig.Text;
                string[] listFilesConfigs = Directory.GetFiles(pathFileConfig, "*.qes", SearchOption.AllDirectories);
                //Đọc tất cả các file config chứa trong thư mục tới
                string pathFile = txtPathService.Text;
                string[] listFiles = Directory.GetFiles(pathFile, "*.qes", SearchOption.AllDirectories);

                long totalCopy = 0;
                //Tìm trong thư mục hiện tại xem
                foreach (var fullPath in listFiles)
                {
                    //Lấy tên file
                    string fileName = GetFileName(fullPath);
                    if (string.IsNullOrEmpty(fileName))
                    {
                        MessageBox.Show(@"Khong lay duoc ten file. Vui long kiem tra lai.");
                        return;
                    }

                    foreach (var fullPathConfig in listFilesConfigs)
                    {
                        //Xem có file nào giống thì copy 
                        if (fullPathConfig.Contains(fileName))
                        {
                            //backup lại file theo ngày
                            string fileNameBackup = fileName + DateTime.Now.ToString("yyyyMMdd");
                            string backupPath = fullPath.Replace(fileName, "");
                            backupPath = backupPath + @"BackupConfig\";
                            if (!Directory.Exists(backupPath))
                            {
                                Directory.CreateDirectory(backupPath);
                            }
                            backupPath = backupPath + fileNameBackup;
                            File.Copy(fullPathConfig, backupPath, true);

                            //copy đè file config mới
                            File.Copy(fullPathConfig, fullPath, true);
                            totalCopy++;
                            break;
                        }
                    }
                }

                MessageBox.Show(string.Format("Copy thanh cong {0} file config trong thu muc {1} \r\n " +
                                              "thay cho {2} file config trong thu muc {3}",
                    listFilesConfigs.Length, pathFileConfig, totalCopy, pathFile));
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"error" + ex);
            }
        }

        private void btnCopyFileDll_Click(object sender, EventArgs e)
        {
            try
            {
                /* Lam nhiem vu copy toan bo file dll tu "thu muc config" vao "thu muc chua service"
                 */

                //Đọc tất cả các file config chứa trong thư mục
                string pathFileConfig = txtPathConfig.Text;
                string[] listFilesConfigs = Directory.GetFiles(pathFileConfig, "*.dll", SearchOption.AllDirectories);
                //Đọc tất cả các file config chứa trong thư mục tới
                string pathFile = txtPathService.Text;
                string[] listFiles = Directory.GetFiles(pathFile, "*.dll", SearchOption.AllDirectories);

                var totalCopy = 0;
                //Tìm trong thư mục hiện tại xem
                foreach (var fullPath in listFiles)
                {
                    //Lấy tên file
                    string fileName = GetFileName(fullPath);
                    if (string.IsNullOrEmpty(fileName))
                    {
                        MessageBox.Show(@"Khong lay duoc ten file. Vui long kiem tra lai.");
                        return;
                    }

                    foreach (var fullPathConfig in listFilesConfigs)
                    {
                        //Xem có file nào giống thì copy 
                        if (fullPathConfig.Contains(fileName))
                        {
                            //backup lại file theo ngày
                            string fileNameBackup = fileName + DateTime.Now.ToString("yyyyMMdd");
                            string backupPath = fullPath.Replace(fileName, "");
                            backupPath = backupPath + @"BackupDll\";
                            if (!Directory.Exists(backupPath))
                            {
                                Directory.CreateDirectory(backupPath);
                            }
                            backupPath = backupPath + fileNameBackup;
                            File.Copy(fullPathConfig, backupPath, true);

                            //copy đè file config mới
                            File.Copy(fullPathConfig, fullPath, true);
                            totalCopy++;
                            break;
                        }
                    }
                }

                MessageBox.Show(string.Format("Copy thanh cong {0} file DLL trong thu muc {1} \r\n " +
                                              "thay cho {2} file DLL trong thu muc {3}",
                    listFilesConfigs.Length, pathFileConfig, totalCopy, pathFile));
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"error" + ex);
            }
        }

        private void btnCopyAllFile_Click(object sender, EventArgs e)
        {
            try
            {
                /* Lam nhiem vu copy all file tu thu muc config sang thu muc chua service
                 */

                //Đọc tất cả các file config chứa trong thư mục
                string pathFileConfig = txtPathConfig.Text;
                string[] listFilesConfigs = Directory.GetFiles(pathFileConfig, "*.*", SearchOption.AllDirectories);
                //Đọc tất cả các file config chứa trong thư mục tới
                string pathFile = txtPathService.Text;
                string[] listFiles = Directory.GetFiles(pathFile, "*.*", SearchOption.AllDirectories);

                var totalCopy = 0;
                //Tìm trong thư mục hiện tại xem
                foreach (var fullPath in listFiles)
                {
                    //Lấy tên file
                    string fileName = GetFileName(fullPath);
                    if (string.IsNullOrEmpty(fileName))
                    {
                        MessageBox.Show(@"Khong lay duoc ten file. Vui long kiem tra lai.");
                        return;
                    }

                    foreach (var fullPathConfig in listFilesConfigs)
                    {
                        //Xem có file nào giống thì copy 
                        if (fullPathConfig.Contains(fileName))
                        {
                            //copy đè file config mới
                            File.Copy(fullPathConfig, fullPath, true);
                            totalCopy++;
                            break;
                        }
                    }
                }

                MessageBox.Show(string.Format("Copy thanh cong {0} file trong thu muc {1} \r\n " +
                                              "thay cho {2} file trong thu muc {3}",
                    listFilesConfigs.Length, pathFileConfig, totalCopy, pathFile));
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"error" + ex);
            }
        }

        private void btnGetAllFileDll_Click(object sender, EventArgs e)
        {
            try
            {
                /* Lam nhiem vu : Lay all file config tu "thu muc chua service" vao "thu muc chua file config"
                 * Buoc 1 da tao san nen ko phai lam dieu nay
                 */

                //Đọc tất cả các file config chứa trong thư mục
                string pathFileConfig = txtPathConfig.Text;
                //Đọc tất cả các file config chứa trong thư mục tới
                string pathFile = txtPathService.Text;
                string[] listFiles = Directory.GetFiles(pathFile, "*.dll", SearchOption.AllDirectories);

                var totalCopy = 0;
                //Tìm trong thư mục hiện tại xem
                //File nao lay roi thi bo qua
                var dicFileCopy = new Dictionary<string, string>();
                foreach (var fullPath in listFiles)
                {
                    //Bo qua thu muc stunnel
                    if (fullPath.Contains("\\stunnel\\")) continue;


                    //Lấy tên file
                    string fileName = GetFileName(fullPath);
                    if (string.IsNullOrEmpty(fileName))
                    {
                        MessageBox.Show(@"Khong lay duoc ten file. Vui long kiem tra lai.");
                        return;
                    }
                    if (dicFileCopy.ContainsKey(fileName)) continue; //Da copy roi thi bo qua (Do cac file giong nhau)

                    var newFile = pathFileConfig + "\\" + fileName;
                    //copy đè file config mới
                    File.Copy(fullPath, newFile, true);
                    totalCopy++;
                    dicFileCopy[fileName] = fileName;
                }

                MessageBox.Show(string.Format("Copy thanh cong {0} file DLL trong thu muc {1} \r\n " +
                                              "vao thu muc {2}",
                    totalCopy, pathFile, pathFileConfig));
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"error" + ex);
            }
        }

        #region Copy folder

        private void btnChangeNameService_Click(object sender, EventArgs e)
        {
            try
            {
                /* Lam nhiem vu doc "thu muc chua service" 
                 * sau do tao ra 1 thu muc WorkerNewCopy co dinh dang giong thu muc LinkCompare
                 * Copy toan bo thong tin dll moi nhat sang thu muc moi
                 * Sau khi chay thanh cong thi chuyen sang buoc 2
                 */

                //Đọc tất cả các file config chứa trong thư mục
                string pathThuMucBuildVisionTrading = txtThuMucBuildVisionTrading.Text; //Dia chi thu muc goc
                //string[] allFile = Directory.GetFiles(pathFileConfig, "*.*", SearchOption.AllDirectories);

                if (!Directory.Exists(pathThuMucBuildVisionTrading))
                {
                    MessageBox.Show("Khong ton tai duong dan : " + pathThuMucBuildVisionTrading);
                    return;
                }
                if (!Directory.Exists(txtLinkCompare.Text))
                {
                    MessageBox.Show("Khong ton tai duong dan : " + txtLinkCompare.Text);
                    return;
                }

                //Đọc tất cả các file config chứa trong thư mục se copy toan bo file sang
                //Neu ton tai thi xoa
                var newCopyPath = pathThuMucBuildVisionTrading + "NewCopy";
                if (Directory.Exists(newCopyPath))
                    Directory.Delete(newCopyPath, true);
                Directory.CreateDirectory(newCopyPath);

                var server28Check = txtLinkCompare.Text + "\\DC\\Server28";
                var server31Check = txtLinkCompare.Text + "\\DC\\Server31";
                var server90Check = txtLinkCompare.Text + "\\DC\\Server90";
                var server108Check = txtLinkCompare.Text + "\\DC\\Server108";

                var serverDr88Check = txtLinkCompare.Text + "\\DR\\10.96.128.88";
                var serverDr161Check = txtLinkCompare.Text + "\\DR\\10.98.1.161";

                //Lay all folder trong thu muc Worker
                var allFolder = Directory.GetDirectories(pathThuMucBuildVisionTrading);
                foreach (var folderLink in allFolder)
                {
                    var folderInfo = new DirectoryInfo(folderLink);
                    if (folderInfo.Name.StartsWith("Runner."))
                    {
                        var nameNotRunner = folderInfo.Name.Replace("Runner.", "");
                        var workerNameDc = "QE VT Worker";
                        var workerNameDr = "QE_VT_Worker_";

                        #region Lay thong tin folder moi

                        //Doi ten thu muc
                        //Vi du : Runner.AccountManager thanh QE_VT_Worker_AccountManager va QE VT Worker Account Manager
                        //Tach AccountManager thanh 2 tu
                        var lstName = new List<string>();
                        var currentText = "";
                        for (int i = 0; i < nameNotRunner.Length; i++)
                        {
                            var kyTu = nameNotRunner[i];
                            if (char.IsUpper(kyTu))
                            {
                                //Bat dau la chu cai hoa
                                if (!string.IsNullOrEmpty(currentText))
                                {
                                    lstName.Add(currentText);
                                    currentText = "";
                                }
                                currentText += kyTu;
                            }
                            else
                            {
                                //Neu la chu cai thuong thi 
                                currentText += kyTu;
                            }

                            if (i == nameNotRunner.Length - 1)
                                lstName.Add(currentText);
                        }

                        foreach (var str in lstName)
                        {
                            workerNameDc += " " + str;
                            workerNameDc = workerNameDc.Trim();

                            workerNameDr += str;
                            workerNameDr = workerNameDr.Trim();
                        }

                        #endregion

                        #region Dieu chinh 1 so truong hop dac biet

                        if (workerNameDc.Contains("Pre") || workerNameDc.Contains("Connector"))
                        {

                        }

                        if (workerNameDc.Contains("Post Trade"))
                        {
                            workerNameDc = workerNameDc.Replace("Post Trade", "PostTrade");
                            workerNameDr = workerNameDr.Replace("Post_Trade", "PostTrade");
                        }
                        if (workerNameDc.Contains("C Q G"))
                        {
                            workerNameDc = workerNameDc.Replace("C Q G", "CQG");
                            workerNameDr = workerNameDr.Replace("C_Q_G", "CQG");
                        }

                        if (workerNameDc.Contains("A P I"))
                        {
                            workerNameDc = workerNameDc.Replace("A P I", "API");
                            workerNameDr = workerNameDr.Replace("A_P_I", "API");
                        }
                        if (workerNameDc.Contains("Web Api"))
                        {
                            workerNameDc = workerNameDc.Replace("Web Api", "Web API");
                            workerNameDr = workerNameDr.Replace("Web Api", "Web API");
                        }
                        if (workerNameDc.Contains("I N T L F C"))
                        {
                            workerNameDc = workerNameDc.Replace("I N T L F C", "INTLFC");
                            workerNameDr = workerNameDr.Replace("I_N_T_L_F_C", "INTLFC");
                        }
                        if (workerNameDc.Contains("Pre Trade"))
                        {
                            workerNameDc = workerNameDc.Replace("Pre Trade", "PreTrade");
                            workerNameDr = workerNameDr.Replace("Pre_Trade", "PreTrade");
                        }
                        if (workerNameDr.Contains("Span"))
                        {
                            //Tao name sai quy cach
                            workerNameDr = workerNameDr.Replace("_SpanManager", " Span Manger");
                        }

                        #endregion

                        #region Check thong tin cac server check xem co worker do khong thi dua vao

                        if (workerNameDc.Contains("Pre") || workerNameDc.Contains("Connector"))
                        {

                        }
                        CopyWorker(server28Check, workerNameDc, workerNameDr,
                            txtLinkCompare.Text, folderLink, newCopyPath);
                        CopyWorker(server31Check, workerNameDc, workerNameDr,
                            txtLinkCompare.Text, folderLink, newCopyPath);
                        CopyWorker(server90Check, workerNameDc, workerNameDr,
                            txtLinkCompare.Text, folderLink, newCopyPath);
                        CopyWorker(server108Check, workerNameDc, workerNameDr,
                            txtLinkCompare.Text, folderLink, newCopyPath);

                        CopyWorker(serverDr88Check, workerNameDc, workerNameDr,
                            txtLinkCompare.Text, folderLink, newCopyPath);
                        CopyWorker(serverDr161Check, workerNameDc, workerNameDr,
                            txtLinkCompare.Text, folderLink, newCopyPath);

                        #endregion
                    }
                }

                MessageBox.Show("Thuc hien copy thanh cong den folder : " + newCopyPath);

            }
            catch (Exception ex)
            {
                MessageBox.Show(@"error" + ex);
            }
        }

        private static bool CopyWorker(string serverNameCheck, string workerNameDc, string workerNameDr,
            string folderCheck, string folderGoc, string folderNew)
        {
            try
            {
                bool isDc;
                if (IsExistWorker(serverNameCheck, workerNameDc, workerNameDr, out isDc))
                {
                    //Neu ton tai
                    var folderAdd = serverNameCheck.Replace(folderCheck, folderNew);
                    if (isDc)
                    {
                        folderAdd = folderAdd + "\\" + workerNameDc;
                        DirectoryCopy(folderGoc, folderAdd, true);
                        return true;
                    }
                    else
                    {
                        folderAdd = folderAdd + "\\" + workerNameDr;
                        DirectoryCopy(folderGoc, folderAdd, true);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"error" + ex);
            }
            return false;
        }

        private static Dictionary<string, List<string>> _dicFolderLink = new Dictionary<string, List<string>>();
        private static Dictionary<string, DirectoryInfo> _dicFolderName = new Dictionary<string, DirectoryInfo>();

        private static bool IsExistWorker(string serverNameCheck, string nameDc, string nameDr, out bool isDc)
        {
            isDc = true;
            try
            {
                if (!Directory.Exists(serverNameCheck))
                    return false; //Bo qua xu ly cho link nay

                List<string> allFolder = new List<string>();
                if (!_dicFolderLink.ContainsKey(serverNameCheck))
                {
                    var allF = Directory.GetDirectories(serverNameCheck);
                    _dicFolderLink[serverNameCheck] = allF.ToList();
                }
                allFolder = _dicFolderLink[serverNameCheck];

                foreach (var folderLink in allFolder)
                {
                    DirectoryInfo folderInfo = null;
                    if (!_dicFolderName.ContainsKey(folderLink))
                    {
                        var folder = new DirectoryInfo(folderLink);
                        _dicFolderName[folderLink] = folder;
                    }
                    folderInfo = _dicFolderName[folderLink];
                    if (folderInfo.Name == nameDc)
                    {
                        isDc = true;
                        return true;
                    }
                    if (folderInfo.Name == nameDr)
                    {
                        isDc = false;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"error" + ex);
                MessageBox.Show(
                    "Dung lai chay thong tin sau thong bao nay! Duong dan truyen vao fail serverNameCheck = " +
                    serverNameCheck);
                Process.GetCurrentProcess().Kill();
            }
            return false;
        }

        private static void DirectoryCopy(
            string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the source directory does not exist, throw an exception.
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory does not exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }


            // Get the file contents of the directory to copy.
            FileInfo[] files = dir.GetFiles();

            foreach (FileInfo file in files)
            {
                // Create the path to the new copy of the file.
                string temppath = Path.Combine(destDirName, file.Name);

                // Copy the file.
                file.CopyTo(temppath, false);
            }

            // If copySubDirs is true, copy the subdirectories.
            if (copySubDirs)
            {

                foreach (DirectoryInfo subdir in dirs)
                {
                    // Create the subdirectory.
                    string temppath = Path.Combine(destDirName, subdir.Name);

                    // Copy the subdirectories.
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        #endregion

        private void btnCopyLinkCompareToService_Click(object sender, EventArgs e)
        {
            try
            {
                //Do cau truc thu muc giong nhau sau khi chay buoc 1
                //--> Su dung buoc 2 copy 1 so file co ban de khong phai mat cong beyoncompare tung phan
                string pathFile = txtLinkCompare.Text;
                string[] listFiles = Directory.GetFiles(pathFile, "*.*", SearchOption.AllDirectories);
                var lstFileCopy = new List<string>();
                foreach (var filePath in listFiles)
                {
                    string fileName = GetFileName(filePath);
                    if (string.IsNullOrEmpty(fileName))
                    {
                        MessageBox.Show(@"Khong lay duoc ten file. Vui long kiem tra lai.");
                        return;
                    }

                    if(fileName.Equals("Logo.ico"))
                        lstFileCopy.Add(filePath); //Lay all file ico
                    else if(fileName.Equals("Worker.dll"))
                        lstFileCopy.Add(filePath); //Lay all file ico
                    else if(fileName.Equals("VisionTradingWorker.dll"))
                        lstFileCopy.Add(filePath); //Lay all file ico
                    else if (fileName.Equals("BrokerConfig.xml"))
                        lstFileCopy.Add(filePath); //Lay all file broker config
                    else if (fileName.StartsWith("QE_VT_"))
                        lstFileCopy.Add(filePath); //Lay all file chay service
                    else
                    {
                        
                    }
                }

                var pathService = txtPathService.Text; 
                if (!pathService.EndsWith("NewCopy"))
                    pathService += "NewCopy";//Thu muc moi neu link chua co (Dung cho luoi thay doi)
                var pathCompare = txtLinkCompare.Text;
                foreach (var path in lstFileCopy)
                {
                    //Check thong tin dia chi file
                    var newPath = path.Replace(pathCompare, pathService);
                    if (!File.Exists(newPath))
                    {
                        //Neu chua folder nay thi van cho thuc hien
                        //int indexEnd = newPath.LastIndexOf("\\", StringComparison.Ordinal);
                        //var newFolder = newPath.Substring(0, indexEnd);
                        var currentDirec = Directory.GetParent(newPath); //dia chi thu muc hien tai
                        if(Directory.Exists(currentDirec.FullName))
                            File.Copy(path, newPath, true);
                        else
                        {
                            //MessageBox.Show("Khong ton tai file path = " + newPath);
                            continue;
                        }
                    }

                    //Copy thong tin file
                    File.Copy(path, newPath, true);
                }

                MessageBox.Show(string.Format("Da copy xong {0} file den thu muc {1}", 
                    lstFileCopy.Count, pathService));
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"error" + ex);
            }
        }
    }

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}

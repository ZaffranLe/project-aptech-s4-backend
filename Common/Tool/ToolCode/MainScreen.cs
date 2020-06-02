using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CommonicationMemory.CodeGeneration;
using CommonicationMemory.Common;
using CommonicationMemory.Config;
using CommonicationMemory.Properties;

namespace CommonicationMemory
{
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Text;

    public partial class MainScreen : Form
    {
        #region Constructor

        public MainScreen()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Methods

        private void PopulateTablesGrid()
        {
            List<DatabaseTable> listDatabaseTables = TierGeneratorSettings.Instance.Database.Tables;
            listDatabaseTables.Sort((l, r) => String.Compare(l.TableName, r.TableName, StringComparison.Ordinal));

            gvTables.DataSource = listDatabaseTables.FindAll(x => x.TableType != "VIEW");
        }

        private void GeneraterCode(bool check = false)
        {
            if (!check)
                if (!ValidateData()) return;

            // Set the Tier Generator Setting Class
            SetSettingObject();

            if (!check)
            {
                // Generate Business Layer
                var blgenerator = new BusinessLayerGenerator();
                blgenerator.Generate();
            }

            // Generate Other CommonFiles
            var cfGenerator = new StoreProceduceGenerator();

            //cfGenerator.GenerateAppDotConfig(); // App.config
            cfGenerator.GenerateSqlStoreProcedures(check); // store procedures 

            MessageBox.Show("Successfully generated.", "Code generation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void GeneraterDrop(bool check = false)
        {
            if (!check)
                if (!ValidateData()) return;

            // Set the Tier Generator Setting Class
            SetSettingObject();

            // Generate Other CommonFiles
            var cfGenerator = new StoreProceduceGenerator();

            //cfGenerator.GenerateAppDotConfig(); // App.config
            cfGenerator.GenerateSqlStoreProceduresDrop(check); // store procedures 

            MessageBox.Show("Successfully generated.", "Code generation", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void GeneraterCreateAll(bool check = false)
        {
            if (!check)
                if (!ValidateData()) return;

            // Set the Tier Generator Setting Class
            SetSettingObject();

            // Generate Other CommonFiles
            var cfGenerator = new StoreProceduceGenerator();

            //cfGenerator.GenerateAppDotConfig(); // App.config
            cfGenerator.GenerateSqlStoreProceduresCreateAll(check); // store procedures 

            MessageBox.Show("Successfully generated.", "Code generation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool ValidateData()
        {
            string errorMsg = string.Empty;

            // Get the tables count....
            if (GetSelectedTableCount() == 0)
            {
                errorMsg += "Please select the tables.\n";
            }

            // Check Project NAme
            if (txtProjectName.Text.Trim().Length == 0)
            {
                errorMsg += "Please provide Project Name.\n";
            }

            // Check 
            if (txtOutputDir.Text.Trim().Length == 0)
            {
                errorMsg += "Please provide Output directory.\n";
            }

            if (errorMsg != string.Empty)
            {
                MessageBox.Show(errorMsg, "Data required for code generation.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            return true;
        }

        private void SetSettingObject()
        {
            var setting = TierGeneratorSettings.Instance;

            setting.CodeGenerationPath = txtOutputDir.Text.Trim();
            setting.ClassPrefix = txtClassPrefix.Text.Trim();
            setting.ProjectNameSpace = txtProjectName.Text.Trim();
            setting.StoreProcedurePrefix = txtSpPrefix.Text.Trim();

            setting.GenerateBusinessLayer = chkBL.Checked;
            setting.FoudationLink = txtFoudationLink.Text.Trim();
            setting.MemoryWorkerBaseLink = txtMemoryWorkerBase.Text.Trim();
            setting.CheckGenByForder = chkGeneratorByFolder.Checked;
        }

        private int GetSelectedTableCount()
        {
            int count = 0;
            var database = TierGeneratorSettings.Instance.Database;

            if (database != null && database.Tables.Count > 0)
            {
                foreach (var table in database.Tables)
                {
                    if (table.IsSelected) count++;
                }

            }
            return count;
        }

        private void LoadDefaultValues()
        {
            txtProjectName.Text = ConfigGlobal.SettingConfig.Setting_ProjectName;
            txtClassPrefix.Text = ConfigGlobal.SettingConfig.Setting_ClassPrefix;
            txtSpPrefix.Text = ConfigGlobal.SettingConfig.Setting_SpPrefix;
            txtOutputDir.Text = ConfigGlobal.SettingConfig.Setting_OutputDirectory;
            txtMemoryWorkerBase.Text = ConfigGlobal.SettingConfig.Setting_MemoryWorkerBase;
            txtFoudationLink.Text = ConfigGlobal.SettingConfig.Setting_FoudationLink;
            chkGeneratorByFolder.Checked = ConfigGlobal.SettingConfig.Setting_CheckGenByForder;
        }

        private void SaveSetting()
        {
            ConfigGlobal.SettingConfig.Setting_ProjectName = txtProjectName.Text;
            ConfigGlobal.SettingConfig.Setting_ClassPrefix = txtClassPrefix.Text;
            ConfigGlobal.SettingConfig.Setting_SpPrefix = txtSpPrefix.Text;
            ConfigGlobal.SettingConfig.Setting_OutputDirectory = txtOutputDir.Text;
            ConfigGlobal.SettingConfig.Setting_FoudationLink = txtFoudationLink.Text;
            ConfigGlobal.SettingConfig.Setting_MemoryWorkerBase = txtMemoryWorkerBase.Text;
            ConfigGlobal.SettingConfig.Setting_CheckGenByForder = chkGeneratorByFolder.Checked;
            ConfigGlobal.Save();
        }

        #endregion

        #region Events

        private void MainScreen_Shown(object sender, EventArgs e)
        {
            GetDicKey();

            var loginScreen = new LoginScreen();
            var result = loginScreen.ShowDialog();

            if (result == DialogResult.OK)
            {
                LoadDataBase();
            }
            else
            {
                Application.Exit();
            }
        }

        private void LoadDataBase()
        {
            if (OracleHelper.IsConectOracle)
            {
                button2.Enabled = false;
                LoadOracle();
            }
            else
            {
                PopulateTablesGrid();
            }
            LoadDefaultValues();
        }

        private void LoadOracle()
        {
            //Đọc thông tin file text auto number
            var dicAll = OracleHelper.GetAllTableColumns();
            DataTable dt = OracleHelper.GetDt("select table_name from user_tables order by table_name");
            var listDatabaseTables = new List<DatabaseTable>();
            var dic = TierGeneratorSettings.Instance.DicEntites;
            var dicFields = TierGeneratorSettings.Instance.DicEntites;

            foreach (DataRow row in dt.Rows)
            {
                var isSelect = false;
                var tableName = row["TABLE_NAME"].ToString();
                foreach (var dicField in dicFields)
                {
                    if (dicField.Key.Equals(tableName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        tableName = dicField.Key;
                    }
                }

                if (dic.ContainsKey(tableName))
                    isSelect = true;

                if (!dicAll.ContainsKey(tableName)) continue;
                var listCol = dicAll[tableName];

                listDatabaseTables.Add(new DatabaseTable
                {
                    TableName = tableName,
                    IsSelected = isSelect,
                    TableType = "TABLE",
                    Columns = listCol.OrderBy(x => x.Name).ToList(),
                });
            }

            if (TierGeneratorSettings.Instance.Database == null)
                TierGeneratorSettings.Instance.Database = new Database();
            TierGeneratorSettings.Instance.Database.Tables = listDatabaseTables;
            gvTables.DataSource = listDatabaseTables.FindAll(x => x.TableType != "VIEW");
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            int i = 0;
            foreach (DataGridViewRow row in gvTables.Rows)
            {
                row.Cells["IsSelected"].Value = chkSelectAll.Checked;
                gvTables.UpdateCellValue(0, i);
                i++;
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                //Doc lai file entity
                ReloadListFileEntity();

                GeneraterCode();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error to generate code.\n" + ex.Message, "Error to generate code.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SaveSetting();
        }

        private void ReloadListFileEntity()
        {
            if (string.IsNullOrWhiteSpace(ConfigGlobal.SettingConfig.Setting_EntityPath)) return;
            var listFile = ReadDirectory(ConfigGlobal.SettingConfig.Setting_EntityPath, ".cs");
            if (TierGeneratorSettings.Instance.DicEntites == null)
                TierGeneratorSettings.Instance.DicEntites = new Dictionary<string, Dictionary<string, string>>();

            var dic = new Dictionary<string, Dictionary<string, string>>();
            foreach (var file in listFile)
            {
                var fileName = file.Key.Replace(".cs", "");
                dic[fileName] = new Dictionary<string, string>();
                //Lấy danh sách fields

                if (File.Exists(file.Value))
                {
                    using (var readStream = new StreamReader(file.Value))
                    {
                        string line;
                        var isBeginReadFields = false;
                        var listString = new List<string>();
                        while ((line = readStream.ReadLine()) != null)
                        {
                            if (string.IsNullOrWhiteSpace(line)) continue;
                            if (line.Contains("public enum"))
                            {
                                isBeginReadFields = true;
                                continue;
                            }
                            if (!isBeginReadFields) continue;
                            if (line.Contains("{")) continue;
                            if (line.Contains("}"))
                            {
                                isBeginReadFields = false;
                                continue;
                            }
                            listString.Add(line.Replace(",", "").Trim());
                        }
                        var dicTmp = dic[fileName];
                        foreach (var str in listString)
                        {
                            dicTmp[str] = str;
                        }
                        dic[fileName] = dicTmp;
                        readStream.Close();
                    }
                    
                }
            }

            TierGeneratorSettings.Instance.DicEntites = dic;
        }

        private void btnOpenDir_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtOutputDir.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void GetDicKey()
        {
            var url = Application.StartupPath + @"\Config\AutoGenNumber.txt";
            if (!File.Exists(url))
            {
                return;
            }

            using (var readStream = new StreamReader(url))
            {
                string line;
                var dic = new Dictionary<string, List<string>>();
                while ((line = readStream.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    var listTmp = line.Split(':').ToList();
                    if (listTmp.Count < 2) continue;
                    if (!dic.ContainsKey(listTmp[0]))
                        dic[listTmp[0]] = new List<string> { listTmp[1] };
                    else
                        dic[listTmp[0]].Add(listTmp[1]);
                }
                OracleHelper.DicAutoKey = dic;
                readStream.Close();
            }
                
        }

        #endregion

        private void txtSearchTable_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string search = txtSearchTable.Text;
                var listDatabaseTables = TierGeneratorSettings.Instance.Database.Tables;
                listDatabaseTables.Sort((l, r) => String.Compare(l.TableName, r.TableName, StringComparison.Ordinal));

                if (string.IsNullOrEmpty(search))
                {
                    gvTables.DataSource = listDatabaseTables.FindAll(t => t.TableType != "VIEW");
                    return;
                }
                //L?c theo table
                var list = listDatabaseTables;
                list = list.FindAll(t => t.TableName.ToLower().Contains(search.ToLower()) && t.TableType != "VIEW");
                gvTables.DataSource = list;
            }
            catch (Exception ex)
            {

            }
        }

        private void chkGenKey_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                TierGeneratorSettings.Instance.GenObjectKeys = chkGenKey.Checked;
            }
            catch (Exception ex)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                GeneraterCode(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error to generate code.\n" + ex.Message, "Error to generate code.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SaveSetting();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            // Set the Tier Generator Setting Class
            SetSettingObject();

            // Generate Business Layer
            var blgenerator = new BusinessLayerGenerator();
            blgenerator.Generate();

            // Generate Other CommonFiles
            var cfGenerator = new StoreProceduceGenerator();

            //cfGenerator.GenerateAppDotConfig(); // App.config
            cfGenerator.GenerateSqlAutoNumber(); // store procedures 

            MessageBox.Show("Successfully generated.", "Code generation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private Dictionary<string, string> ReadDirectory(string targetDirectory, string endWith)
        {
            var dicFileName = new Dictionary<string, string>();
            if (string.IsNullOrWhiteSpace(targetDirectory)) return dicFileName;
            if (!Directory.Exists(targetDirectory)) return dicFileName;

            var fileEntries = Directory.GetFiles(targetDirectory);
            foreach (var fileName in fileEntries)
            {
                if (!fileName.EndsWith(endWith)) continue;
                var fileN = Path.GetFileName(fileName);
                if (fileN != null)
                    dicFileName[fileN] = fileName;
            }

            var subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (var subdirectory in subdirectoryEntries)
            {
                var listTemp = ReadDirectory(subdirectory, endWith);
                if (listTemp != null && listTemp.Count > 0)
                {
                    foreach (var fileName in listTemp)
                    {
                        dicFileName[fileName.Key] = fileName.Value;
                    }
                }
            }
            return dicFileName;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                GeneraterDrop();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error to generate code.\n" + ex.Message, "Error to generate code.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SaveSetting();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            button3.Visible = checkBox1.Checked;
            button1.Visible = checkBox1.Checked;
            button2.Visible = checkBox1.Checked;
            button4.Visible = checkBox1.Checked;
            button5.Visible = checkBox1.Checked;
            chkGenProcess.Visible = checkBox1.Checked;
            chkUseMaxKey.Visible = checkBox1.Checked;
            chkGenKey.Visible = checkBox1.Checked;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                GeneraterCreateAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error to generate code.\n" + ex.Message, "Error to generate code.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SaveSetting();
        }

        private void chkGenProcess_CheckedChanged(object sender, EventArgs e)
        {
            TierGeneratorSettings.Instance.GenProcess = chkGenProcess.Checked;
        }

        private void chkUseSequence_CheckedChanged(object sender, EventArgs e)
        {
            TierGeneratorSettings.Instance.UseMaxKey = chkUseMaxKey.Checked;
        }

        public void LoadConfig()
        {
            var path = Application.StartupPath + @"\Config\Config.txt";
        }
    }
}
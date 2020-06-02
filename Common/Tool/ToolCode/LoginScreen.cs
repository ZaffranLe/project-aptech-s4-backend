using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CommonicationMemory.Common;
using CommonicationMemory.Config;
using CommonicationMemory.DatabaseSchema;
using CommonicationMemory.Properties;
using MySql.Data.MySqlClient;

namespace CommonicationMemory
{
    public partial class LoginScreen : Form
    {

        Database _database = null;
        string _connectionString = string.Empty;

        #region Constructor

        public LoginScreen()
        {
            InitializeComponent();

        }

        #endregion

        #region Properties


        /// <summary>
        /// get the databse schema
        /// </summary>
        public Database Database
        {
            get { return _database; }
        }


        #endregion

        #region Private Methods

        /// <summary>
        /// Method to validate the forms
        /// </summary>
        /// <returns>true for valid Data</returns>
        private bool IsValidate()
        {
            //errorProvider1.Clear();
            bool result = true;

            // Sql Server Name
            if (txtSqlServer.Text.Trim().Length <= 0)
            {
                errorProvider1.SetError(txtSqlServer, "Please Provide SQL Server.");
                result = false;
            }

            // CataLog
            if (txtCatalog.Text.Trim().Length <= 0)
            {
                errorProvider1.SetError(txtCatalog, "Please Provide Catalog Name.");
                result = false;
            }

            return result;

        }

        /// <summary>
        /// Test the connection
        /// </summary>
        /// <returns></returns>
        private bool TestConnection()
        {
            string connectionString = GetConnectionString();
            try
            {
                return SqlDatabaseSchema.TestConnection(connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect.\n" + ex.Message);
            }
            return false;

        }

        /// <summary>
        /// Load schema in the database object
        /// </summary>
        private void LoadSchema()
        {
            _connectionString = GetConnectionString();
            IDatabaseSchema dbSchema = new SqlDatabaseSchema();
            _database = dbSchema.GetDataBaseSchema(txtSqlServer.Text, txtCatalog.Text, _connectionString);

            //Load them DatabaseHist neu co
            if (!string.IsNullOrEmpty(txtCatalog2.Text))
            {
                var connectHist = GetConnectionString2();
                var databaseHist = dbSchema.GetDataBaseSchema(txtSqlServer.Text, txtCatalog2.Text, connectHist);
                if (databaseHist != null)
                {
                    foreach (var tableHist in databaseHist.Tables)
                    {
                        //Co truong hop lay giong bang nen check them
                        var table = _database.Tables.Find(s => s.TableName == tableHist.TableName);
                        if(table != null)
                            continue; //Da add

                        _database.Tables.Add(tableHist);
                    }

                   // _database.Tables.AddRange(databaseHist.Tables);
                }
            }
            
            // Set in global Valiable
            TierGeneratorSettings.Instance.Database = _database;
            TierGeneratorSettings.Instance.ConnectionString = _connectionString;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Method to Get The Connection string
        /// </summary>
        /// <returns>Connection String</returns>
        private string GetConnectionString()
        {
            string connectionString = string.Empty;
            if (rbtnWindowsAuthentication.Checked)
            {
                connectionString = "Data Source=" + txtSqlServer.Text + ";" +
                         "Initial Catalog=" + txtCatalog.Text + ";" +
                         "Integrated Security=SSPI";
            }
            else
            {
                connectionString = "Data Source=" + txtSqlServer.Text + ";" +
                         "Initial Catalog=" + txtCatalog.Text + ";" +
                         "User Id = " + txtLogin.Text + ";Password = " + txtPassword.Text;

            }

            return connectionString;
        }

        private string GetConnectionString2()
        {
            string connectionString = string.Empty;
            if (rbtnWindowsAuthentication.Checked)
            {
                connectionString = "Data Source=" + txtSqlServer.Text + ";" +
                         "Initial Catalog=" + txtCatalog2.Text + ";" +
                         "Integrated Security=SSPI";
            }
            else
            {
                connectionString = "Data Source=" + txtSqlServer.Text + ";" +
                         "Initial Catalog=" + txtCatalog2.Text + ";" +
                         "User Id = " + txtLogin.Text + ";Password = " + txtPassword.Text;

            }

            return connectionString;

        }

        private string GetConnectionStringMySql()
        {
            var server = txtSqlServer.Text;
            var database = txtCatalog.Text;
            var uid = txtLogin.Text;
            var password = txtPassword.Text;
            var port = txtPort.Text;
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + 
                               "UID=" + uid + ";" + "PASSWORD=" + password + ";CharSet=utf8;Port=" + port + ";";


            return connectionString;
        }

        /// <summary>
        /// Method to load default values
        /// </summary>
        private void LoadDefaultValues()
        {

            ConfigGlobal.Init();

            txtLinkMapEntity.Text = ConfigGlobal.SettingConfig.Setting_EntityPath;
            txtCatalog.Text = ConfigGlobal.SettingConfig.Database_Catalog;
            txtSqlServer.Text = ConfigGlobal.SettingConfig.DataBase_SqlServer;
            rbtnSqlServerAuthentication.Checked = ConfigGlobal.SettingConfig.Database_SqlAuthentication;
            

            if (rbtnSqlServerAuthentication.Checked)
            {
                txtPassword.Text = ConfigGlobal.SettingConfig.Database_Password;
                txtLogin.Text = ConfigGlobal.SettingConfig.Database_UserName;
            }
            else
            {
                txtPassword.Text = "";
                txtLogin.Text = "";
            }
        }

        /// <summary>
        /// method to save setting
        /// </summary>
        private void SaveSetting()
        {
            ConfigGlobal.SettingConfig.Database_Catalog = txtCatalog.Text;
            ConfigGlobal.SettingConfig.DataBase_SqlServer = txtSqlServer.Text;
            ConfigGlobal.SettingConfig.Database_SqlAuthentication = rbtnSqlServerAuthentication.Checked;
            ConfigGlobal.SettingConfig.Database_Password = txtPassword.Text;
            ConfigGlobal.SettingConfig.Database_UserName = txtLogin.Text;

            ConfigGlobal.Save();

        }

        #endregion

        #region Events

        private void LoginScreen_Load(object sender, EventArgs e)
        {
            LoadDefaultValues();
        }

        private void rbtnSqlServerAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            gbSQLServerCredential.Enabled = rbtnSqlServerAuthentication.Checked;
        }

        private void btnConnection_Click(object sender, EventArgs e)
        {

            if (!IsValidate()) return;

            if (TestConnection())
            {
                try
                { // Load schema
                    LoadSchema();

                    // Save setting for future
                    SaveSetting();


                    // Close Form
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error to load schema.\n" + ex.Message, "Error to load schema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        #endregion

        private void btnConnectOracle_Click(object sender, EventArgs e)
        {
            LoadListFile();
            LoadListFieldsBool();

            ConfigGlobal.SettingConfig.Setting_EntityPath = txtLinkMapEntity.Text.Trim();
            ConfigGlobal.Save();

            var loginScreen = new ConnectStringForm();
            var result = loginScreen.ShowDialog();
            this.DialogResult = result;
            this.Close();
        }

        private void TextBox1TextChanged(object sender, EventArgs e)
        {
            LoadListFile();
            LoadListFieldsBool();
        }

        private void LoadListFile()
        {
            if (string.IsNullOrWhiteSpace(txtLinkMapEntity.Text)) return;
            var listFile = ReadDirectory(txtLinkMapEntity.Text, ".cs");
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

        private void LoadListFieldsBool()
        {
            var url = Application.StartupPath + @"\Config\BoolFields.txt";
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
                OracleHelper.DicBoolFields = dic;
                readStream.Close();
            }

                
            
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

        private void btnConnectMySql_Click(object sender, EventArgs e)
        {
            ConfigGlobal.SettingConfig.Setting_EntityPath = txtLinkMapEntity.Text.Trim();
            ConfigGlobal.Save();

            //Connect MySql
            string connectionString = GetConnectionStringMySql();
            //connStr = "SERVER=10.8.6.87;DATABASE=DatabaseWorking;UID=lemon;PASSWORD=Quantedge@123;CharSet=utf8;Port=3306;";
            var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                LoadSchemaMySql();
                //MessageBox.Show("Connect successfull ! ");
                connection.Close();

                // Close Form
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex.ToString());
            }
            
        }

        private void LoadSchemaMySql()
        {
            _connectionString = GetConnectionStringMySql();
            IDatabaseSchema dbSchema = new SqlDatabaseSchema();
            _database = dbSchema.GetDataBaseSchemaMySql(txtSqlServer.Text, txtCatalog.Text, _connectionString);

            //Load them DatabaseHist neu co
            if (!string.IsNullOrEmpty(txtCatalog2.Text))
            {
                var connectHist = GetConnectionString2();
                var databaseHist = dbSchema.GetDataBaseSchemaMySql(txtSqlServer.Text, txtCatalog2.Text, connectHist);
                if (databaseHist != null)
                {
                    foreach (var tableHist in databaseHist.Tables)
                    {
                        //Co truong hop lay giong bang nen check them
                        var table = _database.Tables.Find(s => s.TableName == tableHist.TableName);
                        if (table != null)
                            continue; //Da add

                        _database.Tables.Add(tableHist);
                    }

                    // _database.Tables.AddRange(databaseHist.Tables);
                }
            }

            // Set in global Valiable
            TierGeneratorSettings.Instance.Database = _database;
            TierGeneratorSettings.Instance.ConnectionString = _connectionString;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void rbtnSqlServerAuthentication_CheckedChanged_1(object sender, EventArgs e)
        {
            gbSQLServerCredential.Enabled = rbtnSqlServerAuthentication.Checked;
        }
    }
}
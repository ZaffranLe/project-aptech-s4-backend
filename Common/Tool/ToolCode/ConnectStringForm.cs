using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonicationMemory.Config;
using CommonicationMemory.Properties;

namespace CommonicationMemory
{
    public partial class ConnectStringForm : Form
    {
        public ConnectStringForm()
        {
            InitializeComponent();
            LoadDefaultValues();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                OracleHelper.ConnectionString = txtConnectionString.Text.Trim();
                OracleHelper.IsConectOracle = true;
                OracleHelper.GetDt("select 5 from dual");

                DialogResult = DialogResult.OK;
                Close();


                ConfigGlobal.SettingConfig.Setting_ConnectionString = txtConnectionString.Text.Trim();
                ConfigGlobal.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadDefaultValues()
        {
            txtConnectionString.Text = ConfigGlobal.SettingConfig.Setting_ConnectionString;
        }
    }
}

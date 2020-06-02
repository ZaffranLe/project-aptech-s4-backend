using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace ConnectCsharpToMysql
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }


        //Insert button clicked
        private void bInsert_Click(object sender, EventArgs e)
        {
            var server = txtHost.Text;
            var database = txtDatabase.Text;
            var uid = txtUserName.Text;
            var password = txtPassword.Text;
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";CharSet=utf8;Port=3306;";


            var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                MessageBox.Show("Connect successfull ! ");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex.ToString());
            }
        }

        private void btnConnectQueryString_Click(object sender, EventArgs e)
        {
            string connectionString = txtQueryString.Text;
            var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                MessageBox.Show("Connect successfull ! ");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex.ToString());
            }
        }
    }
}
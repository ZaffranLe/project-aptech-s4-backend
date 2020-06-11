using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;
using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace RoleGen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCreateScript_Click_1(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtUrlRole.Text))
            {
                MessageBox.Show("", "Chưa nhập đường dẫn đến ma trận phân quyền", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(txtSheetName.Text))
            {
                MessageBox.Show("", "Chưa nhập tên Sheet", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return;
            }
            

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook sheet = excel.Workbooks.Open(txtUrlRole.Text);

            try
            {
                #region Gen script SQL
                //Danh cho MXV

                //ma tran phan quyen mxv
                Microsoft.Office.Interop.Excel.Worksheet x = excel.ActiveWorkbook.Sheets[txtSheetName.Text] as Microsoft.Office.Interop.Excel.Worksheet;
                Excel.Range userRange = x.UsedRange;
                int countRecords = userRange.Rows.Count;
                //so cot la nhom quyen
                int rowRoleGroupStart = 1;
                int rowRoleGroupEnd = 16;

                //           var a = (x.Cells[5, 4] as Excel.Range).Value2;
                string textGen = "";
                string strPermission = "";
                string strRoleRef = "";
                int id = 1;


                txtDisplay.Text = txtDisplay.Text + "DELETE Role;  " + "\r\n";
                txtDisplay.Text = txtDisplay.Text + "INSERT INTO [dbo].[Role]([CreatedAt], [CreatedBy], [Description], [Id], [Name], [UpdatedAt], [UpdatedBy]) VALUES ('2020-06-05 13:57:24.000', 1, N'Nhóm Admin', 1, N'Admin', '2020-06-05 13:57:42.000', 1);" + "\r\n";
                txtDisplay.Text = txtDisplay.Text + "INSERT INTO [dbo].[UserRole]([IdRole],[IdUserLogin]) VALUES (1,1)" + "\r\n";

                txtDisplay.Text = txtDisplay.Text + "DELETE Permission;" + "\r\n";
                txtDisplay.Text = txtDisplay.Text + "DELETE RolePermission;" + "\r\n";

                Dictionary<int, string> ArrNewKey = new Dictionary<int, string>();
                Dictionary<int, string> ArrOldKey = new Dictionary<int, string>();
                string groupType = "";
                string roleTypeId = "";
                string forderEntity = "C:\\KhoiVuData\\";
                if (!Directory.Exists(forderEntity))
                    Directory.CreateDirectory(forderEntity);
                string file = forderEntity + "RoleDefinationEnum.cs";
                var stringBuild = new StringBuilder();
                stringBuild.AppendLine("namespace ElectricShop.Common.Enum");
                stringBuild.AppendLine("{");
                stringBuild.AppendLine("\tpublic enum RoleDefinitionEnum");
                stringBuild.AppendLine("\t{");
                stringBuild.AppendLine("\t\tNone" + " = " + id + ",");

                for (var i = 2; i <= 250; i++)
                {
                    var description = (x.Cells[i, 2] as Excel.Range).Value2;
                    var name = (x.Cells[i, 1] as Excel.Range).Value2;
                    if (description == null || name == null) continue;

                    strPermission =
                        "INSERT [dbo].[Permission]([CreatedAt], [CreatedBy], [Description], [Id], [Name], [UpdatedAt], [UpdatedBy]) VALUES (CAST(0x0000A38100A8D31E AS DateTime), 1, " +
                         "N'" + description + "', " + id + ", N'"
                        + name + "', CAST(0x0000A38100A8D31E AS DateTime), 1);";
                    

                    txtDisplay.Text = txtDisplay.Text + strPermission + "\r\n";

                    //add vao nhom full quyen
                    string strAddFull =
                        "INSERT INTO RolePermission (IdPermission, IdRole) VALUES ("+ id +", 1);";
                    txtDisplay.Text = txtDisplay.Text + strAddFull + "\r\n";

                    #region gen file Enum
                    stringBuild.AppendLine("\t\t" + name + " = " + (int)(id + 1) + ",");
                    #endregion

                    id++;

                }
                stringBuild.AppendLine("\t}");// dong class
                stringBuild.AppendLine("}"); // dong namespace
                using (StreamWriter sw = new StreamWriter(file))
                {
                    sw.WriteLine(stringBuild.ToString());
                    sw.Close();
                }
                sheet.Close();
                excel.Workbooks.Close();
                MessageBox.Show("", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                #endregion



            }
            catch (Exception exception)
            {
                sheet.Close();
                excel.Workbooks.Close();
                MessageBox.Show("", exception.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Copy text
            if (!string.IsNullOrWhiteSpace(txtDisplay.Text)) Clipboard.SetText(txtDisplay.Text);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtUrlRole_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

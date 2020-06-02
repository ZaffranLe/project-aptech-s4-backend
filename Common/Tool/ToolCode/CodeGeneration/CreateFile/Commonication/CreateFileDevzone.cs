using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CommonicationMemory.Common;

namespace CommonicationMemory.CodeGeneration.CreateFile.Commonication
{
    public class CreateFileDevzone
    {
        public void CreateFile(StreamWriter sw, List<DatabaseTable> listTable)
        {
            try
            {
                listTable.Sort((l, r) => String.Compare(l.TableName, r.TableName, StringComparison.Ordinal));
                sw.WriteLine();

                //Insert func ở đây
                foreach (var table in listTable)
                {
                    if (table.IsSelected)
                    {
                        string tableName = table.TableName;
                        int count = table.Columns.Count;
                        var listColums = new List<DatabaseColumn>();
                        table.Columns.Sort((l, r) => String.Compare(l.Name, r.Name, StringComparison.Ordinal));

                        //Insert đoạn đầu tiên
                        sw.WriteLine("h3. " + table.TableName);
                        sw.WriteLine("\r\n");
                        //sw.WriteLine("Tên Dictionary : user." + table.TableName);
                        //sw.WriteLine("Value là chuối Json của thông tin dưới đây:");
                        //sw.WriteLine("\r\n");

                        sw.WriteLine("|*Trường*|*Kiểu*|*Ghi chú*|");
                        //write $group, $name, $key
                        sw.WriteLine("|$group|string|Nhóm user|");
                        sw.WriteLine("|$name|string|Tên " + table.TableName + "|");
                        for (int i = 0; i < count; i++)
                        {
                            DatabaseColumn column = table.Columns[i];
                            if (column.IsPK || column.IsFK)
                            {
                                listColums.Add(column);
                            }
                        }
                        if (listColums.Count == 1)
                        {
                            DatabaseColumn column = listColums[0];
                            string keyName = GetKeyName(column);
                            sw.WriteLine("|$key|" + keyName + "|Là " + column.Name + "|");
                        }
                        else
                        {
                            string keyName = "any";
                            string columnName = "";
                            foreach (var column in listColums)
                            {
                                columnName = column.Name + "#";
                            }

                            sw.WriteLine("|$key|" + keyName + "|Là " + columnName + "|");
                        }

                        //Lưu thông tin các trường còn lại
                        for (int i = 0; i < count; i++)
                        {
                            DatabaseColumn column = table.Columns[i];
                            if (column.IsPK || column.IsFK)
                            {
                                continue;
                            }

                            string keyName = GetKeyName(column);
                            sw.WriteLine("|" + column.Name + "|" + keyName + "||");
                        }


                        sw.WriteLine("\r\n");
                        sw.WriteLine("\r\n");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private string GetKeyName(DatabaseColumn column)
        {
            string keyName = "";
            switch (column.DataType)
            {
                case "bit":
                    {
                        return "bool";
                        break;
                    }
                case "binary":
                case "char":
                case "nchar":
                case "nvarchar":
                case "varbinary":
                case "varchar":
                    {
                        keyName = "string";
                        break;
                    }
                case "bigint":
                case "int":
                case "decimal":
                    {
                        keyName = "number";
                        break;
                    }
            }

            if (string.IsNullOrEmpty(keyName))
                keyName = column.DataType;

            if (keyName.Equals("datetime"))
                return "datetime";
            return keyName.ToLower();
        }
    }
}

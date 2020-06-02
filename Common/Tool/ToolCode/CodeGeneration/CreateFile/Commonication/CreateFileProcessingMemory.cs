using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using CommonicationMemory.Common;

namespace CommonicationMemory.CodeGeneration.CreateFile.Commonication
{
    public class CreateFileProcessingMemory
    {
        //listEntityCommand.AddRange(CheckMemoryAccount.CheckInfo());

        public void CreateFile(StreamWriter sw, List<DatabaseTable> listTable)
        {
            try
            {
                //Insert func ở đây
                StringBuilder stringBuilder = FunctionBuild_GetFileProcessingMemory(listTable);
                sw.WriteLine(stringBuilder.ToString());
                //End insert func
                sw.WriteLine();
            }
            catch (Exception ex)
            {

            }
        }

        private StringBuilder FunctionBuild_GetFileProcessingMemory(List<DatabaseTable> listTable)
        {
            var functionBuild = new StringBuilder();
            try
            {
                string textFunction = GetTextFile("ProcessingMemory.txt");
                functionBuild.AppendLine(textFunction);

                var stringBuilderCheck = new StringBuilder();
                var stringBuilderCast = new StringBuilder();

                foreach (var table in listTable)
                {
                    if (table.IsSelected)
                    {
                        string tableName = table.TableName;

                        stringBuilderCheck.AppendLine("if (request.EntityName == " + tableName + ".EntityName())");
                        stringBuilderCheck.AppendLine("listEntityCommand.AddRange(CheckMemory" + tableName + ".CheckInfo(");
                        stringBuilderCheck.AppendLine("new List<" + tableName + ">(request.ListEntitys.Cast<" + tableName + ">()), request.WorkerSender)); ");

                        stringBuilderCast.AppendLine("if (entityName.Equals(" + tableName + ".EntityName()))");
                        stringBuilderCast.AppendLine("{");
                        stringBuilderCast.AppendLine("var listValue = MemoryInfo.GetAll" + tableName + "();");
                        stringBuilderCast.AppendLine("listBaseEntity = listValue.Cast<BaseEntity>().ToList();");
                        stringBuilderCast.AppendLine("}");
                    }
                }

                functionBuild.Replace("#FUNCTION_CHECK_MEMORY", stringBuilderCheck.ToString());
                functionBuild.Replace("#FUNCTION_CAST_MEMORY", stringBuilderCast.ToString());
            }
            catch (Exception ex)
            {

            }
            return functionBuild;
        }

        private string GetTextFile(string fileName)
        {
            string textFunction = "";
            try
            {
                string fileFuncTemplate = GetAppPath() + "\\CodeGeneration\\FuncBuild\\" + fileName.Trim();
                if (File.Exists(fileFuncTemplate))
                {
                    //Nếu có template func xử lý thì insert luôn func
                    var streamReader = new StreamReader(fileFuncTemplate);
                    textFunction = streamReader.ReadToEnd();
                    streamReader.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return textFunction;
        }

        private static string GetAppPath()
        {
            string path = Assembly.GetExecutingAssembly().Location;
            path = path.Remove(path.LastIndexOf('\\'));
            return path;
        }
    }
}

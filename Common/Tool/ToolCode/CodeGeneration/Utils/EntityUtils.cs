using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonicationMemory.CodeGeneration.Utils
{
    public class EntityUtils
    {
        public static Dictionary<string, string> FindFieldInEntity(string fileText,
            out Dictionary<string, string> dicComment, out string nameSpace, out string messageType)
        {
            var dicField = new Dictionary<string, string>();
            dicComment = new Dictionary<string, string>();
            nameSpace = "";
            messageType = "";
            try
            {
                if (fileText.Contains("ChatManagerRequest"))
                {

                }
                string[] splits = fileText.Split("\r\n".ToCharArray());
                int count = 0;
                if (splits.Length > 0)
                {
                    foreach (string split in splits)
                    {
                        count = 0;
                        if (string.IsNullOrEmpty(split)) continue;

                        if (split.Contains("namespace "))
                            nameSpace = split.Replace("namespace ", "").Trim();
                        if (split.Contains("MessageType."))
                        {
                            int indexType = split.IndexOf("MessageType.");
                            int indexNgoac = split.IndexOf(")", indexType);
                            messageType = split.Substring(indexType, indexNgoac - indexType);
                        }

                        if (split.Contains("public") &&
                            split.Contains("get") &&
                            split.Contains("set"))
                        {
                            //Bỏ qua code get,set đã được rào
                            if (split.Trim().StartsWith("//")) continue;

                            //Bỏ qua các trường không lưu database
                            if (split.Contains("NotSetDb")) continue;

                            if (split.Contains("?"))
                            {

                            }

                            //Xử lý lấy thông tin
                            string result = split.Replace("public ", "").Trim();
                            string[] splitResult = result.Split(" ".ToCharArray());
                            string tenBien = "";
                            string tenField = "";

                            foreach (var text in splitResult)
                            {
                                if (!string.IsNullOrWhiteSpace(text.Trim()))
                                {
                                    count++;
                                    if (count == 1)
                                    {
                                        //Đây là tên biến
                                        tenBien = text.Trim();
                                    }
                                    if (count == 2)
                                    {
                                        //Đây là tên entity field
                                        tenField = text.Trim();
                                        dicField[tenField] = tenBien; //AccountId string
                                    }
                                }
                            }

                            //Lấy thông tin comment
                            if (result.Contains("//"))
                            {
                                int index = result.IndexOf("//", StringComparison.Ordinal);
                                string comment = result.Substring(index, result.Length - index);
                                dicComment[tenField] = comment;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"error" + ex);
            }
            return dicField;
        }
    }
}

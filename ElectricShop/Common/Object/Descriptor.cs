using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Anotar.NLog;
using ElectricShop.Common.Enum;

namespace ElectricShop.Common.Object
{
    public class Descriptor
    {
        public Descriptor()
        {
            Descriptors = new List<Descriptor>();
        }

        public string FieldName { get; set; }

        public string FieldValue { get; set; }

        public DataOperator Operator { get; set; }

        public LogicalOperator Logical { get; set; }

        public List<Descriptor> Descriptors { get; set; }

        public static string GetInputValue(params object[] value)
        {
            try
            {
                var list = GetRawList(value);
                if (list == null || list.Count <= 0) return string.Empty;
                if (list.Count == 1) return ConvertToString(list[0]);
                var resultArr = new StringBuilder();
                resultArr.Append('(');
                bool isFirst = true;
                foreach (var obj in list)
                {
                    var str = ConvertToString(obj);
                    if (isFirst)
                    {
                        resultArr.Append(str);
                        isFirst = false;
                    }
                    else
                    {
                        resultArr.Append(',');
                        resultArr.Append(str);
                    }
                }
                resultArr.Append(')');
                return resultArr.ToString();
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
            return string.Empty;
        }

        private static List<object> GetRawList(params object[] value)
        {
            try
            {
                if (value == null) return new List<object>() { null };
                if (value.Length <= 0) return null;
                var listReturn = new List<object>();
                foreach (var obj in value)
                {
                    if (obj is string)
                    {
                        listReturn.Add(obj);
                        continue;
                    }
                    if (obj is IEnumerable)
                    {
                        var enumList = (obj as IEnumerable).GetEnumerator();
                        while (enumList.MoveNext())
                        {
                            listReturn.Add(enumList.Current);
                        }
                    }
                    else
                        listReturn.Add(obj);
                }
                return listReturn;
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
            return null;
        }

        private static string ConvertToString(object value)
        {
            try
            {
                if (value == null) return "NULL";
                if (value is DateTime)
                {
                    var date = (DateTime)value;
                    if(CommonGlobalConfig.IsUseMySql)
                    {
                        //MySql dinh dang datetime khac sql
                        return string.Format("'{0}/{1}/{2} {3}:{4}:{5}'", date.Year, date.Month, date.Day, date.Hour,
                            date.Minute, date.Second);
                    }
                    return string.Format("'{0}/{1}/{2} {3}:{4}:{5}'", date.Month, date.Day, date.Year, date.Hour,
                        date.Minute, date.Second);
                }
                if (value is bool)
                {
                    return (bool)value ? "'1'" : "'0'";
                }
                string data = value.ToString();
                //check neu du lieu cho LIKE
                if ((data.Contains("%") && data.Contains("'")) || data.Contains("'"))
                    return data;
                return string.Format("'{0}'", value);
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
            return string.Empty;
        }
    }
}
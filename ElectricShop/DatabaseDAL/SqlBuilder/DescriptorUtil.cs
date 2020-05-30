using System;
using System.Collections.Generic;
using System.Text;
using Anotar.NLog;
using ElectricShop.Common.Enum;
using ElectricShop.Common.Object;

namespace ElectricShop.DatabaseDAL.SqlBuilder
{
    public class DescriptorUtil
    {
        public static string GetWhereString(DescriptorColection colection)
        {
            try
            {
                if (colection == null) return string.Empty;
                var agrs = GetStringByList(colection.Logical, colection.Descriptors);
                if (string.IsNullOrWhiteSpace(agrs)) return string.Empty;
                return " WHERE " + agrs;
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
            return string.Empty;
        }

        private static string GetStringByList(LogicalOperator logical, List<Descriptor> list)
        {
            try
            {
                var result = new StringBuilder();
                var logic = GetLogical(logical);
                if (list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        var descriptor = list[i];
                        string data;
                        if (descriptor.Descriptors.Count <= 0)
                            data = GetString(descriptor);
                        else
                            data = GetStringByList(descriptor.Logical, descriptor.Descriptors);
                        if (string.IsNullOrWhiteSpace(data)) continue;
                        if (i == 0)
                            result.Append(data);
                        else
                            if (result.Length > 0)
                                result.Append(logic).Append(data);
                            else
                                result.Append(data);
                    }
                }
                if (result.Length <= 0) return string.Empty;
                if (list.Count == 1)
                    return result.ToString();
                return string.Format("({0})", result);
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
            return string.Empty;
        }

        private static string GetString(Descriptor des)
        {
            try
            {
                if (des == null || string.IsNullOrWhiteSpace(des.FieldName)) return string.Empty;
                switch (des.Operator)
                {
                    case DataOperator.IsIn:
                    case DataOperator.IsNotIn:
                        if (string.IsNullOrWhiteSpace(des.FieldValue) || "''".Equals(des.FieldValue))
                            return string.Format("{0}{1}('-9999')", des.FieldName, GetOpearor(des.Operator));
                        if (des.FieldValue.StartsWith("(") && des.FieldValue.EndsWith(")"))
                            return string.Format("{0}{1}{2}", des.FieldName, GetOpearor(des.Operator), des.FieldValue);
                        return string.Format("{0}{1}({2})", des.FieldName, GetOpearor(des.Operator), des.FieldValue);
                    case DataOperator.IsNull:
                    case DataOperator.IsNotNull:
                        return string.Format("{0}{1}", des.FieldName, GetOpearor(des.Operator));
                    case DataOperator.NotLike:
                    case DataOperator.Like:
                        return string.Format("{0}{1}{2}", des.FieldName, GetOpearor(des.Operator),
                            //replace dấu '
                            string.Format("N'%{0}%'", des.FieldValue.Length >= 3 ? des.FieldValue.Substring(1, des.FieldValue.Length - 2) : des.FieldValue.Replace("'", "")));
                    default:
                        return string.Format("{0}{1}{2}", des.FieldName, GetOpearor(des.Operator), des.FieldValue);
                }
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
            return string.Empty;
        }

        private static string GetLogical(LogicalOperator logical)
        {
            try
            {
                if (logical == LogicalOperator.AND)
                    return " AND ";
                if (logical == LogicalOperator.OR)
                    return " OR ";
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
            return string.Empty;
        }

        private static string GetOpearor(DataOperator oper)
        {
            try
            {
                switch (oper)
                {
                    case DataOperator.Like:
                        return " LIKE ";
                    case DataOperator.NotLike:
                        return " NOT LIKE ";
                    case DataOperator.IsIn:
                        return " IN ";
                    case DataOperator.IsNotIn:
                        return " NOT IN ";
                    case DataOperator.IsEqualTo:
                        return " = ";
                    case DataOperator.IsNotEqualTo:
                        return " <> ";
                    case DataOperator.IsGreaterThan:
                        return " > ";
                    case DataOperator.IsGreaterThanOrEqualTo:
                        return " >= ";
                    case DataOperator.IsLessThan:
                        return " < ";
                    case DataOperator.IsLessThanOrEqualTo:
                        return " <= ";
                    case DataOperator.IsNull:
                        return " IS NULL ";
                    case DataOperator.IsNotNull:
                        return " IS NOT NULL ";
                }
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
            return string.Empty;
        }
    }
}

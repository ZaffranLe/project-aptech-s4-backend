using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CommonicationMemory.Common;
using CommonicationMemory.Config;
using CommonicationMemory.Properties;

namespace CommonicationMemory.CodeGeneration.CreateFile.DatalayerWorker
{
    public class CreateFileMemoryGetByField
    {
        public void CreateFile(string folderPath, string fileName, List<DatabaseTable> listTable)
        {
            try
            {
                listTable.Sort((l, r) => String.Compare(l.TableName, r.TableName, StringComparison.Ordinal));
                var stringBuild = new StringBuilder(); 
                var headerFile = new StringBuilder();
                headerFile.AppendLine(@"using System;
                                        using System.Collections.Generic;
                                        using System.Globalization;
                                        using System.Linq;
                                        using Anotar.NLog;
                                        using QuantEdge.Entity.Keys;
                                        using QuantEdge.Common.Enum;
                                        using QuantEdge.Entity.Entities;
                                        using QuantEdge.Lib.Broadcast;
                                        ");
                headerFile.AppendLine("namespace QuantEdge.Lib.Memory");
                headerFile.AppendLine("{");
                var endFile = new StringBuilder();
                endFile.AppendLine("}");


                var headerclassBuilder = new StringBuilder();
                headerclassBuilder.AppendLine("public partial class MemoryInfo : Memory");
                headerclassBuilder.AppendLine("{");
                var endclassBuilder = new StringBuilder();
                endclassBuilder.AppendLine("}");

                stringBuild.Append(headerFile.ToString());
                stringBuild.Append(headerclassBuilder.ToString());

                foreach (var table in listTable)
                {
                    if (table.IsSelected)
                    {
                        string tableName = table.TableName;
                        var listColums = table.Columns;
                        stringBuild.AppendLine(FunctionBuild_GetMemory(tableName, listColums).ToString());
                    }
                }

                //End insert func
                stringBuild.Append(endclassBuilder.ToString());
                stringBuild.Append(endFile.ToString());
                stringBuild.AppendLine();

                //Lưu vào thư mục build
                using (var sw = new StreamWriter(folderPath + fileName))
                {
                    sw.WriteLine(stringBuild.ToString());
                    sw.Close();
                }
                //Lưu vào thư mục foudation nếu có
                if (!string.IsNullOrEmpty(ConfigGlobal.SettingConfig.Setting_MemoryWorkerBase) && ConfigGlobal.SettingConfig.Setting_CheckGenByForder)
                {
                    string fileCs = ConfigGlobal.SettingConfig.Setting_MemoryWorkerBase + "\\" + fileName;
                    if (File.Exists(fileCs))
                    {
                        //Tồn tại mới lưu
                        File.Delete(fileCs);
                        using (var sw = new StreamWriter(fileCs))
                        {
                            sw.WriteLine(stringBuild.ToString());
                            sw.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private StringBuilder FunctionBuild_GetMemory(string tableName, List<DatabaseColumn> listColumns)
        {
            var functionBuild = new StringBuilder();
            try
            {
                //ví dụ
                //#region MemberOtherInfo
                //public static List<MemberOtherInfo> GetListMemberOtherInfo(string fieldValue, MemberOtherInfo.MemberOtherInfoFields fieldName)
                //{
                //    if (string.IsNullOrEmpty(fieldValue)) fieldValue = "";
                //    var listValue = new List<MemberOtherInfo>();
                //    if (fieldName == MemberOtherInfo.MemberOtherInfoFields.LicenseCode)
                //    {
                //        listValue = DicMemberOtherInfo.Values.ToList().FindAll(obj =>
                //                        fieldValue.Equals(obj.LicenseCode, StringComparison.InvariantCultureIgnoreCase));
                //    }
                //    else if (fieldName == MemberOtherInfo.MemberOtherInfoFields.Mobile)
                //    {
                //        listValue = DicMemberOtherInfo.Values.ToList().FindAll(u =>
                //                        fieldValue.Equals(u.Mobile, StringComparison.InvariantCultureIgnoreCase));
                //    }

                //    return listValue.Select(value => value.Clone() as MemberOtherInfo).ToList();
                //}
                //#endregion

                functionBuild.AppendLine("#region " + tableName);

                functionBuild.AppendLine("public static List<" + tableName + "> GetList" + tableName + "ByField(string fieldValue, " + tableName + "." + tableName + "Fields fieldName)");
                functionBuild.AppendLine("{");
                functionBuild.AppendLine("if (string.IsNullOrEmpty(fieldValue)) fieldValue = \"\";");
                functionBuild.AppendLine("var listValue = new List<" + tableName + ">();");

                var listColumnsKeys = new List<DatabaseColumn>();
                int count1 = listColumns.Count;
                for (int i = 0; i < count1; i++)
                {
                    DatabaseColumn column = listColumns[i];
                    if (column.IsPK || column.IsFK)
                    {
                        listColumnsKeys.Add(column);
                    }
                }

                #region ReFunction
                int count = listColumns.Count;
                bool isFirstLine = true;
                for (int i = 0; i < count; i++)
                {
                    DatabaseColumn column = listColumns[i];
                    //Check chỉ generator những cột không phải columns key của bảng có nhiều khóa
                    bool isContinue = false;
                    if (listColumnsKeys.Count > 1)
                    {
                        foreach (var columnsKey in listColumnsKeys)
                        {
                            if (column.Name.Equals(columnsKey.Name))
                                isContinue = true;
                        }
                    }
                    if (isContinue) continue;
                    //end check

                    if (column.CSharpDataTypeName.Equals("byte[]") ||
                       column.CSharpDataTypeName.Equals("byte[]?") ||
                        column.CSharpDataTypeName.Equals("DateTime") ||
                       column.CSharpDataTypeName.Equals("DateTime?")
                        )
                        continue;
                    if (column.Name.Equals("ActorChanged") ||
                       column.Name.Equals("TimeChanged"))
                        continue;

                    string ifLine = "if (fieldName == ";
                    if (i > 0 && !isFirstLine)
                    {
                        ifLine = "else if (fieldName == ";
                    }

                    ifLine += tableName + "." + tableName + "Fields." + column.Name + ")";
                    functionBuild.AppendLine(ifLine);
                    isFirstLine = false;
                    functionBuild.AppendLine("{");
                    if (IsNullValue(column, tableName))
                    {
                        functionBuild.AppendLine("listValue = Dic" + tableName + ".Values.ToList().FindAll(obj => obj." + column.Name + ".HasValue &&");
                        functionBuild.AppendLine("fieldValue.Equals(obj." + column.Name + ".Value.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));");
                    }
                    else
                    {
                        functionBuild.AppendLine("listValue = Dic" + tableName + ".Values.ToList().FindAll(obj =>");
                        functionBuild.AppendLine("fieldValue.Equals(obj." + column.Name + ".ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));");
                    }

                    functionBuild.AppendLine("}");
                }
                #endregion

                functionBuild.AppendLine("return listValue.Select(value => value.Clone() as " + tableName + ").ToList();");
                functionBuild.AppendLine("}");

                functionBuild.AppendLine("#endregion");
            }
            catch (Exception ex)
            {

            }
            return functionBuild;
        }

        public static bool IsNullValue(DatabaseColumn column, string tableName)
        {
            //Check xem phải dữ liệu có nullable hay không
            switch (column.CSharpDataTypeName)
            {
                case "decimal?":
                    {
                        #region DECIMAL?
                        switch (column.Name)
                        {
                            case "StopPx":
                            case "CostPrice":
                            case "PriceCustomer":
                            case "IMLimit":
                            case "PositionLimit":
                            case "PositionTradingLimit":
                            case "MarginMultiplier":
                            case "WarningRatio":
                            case "StoplossRatio":
                            case "StatusRatio":
                            case "MarginCreditInterestRate":
                            case "DetailMarginCreditLimit":
                            case "TotalMarginCreditLimit":
                            case "SplitTKKQ":
                            case "FeeCustomer":
                            case "FeeOther":
                            case "TckqRate":
                            case "OrderPrice":
                            case "FillPrice":
                            case "LimitPrice":
                            case "QuantityLinked":
                            case "YesterdaySettlement":
                                return true;
                            case "FeePartner":
                                if (tableName.Equals("FeeRefundMonth"))
                                    return false;
                                return true;
                            case "Price":
                                if (tableName.Equals("OrderTransaction") ||
                                    tableName.Equals("OrderTransactionHist") ||
                                    tableName.Equals("OrderTransactionDaily") ||
                                    tableName.Equals("OrderTransactionAction"))
                                    return false;
                                return true;
                            default:
                                return false;
                        }
                        return false; 
                        #endregion
                    }
                case "long?":
                    switch (column.Name)
                    {
                        #region LONG?
                        case "ApprovalBy":
                        case "MemberParent":
                        case "SessionParentId":
                        case "OrderTransactionIdBuy":
                        case "OrderTransactionIdSell":
                            return true;
                        case "OrderTransactionBuyId":
                        case "OrderTransactionSellId":
                            if (tableName.Equals("LinkedTransactionHist"))
                                return false;
                            return true;
                        case "ActorCreater":
                            if (tableName.Equals("MemberInfo"))
                                return false;
                            return true;
                        case "OrderTransactionId":
                            if (tableName.Equals("OpenPositionDetailDaily") ||
                                tableName.Equals("OrderTransactionAction") ||
                                tableName.Equals("OrderTransactionDaily") ||
                                tableName.Equals("OrderTransactionHist") ||
                                tableName.Equals("OpenPositionDetailHist"))
                                return false;
                            return true;
                        case "CreateBy":
                            if (tableName.Equals("ApprovalAccount") ||
                                tableName.Equals("ApprovalDealing") ||
                                tableName.Equals("ApprovalMember") ||
                                tableName.Equals("ApprovalPreRisk") ||
                                tableName.Equals("ForexRateDaily") ||
                                tableName.Equals("ApprovalSystem") ||
                                tableName.Equals("ManualPriceAction") ||
                                tableName.Equals("ApprovalOrder"))
                                return false;
                            return true;
                        case "PartnerId":
                            if (tableName.Equals("LinkedTransactionHist") ||
                                tableName.Equals("MarginManualStatus") ||
                                tableName.Equals("OpenPositionDetail") ||
                                tableName.Equals("OrderTransaction") ||
                                tableName.Equals("OrderTransactionHist") ||
                                tableName.Equals("OrderTransactionDaily") ||
                                tableName.Equals("LinkedTransaction") ||
                                tableName.Equals("OrderTransactionAction") ||
                                tableName.Equals("OpenPositionDetailDaily") ||
                                tableName.Equals("OpenPositionDetailHist"))
                                return false;
                            return true;
                        default:
                            return false; 
                        #endregion
                    }
                case "bool?":
                    switch (column.Name)
                    {
                        #region BOOL?
                        case "IsExpriedChanged":
                        case "Enable":
                        case "IsBuy":
                            if (tableName.Equals("OrderTransaction") ||
                                tableName.Equals("OrderTransactionDaily") ||
                                tableName.Equals("BrokerOrder") ||
                                tableName.Equals("TradingDeal") ||
                                tableName.Equals("TradingDealAction") ||
                                tableName.Equals("TradingDealHist") ||
                                tableName.Equals("OpenPositionDetail") ||
                                tableName.Equals("OpenPositionDetailDaily") ||
                                tableName.Equals("OpenPositionDetailHist") ||
                                tableName.Equals("OrderTransactionAction") ||
                                tableName.Equals("OrderTransactionHist"))
                                return false;
                            return true;
                        default:
                            return false; 
                        #endregion
                    }
                case "int?":
                    switch (column.Name)
                    {
                        #region INT?
                        case "PositionLiquidationMethod":
                        case "MarginCalculationMethod":
                        case "TimeZoneUtc":
                        case "RoleGroupType":
                            return true;
                        case "MarginRefSource":
                            if (tableName.Equals("Symbol") ||
                                tableName.Equals("SymbolCQG") ||
                                tableName.Equals("BaseSymbolAction") ||
                                tableName.Equals("BaseSymbol"))
                                return false;
                            return true;
                        case "Status":
                            if (tableName.Equals("ApprovalAccount") ||
                                tableName.Equals("ApprovalDealing") ||
                                tableName.Equals("ApprovalMember") ||
                                tableName.Equals("ApprovalPreRisk") ||
                                tableName.Equals("ApprovalSystem") ||
                                tableName.Equals("MemberInfo") ||
                                tableName.Equals("UserInfo") ||
                                tableName.Equals("RoleGroup") ||
                                tableName.Equals("ForexRate") ||
                                tableName.Equals("ForexRateAction") ||
                                tableName.Equals("ForexRateDaily") ||
                                tableName.Equals("ApprovalOrder"))
                                return false;
                            return true;
                        case "BrokerId":
                            if (tableName.Equals("TradingDeal") ||
                                tableName.Equals("OrderTransactionHist") ||
                                tableName.Equals("BrokerOrder") ||
                                tableName.Equals("TradingDealHist") ||
                                tableName.Equals("TradingDealAction") ||
                                tableName.Equals("OrderTransactionDaily") ||
                                tableName.Equals("OrderTransaction") ||
                                tableName.Equals("OpenPositionDetailDaily") ||
                                tableName.Equals("OrderTransactionAction"))
                                return false;
                            return true;
                        case "NumbreDecimal":
                            return false;
                        default:
                            return false; 
                        #endregion
                    }
                default:
                    return false;
            }
        }
    }
}

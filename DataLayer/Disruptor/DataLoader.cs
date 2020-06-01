using System;
using System.Collections.Generic;
using System.Linq;
using Anotar.NLog;
using QuantEdge.Common;
using QuantEdge.Common.Enum;
using QuantEdge.Common.Object;
using QuantEdge.Entity;
using QuantEdge.Entity.Entities;
using QuantEdge.Lib.Common;
using QuantEdge.Lib.Interface;
using QuantEdge.Message.Request;

namespace QuantEdge.Lib.Disruptor
{
    public class DataLoader : ILoader
    {
        private IProcess _process;

        public DataLoader(IProcess process)
        {
            _process = process;
        }

        public void ProcessData(ref GetStartupMemoryRequest request)
        {
            //TODO : lay du lieu o day, chu y dung nguyen msg nay, ko clone
            try
            {
                #region Comment
                //if (request.Query != null)
                //{
                //    request.TemporaryTopic = null; // ghi nhan xu ly tu memory
                //    ConsoleLog.WriteLine("LOAD ENTITY = {0} BY CURRENT", request.Query.EntityName);
                //    //Lay du lieu tu database
                //    if (request.Query.QueryAction == EntityGet.GetCustomStore)
                //    {
                //        //Run by store name 
                //        using (var procedureEntityDao = new ProcedureEntityDao())
                //        {
                //            var lstEntity = procedureEntityDao.GetListEntityByProcedureName(request.Query.EntityName,
                //                request.Query.StoreName);

                //            request.Query.ReturnValue = lstEntity.Select(c =>
                //                new Entity.Entity(c)).ToList();
                //        }

                //        #region Xu ly lay custom thong tin MemberInfo, UserInfo
                //        if (request.Query.EntityName == MemberInfo.EntityName() ||
                //            request.Query.EntityName == UserInfo.EntityName())
                //        {
                //            if (request.RequestWorker == WorkerType.Authentication ||
                //                request.RequestWorker == WorkerType.Dealing ||
                //                request.RequestWorker == WorkerType.Pricing ||
                //                request.RequestWorker == WorkerType.PriceCollector ||
                //                request.RequestWorker == WorkerType.RFQ ||
                //                request.RequestWorker == WorkerType.System ||
                //                request.RequestWorker == WorkerType.Session ||
                //                request.RequestWorker == WorkerType.Chat)
                //            {
                //                //Chi can lay theo store la du
                //            }
                //            else
                //            {
                //                //Lay them 1 so thong tin 
                //                var entityName = request.Query.EntityName;
                //                #region Lay customer theo FxDeal Working
                //                if (entityName == MemberInfo.EntityName())
                //                {
                //                    var lstBaseEntity =
                //                        ProcedureEntityDao.GetListMemberCustomerInfoFxDeal();
                //                    foreach (var value in lstBaseEntity)
                //                    {
                //                        var entity = new Entity.Entity(value);
                //                        if (request.Query.ReturnValue == null)
                //                            request.Query.ReturnValue = new List<Entity.Entity>();
                //                        request.Query.ReturnValue.Add(entity);
                //                    }
                //                }
                //                if (entityName == UserInfo.EntityName())
                //                {
                //                    var lstBaseEntity =
                //                        ProcedureEntityDao.GetListUserCustomerInfoFxDeal();

                //                    foreach (var value in lstBaseEntity)
                //                    {
                //                        var entity = new Entity.Entity(value);
                //                        if (request.Query.ReturnValue == null)
                //                            request.Query.ReturnValue = new List<Entity.Entity>();
                //                        request.Query.ReturnValue.Add(entity);
                //                    }
                //                }
                //                #endregion

                //                #region Lay customer la nhom khach hang, customer vang lai chi dinh cho chi nhanh
                //                if (entityName == MemberInfo.EntityName())
                //                {
                //                    //Customer la nhom khach hang
                //                    var lstGroupCustomer = GetListMemberInGroupCustomer();

                //                    var lstEntity = new List<Entity.Entity>();
                //                    foreach (var value in lstGroupCustomer)
                //                    {
                //                        lstEntity.Add(new Entity.Entity(value));
                //                    }

                //                    //Customer vang lai cho chi nhanh
                //                    var lstMemberVangLai = GetListMemberInCustomerVangLai();
                //                    foreach (var memberInfo in lstMemberVangLai)
                //                    {
                //                        lstEntity.Add(new Entity.Entity(memberInfo));
                //                    }

                //                    //Do dữ liêu ràng buộc nên dùng lstVangLai để lấy luôn userinfo cho vang lai
                //                    var lstUserVangLai = GetListUserInfoInCustomerVangLai();
                //                    foreach (var userInfo in lstUserVangLai)
                //                    {
                //                        lstEntity.Add(new Entity.Entity(userInfo));
                //                    }

                //                    //Them vao lst
                //                    if (request.Query.ReturnValue == null)
                //                        request.Query.ReturnValue = new List<Entity.Entity>();
                //                    request.Query.ReturnValue.AddRange(lstEntity);
                //                }
                //                if (entityName == UserInfo.EntityName())
                //                {
                //                    //Customer la nhom khach hang (Ko can lay do nhom khach hang ko co userinfo)

                //                    //Customer vang lai cho chi nhanh (Da lay ben tren)
                //                }
                //                #endregion
                //            }
                //        }
                //        #endregion
                //    }
                //    else
                //    {
                //        if (request.Query.EntityName == SessionHistory.EntityName() &&
                //            request.Query.IsGetMaxKey == false)
                //        {
                //            LogTo.Info("Chi lay thong tin 400 phien gan nhat");

                //            #region SessionHistory

                //            var lstSessionHistory400 =
                //                Select400SessionHistory(400);

                //            var lstEntity = new List<Entity.Entity>();
                //            foreach (var value in lstSessionHistory400)
                //            {
                //                lstEntity.Add(new Entity.Entity(value));
                //            }
                //            request.Query.ReturnValue = lstEntity;
                //            #endregion
                //        }
                //        else
                //        {
                //            //Lay thong tin mac dinh theo query
                //            request.Query = GetEntityQuery(request.Query, false);
                //        }
                //    }

                //    #region Xu ly sau khi lay du lieu
                //    #region Xử lý bỏ qua 1 số dữ liệu không cần thiết
                //    if (request.Query.ReturnValue != null)
                //    {
                //        if (request.Query.EntityName == UserLayout.EntityName())
                //        {
                //            #region Khong can dung thong tin nay tren worker
                //            //Chi can de check ton tai, khong can lay du lieu layout
                //            foreach (var baseEntity in request.Query.ReturnValue)
                //            {
                //                var userLayout = baseEntity.GetEntity() as UserLayout;
                //                userLayout.CurrentLayout = null;
                //                userLayout.DefaultLayout = null;
                //            }
                //            #endregion
                //        }
                //        else if (request.Query.EntityName == ApprovalOrder.EntityName())
                //        {
                //            #region Khong can dung thong tin nay tren worker
                //            //Chi can id va trang thai de check, ko can du lieu ben trong
                //            foreach (var baseEntity in request.Query.ReturnValue)
                //            {
                //                var value = baseEntity.GetEntity() as ApprovalOrder;
                //                if (value.Status == (int)ApprovalStatus.Approval
                //                   || value.Status == (int)ApprovalStatus.Cancel
                //                   || value.Status == (int)ApprovalStatus.Reject)
                //                {
                //                    value.CurrentValue = null;
                //                    value.NewValue = null;
                //                }
                //            }
                //            #endregion
                //        }
                //        else if (request.Query.EntityName == ApprovalPreRisk.EntityName())
                //        {
                //            #region Khong can dung thong tin nay tren worker
                //            //Chi can id va trang thai de check, ko can du lieu ben trong
                //            foreach (var baseEntity in request.Query.ReturnValue)
                //            {
                //                var value = baseEntity.GetEntity() as ApprovalPreRisk;
                //                if (value.Status == (int)ApprovalStatus.Approval
                //                   || value.Status == (int)ApprovalStatus.Cancel
                //                   || value.Status == (int)ApprovalStatus.Reject)
                //                {
                //                    value.CurrentValue = null;
                //                    value.NewValue = null;
                //                }
                //            }
                //            #endregion
                //        }
                //        else if (request.Query.EntityName == ApprovalMember.EntityName())
                //        {
                //            #region Khong can dung thong tin nay tren worker
                //            //Chi can id va trang thai de check, ko can du lieu ben trong
                //            foreach (var baseEntity in request.Query.ReturnValue)
                //            {
                //                var value = baseEntity.GetEntity() as ApprovalMember;
                //                if (value.Status == (int)ApprovalStatus.Approval
                //                   || value.Status == (int)ApprovalStatus.Cancel
                //                   || value.Status == (int)ApprovalStatus.Reject)
                //                {
                //                    value.CurrentValue = null;
                //                    value.NewValue = null;
                //                }
                //            }
                //            #endregion
                //        }
                //        else if (request.Query.EntityName == ApprovalDealing.EntityName())
                //        {
                //            #region Khong can dung thong tin nay tren worker
                //            //Chi can id va trang thai de check, ko can du lieu ben trong
                //            foreach (var baseEntity in request.Query.ReturnValue)
                //            {
                //                var value = baseEntity.GetEntity() as ApprovalDealing;
                //                if (value.Status == (int)ApprovalStatus.Approval
                //                   || value.Status == (int)ApprovalStatus.Cancel
                //                   || value.Status == (int)ApprovalStatus.Reject)
                //                {
                //                    value.CurrentValue = null;
                //                    value.NewValue = null;
                //                }
                //            }
                //            #endregion
                //        }
                //        else if (request.Query.EntityName == ApprovalSystem.EntityName())
                //        {
                //            #region Khong can dung thong tin nay tren worker
                //            //Chi can id va trang thai de check, ko can du lieu ben trong
                //            foreach (var baseEntity in request.Query.ReturnValue)
                //            {
                //                var value = baseEntity.GetEntity() as ApprovalSystem;
                //                if (value.Status == (int)ApprovalStatus.Approval
                //                   || value.Status == (int)ApprovalStatus.Cancel
                //                   || value.Status == (int)ApprovalStatus.Reject)
                //                {
                //                    value.CurrentValue = null;
                //                    value.NewValue = null;
                //                }
                //            }
                //            #endregion
                //        }
                //    }

                //    #endregion

                //    #region GetMaxKey nếu worker yêu cầu
                //    if (request.Query.IsGetMaxKey)
                //    {
                //        string keyName = HistoryUtils.GetKeyName(request.Query.EntityName);
                //        object obj = GetMaxId(request.Query.EntityName, keyName) ?? (long)0;
                //        if (obj is DBNull) obj = (long)0;
                //        //Đối với các bảng có bảng history thì lấy thêm dữ liệu trong bảng hist để tạo max
                //        string entityNameHistory = GetEntityNameHist(request.Query.EntityName);
                //        if (!string.IsNullOrEmpty(entityNameHistory))
                //        {
                //            try
                //            {
                //                //keyName vẫn là keyName của bảng gốc
                //                object objHist = GetMaxId(entityNameHistory, keyName) ?? (long)0;
                //                if (objHist is DBNull)
                //                    objHist = obj;
                //                if (obj is DateTime)
                //                {
                //                    if ((DateTime)obj < (DateTime)objHist)
                //                        obj = objHist; //Nếu key bên hist lớn hơn thì max key là hist
                //                }
                //                else if (obj is string)
                //                {
                //                    if (String.Compare(obj.ToString(), objHist.ToString(), StringComparison.Ordinal) > 0)
                //                        obj = objHist;
                //                }
                //                else if (obj is decimal)
                //                {
                //                    //Phai xu ly cast lai do decimal lay tu oracle ko cast truc tiep sang long
                //                    var a1 = (decimal)obj;
                //                    var a2 = (decimal)objHist;
                //                    if (a1 < a2)
                //                        obj = (long)a2;
                //                }
                //                else
                //                {
                //                    if ((long)obj < (long)objHist) obj = objHist; //Nếu key bên hist lớn hơn thì max key là hist
                //                }
                //            }
                //            catch (Exception ex)
                //            {
                //                LogTo.Error("ENTITY NAME NOT CAST = " + request.Query.EntityName + " ERORR =" + ex.ToString());
                //            }

                //        }
                //        request.Query.ReturnMaxKey = obj;
                //    }
                //    #endregion

                //    #endregion
                //}

                //request.Done = true; 
                #endregion
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
            #region lay du lieu o day, chu y dung nguyen msg nay, ko clone
            #endregion

            // cuoi cung thi call ham nay
            Sendback(request);
        }

        private void Sendback(GetStartupMemoryRequest request)
        {
            _process.ProcessMsg(new ReceiveData()
            {
                Message = request
            }, (byte)QueueType.MainQueue);
        }

        //Cac ham lay o ProcessReadDatabaseUtils tren Dataprovider
        #region Cac ham lay o file ProcessReadDatabaseUtils tren Dataprovider
        private static EntityQuery GetEntityQuery(EntityQuery entityQuery,
            bool isClientRequest = true, bool isGetCurrent = false)
        {
            try
            {
                //Hien tai dang khong su dung
                //Rao du lieu de bo build DAL

                //var getListEntityDao = new GetListEntityDao();
                //if (entityQuery.IsNotGetValue) return entityQuery; //Lấy maxKey mà không lấy dữ liệu

                ////if (isClientRequest && (entityQuery.ItemsCount <= 0 || entityQuery.ItemsCount > Consts.MAX_LENGHT_QUERRY))
                ////{
                ////    //Nếu là client request không trả quá ItemCounts bản ghi
                ////    entityQuery.ItemsCount = Consts.MAX_LENGHT_QUERRY;
                ////}

                //entityQuery.ReturnValue = getListEntityDao.Select(entityQuery, isGetCurrent).Select(c =>
                //                    new Entity.Entity(c)).ToList();
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
            return entityQuery;
        }
        private static object GetMaxId(string entityName, string fieldName)
        {
            try
            {
                //Hien tai dang khong su dung
                //Rao du lieu de bo build DAL

                //var getListEntityDao = new GetListEntityDao();
                //return getListEntityDao.GetMaxId(entityName, fieldName);
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
            return null;
        }

        private static List<BaseEntity> GetEntityByCommandText(string entityName, string commandText)
        {
            try
            {
                //var getListEntityDao = new GetListEntityDao();
                //return getListEntityDao.GetEntityByCommandText(entityName, commandText);
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
            return new List<BaseEntity>();
        }
        #endregion

        #region Cacs ham lay o file EntityHistoryUtils tren Dataprovider
        //Xem bang này có bảng lịch sử clone từ bảng này ra hay không?
        //Su dung chu yeu cho viec getMaxKey, phai lay max tu ca 2 bang moi co maxKey chuan
        private static string GetEntityNameHist(string entityName)
        {
            if (entityName == ApprovalOrder.EntityName())
                return ApprovalOrderHist.EntityName();
            if (entityName == ApprovalPreRisk.EntityName())
                return ApprovalPreRiskHist.EntityName();
            if (entityName == ApprovalAccount.EntityName())
                return ApprovalAccountHist.EntityName();
            if (entityName == ApprovalMember.EntityName())
                return ApprovalMemberHist.EntityName();
            if (entityName == ApprovalDealing.EntityName())
                return ApprovalDealingHist.EntityName();
            if (entityName == ApprovalSystem.EntityName())
                return ApprovalSystemHist.EntityName();
            return null;
        }
        #endregion

        #region Cac ham lay o file DataProviderUtils tren Dataprovider
        public static List<MemberInfo> GetListMemberInGroupCustomer()
        {
            //Lay tat ca member la nhom khach hang
            try
            {
                var entityQry = new EntityQuery
                {
                    EntityName = MemberInfo.EntityName(),
                    QueryAction = EntityGet.GetCustomValues
                };
                var colection = new DescriptorColection
                {
                    Logical = LogicalOperator.AND
                };

                var descriptorSession = new Descriptor { Logical = LogicalOperator.AND };
                descriptorSession.Descriptors.Add(new Descriptor
                {
                    FieldName = MemberInfo.MemberInfoFields.MemberType.ToString(),
                    FieldValue = Descriptor.GetInputValue((int)MemberType.Customer),
                    Operator = DataOperator.IsEqualTo
                });
                descriptorSession.Descriptors.Add(new Descriptor
                {
                    FieldName = MemberInfo.MemberInfoFields.MemberParent.ToString(),
                    FieldValue = Descriptor.GetInputValue(null),
                    Operator = DataOperator.IsNull
                });
                colection.Descriptors.Add(descriptorSession);

                entityQry.DescriptorColection = colection;
                entityQry = GetEntityQuery(entityQry);

                if (entityQry.ReturnValue != null)
                {
                    var listValue = entityQry.ReturnValue.Select(baseEntity =>
                                                             baseEntity.GetEntity() as MemberInfo).ToList();
                    return listValue;
                }
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
            return new List<MemberInfo>();
        }

        public static List<MemberInfo> GetListMemberInCustomerVangLai()
        {
            //Lay tat ca member la nhom khach hang
            try
            {
                //Select * from MemberInfo where MemberType = 3 and CustomerGroup = 3
                var entityQry = new EntityQuery
                {
                    EntityName = MemberInfo.EntityName(),
                    QueryAction = EntityGet.GetCustomValues
                };
                var colection = new DescriptorColection
                {
                    Logical = LogicalOperator.AND
                };

                var descriptorSession = new Descriptor { Logical = LogicalOperator.AND };
                descriptorSession.Descriptors.Add(new Descriptor
                {
                    FieldName = MemberInfo.MemberInfoFields.MemberType.ToString(),
                    FieldValue = Descriptor.GetInputValue((int)MemberType.Customer),
                    Operator = DataOperator.IsEqualTo
                });
                descriptorSession.Descriptors.Add(new Descriptor
                {
                    FieldName = MemberInfo.MemberInfoFields.CustomerGroup.ToString(),
                    FieldValue = Descriptor.GetInputValue((int)CustomerGroup.VL),
                    Operator = DataOperator.IsEqualTo
                });
                colection.Descriptors.Add(descriptorSession);

                entityQry.DescriptorColection = colection;
                entityQry = GetEntityQuery(entityQry);

                if (entityQry.ReturnValue != null)
                {
                    var listValue = entityQry.ReturnValue.Select(baseEntity =>
                                                             baseEntity.GetEntity() as MemberInfo).ToList();
                    return listValue;
                }
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
            return new List<MemberInfo>();
        }

        public static List<UserInfo> GetListUserInfoInCustomerVangLai()
        {
            //Lay tat ca member la nhom khach hang
            try
            {
                //Select * from UserInfo where MemberId in (Select MemberId from MemberInfo where MemberType = 3 and CustomerGroup = 3)
                //var descriptorColection = new DescriptorColection
                //{
                //    Logical = LogicalOperator.AND
                //};

                //descriptorColection.Descriptors.Add(new Descriptor
                //{
                //    FieldName = MemberInfo.MemberInfoFields.MemberType.ToString(),
                //    FieldValue = Descriptor.GetInputValue((int)MemberType.Customer),
                //    Operator = DataOperator.IsEqualTo
                //});
                //descriptorColection.Descriptors.Add(new Descriptor
                //{
                //    FieldName = MemberInfo.MemberInfoFields.CustomerGroup.ToString(),
                //    FieldValue = Descriptor.GetInputValue((int)CustomerGroup.VL),
                //    Operator = DataOperator.IsEqualTo
                //});
                //var customQuery = EntityBaseSql.GetSqlCommandByFieldAndCustomValues(
                //        MemberInfo.MemberInfoFields.MemberId.ToString(), descriptorColection,
                //        MemberInfo.EntityName());

                //var entityQry = new EntityQuery
                //{
                //    EntityName = UserInfo.EntityName(),
                //    QueryAction = EntityGet.GetCustomValues
                //};
                //var colection = new DescriptorColection
                //{
                //    Logical = LogicalOperator.AND
                //};

                //colection.Descriptors.Add(Descriptor.GetDescriptorList(
                //    UserInfo.UserInfoFields.MemberId.ToString(), DataOperator.IsIn, false, customQuery));

                //entityQry.DescriptorColection = colection;
                //entityQry = GetEntityQuery(entityQry);

                //if (entityQry.ReturnValue != null)
                //{
                //    var listValue = entityQry.ReturnValue.Select(baseEntity =>
                //                                             baseEntity.GetEntity() as UserInfo).ToList();
                //    return listValue;
                //}
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
            return new List<UserInfo>();
        }

        public static List<long> GetListMemberIdBranch(List<Entity.Entity> lstEntity)
        {
            var lstResult = new List<long>();
            if (lstEntity == null) return lstResult;
            foreach (var entity in lstEntity)
            {
                var memberInfo = entity.GetEntity() as MemberInfo;
                if(memberInfo.MemberType != (int)MemberType.Client)
                    continue;

                if(memberInfo.MemberParent.HasValue)
                    lstResult.Add(memberInfo.MemberId);
            }
            return lstResult;
        }
        #endregion

        #region Cac ham lay o file DataTimeUtils tren Dataprovider
        public static List<SessionHistory> Select400SessionHistory(long itemCount)
        {
            //SELECT * FROM (SELECT * FROM SessionHistory  ORDER BY SessionId DESC ) WHERE ROWNUM <= 400;
            var entityQry = new EntityQuery
            {
                EntityName = SessionHistory.EntityName(),
                QueryAction = EntityGet.GetCustomValues,
            };
            entityQry.ItemsCount = itemCount;
            entityQry.ItemsSort = SessionHistory.SessionHistoryFields.SessionId.ToString();
            entityQry.SortType = "DESC";
            entityQry = GetEntityQuery(entityQry);
            var sessionHistories = new List<SessionHistory>();
            if (entityQry.ReturnValue != null)
            {
                sessionHistories = entityQry.ReturnValue.Select(baseEntity =>
                        baseEntity.GetEntity() as SessionHistory).ToList();
            }
            return sessionHistories;
        }
        #endregion
    }
}

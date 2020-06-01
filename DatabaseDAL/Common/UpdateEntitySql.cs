using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Anotar.NLog;
using ElectricShop.Entity;

namespace ElectricShop.DatabaseDAL.Common
{
    public class UpdateEntitySql : IDisposable
    {
        //Cập nhật tạo mới thông tin mặc định, gửi bao nhiêu cập nhật bấy nhiêu
        public bool UpdateDefault(List<EntityCommand> listEntityActionCommand)
        {
            //MainConection
            SqlConnection mainConnection = null;
            SqlTransaction transactionWorking = null;

            
            var dicWorking = new Dictionary<SqlCommand, EntityCommand>();

            var dicBaseSqlWorking = new Dictionary<SqlCommand, EntityBaseSql>();

            //Biến báo lỗi sql tại đâu
            bool isQueryWorkingSuccess = false; //Lỗi tại db working
            bool isCommitWorkingSuccess = false; //Lỗi commit working

            string stringCommand = "";
            try
            {
                foreach (var entityCommand in listEntityActionCommand)
                {
                    #region LẤY DIC WORKING  theo COMMAND
                    //Console.WriteLine(entityCommand.BaseEntity.GetName() + " "+listEntityActionCommand.Count);
                    stringCommand = entityCommand.BaseEntity.GetName() + "_" + entityCommand.EntityAction;
                    var entityBaseSql = EntityManager.Instance.GetMyEntity(entityCommand.BaseEntity.GetName());
                    var sqlCommand = entityBaseSql.GetSqlCommand(entityCommand);
                    sqlCommand.CommandText = entityBaseSql.GetSqlAction(entityCommand);

                    if (mainConnection == null)
                    {
                        mainConnection = EntityManager.Instance.GetConnection();
                        transactionWorking = mainConnection.BeginTransaction();
                    }

                    sqlCommand.Connection = mainConnection;
                    sqlCommand.Transaction = transactionWorking;

                    dicWorking[sqlCommand] = entityCommand;
                    dicBaseSqlWorking[sqlCommand] = entityBaseSql;
                    #endregion
                }

                #region EXECUTE NON QUERY
                foreach (var valueKey in dicWorking)
                {
                    var sqlCommand = valueKey.Key;
                    sqlCommand.ExecuteNonQuery();
                    var entityBaseSql = dicBaseSqlWorking[sqlCommand];
                    entityBaseSql.UpdateEntityId(valueKey.Value, sqlCommand);
                }
                isQueryWorkingSuccess = true; //Báo thành công db working
                
                #endregion

                #region TRANSACTION COMMIT

                if (transactionWorking != null)
                {
                    transactionWorking.Commit();
                    isCommitWorkingSuccess = true;
                }
                
                #endregion

                return true;
            }
            catch (Exception ex)
            {
                /* Có các trường hợp sau xảy ra
                 * 1. Mất kết nối trước khi hoặc đang gọi hàm execution non query
                 * A network-related or instance-specific error occurred while establishing a connection to SQL Server. The server was not found or was not accessible. Verify that the instance name is correct and that SQL Server is configured to allow remote connections. (provider: Named Pipes Provider, error: 40 - Could not open a connection to SQL Server)
                 * 2. Mất kết nối sau khi exection non query và trước khi commit
                 * Timeout expired.  The timeout period elapsed prior to completion of the operation or the server is not responding.
                 */

                #region Comment code not test
                //if (!isQueryWorkingSuccess || !isQueryHistSuccess)
                //{
                //    #region Lỗi trong quá trình execution non query
                //    // Handle the exception if the transaction fails to commit.
                //    try
                //    {
                //        if (ex.Message.Contains("A network-related or instance-specific error occurred while " +
                //                "establishing a connection to SQL Server"))
                //        {
                //            //Lỗi mất kết nối, có gọi hàm rollback cũng không giải quyết vấn đề gì, dữ liệu transaction chưa được đưa vào db
                //            throw new Exception("UpdateEntitySql::UpdateDefault:EntityError: "
                //                            + stringCommand + ":Error occured.", ex);
                //        }
                //        else
                //        {
                //            //Lỗi do dữ liệu hoặc vấn đề nào đó trong execution non query
                //            // Attempt to roll back the transaction.
                //            if (transactionWorking != null)
                //                transactionWorking.Rollback();
                //            if (transactionHist != null)
                //                transactionHist.Rollback();
                //        }
                //    }
                //    catch (Exception exRollback)
                //    {
                //        //Ngắt kết nối trước khi rollback thì có lỗi sau :
                //        //This SqlTransaction has completed; it is no longer usable.

                //        // Throws an InvalidOperationException if the connection  
                //        // is closed or the transaction has already been rolled  
                //        // back on the server.
                //        LogAndWriteEntityComandWhenRollbackExeption(listEntityActionCommand);
                //        LogTo.Error("UpdateEntitySql::UpdateDefault:EntityError: " + stringCommand +
                //            ":Error occured.", ex.Message);
                //        throw new Exception("Error for Rollback: ", exRollback);
                //    }
                //    #endregion
                //}
                //else
                //{
                //    #region Lỗi trong quá trình commit
                //    try
                //    {
                //        string exTimeOut = "Timeout expired.  The timeout period " +
                //                           "elapsed prior to completion of the operation or the server is not responding.";
                //        if (!isCommitWorkingSuccess && transactionWorking != null)
                //        {
                //            #region Nếu lỗi trong quá trình xử lý commit tại db working
                //            if (ex.Message.Contains(exTimeOut))
                //            {
                //                //Mất kết nối khi commit, chưa commit bảng hist do đó ko cần xử lý hist
                //                //Nếu có khả năng thì check xem commit thành công hay chưa, nếu thành công và mất kết nối luôn thì phải rollback lại
                //                LogTo.Error("UpdateEntitySql::UpdateDefault:EntityError: " + stringCommand +
                //                                ":Error occured.", ex.Message);
                //            }
                //            else
                //            {
                //                //Lỗi khác khi đang commit
                //                // Attempt to roll back the transaction.
                //                transactionWorking.Rollback(); //ko cần rollback hist
                //            }
                //            #endregion
                //        }
                //        else if (!isCommitHistSuccess)
                //        {
                //            #region Nếu lỗi trong quá trình xử lý commit tại db hist
                //            if (ex.Message.Contains(exTimeOut))
                //            {
                //                //Mất kết nối khi commit, chưa commit bảng hist do đó ko cần xử lý hist
                //                //Nếu có khả năng thì check xem commit thành công hay chưa, nếu thành công và mất kết nối luôn thì phải rollback lại
                //                //Nhớ xử lý rollback lại commit db working nếu có khả năng
                //                LogTo.Error("UpdateEntitySql::UpdateDefault:EntityError: " + stringCommand +
                //                                ":Error occured.", ex.Message);
                //            }
                //            else
                //            {
                //                // Attempt to roll back the transaction.
                //                if (transactionWorking != null)
                //                    transactionWorking.Rollback();
                //                if (transactionHist != null)
                //                    transactionHist.Rollback();
                //            }
                //            #endregion
                //        }
                //    }
                //    catch (Exception exRollback)
                //    {
                //        // Throws an InvalidOperationException if the connection  
                //        // is closed or the transaction has already been rolled  
                //        // back on the server.
                //        LogAndWriteEntityComandWhenRollbackExeption(listEntityActionCommand);
                //        LogTo.Error("UpdateEntitySql::UpdateDefault:EntityError: " + stringCommand +
                //            ":Error occured.", ex.Message);
                //        throw new Exception("Error for Rollback: ", exRollback);
                //    }
                //    #endregion
                //} 
                #endregion

                if (isCommitWorkingSuccess)
                {
                    //Không xử lý rollback nếu commit thành công
                    throw new Exception("UpdateEntitySql::UpdateDefault:EntityError: "
                        + stringCommand + ":Error occured.", ex);
                }
                else
                {
                    #region ROLLBACK

                    try
                    {
                        if (transactionWorking != null)
                            transactionWorking.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                        // Throws an InvalidOperationException if the connection  
                        // is closed or the transaction has already been rolled  
                        // back on the server.
                        LogTo.Error("UpdateEntitySql::UpdateDefault:EntityError: " + stringCommand +
                            ":Error occured.", ex.Message);
                        throw new Exception("Error for Rollback: ", exRollback);
                    }
                    throw new Exception("UpdateEntitySql::UpdateDefault:EntityError: "
                        + stringCommand + ":Error occured.", ex);
                    #endregion
                }

            }
            finally
            {
                #region FINALLY
                if (mainConnection != null)
                {
                    mainConnection.Close();
                    mainConnection.Dispose();
                    mainConnection = null;
                }
                

                foreach (var sqlCommand in dicWorking.Keys)
                {
                    sqlCommand.Dispose();
                }
                

                
                if (transactionWorking != null)
                {
                    transactionWorking.Dispose();
                    transactionWorking = null;
                }

                dicWorking.Clear();
                dicWorking = null;
                
                dicBaseSqlWorking.Clear();
                dicBaseSqlWorking = null;
                

                #endregion
            }
            return false;
        }



        public void Dispose()
        {
        }
    }
}

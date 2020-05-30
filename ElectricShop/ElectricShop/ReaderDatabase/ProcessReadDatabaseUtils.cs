using System;
using System.Collections.Generic;
using System.Linq;
using Anotar.NLog;
using ElectricShop.DatabaseDAL.Common;
using ElectricShop.Entity;

namespace ElectricShop.ReaderDatabase
{
    public class ProcessReadDatabaseUtils
    {
        //Hàm này hiện tại sử dụng cho các worker gửi message sang lấy dữ liệu
        public static List<EntityQuery> GetListEntityQuery(List<EntityQuery> listEntityQuery)
        {
            try
            {
                //isGetRef = false tuc la chi dinh lay bang nao thi select bang do (Dung cho lay bang working ko lay bang hist)
                var getListEntityDao = new GetListEntityDao();
                foreach (var entityQuery in listEntityQuery)
                {
                    if(entityQuery.IsNotGetValue) continue; //Lấy maxKey mà không lấy dữ liệu

                    entityQuery.ReturnValue = getListEntityDao.Select(entityQuery).Select(c => new Entity.Entity(c)).ToList(); 
                }
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
                return null; //Không cho phép ready worker khi không lấy được thông tin
            }
            return listEntityQuery;
        }

        public static EntityQuery GetEntityQuery(EntityQuery entityQuery)
        {
            try
            {
                var getListEntityDao = new GetListEntityDao();
                if (entityQuery.IsNotGetValue) return entityQuery; //Lấy maxKey mà không lấy dữ liệu

                entityQuery.ReturnValue = getListEntityDao.Select(entityQuery).Select(c => new Entity.Entity(c)).ToList();
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
            return entityQuery;
        }

        public static object GetMaxId(string entityName, string fieldName)
        {
            try
            {
                var getListEntityDao = new GetListEntityDao();
                return getListEntityDao.GetMaxId(entityName, fieldName);
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
            return null;
        }

        public static long GetTotalRowBySql(string entityName, string sqlWhere)
        {
            try
            {
                var getListEntityDao = new GetListEntityDao();
                return getListEntityDao.GetTotalRowBySql(entityName, sqlWhere);
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
            return -1;
        }

        public static List<BaseEntity> GetEntityByCommandText(string entityName, string commandText)
        {
            try
            {
                var getListEntityDao = new GetListEntityDao();
                return getListEntityDao.SelectTextQuerry(commandText, entityName);
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
                throw new Exception(string.Format("GetEntityByCommandText {0}::{1}", entityName, commandText));
            }
        }
    }
}

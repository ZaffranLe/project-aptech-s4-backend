using System.Collections.Generic;
using ElectricShop.Entity;

namespace ElectricShop.DatabaseDAL.Common
{
    public class GetListEntityDao
    {
        #region Data Members

        private readonly GetListEntitySql _dataObject;

        #endregion

        #region Constructor

        public GetListEntityDao()
        {
            
                _dataObject = new GetListEntitySql();
        }

        #endregion

        #region Public Methods
        public List<BaseEntity> Select(EntityQuery entityQuery)
        {
            //Thông tin GetAll, GetCustom được tách ở phía dưới
            return _dataObject.GetAllEntity(entityQuery);
        }

        public object GetMaxId(string entityName, string fieldName)
        {
            
            return _dataObject.GetMaxId(entityName, fieldName);
        }

        public List<BaseEntity> SelectTextQuerry(string textQuerry, string entityName)
        {
            
            return _dataObject.SelectTextQuerry(textQuerry, entityName);
        }

        public long GetTotalRowBySql(string entityName, string whereSql)
        {

            var obj = _dataObject.GetTotalRowBySql(entityName, whereSql);
            if (obj != null)
            {
                long idParse;
                long.TryParse(obj.ToString(), out idParse);
                return idParse;
            }
            return -1;
        }
        #endregion
    }
}

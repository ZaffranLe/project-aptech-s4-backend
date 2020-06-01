using System;
using System.Collections.Generic;
using ElectricShop.Entity;

namespace ElectricShop.DatabaseDAL.Common
{
    public class ProcedureEntityDao : IDisposable
    {
        #region Data Members

        private ProcedureEntitySql _dataObject;

        #endregion

        #region Constructor

        public ProcedureEntityDao()
        {
            _dataObject = new ProcedureEntitySql();
        }

        #endregion

        

        public bool ProcessProcedureByName(string name)
        {
            return _dataObject.ProcessProcedureByName(name);
        }
        
        public List<BaseEntity> GetListEntityByProcedureName(string entityName, string procedureName)
        {
            return _dataObject.GetListEntityByProcedureName(entityName, procedureName);
        }

        public List<BaseEntity> GetListEntityByProcedureName(string entityName, string procedureName, List<DefinitionStoreProce> listDefinition)
        {
            return _dataObject.GetListEntityByProcedureNameWithParam(entityName, procedureName, listDefinition);
        }
        public void Dispose()
        {
            if (_dataObject != null)
                _dataObject.Dispose();
            _dataObject = null;
        }
    }
}

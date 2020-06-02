using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using ElectricShop.Entity;
using ElectricShop.Entity.Entities;
using ElectricShop.DatabaseDAL.Common;
using System.Data;
using Anotar.NLog;

namespace ElectricShop.DatabaseDAL.EntitySql
{
	public class $CLASS_NAME$Sql : EntityBaseSql 
	{

        #region Constructor

		public $CLASS_NAME$Sql()
		{
			 Init();
		}

        #endregion
		
		#region Init

		private void Init()
		{
           Insertsql = "$storeProceduceNameInsert$";
           Updatesql = "$storeProceduceNameUpdate$";
           Deletesql = "$storeProceduceNameDelete$";
           Selectsql = "$storeProceduceNameSelect$";
		}

        #endregion

        #region Public override Methods

        public override OracleCommand GetSqlCommandForInsert(BaseEntity baseEntity)
		{
			var sqlCommand = new OracleCommand { CommandType = CommandTypeProcedure };
            if (baseEntity != null)
            {
                var businessObject = baseEntity as $CLASS_NAME$;
                if (businessObject != null)
				{

				$INSERT_PARAMETER$

				}
			}
            return sqlCommand;	
		}

		public override BaseEntity UpdateEntityId(BaseEntity baseEntity, OracleCommand sqlCommand)
        {
            if (baseEntity != null)
            {
                $GET_RETURNED_VALUE_CHECK$
                var businessObject = baseEntity as $CLASS_NAME$;

                if (businessObject != null)
                {
                $GET_RETURNED_VALUE$
                }
                return businessObject;
            }
            return null;
        }

        public override OracleCommand GetSqlCommandForUpdate(BaseEntity baseEntity)
        {
            var sqlCommand = new OracleCommand { CommandType = CommandTypeProcedure };
            if (baseEntity != null)
            {
                var businessObject = baseEntity as $CLASS_NAME$;

                if (businessObject != null)
                {

				$UPDATE_PARAMETER$
				
				}
			}       
         return sqlCommand;
		}
        
        public override OracleCommand GetSqlCommandForDelete(BaseEntity baseEntity)
		{
            var sqlCommand = new OracleCommand {CommandType = CommandTypeProcedure};
            if (baseEntity != null)
            {
                var businessObject = baseEntity as $CLASS_NAME$;
                if (businessObject != null)
                {

				$SELECT_BY_PK_PARAMETERS$
				
				}
            }
            return sqlCommand;
        }
        
		public override List<BaseEntity> PopulateBusinessObjectFromReader(IDataReader dataReader)
        {
			var list = new List<BaseEntity>();
            _dicIndex = new Dictionary<string, int>();
            while (dataReader.Read())
            {
                var businessObject = new $CLASS_NAME$();
                PopulateBusinessObjectFromReader(businessObject, dataReader);
                list.Add(businessObject);
            }

            return list;
		}     

        #endregion

        #region Private Methods
        public static Dictionary<string, int> _dicIndex = new Dictionary<string, int>();
	    public static void FillDicIndex(IDataReader dataReader)
	    {
	        for (int i = 0; i < dataReader.FieldCount; i++)
	        {
	            var columnName = dataReader.GetName(i);
	            _dicIndex[columnName.ToLower()] = i;
	        }
	    }
	    public static int GetIndex(string name)
	    {
	        if (_dicIndex.ContainsKey(name.ToLower()))
	            return _dicIndex[name.ToLower()];
	        //LogTo.Info("Khong tim thay column name = " + name.ToLower());
	        return -1;
	    }

    internal void PopulateBusinessObjectFromReader($CLASS_NAME$ businessObject, IDataReader dataReader)
        {
            if (_dicIndex.Count == 0) FillDicIndex(dataReader);
		$POPULATE_BUSINESS_OBJECT_PARAMERTERS$

        }

        #endregion
	}
}

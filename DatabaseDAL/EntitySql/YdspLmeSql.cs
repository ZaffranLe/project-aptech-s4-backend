using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ElectricShop.DatabaseDAL.Common;
using ElectricShop.Entity;
using ElectricShop.Entity.Entities;

namespace ElectricShop.DatabaseDAL.EntitySql
{
	public class YdspLmeSql : EntityBaseSql 
	{

        #region Constructor

		public YdspLmeSql()
		{
			 Init();
		}

        #endregion
		
		#region Init

		private void Init()
		{
           Insertsql = "YdspLme_Insert";
           Updatesql = "YdspLme_Update";
           Deletesql = "YdspLme_DeleteByPrimaryKey";
           Selectsql = "YdspLme_SelectAll";
		}

        #endregion

        #region Public override Methods

        public override SqlCommand GetSqlCommandForInsert(BaseEntity baseEntity)
		{
			var sqlCommand = new SqlCommand { CommandType = CommandTypeProcedure };
            if (baseEntity != null)
            {
                var businessObject = baseEntity as YdspLme;
                if (businessObject != null)
				{

								sqlCommand.Parameters.Add(new SqlParameter("@BussinessDate", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.BussinessDate));
				sqlCommand.Parameters.Add(new SqlParameter("@FileLme", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.FileLme));
				sqlCommand.Parameters.Add(new SqlParameter("@Price", SqlDbType.Text, 2147483647, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.Price));
				sqlCommand.Parameters.Add(new SqlParameter("@TimeChanged", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.TimeChanged));


				}
			}
            return sqlCommand;	
		}

		public override BaseEntity UpdateEntityId(BaseEntity baseEntity, SqlCommand sqlCommand)
        {
            if (baseEntity != null)
            {
                return baseEntity;
                var businessObject = baseEntity as YdspLme;

                if (businessObject != null)
                {
                
                }
                return businessObject;
            }
            return null;
        }

        public override SqlCommand GetSqlCommandForUpdate(BaseEntity baseEntity)
        {
            var sqlCommand = new SqlCommand { CommandType = CommandTypeProcedure };
            if (baseEntity != null)
            {
                var businessObject = baseEntity as YdspLme;

                if (businessObject != null)
                {

								sqlCommand.Parameters.Add(new SqlParameter("@BussinessDate", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.BussinessDate));
				sqlCommand.Parameters.Add(new SqlParameter("@FileLme", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.FileLme));
				sqlCommand.Parameters.Add(new SqlParameter("@Price", SqlDbType.Text, 2147483647, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.Price));
				sqlCommand.Parameters.Add(new SqlParameter("@TimeChanged", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.TimeChanged));

				
				}
			}       
         return sqlCommand;
		}
        
        public override SqlCommand GetSqlCommandForDelete(BaseEntity baseEntity)
		{
            var sqlCommand = new SqlCommand {CommandType = CommandTypeProcedure};
            if (baseEntity != null)
            {
                var businessObject = baseEntity as YdspLme;
                if (businessObject != null)
                {

								sqlCommand.Parameters.Add(new SqlParameter("@FileLme", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.FileLme));

				
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
                var businessObject = new YdspLme();
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

    internal void PopulateBusinessObjectFromReader(YdspLme businessObject, IDataReader dataReader)
        {
            if (_dicIndex.Count == 0) FillDicIndex(dataReader);
		
			if (GetIndex(YdspLme.YdspLmeFields.BussinessDate.ToString()) != -1)
				if (!dataReader.IsDBNull(GetIndex(YdspLme.YdspLmeFields.BussinessDate.ToString())))
				{
					businessObject.BussinessDate = dataReader.GetDateTime(GetIndex(YdspLme.YdspLmeFields.BussinessDate.ToString()));
				}

				businessObject.FileLme = dataReader.GetString(GetIndex(YdspLme.YdspLmeFields.FileLme.ToString()));

			if (GetIndex(YdspLme.YdspLmeFields.Price.ToString()) != -1)
				if (!dataReader.IsDBNull(GetIndex(YdspLme.YdspLmeFields.Price.ToString())))
				{
					businessObject.Price = dataReader.GetString(GetIndex(YdspLme.YdspLmeFields.Price.ToString()));
				}

			if (GetIndex(YdspLme.YdspLmeFields.TimeChanged.ToString()) != -1)
				if (!dataReader.IsDBNull(GetIndex(YdspLme.YdspLmeFields.TimeChanged.ToString())))
				{
					businessObject.TimeChanged = dataReader.GetDateTime(GetIndex(YdspLme.YdspLmeFields.TimeChanged.ToString()));
				}


        }

        #endregion
	}
}


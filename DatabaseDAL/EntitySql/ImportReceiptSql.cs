using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ElectricShop.Entity;
using ElectricShop.Entity.Entities;
using ElectricShop.DatabaseDAL.Common;
using System.Data;
using Anotar.NLog;

namespace ElectricShop.DatabaseDAL.EntitySql
{
	public class ImportReceiptSql : EntityBaseSql 
	{

        #region Constructor

		public ImportReceiptSql()
		{
			 Init();
		}

        #endregion
		
		#region Init

		private void Init()
		{
           Insertsql = "ImportReceipt_Insert";
           Updatesql = "ImportReceipt_Update";
           Deletesql = "ImportReceipt_DeleteByPrimaryKey";
           Selectsql = "ImportReceipt_SelectAll";
		}

        #endregion

        #region Public override Methods

        public override SqlCommand GetSqlCommandForInsert(BaseEntity baseEntity)
		{
			var sqlCommand = new SqlCommand { CommandType = CommandTypeProcedure };
            if (baseEntity != null)
            {
                var businessObject = baseEntity as ImportReceipt;
                if (businessObject != null)
				{

								sqlCommand.Parameters.Add(new SqlParameter("@CreatedAt", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.CreatedAt));
				sqlCommand.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.CreatedBy));
				sqlCommand.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.Date));
				sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.Id));
				sqlCommand.Parameters.Add(new SqlParameter("@IdEmployee", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.IdEmployee));
				sqlCommand.Parameters.Add(new SqlParameter("@IdProvider", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.IdProvider));
				sqlCommand.Parameters.Add(new SqlParameter("@ListProductId", SqlDbType.VarChar, 255, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.ListProductId));
				sqlCommand.Parameters.Add(new SqlParameter("@TotalPrice", SqlDbType.Decimal, 17, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.TotalPrice));
				sqlCommand.Parameters.Add(new SqlParameter("@UpdatedAt", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.UpdatedAt));
				sqlCommand.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.UpdatedBy));


				}
			}
            return sqlCommand;	
		}

		public override BaseEntity UpdateEntityId(BaseEntity baseEntity, SqlCommand sqlCommand)
        {
            if (baseEntity != null)
            {
                return baseEntity;
                var businessObject = baseEntity as ImportReceipt;

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
                var businessObject = baseEntity as ImportReceipt;

                if (businessObject != null)
                {

								sqlCommand.Parameters.Add(new SqlParameter("@CreatedAt", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.CreatedAt));
				sqlCommand.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.CreatedBy));
				sqlCommand.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.Date));
				sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.Id));
				sqlCommand.Parameters.Add(new SqlParameter("@IdEmployee", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.IdEmployee));
				sqlCommand.Parameters.Add(new SqlParameter("@IdProvider", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.IdProvider));
				sqlCommand.Parameters.Add(new SqlParameter("@ListProductId", SqlDbType.VarChar, 255, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.ListProductId));
				sqlCommand.Parameters.Add(new SqlParameter("@TotalPrice", SqlDbType.Decimal, 17, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.TotalPrice));
				sqlCommand.Parameters.Add(new SqlParameter("@UpdatedAt", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.UpdatedAt));
				sqlCommand.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.UpdatedBy));

				
				}
			}       
         return sqlCommand;
		}
        
        public override SqlCommand GetSqlCommandForDelete(BaseEntity baseEntity)
		{
            var sqlCommand = new SqlCommand {CommandType = CommandTypeProcedure};
            if (baseEntity != null)
            {
                var businessObject = baseEntity as ImportReceipt;
                if (businessObject != null)
                {

								sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.Id));

				
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
                var businessObject = new ImportReceipt();
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

    internal void PopulateBusinessObjectFromReader(ImportReceipt businessObject, IDataReader dataReader)
        {
            if (_dicIndex.Count == 0) FillDicIndex(dataReader);
		
			if (GetIndex(ImportReceipt.ImportReceiptFields.CreatedAt.ToString()) != -1)
				if (!dataReader.IsDBNull(GetIndex(ImportReceipt.ImportReceiptFields.CreatedAt.ToString())))
				{
					businessObject.CreatedAt = dataReader.GetDateTime(GetIndex(ImportReceipt.ImportReceiptFields.CreatedAt.ToString()));
				}

			if (GetIndex(ImportReceipt.ImportReceiptFields.CreatedBy.ToString()) != -1)
				businessObject.CreatedBy = dataReader.GetInt32(GetIndex(ImportReceipt.ImportReceiptFields.CreatedBy.ToString()));

			if (GetIndex(ImportReceipt.ImportReceiptFields.Date.ToString()) != -1)
				businessObject.Date = dataReader.GetDateTime(GetIndex(ImportReceipt.ImportReceiptFields.Date.ToString()));

				businessObject.Id = dataReader.GetInt32(GetIndex(ImportReceipt.ImportReceiptFields.Id.ToString()));

			if (GetIndex(ImportReceipt.ImportReceiptFields.IdEmployee.ToString()) != -1)
				businessObject.IdEmployee = dataReader.GetInt32(GetIndex(ImportReceipt.ImportReceiptFields.IdEmployee.ToString()));

			if (GetIndex(ImportReceipt.ImportReceiptFields.IdProvider.ToString()) != -1)
				businessObject.IdProvider = dataReader.GetInt32(GetIndex(ImportReceipt.ImportReceiptFields.IdProvider.ToString()));

			if (GetIndex(ImportReceipt.ImportReceiptFields.ListProductId.ToString()) != -1)
				if (!dataReader.IsDBNull(GetIndex(ImportReceipt.ImportReceiptFields.ListProductId.ToString())))
				{
					businessObject.ListProductId = dataReader.GetString(GetIndex(ImportReceipt.ImportReceiptFields.ListProductId.ToString()));
				}

			if (GetIndex(ImportReceipt.ImportReceiptFields.TotalPrice.ToString()) != -1)
				businessObject.TotalPrice = dataReader.GetDecimal(GetIndex(ImportReceipt.ImportReceiptFields.TotalPrice.ToString()));

			if (GetIndex(ImportReceipt.ImportReceiptFields.UpdatedAt.ToString()) != -1)
				if (!dataReader.IsDBNull(GetIndex(ImportReceipt.ImportReceiptFields.UpdatedAt.ToString())))
				{
					businessObject.UpdatedAt = dataReader.GetDateTime(GetIndex(ImportReceipt.ImportReceiptFields.UpdatedAt.ToString()));
				}

			if (GetIndex(ImportReceipt.ImportReceiptFields.UpdatedBy.ToString()) != -1)
				businessObject.UpdatedBy = dataReader.GetInt32(GetIndex(ImportReceipt.ImportReceiptFields.UpdatedBy.ToString()));


        }

        #endregion
	}
}

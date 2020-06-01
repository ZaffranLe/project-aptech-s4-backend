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
	public class OrderDetailSql : EntityBaseSql 
	{

        #region Constructor

		public OrderDetailSql()
		{
			 Init();
		}

        #endregion
		
		#region Init

		private void Init()
		{
           Insertsql = "OrderDetail_Insert";
           Updatesql = "OrderDetail_Update";
           Deletesql = "OrderDetail_DeleteByPrimaryKey";
           Selectsql = "OrderDetail_SelectAll";
		}

        #endregion

        #region Public override Methods

        public override SqlCommand GetSqlCommandForInsert(BaseEntity baseEntity)
		{
			var sqlCommand = new SqlCommand { CommandType = CommandTypeProcedure };
            if (baseEntity != null)
            {
                var businessObject = baseEntity as OrderDetail;
                if (businessObject != null)
				{

								sqlCommand.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar, 255, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.Address));
				sqlCommand.Parameters.Add(new SqlParameter("@CreatedAt", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.CreatedAt));
				sqlCommand.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.CreatedBy));
				sqlCommand.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.Date));
				sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.Id));
				sqlCommand.Parameters.Add(new SqlParameter("@IdCustomer", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.IdCustomer));
				sqlCommand.Parameters.Add(new SqlParameter("@IdEmployee", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.IdEmployee));
				sqlCommand.Parameters.Add(new SqlParameter("@ListProductId", SqlDbType.VarChar, 255, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.ListProductId));
				sqlCommand.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 255, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.Name));
				sqlCommand.Parameters.Add(new SqlParameter("@OrderStatus", SqlDbType.VarChar, 255, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.OrderStatus));
				sqlCommand.Parameters.Add(new SqlParameter("@Phone", SqlDbType.VarChar, 255, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.Phone));
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
                var businessObject = baseEntity as OrderDetail;

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
                var businessObject = baseEntity as OrderDetail;

                if (businessObject != null)
                {

								sqlCommand.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar, 255, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.Address));
				sqlCommand.Parameters.Add(new SqlParameter("@CreatedAt", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.CreatedAt));
				sqlCommand.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.CreatedBy));
				sqlCommand.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.Date));
				sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.Id));
				sqlCommand.Parameters.Add(new SqlParameter("@IdCustomer", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.IdCustomer));
				sqlCommand.Parameters.Add(new SqlParameter("@IdEmployee", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.IdEmployee));
				sqlCommand.Parameters.Add(new SqlParameter("@ListProductId", SqlDbType.VarChar, 255, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.ListProductId));
				sqlCommand.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 255, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.Name));
				sqlCommand.Parameters.Add(new SqlParameter("@OrderStatus", SqlDbType.VarChar, 255, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.OrderStatus));
				sqlCommand.Parameters.Add(new SqlParameter("@Phone", SqlDbType.VarChar, 255, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.Phone));
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
                var businessObject = baseEntity as OrderDetail;
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
                var businessObject = new OrderDetail();
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

    internal void PopulateBusinessObjectFromReader(OrderDetail businessObject, IDataReader dataReader)
        {
            if (_dicIndex.Count == 0) FillDicIndex(dataReader);
		
			if (GetIndex(OrderDetail.OrderDetailFields.Address.ToString()) != -1)
				if (!dataReader.IsDBNull(GetIndex(OrderDetail.OrderDetailFields.Address.ToString())))
				{
					businessObject.Address = dataReader.GetString(GetIndex(OrderDetail.OrderDetailFields.Address.ToString()));
				}

			if (GetIndex(OrderDetail.OrderDetailFields.CreatedAt.ToString()) != -1)
				if (!dataReader.IsDBNull(GetIndex(OrderDetail.OrderDetailFields.CreatedAt.ToString())))
				{
					businessObject.CreatedAt = dataReader.GetDateTime(GetIndex(OrderDetail.OrderDetailFields.CreatedAt.ToString()));
				}

			if (GetIndex(OrderDetail.OrderDetailFields.CreatedBy.ToString()) != -1)
				businessObject.CreatedBy = dataReader.GetInt32(GetIndex(OrderDetail.OrderDetailFields.CreatedBy.ToString()));

			if (GetIndex(OrderDetail.OrderDetailFields.Date.ToString()) != -1)
				businessObject.Date = dataReader.GetDateTime(GetIndex(OrderDetail.OrderDetailFields.Date.ToString()));

				businessObject.Id = dataReader.GetInt32(GetIndex(OrderDetail.OrderDetailFields.Id.ToString()));

			if (GetIndex(OrderDetail.OrderDetailFields.IdCustomer.ToString()) != -1)
				if (!dataReader.IsDBNull(GetIndex(OrderDetail.OrderDetailFields.IdCustomer.ToString())))
				{
					businessObject.IdCustomer = dataReader.GetInt32(GetIndex(OrderDetail.OrderDetailFields.IdCustomer.ToString()));
				}

			if (GetIndex(OrderDetail.OrderDetailFields.IdEmployee.ToString()) != -1)
				businessObject.IdEmployee = dataReader.GetInt32(GetIndex(OrderDetail.OrderDetailFields.IdEmployee.ToString()));

			if (GetIndex(OrderDetail.OrderDetailFields.ListProductId.ToString()) != -1)
				if (!dataReader.IsDBNull(GetIndex(OrderDetail.OrderDetailFields.ListProductId.ToString())))
				{
					businessObject.ListProductId = dataReader.GetString(GetIndex(OrderDetail.OrderDetailFields.ListProductId.ToString()));
				}

			if (GetIndex(OrderDetail.OrderDetailFields.Name.ToString()) != -1)
				if (!dataReader.IsDBNull(GetIndex(OrderDetail.OrderDetailFields.Name.ToString())))
				{
					businessObject.Name = dataReader.GetString(GetIndex(OrderDetail.OrderDetailFields.Name.ToString()));
				}

			if (GetIndex(OrderDetail.OrderDetailFields.OrderStatus.ToString()) != -1)
				if (!dataReader.IsDBNull(GetIndex(OrderDetail.OrderDetailFields.OrderStatus.ToString())))
				{
					businessObject.OrderStatus = dataReader.GetString(GetIndex(OrderDetail.OrderDetailFields.OrderStatus.ToString()));
				}

			if (GetIndex(OrderDetail.OrderDetailFields.Phone.ToString()) != -1)
				if (!dataReader.IsDBNull(GetIndex(OrderDetail.OrderDetailFields.Phone.ToString())))
				{
					businessObject.Phone = dataReader.GetString(GetIndex(OrderDetail.OrderDetailFields.Phone.ToString()));
				}

			if (GetIndex(OrderDetail.OrderDetailFields.TotalPrice.ToString()) != -1)
				businessObject.TotalPrice = dataReader.GetDecimal(GetIndex(OrderDetail.OrderDetailFields.TotalPrice.ToString()));

			if (GetIndex(OrderDetail.OrderDetailFields.UpdatedAt.ToString()) != -1)
				if (!dataReader.IsDBNull(GetIndex(OrderDetail.OrderDetailFields.UpdatedAt.ToString())))
				{
					businessObject.UpdatedAt = dataReader.GetDateTime(GetIndex(OrderDetail.OrderDetailFields.UpdatedAt.ToString()));
				}

			if (GetIndex(OrderDetail.OrderDetailFields.UpdatedBy.ToString()) != -1)
				businessObject.UpdatedBy = dataReader.GetInt32(GetIndex(OrderDetail.OrderDetailFields.UpdatedBy.ToString()));


        }

        #endregion
	}
}

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
	public class UserInfoSql : EntityBaseSql 
	{

        #region Constructor

		public UserInfoSql()
		{
			 Init();
		}

        #endregion
		
		#region Init

		private void Init()
		{
           Insertsql = "UserInfo_Insert";
           Updatesql = "UserInfo_Update";
           Deletesql = "UserInfo_DeleteByPrimaryKey";
           Selectsql = "UserInfo_SelectAll";
		}

        #endregion

        #region Public override Methods

        public override SqlCommand GetSqlCommandForInsert(BaseEntity baseEntity)
		{
			var sqlCommand = new SqlCommand { CommandType = CommandTypeProcedure };
            if (baseEntity != null)
            {
                var businessObject = baseEntity as UserInfo;
                if (businessObject != null)
				{

								sqlCommand.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar, 255, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.Address));
				sqlCommand.Parameters.Add(new SqlParameter("@CreatedAt", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.CreatedAt));
				sqlCommand.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.CreatedBy));
				sqlCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 255, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.Email));
				sqlCommand.Parameters.Add(new SqlParameter("@IdUserLogin", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.IdUserLogin));
				sqlCommand.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 255, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.Name));
				sqlCommand.Parameters.Add(new SqlParameter("@Phone", SqlDbType.VarChar, 255, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.Phone));
				sqlCommand.Parameters.Add(new SqlParameter("@UpdatedAt", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.UpdatedAt));
				sqlCommand.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.UpdatedBy));
				sqlCommand.Parameters.Add(new SqlParameter("@UserStatus", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.UserStatus));


				}
			}
            return sqlCommand;	
		}

		public override BaseEntity UpdateEntityId(BaseEntity baseEntity, SqlCommand sqlCommand)
        {
            if (baseEntity != null)
            {
                return baseEntity;
                var businessObject = baseEntity as UserInfo;

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
                var businessObject = baseEntity as UserInfo;

                if (businessObject != null)
                {

								sqlCommand.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar, 255, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.Address));
				sqlCommand.Parameters.Add(new SqlParameter("@CreatedAt", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.CreatedAt));
				sqlCommand.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.CreatedBy));
				sqlCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 255, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.Email));
				sqlCommand.Parameters.Add(new SqlParameter("@IdUserLogin", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.IdUserLogin));
				sqlCommand.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 255, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.Name));
				sqlCommand.Parameters.Add(new SqlParameter("@Phone", SqlDbType.VarChar, 255, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.Phone));
				sqlCommand.Parameters.Add(new SqlParameter("@UpdatedAt", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.UpdatedAt));
				sqlCommand.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.UpdatedBy));
				sqlCommand.Parameters.Add(new SqlParameter("@UserStatus", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.UserStatus));

				
				}
			}       
         return sqlCommand;
		}
        
        public override SqlCommand GetSqlCommandForDelete(BaseEntity baseEntity)
		{
            var sqlCommand = new SqlCommand {CommandType = CommandTypeProcedure};
            if (baseEntity != null)
            {
                var businessObject = baseEntity as UserInfo;
                if (businessObject != null)
                {

								sqlCommand.Parameters.Add(new SqlParameter("@IdUserLogin", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.IdUserLogin));

				
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
                var businessObject = new UserInfo();
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

    internal void PopulateBusinessObjectFromReader(UserInfo businessObject, IDataReader dataReader)
        {
            if (_dicIndex.Count == 0) FillDicIndex(dataReader);
		
			if (GetIndex(UserInfo.UserInfoFields.Address.ToString()) != -1)
				if (!dataReader.IsDBNull(GetIndex(UserInfo.UserInfoFields.Address.ToString())))
				{
					businessObject.Address = dataReader.GetString(GetIndex(UserInfo.UserInfoFields.Address.ToString()));
				}

			if (GetIndex(UserInfo.UserInfoFields.CreatedAt.ToString()) != -1)
				if (!dataReader.IsDBNull(GetIndex(UserInfo.UserInfoFields.CreatedAt.ToString())))
				{
					businessObject.CreatedAt = dataReader.GetDateTime(GetIndex(UserInfo.UserInfoFields.CreatedAt.ToString()));
				}

			if (GetIndex(UserInfo.UserInfoFields.CreatedBy.ToString()) != -1)
				businessObject.CreatedBy = dataReader.GetInt32(GetIndex(UserInfo.UserInfoFields.CreatedBy.ToString()));

			if (GetIndex(UserInfo.UserInfoFields.Email.ToString()) != -1)
				if (!dataReader.IsDBNull(GetIndex(UserInfo.UserInfoFields.Email.ToString())))
				{
					businessObject.Email = dataReader.GetString(GetIndex(UserInfo.UserInfoFields.Email.ToString()));
				}

				businessObject.IdUserLogin = dataReader.GetInt32(GetIndex(UserInfo.UserInfoFields.IdUserLogin.ToString()));

			if (GetIndex(UserInfo.UserInfoFields.Name.ToString()) != -1)
				businessObject.Name = dataReader.GetString(GetIndex(UserInfo.UserInfoFields.Name.ToString()));

			if (GetIndex(UserInfo.UserInfoFields.Phone.ToString()) != -1)
				if (!dataReader.IsDBNull(GetIndex(UserInfo.UserInfoFields.Phone.ToString())))
				{
					businessObject.Phone = dataReader.GetString(GetIndex(UserInfo.UserInfoFields.Phone.ToString()));
				}

			if (GetIndex(UserInfo.UserInfoFields.UpdatedAt.ToString()) != -1)
				if (!dataReader.IsDBNull(GetIndex(UserInfo.UserInfoFields.UpdatedAt.ToString())))
				{
					businessObject.UpdatedAt = dataReader.GetDateTime(GetIndex(UserInfo.UserInfoFields.UpdatedAt.ToString()));
				}

			if (GetIndex(UserInfo.UserInfoFields.UpdatedBy.ToString()) != -1)
				businessObject.UpdatedBy = dataReader.GetInt32(GetIndex(UserInfo.UserInfoFields.UpdatedBy.ToString()));

			if (GetIndex(UserInfo.UserInfoFields.UserStatus.ToString()) != -1)
				businessObject.UserStatus = dataReader.GetInt32(GetIndex(UserInfo.UserInfoFields.UserStatus.ToString()));


        }

        #endregion
	}
}

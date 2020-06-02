using ElectricShop.Entity.Keys;
using System;using System.IO;
using System.Text;
using System.Data;

namespace ElectricShop.Entity.Entities
{
	public class UserRole: BaseEntity
	{
		#region InnerClass

		public enum UserRoleFields
		{
			IdRole,
			IdUserLogin
		}

		public enum UserRoleKey
		{
			IdRole,
			IdUserLogin
		}
		public UserRoleKeys GetUserRoleKeys()
		{
			return new UserRoleKeys
			{
				IdRole = IdRole,
				IdUserLogin = IdUserLogin
			};
		}
		#endregion

		#region Contructor

		public UserRole()
		{
		}

		#endregion

		#region Properties

		public int  IdRole { get; set; } //Key 
		public int  IdUserLogin { get; set; } //Key 

		#endregion

		#region Validation

		public override bool IsValid()
		{
			return true;
		}

		#endregion

		#region EntityName, GetName

		public static string EntityName()
		{
			return typeof(UserRole).Name;
		}

		public override string GetName()
		{
			return EntityName();
		}

		#endregion

#region Custom Method
#endregion


	}
}


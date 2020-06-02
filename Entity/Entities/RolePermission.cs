using ElectricShop.Entity.Keys;
using System;using System.IO;
using System.Text;
using System.Data;

namespace ElectricShop.Entity.Entities
{
	public class RolePermission: BaseEntity
	{
		#region InnerClass

		public enum RolePermissionFields
		{
			IdPermission,
			IdRole
		}

		public enum RolePermissionKey
		{
			IdPermission,
			IdRole
		}
		public RolePermissionKeys GetRolePermissionKeys()
		{
			return new RolePermissionKeys
			{
				IdPermission = IdPermission,
				IdRole = IdRole
			};
		}
		#endregion

		#region Contructor

		public RolePermission()
		{
		}

		#endregion

		#region Properties

		public int  IdPermission { get; set; } //Key 
		public int  IdRole { get; set; } //Key 

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
			return typeof(RolePermission).Name;
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


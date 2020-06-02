using System;using System.IO;
using System.Text;
using System.Data;

namespace ElectricShop.Entity.Entities
{
	public class Permission: BaseEntity
	{
		#region InnerClass

		public enum PermissionFields
		{
			CreatedAt,
			CreatedBy,
			Description,
			Id,
			Name,
			UpdatedAt,
			UpdatedBy
		}

		public enum PermissionKey
		{
			Id
		}
		#endregion

		#region Contructor

		public Permission()
		{
		}

		#endregion

		#region Properties

		public DateTime?  CreatedAt { get; set; }
		public int  CreatedBy { get; set; }
		public string  Description { get; set; }
		public int  Id { get; set; } //Key 
		public string  Name { get; set; }
		public DateTime?  UpdatedAt { get; set; }
		public int  UpdatedBy { get; set; }

		#endregion

		#region Validation

		public override bool IsValid()
		{
			if (Description == null)
				throw new NoNullAllowedException("Field: Description in entity: Permission is Null");

			if (Description != null && Description.Length > 255 )
				throw new InvalidDataException("Field: Description in entity: Permission is over-size: 255, value=" + Description);
			if (Name == null)
				throw new NoNullAllowedException("Field: Name in entity: Permission is Null");

			if (Name != null && Name.Length > 255 )
				throw new InvalidDataException("Field: Name in entity: Permission is over-size: 255, value=" + Name);
			return true;
		}

		#endregion

		#region EntityName, GetName

		public static string EntityName()
		{
			return typeof(Permission).Name;
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


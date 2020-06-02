using System;using System.IO;
using System.Text;
using System.Data;

namespace ElectricShop.Entity.Entities
{
	public class Property: BaseEntity
	{
		#region InnerClass

		public enum PropertyFields
		{
			CreatedAt,
			CreatedBy,
			Data,
			Id,
			IdProduct,
			Name,
			UpdatedAt,
			UpdatedBy
		}

		public enum PropertyKey
		{
			Id
		}
		#endregion

		#region Contructor

		public Property()
		{
		}

		#endregion

		#region Properties

		public DateTime?  CreatedAt { get; set; }
		public int  CreatedBy { get; set; }
		public string  Data { get; set; }
		public int  Id { get; set; } //Key 
		public int  IdProduct { get; set; }
		public string  Name { get; set; }
		public DateTime?  UpdatedAt { get; set; }
		public int  UpdatedBy { get; set; }

		#endregion

		#region Validation

		public override bool IsValid()
		{
			if (Data == null)
				throw new NoNullAllowedException("Field: Data in entity: Property is Null");

			if (Data != null && Data.Length > 255 )
				throw new InvalidDataException("Field: Data in entity: Property is over-size: 255, value=" + Data);
			if (Name == null)
				throw new NoNullAllowedException("Field: Name in entity: Property is Null");

			if (Name != null && Name.Length > 255 )
				throw new InvalidDataException("Field: Name in entity: Property is over-size: 255, value=" + Name);
			return true;
		}

		#endregion

		#region EntityName, GetName

		public static string EntityName()
		{
			return typeof(Property).Name;
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


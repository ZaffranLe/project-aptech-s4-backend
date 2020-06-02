using System;using System.IO;
using System.Text;
using System.Data;

namespace ElectricShop.Entity.Entities
{
	public class Manufacturer: BaseEntity
	{
		#region InnerClass

		public enum ManufacturerFields
		{
			CreatedAt,
			CreatedBy,
			Id,
			Name,
			UpdatedAt,
			UpdatedBy
		}

		public enum ManufacturerKey
		{
			Id
		}
		#endregion

		#region Contructor

		public Manufacturer()
		{
		}

		#endregion

		#region Properties

		public DateTime?  CreatedAt { get; set; }
		public int  CreatedBy { get; set; }
		public int  Id { get; set; } //Key 
		public string  Name { get; set; }
		public DateTime?  UpdatedAt { get; set; }
		public int  UpdatedBy { get; set; }

		#endregion

		#region Validation

		public override bool IsValid()
		{
			if (Name == null)
				throw new NoNullAllowedException("Field: Name in entity: Manufacturer is Null");

			if (Name != null && Name.Length > 255 )
				throw new InvalidDataException("Field: Name in entity: Manufacturer is over-size: 255, value=" + Name);
			return true;
		}

		#endregion

		#region EntityName, GetName

		public static string EntityName()
		{
			return typeof(Manufacturer).Name;
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


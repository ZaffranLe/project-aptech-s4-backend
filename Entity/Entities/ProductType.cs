using System;using System.IO;
using System.Text;
using System.Data;

namespace ElectricShop.Entity.Entities
{
	public class ProductType: BaseEntity
	{
		#region InnerClass

		public enum ProductTypeFields
		{
			CreatedAt,
			CreatedBy,
			Id,
			Name,
			UpdatedAt,
			UpdatedBy
		}

		public enum ProductTypeKey
		{
			Id
		}
		#endregion

		#region Contructor

		public ProductType()
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
				throw new NoNullAllowedException("Field: Name in entity: ProductType is Null");

			if (Name != null && Name.Length > 255 )
				throw new InvalidDataException("Field: Name in entity: ProductType is over-size: 255, value=" + Name);
			return true;
		}

		#endregion

		#region EntityName, GetName

		public static string EntityName()
		{
			return typeof(ProductType).Name;
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


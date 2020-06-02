using System;using System.IO;
using System.Text;
using System.Data;

namespace ElectricShop.Entity.Entities
{
	public class Product: BaseEntity
	{
		#region InnerClass

		public enum ProductFields
		{
			CreatedAt,
			CreatedBy,
			Description,
			Id,
			IdDisplay,
			IdManufacturer,
			IdProductType,
			Name,
			Quantity,
			SupportDuration,
			UnitPrice,
			UpdatedAt,
			UpdatedBy
		}

		public enum ProductKey
		{
			Id
		}
		#endregion

		#region Contructor

		public Product()
		{
		}

		#endregion

		#region Properties

		public DateTime?  CreatedAt { get; set; }
		public int  CreatedBy { get; set; }
		public string  Description { get; set; }
		public int  Id { get; set; } //Key 
		public string  IdDisplay { get; set; }
		public int  IdManufacturer { get; set; }
		public int  IdProductType { get; set; }
		public string  Name { get; set; }
		public int?  Quantity { get; set; }
		public int?  SupportDuration { get; set; }
		public decimal?  UnitPrice { get; set; }
		public DateTime?  UpdatedAt { get; set; }
		public int  UpdatedBy { get; set; }

		#endregion

		#region Validation

		public override bool IsValid()
		{

			if (Description != null && Description.Length > 255 )
				throw new InvalidDataException("Field: Description in entity: Product is over-size: 255, value=" + Description);
			if (IdDisplay == null)
				throw new NoNullAllowedException("Field: IdDisplay in entity: Product is Null");

			if (IdDisplay != null && IdDisplay.Length > 50 )
				throw new InvalidDataException("Field: IdDisplay in entity: Product is over-size: 50, value=" + IdDisplay);
			if (Name == null)
				throw new NoNullAllowedException("Field: Name in entity: Product is Null");

			if (Name != null && Name.Length > 255 )
				throw new InvalidDataException("Field: Name in entity: Product is over-size: 255, value=" + Name);
			return true;
		}

		#endregion

		#region EntityName, GetName

		public static string EntityName()
		{
			return typeof(Product).Name;
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


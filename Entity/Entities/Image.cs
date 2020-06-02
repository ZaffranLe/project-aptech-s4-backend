using System;using System.IO;
using System.Text;
using System.Data;

namespace ElectricShop.Entity.Entities
{
	public class Image: BaseEntity
	{
		#region InnerClass

		public enum ImageFields
		{
			CreatedAt,
			CreatedBy,
			Id,
			IdProduct,
			Name,
			UpdatedAt,
			UpdatedBy
		}

		public enum ImageKey
		{
			Id
		}
		#endregion

		#region Contructor

		public Image()
		{
		}

		#endregion

		#region Properties

		public DateTime?  CreatedAt { get; set; }
		public int  CreatedBy { get; set; }
		public int  Id { get; set; } //Key 
		public int  IdProduct { get; set; }
		public string  Name { get; set; }
		public DateTime?  UpdatedAt { get; set; }
		public int  UpdatedBy { get; set; }

		#endregion

		#region Validation

		public override bool IsValid()
		{
			if (Name == null)
				throw new NoNullAllowedException("Field: Name in entity: Image is Null");

			if (Name != null && Name.Length > 255 )
				throw new InvalidDataException("Field: Name in entity: Image is over-size: 255, value=" + Name);
			return true;
		}

		#endregion

		#region EntityName, GetName

		public static string EntityName()
		{
			return typeof(Image).Name;
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


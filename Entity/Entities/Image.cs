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
			ImageUrl,
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
		public string  ImageUrl { get; set; }
		public DateTime?  UpdatedAt { get; set; }
		public int  UpdatedBy { get; set; }

		#endregion

		#region Validation

		public override bool IsValid()
		{
			if (ImageUrl == null)
				throw new NoNullAllowedException("Field: ImageUrl in entity: Image is Null");

			if (ImageUrl != null && ImageUrl.Length > 255 )
				throw new InvalidDataException("Field: ImageUrl in entity: Image is over-size: 255, value=" + ImageUrl);
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


using System;using System.IO;
using System.Text;
using System.Data;

namespace ElectricShop.Entity.Entities
{
	public class Post: BaseEntity
	{
		#region InnerClass

		public enum PostFields
		{
			Content,
			CreatedAt,
			CreatedBy,
			Id,
			Tittle,
			UpdatedAt,
			UpdatedBy
		}

		public enum PostKey
		{
			Id
		}
		#endregion

		#region Contructor

		public Post()
		{
		}

		#endregion

		#region Properties

		public string  Content { get; set; }
		public DateTime?  CreatedAt { get; set; }
		public int  CreatedBy { get; set; }
		public int  Id { get; set; } //Key 
		public string  Tittle { get; set; }
		public DateTime?  UpdatedAt { get; set; }
		public int  UpdatedBy { get; set; }

		#endregion

		#region Validation

		public override bool IsValid()
		{

			if (Content != null && Content.Length > 2147483647 )
				throw new InvalidDataException("Field: Content in entity: Post is over-size: 2147483647, value=" + Content);
			if (Tittle == null)
				throw new NoNullAllowedException("Field: Tittle in entity: Post is Null");

			if (Tittle != null && Tittle.Length > 255 )
				throw new InvalidDataException("Field: Tittle in entity: Post is over-size: 255, value=" + Tittle);
			return true;
		}

		#endregion

		#region EntityName, GetName

		public static string EntityName()
		{
			return typeof(Post).Name;
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


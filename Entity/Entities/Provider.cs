using System;using System.IO;
using System.Text;
using System.Data;

namespace ElectricShop.Entity.Entities
{
	public class Provider: BaseEntity
	{
		#region InnerClass

		public enum ProviderFields
		{
			Address,
			CreatedAt,
			CreatedBy,
			Email,
			Id,
			Name,
			Phone,
			UpdatedAt,
			UpdatedBy
		}

		public enum ProviderKey
		{
			Id
		}
		#endregion

		#region Contructor

		public Provider()
		{
		}

		#endregion

		#region Properties

		public string  Address { get; set; }
		public DateTime?  CreatedAt { get; set; }
		public int  CreatedBy { get; set; }
		public string  Email { get; set; }
		public int  Id { get; set; } //Key 
		public string  Name { get; set; }
		public string  Phone { get; set; }
		public DateTime?  UpdatedAt { get; set; }
		public int  UpdatedBy { get; set; }

		#endregion

		#region Validation

		public override bool IsValid()
		{

			if (Address != null && Address.Length > 255 )
				throw new InvalidDataException("Field: Address in entity: Provider is over-size: 255, value=" + Address);

			if (Email != null && Email.Length > 255 )
				throw new InvalidDataException("Field: Email in entity: Provider is over-size: 255, value=" + Email);
			if (Name == null)
				throw new NoNullAllowedException("Field: Name in entity: Provider is Null");

			if (Name != null && Name.Length > 255 )
				throw new InvalidDataException("Field: Name in entity: Provider is over-size: 255, value=" + Name);

			if (Phone != null && Phone.Length > 255 )
				throw new InvalidDataException("Field: Phone in entity: Provider is over-size: 255, value=" + Phone);
			return true;
		}

		#endregion

		#region EntityName, GetName

		public static string EntityName()
		{
			return typeof(Provider).Name;
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


using System;using System.IO;
using System.Text;
using System.Data;

namespace ElectricShop.Entity.Entities
{
	public class Customer: BaseEntity
	{
		#region InnerClass

		public enum CustomerFields
		{
			Address,
			CreatedAt,
			CreatedBy,
			Id,
			Name,
			Phone,
			UpdatedAt,
			UpdatedBy
		}

		public enum CustomerKey
		{
			Id
		}
		#endregion

		#region Contructor

		public Customer()
		{
		}

		#endregion

		#region Properties

		public string  Address { get; set; }
		public DateTime?  CreatedAt { get; set; }
		public int  CreatedBy { get; set; }
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
				throw new InvalidDataException("Field: Address in entity: Customer is over-size: 255, value=" + Address);
			if (Name == null)
				throw new NoNullAllowedException("Field: Name in entity: Customer is Null");

			if (Name != null && Name.Length > 255 )
				throw new InvalidDataException("Field: Name in entity: Customer is over-size: 255, value=" + Name);

			if (Phone != null && Phone.Length > 255 )
				throw new InvalidDataException("Field: Phone in entity: Customer is over-size: 255, value=" + Phone);
			return true;
		}

		#endregion

		#region EntityName, GetName

		public static string EntityName()
		{
			return typeof(Customer).Name;
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


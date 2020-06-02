using System;using System.IO;
using System.Text;
using System.Data;

namespace ElectricShop.Entity.Entities
{
	public class OrderDetail: BaseEntity
	{
		#region InnerClass

		public enum OrderDetailFields
		{
			Address,
			CreatedAt,
			CreatedBy,
			Date,
			Id,
			IdCustomer,
			IdEmployee,
			ListProductId,
			Name,
			OrderStatus,
			Phone,
			TotalPrice,
			UpdatedAt,
			UpdatedBy
		}

		public enum OrderDetailKey
		{
			Id
		}
		#endregion

		#region Contructor

		public OrderDetail()
		{
		}

		#endregion

		#region Properties

		public string  Address { get; set; }
		public DateTime?  CreatedAt { get; set; }
		public int  CreatedBy { get; set; }
		public DateTime  Date { get; set; }
		public int  Id { get; set; } //Key 
		public int?  IdCustomer { get; set; }
		public int  IdEmployee { get; set; }
		public string  ListProductId { get; set; }
		public string  Name { get; set; }
		public string  OrderStatus { get; set; }
		public string  Phone { get; set; }
		public decimal  TotalPrice { get; set; }
		public DateTime?  UpdatedAt { get; set; }
		public int  UpdatedBy { get; set; }

		#endregion

		#region Validation

		public override bool IsValid()
		{

			if (Address != null && Address.Length > 255 )
				throw new InvalidDataException("Field: Address in entity: OrderDetail is over-size: 255, value=" + Address);

			if (ListProductId != null && ListProductId.Length > 255 )
				throw new InvalidDataException("Field: ListProductId in entity: OrderDetail is over-size: 255, value=" + ListProductId);

			if (Name != null && Name.Length > 255 )
				throw new InvalidDataException("Field: Name in entity: OrderDetail is over-size: 255, value=" + Name);

			if (OrderStatus != null && OrderStatus.Length > 255 )
				throw new InvalidDataException("Field: OrderStatus in entity: OrderDetail is over-size: 255, value=" + OrderStatus);

			if (Phone != null && Phone.Length > 255 )
				throw new InvalidDataException("Field: Phone in entity: OrderDetail is over-size: 255, value=" + Phone);
			if (TotalPrice == null)
				throw new NoNullAllowedException("Field: TotalPrice in entity: OrderDetail is Null");
			return true;
		}

		#endregion

		#region EntityName, GetName

		public static string EntityName()
		{
			return typeof(OrderDetail).Name;
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


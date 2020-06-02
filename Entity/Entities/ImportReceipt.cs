using System;using System.IO;
using System.Text;
using System.Data;

namespace ElectricShop.Entity.Entities
{
	public class ImportReceipt: BaseEntity
	{
		#region InnerClass

		public enum ImportReceiptFields
		{
			CreatedAt,
			CreatedBy,
			Date,
			Id,
			IdEmployee,
			IdProvider,
			ListProductId,
			TotalPrice,
			UpdatedAt,
			UpdatedBy
		}

		public enum ImportReceiptKey
		{
			Id
		}
		#endregion

		#region Contructor

		public ImportReceipt()
		{
		}

		#endregion

		#region Properties

		public DateTime?  CreatedAt { get; set; }
		public int  CreatedBy { get; set; }
		public DateTime  Date { get; set; }
		public int  Id { get; set; } //Key 
		public int  IdEmployee { get; set; }
		public int  IdProvider { get; set; }
		public string  ListProductId { get; set; }
		public decimal  TotalPrice { get; set; }
		public DateTime?  UpdatedAt { get; set; }
		public int  UpdatedBy { get; set; }

		#endregion

		#region Validation

		public override bool IsValid()
		{

			if (ListProductId != null && ListProductId.Length > 255 )
				throw new InvalidDataException("Field: ListProductId in entity: ImportReceipt is over-size: 255, value=" + ListProductId);
			if (TotalPrice == null)
				throw new NoNullAllowedException("Field: TotalPrice in entity: ImportReceipt is Null");
			return true;
		}

		#endregion

		#region EntityName, GetName

		public static string EntityName()
		{
			return typeof(ImportReceipt).Name;
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


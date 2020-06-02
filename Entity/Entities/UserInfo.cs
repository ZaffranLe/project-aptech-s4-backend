using System;using System.IO;
using System.Text;
using System.Data;

namespace ElectricShop.Entity.Entities
{
	public class UserInfo: BaseEntity
	{
		#region InnerClass

		public enum UserInfoFields
		{
			Address,
			CreatedAt,
			CreatedBy,
			Email,
			IdUserLogin,
			Name,
			Phone,
			UpdatedAt,
			UpdatedBy,
			UserStatus
		}

		public enum UserInfoKey
		{
			IdUserLogin
		}
		#endregion

		#region Contructor

		public UserInfo()
		{
		}

		#endregion

		#region Properties

		public string  Address { get; set; }
		public DateTime?  CreatedAt { get; set; }
		public int  CreatedBy { get; set; }
		public string  Email { get; set; }
		public int  IdUserLogin { get; set; } //Key 
		public string  Name { get; set; }
		public string  Phone { get; set; }
		public DateTime?  UpdatedAt { get; set; }
		public int  UpdatedBy { get; set; }
		public int  UserStatus { get; set; }

		#endregion

		#region Validation

		public override bool IsValid()
		{

			if (Address != null && Address.Length > 255 )
				throw new InvalidDataException("Field: Address in entity: UserInfo is over-size: 255, value=" + Address);

			if (Email != null && Email.Length > 255 )
				throw new InvalidDataException("Field: Email in entity: UserInfo is over-size: 255, value=" + Email);
			if (Name == null)
				throw new NoNullAllowedException("Field: Name in entity: UserInfo is Null");

			if (Name != null && Name.Length > 255 )
				throw new InvalidDataException("Field: Name in entity: UserInfo is over-size: 255, value=" + Name);

			if (Phone != null && Phone.Length > 255 )
				throw new InvalidDataException("Field: Phone in entity: UserInfo is over-size: 255, value=" + Phone);
			return true;
		}

		#endregion

		#region EntityName, GetName

		public static string EntityName()
		{
			return typeof(UserInfo).Name;
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


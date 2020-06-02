using System;using System.IO;
using System.Text;
using System.Data;

namespace ElectricShop.Entity.Entities
{
	public class UserLogin: BaseEntity
	{
		#region InnerClass

		public enum UserLoginFields
		{
			Id,
			Password,
			Username
		}

		public enum UserLoginKey
		{
			Id
		}
		#endregion

		#region Contructor

		public UserLogin()
		{
		}

		#endregion

		#region Properties

		public int  Id { get; set; } //Key 
		public string  Password { get; set; }
		public string  Username { get; set; }

		#endregion

		#region Validation

		public override bool IsValid()
		{
			if (Password == null)
				throw new NoNullAllowedException("Field: Password in entity: UserLogin is Null");

			if (Password != null && Password.Length > 255 )
				throw new InvalidDataException("Field: Password in entity: UserLogin is over-size: 255, value=" + Password);
			if (Username == null)
				throw new NoNullAllowedException("Field: Username in entity: UserLogin is Null");

			if (Username != null && Username.Length > 255 )
				throw new InvalidDataException("Field: Username in entity: UserLogin is over-size: 255, value=" + Username);
			return true;
		}

		#endregion

		#region EntityName, GetName

		public static string EntityName()
		{
			return typeof(UserLogin).Name;
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


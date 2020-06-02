namespace ElectricShop.Entity.Keys
{
    public class UserRoleKeys
    {
        public int IdRole { get; set; } //Key 
        public int IdUserLogin { get; set; } //Key 

        public override int GetHashCode()
        {
            unchecked
            {
                int result = IdRole.GetHashCode();
                result = (IdRole * 397) ^ IdUserLogin.GetHashCode();
                return result;
            }
        }

        public override bool Equals(object obj)
        {

            if (obj is UserRoleKeys)
            {
                var item = obj as UserRoleKeys;
                return IdRole == item.IdRole && IdUserLogin == item.IdUserLogin;
            }
            return false;
        }
    }
}

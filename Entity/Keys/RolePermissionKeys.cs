namespace ElectricShop.Entity.Keys
{
    public class RolePermissionKeys
    {
        public int IdRole { get; set; } //Key 
        public int IdPermission { get; set; } //Key 

        public override int GetHashCode()
        {
            unchecked
            {
                int result = IdRole.GetHashCode();
                result = (IdRole * 397) ^ IdPermission.GetHashCode();
                return result;
            }
        }

        public override bool Equals(object obj)
        {

            if (obj is RolePermissionKeys)
            {
                var item = obj as RolePermissionKeys;
                return IdRole == item.IdRole && IdPermission == item.IdPermission;
            }
            return false;
        }
    }
}

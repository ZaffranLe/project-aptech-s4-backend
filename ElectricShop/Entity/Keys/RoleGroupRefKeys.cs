namespace ElectricShop.Entity.Keys
{
    public class RoleGroupRefKeys
    {
        public int RoleId { get; set; }

        public int RoleGroupId { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = RoleId.GetHashCode();
                result = (result * 397) ^ RoleGroupId.GetHashCode();
                return result;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is RoleGroupRefKeys)
            {
                var item = obj as RoleGroupRefKeys;
                return RoleId == item.RoleId && RoleGroupId == item.RoleGroupId;
            }
            return false;
        }
    }
}
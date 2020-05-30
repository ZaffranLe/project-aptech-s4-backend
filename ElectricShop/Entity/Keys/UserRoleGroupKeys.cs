
namespace ElectricShop.Entity.Keys
{
    public class UserRoleGroupKeys
    {
        public long UserId { get; set; }

        public int RoleGroupId { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = UserId.GetHashCode();
                result = (result*397) ^ RoleGroupId.GetHashCode();
                return result;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is UserRoleGroupKeys)
            {
                var item = obj as UserRoleGroupKeys;
                return UserId == item.UserId && RoleGroupId == item.RoleGroupId;
            }
            return false;
        }
    }
}
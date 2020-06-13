using ElectricShop.Entity.Keys;

namespace ElectricShop.Memory
{
    public partial class MemoryInfo : Memory
    {
        public static bool IsExistCustomer(int id)
        {
            if (DicCustomer.ContainsKey(id))
                return true;
            return false;
        }

        public static bool IsExistImage(int id)
        {
            if (DicImage.ContainsKey(id))
                return true;
            return false;
        }

        public static bool IsExistPost(int id)
        {
            if (DicPost.ContainsKey(id))
                return true;
            return false;
        }

        public static bool IsExistImportReceipt(int id)
        {
            if (DicImportReceipt.ContainsKey(id))
                return true;
            return false;
        }

        public static bool IsExistManufacturer(int id)
        {
            if (DicManufacturer.ContainsKey(id))
                return true;
            return false;
        }

        public static bool IsExistOrderDetail(int id)
        {
            if (DicOrderDetail.ContainsKey(id))
                return true;
            return false;
        }

        public static bool IsExistPermission(int id)
        {
            if (DicPermission.ContainsKey(id))
                return true;
            return false;
        }

        public static bool IsExistProduct(int id)
        {
            if (DicProduct.ContainsKey(id))
                return true;
            return false;
        }

        public static bool IsExistProductType(int id)
        {
            if (DicProductType.ContainsKey(id))
                return true;
            return false;
        }

        public static bool IsExistProperty(int id)
        {
            if (DicProperty.ContainsKey(id))
                return true;
            return false;
        }

        public static bool IsExistProvider(int id)
        {
            if (DicProvider.ContainsKey(id))
                return true;
            return false;
        }

        public static bool IsExistRole(int id)
        {
            if (DicRole.ContainsKey(id))
                return true;
            return false;
        }

        public static bool IsExistRolePermission(RolePermissionKeys rolePermissionKeys)
        {
            if (DicRolePermission.ContainsKey(rolePermissionKeys))
                return true;
            return false;
        }

        public static bool IsExistUserInfo(int idUserLogin)
        {
            if (DicUserInfo.ContainsKey(idUserLogin))
                return true;
            return false;
        }

        public static bool IsExistUserLogin(int id)
        {
            if (DicUserLogin.ContainsKey(id))
                return true;
            return false;
        }

        public static bool IsExistUserRole(UserRoleKeys userRoleKeys)
        {
            if (DicUserRole.ContainsKey(userRoleKeys))
                return true;
            return false;
        }

    }
}



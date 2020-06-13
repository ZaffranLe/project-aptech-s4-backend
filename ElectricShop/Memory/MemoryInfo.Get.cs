using Anotar.NLog;
using ElectricShop.Entity.Entities;
using ElectricShop.Entity.Keys;

namespace ElectricShop.Memory
{
    public partial class MemoryInfo : ElectricShop.Memory.Memory
    {
        public static Customer GetCustomer(int id)
        {
            if (DicCustomer.ContainsKey(id))
                return DicCustomer[id].Clone() as Customer;
            Logger.Write("Not get Customer by id = " + id);
            return null;
        }

        public static Image GetImage(int id)
        {
            if (DicImage.ContainsKey(id))
                return DicImage[id].Clone() as Image;
            Logger.Write("Not get Image by id = " + id);
            return null;
        }

        public static Post GetPost(int id)
        {
            if (DicPost.ContainsKey(id))
                return DicPost[id].Clone() as Post;
            LogTo.Error("Not get Post by id = " + id);
            return null;
        }
        public static ImportReceipt GetImportReceipt(int id)
        {
            if (DicImportReceipt.ContainsKey(id))
                return DicImportReceipt[id].Clone() as ImportReceipt;
            Logger.Write("Not get ImportReceipt by id = " + id);
            return null;
        }

        public static Manufacturer GetManufacturer(int id)
        {
            if (DicManufacturer.ContainsKey(id))
                return DicManufacturer[id].Clone() as Manufacturer;
            Logger.Write("Not get Manufacturer by id = " + id);
            return null;
        }

        public static OrderDetail GetOrderDetail(int id)
        {
            if (DicOrderDetail.ContainsKey(id))
                return DicOrderDetail[id].Clone() as OrderDetail;
            Logger.Write("Not get OrderDetail by id = " + id);
            return null;
        }

        public static Permission GetPermission(int id)
        {
            if (DicPermission.ContainsKey(id))
                return DicPermission[id].Clone() as Permission;
            Logger.Write("Not get Permission by id = " + id);
            return null;
        }

        public static Product GetProduct(int id)
        {
            if (DicProduct.ContainsKey(id))
                return DicProduct[id].Clone() as Product;
            Logger.Write("Not get Product by id = " + id);
            return null;
        }

        public static ProductType GetProductType(int id)
        {
            if (DicProductType.ContainsKey(id))
                return DicProductType[id].Clone() as ProductType;
            Logger.Write("Not get ProductType by id = " + id);
            return null;
        }

        public static Property GetProperty(int id)
        {
            if (DicProperty.ContainsKey(id))
                return DicProperty[id].Clone() as Property;
            Logger.Write("Not get Property by id = " + id);
            return null;
        }

        public static Provider GetProvider(int id)
        {
            if (DicProvider.ContainsKey(id))
                return DicProvider[id].Clone() as Provider;
            Logger.Write("Not get Provider by id = " + id);
            return null;
        }

        public static Role GetRole(int id)
        {
            if (DicRole.ContainsKey(id))
                return DicRole[id].Clone() as Role;
            Logger.Write("Not get Role by id = " + id);
            return null;
        }

        public static RolePermission GetRolePermission(RolePermissionKeys rolePermissionKeys)
        {
            if (DicRolePermission.ContainsKey(rolePermissionKeys))
                return DicRolePermission[rolePermissionKeys].Clone() as RolePermission;
            Logger.Write("Not get RolePermission by rolePermissionKeys = " + rolePermissionKeys);
            return null;
        }

        public static UserInfo GetUserInfo(int idUserLogin)
        {
            if (DicUserInfo.ContainsKey(idUserLogin))
                return DicUserInfo[idUserLogin].Clone() as UserInfo;
            Logger.Write("Not get UserInfo by idUserLogin = " + idUserLogin);
            return null;
        }

        public static UserLogin GetUserLogin(int id)
        {
            if (DicUserLogin.ContainsKey(id))
                return DicUserLogin[id].Clone() as UserLogin;
            Logger.Write("Not get UserLogin by id = " + id);
            return null;
        }

        public static UserRole GetUserRole(UserRoleKeys userRoleKeys)
        {
            if (DicUserRole.ContainsKey(userRoleKeys))
                return DicUserRole[userRoleKeys].Clone() as UserRole;
            Logger.Write("Not get UserRole by userRoleKeys = " + userRoleKeys);
            return null;
        }

    }
}



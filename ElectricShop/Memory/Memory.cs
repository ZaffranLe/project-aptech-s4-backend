using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ElectricShop.Entity.Entities;
using ElectricShop.Entity.Keys;

namespace ElectricShop.Memory
{
    public class Memory
    {
        public static Dictionary<int, Customer> DicCustomer = new Dictionary<int, Customer>();

        public static Dictionary<int, Image> DicImage = new Dictionary<int, Image>();

        public static Dictionary<int, ImportReceipt> DicImportReceipt = new Dictionary<int, ImportReceipt>();

        public static Dictionary<int, Manufacturer> DicManufacturer = new Dictionary<int, Manufacturer>();

        public static Dictionary<int, OrderDetail> DicOrderDetail = new Dictionary<int, OrderDetail>();

        public static Dictionary<int, Permission> DicPermission = new Dictionary<int, Permission>();

        public static Dictionary<int, Product> DicProduct = new Dictionary<int, Product>();

        public static Dictionary<int, ProductType> DicProductType = new Dictionary<int, ProductType>();

        public static Dictionary<int, Property> DicProperty = new Dictionary<int, Property>();

        public static Dictionary<int, Provider> DicProvider = new Dictionary<int, Provider>();

        public static Dictionary<int, Role> DicRole = new Dictionary<int, Role>();

        public static Dictionary<RolePermissionKeys, RolePermission> DicRolePermission = new Dictionary<RolePermissionKeys, RolePermission>();

        public static Dictionary<int, UserInfo> DicUserInfo = new Dictionary<int, UserInfo>();

        public static Dictionary<int, UserLogin> DicUserLogin = new Dictionary<int, UserLogin>();

        public static Dictionary<UserRoleKeys, UserRole> DicUserRole = new Dictionary<UserRoleKeys, UserRole>();

        #region Dang ki memory
        

        #endregion
    }
}
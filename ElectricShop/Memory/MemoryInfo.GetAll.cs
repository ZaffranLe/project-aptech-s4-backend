using System.Collections.Generic;
using System.Linq;
using ElectricShop.Entity.Entities;

namespace ElectricShop.Memory
{
    public partial class MemoryInfo : Memory
    {
        public static List<Customer> GetAllCustomer()
        {
            return DicCustomer.Select(obj => obj.Value.Clone() as Customer).ToList();
        }

        public static List<Image> GetAllImage()
        {
            return DicImage.Select(obj => obj.Value.Clone() as Image).ToList();
        }

        public static List<ImportReceipt> GetAllImportReceipt()
        {
            return DicImportReceipt.Select(obj => obj.Value.Clone() as ImportReceipt).ToList();
        }
        public static List<Post> GetAllPost()
        {
            return DicPost.Select(obj => obj.Value.Clone() as Post).ToList();
        }

        public static List<Manufacturer> GetAllManufacturer()
        {
            return DicManufacturer.Select(obj => obj.Value.Clone() as Manufacturer).ToList();
        }

        public static List<OrderDetail> GetAllOrderDetail()
        {
            return DicOrderDetail.Select(obj => obj.Value.Clone() as OrderDetail).ToList();
        }

        public static List<Permission> GetAllPermission()
        {
            return DicPermission.Select(obj => obj.Value.Clone() as Permission).ToList();
        }

        public static List<Product> GetAllProduct()
        {
            return DicProduct.Select(obj => obj.Value.Clone() as Product).ToList();
        }

        public static List<ProductType> GetAllProductType()
        {
            return DicProductType.Select(obj => obj.Value.Clone() as ProductType).ToList();
        }

        public static List<Property> GetAllProperty()
        {
            return DicProperty.Select(obj => obj.Value.Clone() as Property).ToList();
        }

        public static List<Provider> GetAllProvider()
        {
            return DicProvider.Select(obj => obj.Value.Clone() as Provider).ToList();
        }

        public static List<Role> GetAllRole()
        {
            return DicRole.Select(obj => obj.Value.Clone() as Role).ToList();
        }

        public static List<RolePermission> GetAllRolePermission()
        {
            return DicRolePermission.Select(obj => obj.Value.Clone() as RolePermission).ToList();
        }

        public static List<UserInfo> GetAllUserInfo()
        {
            return DicUserInfo.Select(obj => obj.Value.Clone() as UserInfo).ToList();
        }

        public static List<UserLogin> GetAllUserLogin()
        {
            return DicUserLogin.Select(obj => obj.Value.Clone() as UserLogin).ToList();
        }

        public static List<UserRole> GetAllUserRole()
        {
            return DicUserRole.Select(obj => obj.Value.Clone() as UserRole).ToList();
        }

    }
}



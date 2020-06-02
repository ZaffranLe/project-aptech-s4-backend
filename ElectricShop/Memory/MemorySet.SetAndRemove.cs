using ElectricShop.Entity.Entities;
using ElectricShop.Entity.Keys;

namespace ElectricShop.Memory
{
    public partial class MemorySet : Memory
    {
        internal static void SetMemory(Customer objectValue)
        {
            DicCustomer[objectValue.Id] = objectValue;
        }
        internal static void RemoveMemory(Customer objectValue)
        {
            if (DicCustomer.ContainsKey(objectValue.Id))
                DicCustomer.Remove(objectValue.Id);
        }

        internal static void SetMemory(Image objectValue)
        {
            DicImage[objectValue.Id] = objectValue;
        }
        internal static void RemoveMemory(Image objectValue)
        {
            if (DicImage.ContainsKey(objectValue.Id))
                DicImage.Remove(objectValue.Id);
        }

        internal static void SetMemory(ImportReceipt objectValue)
        {
            DicImportReceipt[objectValue.Id] = objectValue;
        }
        internal static void RemoveMemory(ImportReceipt objectValue)
        {
            if (DicImportReceipt.ContainsKey(objectValue.Id))
                DicImportReceipt.Remove(objectValue.Id);
        }

        internal static void SetMemory(Manufacturer objectValue)
        {
            DicManufacturer[objectValue.Id] = objectValue;
        }
        internal static void RemoveMemory(Manufacturer objectValue)
        {
            if (DicManufacturer.ContainsKey(objectValue.Id))
                DicManufacturer.Remove(objectValue.Id);
        }

        internal static void SetMemory(OrderDetail objectValue)
        {
            DicOrderDetail[objectValue.Id] = objectValue;
        }
        internal static void RemoveMemory(OrderDetail objectValue)
        {
            if (DicOrderDetail.ContainsKey(objectValue.Id))
                DicOrderDetail.Remove(objectValue.Id);
        }

        internal static void SetMemory(Permission objectValue)
        {
            DicPermission[objectValue.Id] = objectValue;
        }
        internal static void RemoveMemory(Permission objectValue)
        {
            if (DicPermission.ContainsKey(objectValue.Id))
                DicPermission.Remove(objectValue.Id);
        }

        internal static void SetMemory(Product objectValue)
        {
            DicProduct[objectValue.Id] = objectValue;
        }
        internal static void RemoveMemory(Product objectValue)
        {
            if (DicProduct.ContainsKey(objectValue.Id))
                DicProduct.Remove(objectValue.Id);
        }

        internal static void SetMemory(ProductType objectValue)
        {
            DicProductType[objectValue.Id] = objectValue;
        }
        internal static void RemoveMemory(ProductType objectValue)
        {
            if (DicProductType.ContainsKey(objectValue.Id))
                DicProductType.Remove(objectValue.Id);
        }

        internal static void SetMemory(Property objectValue)
        {
            DicProperty[objectValue.Id] = objectValue;
        }
        internal static void RemoveMemory(Property objectValue)
        {
            if (DicProperty.ContainsKey(objectValue.Id))
                DicProperty.Remove(objectValue.Id);
        }

        internal static void SetMemory(Provider objectValue)
        {
            DicProvider[objectValue.Id] = objectValue;
        }
        internal static void RemoveMemory(Provider objectValue)
        {
            if (DicProvider.ContainsKey(objectValue.Id))
                DicProvider.Remove(objectValue.Id);
        }

        internal static void SetMemory(Role objectValue)
        {
            DicRole[objectValue.Id] = objectValue;
        }
        internal static void RemoveMemory(Role objectValue)
        {
            if (DicRole.ContainsKey(objectValue.Id))
                DicRole.Remove(objectValue.Id);
        }

        internal static void SetMemory(RolePermission objectValue)
        {
            var key = new RolePermissionKeys
            {
                IdPermission = objectValue.IdPermission,
                IdRole = objectValue.IdRole
            };
            DicRolePermission[key] = objectValue;
        }
        internal static void RemoveMemory(RolePermission objectValue)
        {
            var key = new RolePermissionKeys
            {
                IdPermission = objectValue.IdPermission,
                IdRole = objectValue.IdRole
            };
            if (DicRolePermission.ContainsKey(key))
                DicRolePermission.Remove(key);
        }

        internal static void SetMemory(UserInfo objectValue)
        {
            DicUserInfo[objectValue.IdUserLogin] = objectValue;
        }
        internal static void RemoveMemory(UserInfo objectValue)
        {
            if (DicUserInfo.ContainsKey(objectValue.IdUserLogin))
                DicUserInfo.Remove(objectValue.IdUserLogin);
        }

        internal static void SetMemory(UserLogin objectValue)
        {
            DicUserLogin[objectValue.Id] = objectValue;
        }
        internal static void RemoveMemory(UserLogin objectValue)
        {
            if (DicUserLogin.ContainsKey(objectValue.Id))
                DicUserLogin.Remove(objectValue.Id);
        }

        internal static void SetMemory(UserRole objectValue)
        {
            var key = new UserRoleKeys
            {
                IdRole = objectValue.IdRole,
                IdUserLogin = objectValue.IdUserLogin
            };
            DicUserRole[key] = objectValue;
        }
        internal static void RemoveMemory(UserRole objectValue)
        {
            var key = new UserRoleKeys
            {
                IdRole = objectValue.IdRole,
                IdUserLogin = objectValue.IdUserLogin
            };
            if (DicUserRole.ContainsKey(key))
                DicUserRole.Remove(key);
        }

    }
}



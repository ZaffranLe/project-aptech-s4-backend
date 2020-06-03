using ElectricShop.Entity.Entities;
using ElectricShop.Entity.Keys;

namespace ElectricShop.Memory
{
    public partial class MemorySet : Memory
    {
        internal static void SetMemory(Customer objectValue)
        {
            string entityName = objectValue.GetName();
            // chua co thi khoi tao
            if (!DicMaxKeyEntity.ContainsKey(entityName))
                DicMaxKeyEntity[entityName] = 0;
            // co roi thi so sanh roi set max key vao dic
            if (DicMaxKeyEntity[entityName] < objectValue.Id)
            {
                DicMaxKeyEntity[entityName] = objectValue.Id;
            }
            DicCustomer[objectValue.Id] = objectValue;
        }
        internal static void RemoveMemory(Customer objectValue)
        {
            if (DicCustomer.ContainsKey(objectValue.Id))
                DicCustomer.Remove(objectValue.Id);
        }

        internal static void SetMemory(Image objectValue)
        {
            string entityName = objectValue.GetName();
            // chua co thi khoi tao
            if (!DicMaxKeyEntity.ContainsKey(entityName))
                DicMaxKeyEntity[entityName] = 0;
            // co roi thi so sanh roi set max key vao dic
            if (DicMaxKeyEntity[entityName] < objectValue.Id)
            {
                DicMaxKeyEntity[entityName] = objectValue.Id;
            }
            DicImage[objectValue.Id] = objectValue;
        }
        internal static void RemoveMemory(Image objectValue)
        {
            if (DicImage.ContainsKey(objectValue.Id))
                DicImage.Remove(objectValue.Id);
        }

        internal static void SetMemory(ImportReceipt objectValue)
        {
            string entityName = objectValue.GetName();
            // chua co thi khoi tao
            if (!DicMaxKeyEntity.ContainsKey(entityName))
                DicMaxKeyEntity[entityName] = 0;
            // co roi thi so sanh roi set max key vao dic
            if (DicMaxKeyEntity[entityName] < objectValue.Id)
            {
                DicMaxKeyEntity[entityName] = objectValue.Id;
            }
            DicImportReceipt[objectValue.Id] = objectValue;
        }
        internal static void RemoveMemory(ImportReceipt objectValue)
        {
            if (DicImportReceipt.ContainsKey(objectValue.Id))
                DicImportReceipt.Remove(objectValue.Id);
        }

        internal static void SetMemory(Manufacturer objectValue)
        {
            string entityName = objectValue.GetName();
            // chua co thi khoi tao
            if (!DicMaxKeyEntity.ContainsKey(entityName))
                DicMaxKeyEntity[entityName] = 0;
            // co roi thi so sanh roi set max key vao dic
            if (DicMaxKeyEntity[entityName] < objectValue.Id)
            {
                DicMaxKeyEntity[entityName] = objectValue.Id;
            }
            DicManufacturer[objectValue.Id] = objectValue;
        }
        internal static void RemoveMemory(Manufacturer objectValue)
        {
            if (DicManufacturer.ContainsKey(objectValue.Id))
                DicManufacturer.Remove(objectValue.Id);
        }

        internal static void SetMemory(OrderDetail objectValue)
        {
            string entityName = objectValue.GetName();
            // chua co thi khoi tao
            if (!DicMaxKeyEntity.ContainsKey(entityName))
                DicMaxKeyEntity[entityName] = 0;
            // co roi thi so sanh roi set max key vao dic
            if (DicMaxKeyEntity[entityName] < objectValue.Id)
            {
                DicMaxKeyEntity[entityName] = objectValue.Id;
            }
            DicOrderDetail[objectValue.Id] = objectValue;
        }
        internal static void RemoveMemory(OrderDetail objectValue)
        {
            if (DicOrderDetail.ContainsKey(objectValue.Id))
                DicOrderDetail.Remove(objectValue.Id);
        }

        internal static void SetMemory(Permission objectValue)
        {
            string entityName = objectValue.GetName();
            // chua co thi khoi tao
            if (!DicMaxKeyEntity.ContainsKey(entityName))
                DicMaxKeyEntity[entityName] = 0;
            // co roi thi so sanh roi set max key vao dic
            if (DicMaxKeyEntity[entityName] < objectValue.Id)
            {
                DicMaxKeyEntity[entityName] = objectValue.Id;
            }
            DicPermission[objectValue.Id] = objectValue;
        }
        internal static void RemoveMemory(Permission objectValue)
        {
            if (DicPermission.ContainsKey(objectValue.Id))
                DicPermission.Remove(objectValue.Id);
        }

        internal static void SetMemory(Product objectValue)
        {
            string entityName = objectValue.GetName();
            // chua co thi khoi tao
            if (!DicMaxKeyEntity.ContainsKey(entityName))
                DicMaxKeyEntity[entityName] = 0;
            // co roi thi so sanh roi set max key vao dic
            if (DicMaxKeyEntity[entityName] < objectValue.Id)
            {
                DicMaxKeyEntity[entityName] = objectValue.Id;
            }
            DicProduct[objectValue.Id] = objectValue;
        }
        internal static void RemoveMemory(Product objectValue)
        {
            if (DicProduct.ContainsKey(objectValue.Id))
                DicProduct.Remove(objectValue.Id);
        }

        internal static void SetMemory(ProductType objectValue)
        {
            string entityName = objectValue.GetName();
            // chua co thi khoi tao
            if (!DicMaxKeyEntity.ContainsKey(entityName))
                DicMaxKeyEntity[entityName] = 0;
            // co roi thi so sanh roi set max key vao dic
            if (DicMaxKeyEntity[entityName] < objectValue.Id)
            {
                DicMaxKeyEntity[entityName] = objectValue.Id;
            }
            DicProductType[objectValue.Id] = objectValue;
        }
        internal static void RemoveMemory(ProductType objectValue)
        {
            if (DicProductType.ContainsKey(objectValue.Id))
                DicProductType.Remove(objectValue.Id);
        }

        internal static void SetMemory(Property objectValue)
        {
            string entityName = objectValue.GetName();
            // chua co thi khoi tao
            if (!DicMaxKeyEntity.ContainsKey(entityName))
                DicMaxKeyEntity[entityName] = 0;
            // co roi thi so sanh roi set max key vao dic
            if (DicMaxKeyEntity[entityName] < objectValue.Id)
            {
                DicMaxKeyEntity[entityName] = objectValue.Id;
            }
            DicProperty[objectValue.Id] = objectValue;
        }
        internal static void RemoveMemory(Property objectValue)
        {
            if (DicProperty.ContainsKey(objectValue.Id))
                DicProperty.Remove(objectValue.Id);
        }

        internal static void SetMemory(Provider objectValue)
        {
            string entityName = objectValue.GetName();
            // chua co thi khoi tao
            if (!DicMaxKeyEntity.ContainsKey(entityName))
                DicMaxKeyEntity[entityName] = 0;
            // co roi thi so sanh roi set max key vao dic
            if (DicMaxKeyEntity[entityName] < objectValue.Id)
            {
                DicMaxKeyEntity[entityName] = objectValue.Id;
            }
            DicProvider[objectValue.Id] = objectValue;
        }
        internal static void RemoveMemory(Provider objectValue)
        {
            if (DicProvider.ContainsKey(objectValue.Id))
                DicProvider.Remove(objectValue.Id);
        }

        internal static void SetMemory(Role objectValue)
        {
            string entityName = objectValue.GetName();
            // chua co thi khoi tao
            if (!DicMaxKeyEntity.ContainsKey(entityName))
                DicMaxKeyEntity[entityName] = 0;
            // co roi thi so sanh roi set max key vao dic
            if (DicMaxKeyEntity[entityName] < objectValue.Id)
            {
                DicMaxKeyEntity[entityName] = objectValue.Id;
            }
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
            string entityName = objectValue.GetName();
            // chua co thi khoi tao
            if (!DicMaxKeyEntity.ContainsKey(entityName))
                DicMaxKeyEntity[entityName] = 0;
            // co roi thi so sanh roi set max key vao dic
            if (DicMaxKeyEntity[entityName] < objectValue.IdUserLogin)
            {
                DicMaxKeyEntity[entityName] = objectValue.IdUserLogin;
            }
            DicUserInfo[objectValue.IdUserLogin] = objectValue;
        }
        internal static void RemoveMemory(UserInfo objectValue)
        {
            if (DicUserInfo.ContainsKey(objectValue.IdUserLogin))
                DicUserInfo.Remove(objectValue.IdUserLogin);
        }

        internal static void SetMemory(UserLogin objectValue)
        {
            string entityName = objectValue.GetName();
            // chua co thi khoi tao
            if (!DicMaxKeyEntity.ContainsKey(entityName))
                DicMaxKeyEntity[entityName] = 0;
            // co roi thi so sanh roi set max key vao dic
            if (DicMaxKeyEntity[entityName] < objectValue.Id)
            {
                DicMaxKeyEntity[entityName] = objectValue.Id;
            }
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



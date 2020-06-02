using System;
using System.Linq;
using Anotar.NLog;
using ElectricShop.Entity;
using ElectricShop.Entity.Entities;

namespace ElectricShop.Memory
{
    public partial class MemorySet : Memory
    {
        public static void SortAllDic()
        {
            try
            {
                DicCustomer = DicCustomer.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
                DicImage = DicImage.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
                DicImportReceipt = DicImportReceipt.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
                DicManufacturer = DicManufacturer.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
                DicOrderDetail = DicOrderDetail.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
                DicPermission = DicPermission.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
                DicProduct = DicProduct.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
                DicProductType = DicProductType.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
                DicProperty = DicProperty.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
                DicProvider = DicProvider.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
                DicRole = DicRole.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
                DicUserInfo = DicUserInfo.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
                DicUserLogin = DicUserLogin.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
            }
            catch (Exception ex) { LogTo.Error(ex.ToString()); }
        }

        public static void ClearAllDic()
        {
            try
            {
                DicCustomer.Clear();
                DicImage.Clear();
                DicImportReceipt.Clear();
                DicManufacturer.Clear();
                DicOrderDetail.Clear();
                DicPermission.Clear();
                DicProduct.Clear();
                DicProductType.Clear();
                DicProperty.Clear();
                DicProvider.Clear();
                DicRole.Clear();
                DicRolePermission.Clear();
                DicUserInfo.Clear();
                DicUserLogin.Clear();
                DicUserRole.Clear();
            }
            catch (Exception ex) { LogTo.Error(ex.ToString()); }
        }

        private static void UpdateAndInsertEntity(BaseEntity entity)
        {
            #region Bảng sinh key bằng tay
            if (entity is Customer)
                SetMemory(entity as Customer);
            else if (entity is Image)
                SetMemory(entity as Image);
            else if (entity is ImportReceipt)
                SetMemory(entity as ImportReceipt);
            else if (entity is Manufacturer)
                SetMemory(entity as Manufacturer);
            else if (entity is OrderDetail)
                SetMemory(entity as OrderDetail);
            else if (entity is Permission)
                SetMemory(entity as Permission);
            else if (entity is Product)
                SetMemory(entity as Product);
            else if (entity is ProductType)
                SetMemory(entity as ProductType);
            else if (entity is Property)
                SetMemory(entity as Property);
            else if (entity is Provider)
                SetMemory(entity as Provider);
            else if (entity is Role)
                SetMemory(entity as Role);
            else if (entity is RolePermission)
                SetMemory(entity as RolePermission);
            else if (entity is UserInfo)
                SetMemory(entity as UserInfo);
            else if (entity is UserLogin)
                SetMemory(entity as UserLogin);
            else if (entity is UserRole)
                SetMemory(entity as UserRole);
            #endregion
            #region Bảng tự sinh Key
            #endregion
        }
        private static void RemoveEntity(BaseEntity entity)
        {
            #region Bảng sinh key bằng tay
            if (entity is Customer)
                RemoveMemory(entity as Customer);
            else if (entity is Image)
                RemoveMemory(entity as Image);
            else if (entity is ImportReceipt)
                RemoveMemory(entity as ImportReceipt);
            else if (entity is Manufacturer)
                RemoveMemory(entity as Manufacturer);
            else if (entity is OrderDetail)
                RemoveMemory(entity as OrderDetail);
            else if (entity is Permission)
                RemoveMemory(entity as Permission);
            else if (entity is Product)
                RemoveMemory(entity as Product);
            else if (entity is ProductType)
                RemoveMemory(entity as ProductType);
            else if (entity is Property)
                RemoveMemory(entity as Property);
            else if (entity is Provider)
                RemoveMemory(entity as Provider);
            else if (entity is Role)
                RemoveMemory(entity as Role);
            else if (entity is RolePermission)
                RemoveMemory(entity as RolePermission);
            else if (entity is UserInfo)
                RemoveMemory(entity as UserInfo);
            else if (entity is UserLogin)
                RemoveMemory(entity as UserLogin);
            else if (entity is UserRole)
                RemoveMemory(entity as UserRole);
            #endregion
            #region Bảng tự sinh Key
            #endregion
        }
    }
}



using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ElectricShop.Entity.Entities;

namespace ElectricShop.Memory
{
    public partial class MemoryInfo : Memory
    {
        #region Customer
        public static List<Customer> GetListCustomerByField(string fieldValue, Customer.CustomerFields fieldName)
        {
            if (string.IsNullOrEmpty(fieldValue)) fieldValue = "";
            var listValue = new List<Customer>();
            if (fieldName == Customer.CustomerFields.Address)
            {
                listValue = DicCustomer.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Address.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Customer.CustomerFields.CreatedBy)
            {
                listValue = DicCustomer.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.CreatedBy.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Customer.CustomerFields.Id)
            {
                listValue = DicCustomer.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Id.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Customer.CustomerFields.Name)
            {
                listValue = DicCustomer.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Name.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Customer.CustomerFields.Phone)
            {
                listValue = DicCustomer.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Phone.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Customer.CustomerFields.UpdatedBy)
            {
                listValue = DicCustomer.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.UpdatedBy.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            return listValue.Select(value => value.Clone() as Customer).ToList();
        }
        #endregion

        #region Image
        public static List<Image> GetListImageByField(string fieldValue, Image.ImageFields fieldName)
        {
            if (string.IsNullOrEmpty(fieldValue)) fieldValue = "";
            var listValue = new List<Image>();
            if (fieldName == Image.ImageFields.CreatedBy)
            {
                listValue = DicImage.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.CreatedBy.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Image.ImageFields.Id)
            {
                listValue = DicImage.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Id.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Image.ImageFields.UpdatedBy)
            {
                listValue = DicImage.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.UpdatedBy.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            return listValue.Select(value => value.Clone() as Image).ToList();
        }
        #endregion

        #region ImportReceipt
        public static List<ImportReceipt> GetListImportReceiptByField(string fieldValue, ImportReceipt.ImportReceiptFields fieldName)
        {
            if (string.IsNullOrEmpty(fieldValue)) fieldValue = "";
            var listValue = new List<ImportReceipt>();
            if (fieldName == ImportReceipt.ImportReceiptFields.CreatedBy)
            {
                listValue = DicImportReceipt.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.CreatedBy.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == ImportReceipt.ImportReceiptFields.Id)
            {
                listValue = DicImportReceipt.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Id.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == ImportReceipt.ImportReceiptFields.IdEmployee)
            {
                listValue = DicImportReceipt.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.IdEmployee.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == ImportReceipt.ImportReceiptFields.IdProvider)
            {
                listValue = DicImportReceipt.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.IdProvider.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == ImportReceipt.ImportReceiptFields.ListProductId)
            {
                listValue = DicImportReceipt.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.ListProductId.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == ImportReceipt.ImportReceiptFields.TotalPrice)
            {
                listValue = DicImportReceipt.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.TotalPrice.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == ImportReceipt.ImportReceiptFields.UpdatedBy)
            {
                listValue = DicImportReceipt.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.UpdatedBy.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            return listValue.Select(value => value.Clone() as ImportReceipt).ToList();
        }
        #endregion

        #region Manufacturer
        public static List<Manufacturer> GetListManufacturerByField(string fieldValue, Manufacturer.ManufacturerFields fieldName)
        {
            if (string.IsNullOrEmpty(fieldValue)) fieldValue = "";
            var listValue = new List<Manufacturer>();
            if (fieldName == Manufacturer.ManufacturerFields.CreatedBy)
            {
                listValue = DicManufacturer.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.CreatedBy.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Manufacturer.ManufacturerFields.Id)
            {
                listValue = DicManufacturer.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Id.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Manufacturer.ManufacturerFields.Name)
            {
                listValue = DicManufacturer.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Name.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Manufacturer.ManufacturerFields.UpdatedBy)
            {
                listValue = DicManufacturer.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.UpdatedBy.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            return listValue.Select(value => value.Clone() as Manufacturer).ToList();
        }
        #endregion

        #region OrderDetail
        public static List<OrderDetail> GetListOrderDetailByField(string fieldValue, OrderDetail.OrderDetailFields fieldName)
        {
            if (string.IsNullOrEmpty(fieldValue)) fieldValue = "";
            var listValue = new List<OrderDetail>();
            if (fieldName == OrderDetail.OrderDetailFields.Address)
            {
                listValue = DicOrderDetail.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Address.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == OrderDetail.OrderDetailFields.CreatedBy)
            {
                listValue = DicOrderDetail.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.CreatedBy.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == OrderDetail.OrderDetailFields.Id)
            {
                listValue = DicOrderDetail.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Id.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == OrderDetail.OrderDetailFields.IdCustomer)
            {
                listValue = DicOrderDetail.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.IdCustomer.ToString(), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == OrderDetail.OrderDetailFields.IdEmployee)
            {
                listValue = DicOrderDetail.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.IdEmployee.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == OrderDetail.OrderDetailFields.ListProductId)
            {
                listValue = DicOrderDetail.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.ListProductId.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == OrderDetail.OrderDetailFields.Name)
            {
                listValue = DicOrderDetail.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Name.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == OrderDetail.OrderDetailFields.OrderStatus)
            {
                listValue = DicOrderDetail.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.OrderStatus.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == OrderDetail.OrderDetailFields.Phone)
            {
                listValue = DicOrderDetail.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Phone.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == OrderDetail.OrderDetailFields.TotalPrice)
            {
                listValue = DicOrderDetail.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.TotalPrice.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == OrderDetail.OrderDetailFields.UpdatedBy)
            {
                listValue = DicOrderDetail.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.UpdatedBy.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            return listValue.Select(value => value.Clone() as OrderDetail).ToList();
        }
        #endregion

        #region Permission
        public static List<Permission> GetListPermissionByField(string fieldValue, Permission.PermissionFields fieldName)
        {
            if (string.IsNullOrEmpty(fieldValue)) fieldValue = "";
            var listValue = new List<Permission>();
            if (fieldName == Permission.PermissionFields.CreatedBy)
            {
                listValue = DicPermission.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.CreatedBy.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Permission.PermissionFields.Description)
            {
                listValue = DicPermission.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Description.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Permission.PermissionFields.Id)
            {
                listValue = DicPermission.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Id.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Permission.PermissionFields.Name)
            {
                listValue = DicPermission.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Name.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Permission.PermissionFields.UpdatedBy)
            {
                listValue = DicPermission.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.UpdatedBy.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            return listValue.Select(value => value.Clone() as Permission).ToList();
        }
        #endregion
        #region Post
        public static List<Post> GetListPostByField(string fieldValue, Post.PostFields fieldName)
        {
            if (string.IsNullOrEmpty(fieldValue)) fieldValue = "";
            var listValue = new List<Post>();
            if (fieldName == Post.PostFields.Content)
            {
                listValue = DicPost.Values.ToList().FindAll(obj =>
                    fieldValue.Equals(obj.Content.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Post.PostFields.CreatedBy)
            {
                listValue = DicPost.Values.ToList().FindAll(obj =>
                    fieldValue.Equals(obj.CreatedBy.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Post.PostFields.Id)
            {
                listValue = DicPost.Values.ToList().FindAll(obj =>
                    fieldValue.Equals(obj.Id.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Post.PostFields.Tittle)
            {
                listValue = DicPost.Values.ToList().FindAll(obj =>
                    fieldValue.Equals(obj.Tittle.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Post.PostFields.UpdatedBy)
            {
                listValue = DicPost.Values.ToList().FindAll(obj =>
                    fieldValue.Equals(obj.UpdatedBy.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            return listValue.Select(value => value.Clone() as Post).ToList();
        }
        #endregion

        #region Product
        public static List<Product> GetListProductByField(string fieldValue, Product.ProductFields fieldName)
        {
            if (string.IsNullOrEmpty(fieldValue)) fieldValue = "";
            var listValue = new List<Product>();
            if (fieldName == Product.ProductFields.CreatedBy)
            {
                listValue = DicProduct.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.CreatedBy.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Product.ProductFields.Description)
            {
                listValue = DicProduct.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Description.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Product.ProductFields.Id)
            {
                listValue = DicProduct.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Id.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Product.ProductFields.IdDisplay)
            {
                listValue = DicProduct.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.IdDisplay.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Product.ProductFields.IdManufacturer)
            {
                listValue = DicProduct.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.IdManufacturer.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Product.ProductFields.IdProductType)
            {
                listValue = DicProduct.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.IdProductType.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Product.ProductFields.Name)
            {
                listValue = DicProduct.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Name.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Product.ProductFields.Quantity)
            {
                listValue = DicProduct.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Quantity.ToString(), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Product.ProductFields.SupportDuration)
            {
                listValue = DicProduct.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.SupportDuration.ToString(), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Product.ProductFields.UnitPrice)
            {
                listValue = DicProduct.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.UnitPrice.ToString(), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Product.ProductFields.UpdatedBy)
            {
                listValue = DicProduct.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.UpdatedBy.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            return listValue.Select(value => value.Clone() as Product).ToList();
        }
        #endregion

        #region ProductType
        public static List<ProductType> GetListProductTypeByField(string fieldValue, ProductType.ProductTypeFields fieldName)
        {
            if (string.IsNullOrEmpty(fieldValue)) fieldValue = "";
            var listValue = new List<ProductType>();
            if (fieldName == ProductType.ProductTypeFields.CreatedBy)
            {
                listValue = DicProductType.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.CreatedBy.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == ProductType.ProductTypeFields.Id)
            {
                listValue = DicProductType.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Id.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == ProductType.ProductTypeFields.Name)
            {
                listValue = DicProductType.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Name.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == ProductType.ProductTypeFields.UpdatedBy)
            {
                listValue = DicProductType.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.UpdatedBy.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            return listValue.Select(value => value.Clone() as ProductType).ToList();
        }
        #endregion

        #region Property
        public static List<Property> GetListPropertyByField(string fieldValue, Property.PropertyFields fieldName)
        {
            if (string.IsNullOrEmpty(fieldValue)) fieldValue = "";
            var listValue = new List<Property>();
            if (fieldName == Property.PropertyFields.CreatedBy)
            {
                listValue = DicProperty.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.CreatedBy.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Property.PropertyFields.Data)
            {
                listValue = DicProperty.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Data.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Property.PropertyFields.Id)
            {
                listValue = DicProperty.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Id.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Property.PropertyFields.IdProduct)
            {
                listValue = DicProperty.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.IdProduct.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Property.PropertyFields.Name)
            {
                listValue = DicProperty.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Name.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Property.PropertyFields.UpdatedBy)
            {
                listValue = DicProperty.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.UpdatedBy.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            return listValue.Select(value => value.Clone() as Property).ToList();
        }
        #endregion

        #region Provider
        public static List<Provider> GetListProviderByField(string fieldValue, Provider.ProviderFields fieldName)
        {
            if (string.IsNullOrEmpty(fieldValue)) fieldValue = "";
            var listValue = new List<Provider>();
            if (fieldName == Provider.ProviderFields.Address)
            {
                listValue = DicProvider.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Address.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Provider.ProviderFields.CreatedBy)
            {
                listValue = DicProvider.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.CreatedBy.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Provider.ProviderFields.Email)
            {
                listValue = DicProvider.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Email.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Provider.ProviderFields.Id)
            {
                listValue = DicProvider.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Id.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Provider.ProviderFields.Name)
            {
                listValue = DicProvider.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Name.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Provider.ProviderFields.Phone)
            {
                listValue = DicProvider.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Phone.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Provider.ProviderFields.UpdatedBy)
            {
                listValue = DicProvider.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.UpdatedBy.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            return listValue.Select(value => value.Clone() as Provider).ToList();
        }
        #endregion

        #region Role
        public static List<Role> GetListRoleByField(string fieldValue, Role.RoleFields fieldName)
        {
            if (string.IsNullOrEmpty(fieldValue)) fieldValue = "";
            var listValue = new List<Role>();
            if (fieldName == Role.RoleFields.CreatedBy)
            {
                listValue = DicRole.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.CreatedBy.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Role.RoleFields.Description)
            {
                listValue = DicRole.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Description.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Role.RoleFields.Id)
            {
                listValue = DicRole.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Id.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Role.RoleFields.Name)
            {
                listValue = DicRole.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Name.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == Role.RoleFields.UpdatedBy)
            {
                listValue = DicRole.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.UpdatedBy.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            return listValue.Select(value => value.Clone() as Role).ToList();
        }
        #endregion

        #region RolePermission
        public static List<RolePermission> GetListRolePermissionByField(string fieldValue, RolePermission.RolePermissionFields fieldName)
        {
            if (string.IsNullOrEmpty(fieldValue)) fieldValue = "";
            var listValue = new List<RolePermission>();
            if (fieldName == RolePermission.RolePermissionFields.IdRole)
            {
                listValue = DicRolePermission.Values.ToList().FindAll(obj =>
                    fieldValue.Equals(obj.IdRole.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == RolePermission.RolePermissionFields.IdPermission)
            {
                listValue = DicRolePermission.Values.ToList().FindAll(obj =>
                    fieldValue.Equals(obj.IdPermission.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            return listValue.Select(value => value.Clone() as RolePermission).ToList();
        }
        #endregion

        #region UserInfo
        public static List<UserInfo> GetListUserInfoByField(string fieldValue, UserInfo.UserInfoFields fieldName)
        {
            if (string.IsNullOrEmpty(fieldValue)) fieldValue = "";
            var listValue = new List<UserInfo>();
            if (fieldName == UserInfo.UserInfoFields.Address)
            {
                listValue = DicUserInfo.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Address.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == UserInfo.UserInfoFields.CreatedBy)
            {
                listValue = DicUserInfo.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.CreatedBy.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == UserInfo.UserInfoFields.Email)
            {
                listValue = DicUserInfo.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Email.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == UserInfo.UserInfoFields.IdUserLogin)
            {
                listValue = DicUserInfo.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.IdUserLogin.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == UserInfo.UserInfoFields.Name)
            {
                listValue = DicUserInfo.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Name.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == UserInfo.UserInfoFields.Phone)
            {
                listValue = DicUserInfo.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Phone.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == UserInfo.UserInfoFields.UpdatedBy)
            {
                listValue = DicUserInfo.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.UpdatedBy.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            return listValue.Select(value => value.Clone() as UserInfo).ToList();
        }
        #endregion

        #region UserLogin
        public static List<UserLogin> GetListUserLoginByField(string fieldValue, UserLogin.UserLoginFields fieldName)
        {
            if (string.IsNullOrEmpty(fieldValue)) fieldValue = "";
            var listValue = new List<UserLogin>();
            if (fieldName == UserLogin.UserLoginFields.Id)
            {
                listValue = DicUserLogin.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Id.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == UserLogin.UserLoginFields.Password)
            {
                listValue = DicUserLogin.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Password.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == UserLogin.UserLoginFields.Username)
            {
                listValue = DicUserLogin.Values.ToList().FindAll(obj =>
                fieldValue.Equals(obj.Username.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            else if (fieldName == UserLogin.UserLoginFields.UserStatus)
            {
                listValue = DicUserLogin.Values.ToList().FindAll(obj =>
                    fieldValue.Equals(obj.UserStatus.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            return listValue.Select(value => value.Clone() as UserLogin).ToList();
        }
        #endregion

        #region UserRole
        public static List<UserRole> GetListUserRoleByField(string fieldValue, UserRole.UserRoleFields fieldName)
        {
            if (string.IsNullOrEmpty(fieldValue)) fieldValue = "";
            var listValue = new List<UserRole>();
            if (fieldName == UserRole.UserRoleFields.IdRole)
            {
                listValue = DicUserRole.Values.ToList().FindAll(obj =>
                    fieldValue.Equals(obj.IdRole.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }else if (fieldName == UserRole.UserRoleFields.IdUserLogin)
            {
                listValue = DicUserRole.Values.ToList().FindAll(obj =>
                    fieldValue.Equals(obj.IdUserLogin.ToString(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase));
            }
            return listValue.Select(value => value.Clone() as UserRole).ToList();
        }
        #endregion

    }
}



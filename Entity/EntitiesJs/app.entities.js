function Customer() {
        this.$type = "QuantEdge.Entity.Entities.Customer, Entity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        this.Address = null;
        this.CreatedAt = null;
        this.CreatedBy = 0;
        this.Id = 0;//Key
        this.Name = null;
        this.Phone = null;
        this.UpdatedAt = null;
        this.UpdatedBy = 0;
    }

function Image() {
        this.$type = "QuantEdge.Entity.Entities.Image, Entity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        this.CreatedAt = null;
        this.CreatedBy = 0;
        this.Id = 0;//Key
        this.IdProduct = 0;
        this.Name = null;
        this.UpdatedAt = null;
        this.UpdatedBy = 0;
    }

function ImportReceipt() {
        this.$type = "QuantEdge.Entity.Entities.ImportReceipt, Entity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        this.CreatedAt = null;
        this.CreatedBy = 0;
        this.Date = new Date();
        this.Id = 0;//Key
        this.IdEmployee = 0;
        this.IdProvider = 0;
        this.ListProductId = null;
        this.TotalPrice = 0;
        this.UpdatedAt = null;
        this.UpdatedBy = 0;
    }

function Manufacturer() {
        this.$type = "QuantEdge.Entity.Entities.Manufacturer, Entity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        this.CreatedAt = null;
        this.CreatedBy = 0;
        this.Id = 0;//Key
        this.Name = null;
        this.UpdatedAt = null;
        this.UpdatedBy = 0;
    }

function OrderDetail() {
        this.$type = "QuantEdge.Entity.Entities.OrderDetail, Entity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        this.Address = null;
        this.CreatedAt = null;
        this.CreatedBy = 0;
        this.Date = new Date();
        this.Id = 0;//Key
        this.IdCustomer = null;
        this.IdEmployee = 0;
        this.ListProductId = null;
        this.Name = null;
        this.OrderStatus = null;
        this.Phone = null;
        this.TotalPrice = 0;
        this.UpdatedAt = null;
        this.UpdatedBy = 0;
    }

function Permission() {
        this.$type = "QuantEdge.Entity.Entities.Permission, Entity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        this.CreatedAt = null;
        this.CreatedBy = 0;
        this.Description = null;
        this.Id = 0;//Key
        this.Name = null;
        this.UpdatedAt = null;
        this.UpdatedBy = 0;
    }

function Product() {
        this.$type = "QuantEdge.Entity.Entities.Product, Entity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        this.CreatedAt = null;
        this.CreatedBy = 0;
        this.Description = null;
        this.Id = 0;//Key
        this.IdDisplay = null;
        this.IdManufacturer = 0;
        this.IdProductType = 0;
        this.Name = null;
        this.Quantity = null;
        this.SupportDuration = null;
        this.UnitPrice = null;
        this.UpdatedAt = null;
        this.UpdatedBy = 0;
    }

function ProductType() {
        this.$type = "QuantEdge.Entity.Entities.ProductType, Entity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        this.CreatedAt = null;
        this.CreatedBy = 0;
        this.Id = 0;//Key
        this.Name = null;
        this.UpdatedAt = null;
        this.UpdatedBy = 0;
    }

function Property() {
        this.$type = "QuantEdge.Entity.Entities.Property, Entity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        this.CreatedAt = null;
        this.CreatedBy = 0;
        this.Data = null;
        this.Id = 0;//Key
        this.IdProduct = 0;
        this.Name = null;
        this.UpdatedAt = null;
        this.UpdatedBy = 0;
    }

function Provider() {
        this.$type = "QuantEdge.Entity.Entities.Provider, Entity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        this.Address = null;
        this.CreatedAt = null;
        this.CreatedBy = 0;
        this.Email = null;
        this.Id = 0;//Key
        this.Name = null;
        this.Phone = null;
        this.UpdatedAt = null;
        this.UpdatedBy = 0;
    }

function Role() {
        this.$type = "QuantEdge.Entity.Entities.Role, Entity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        this.CreatedAt = null;
        this.CreatedBy = 0;
        this.Description = null;
        this.Id = 0;//Key
        this.Name = null;
        this.UpdatedAt = null;
        this.UpdatedBy = 0;
    }

function RolePermission() {
        this.$type = "QuantEdge.Entity.Entities.RolePermission, Entity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        this.IdPermission = 0;//Key
        this.IdRole = 0;//Key
    }

function UserInfo() {
        this.$type = "QuantEdge.Entity.Entities.UserInfo, Entity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        this.Address = null;
        this.CreatedAt = null;
        this.CreatedBy = 0;
        this.Email = null;
        this.IdUserLogin = 0;//Key
        this.Name = null;
        this.Phone = null;
        this.UpdatedAt = null;
        this.UpdatedBy = 0;
        this.UserStatus = 0;
    }

function UserLogin() {
        this.$type = "QuantEdge.Entity.Entities.UserLogin, Entity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        this.Id = 0;//Key
        this.Password = null;
        this.Username = null;
    }

function UserRole() {
        this.$type = "QuantEdge.Entity.Entities.UserRole, Entity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        this.IdRole = 0;//Key
        this.IdUserLogin = 0;//Key
    }


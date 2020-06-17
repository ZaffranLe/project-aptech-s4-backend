USE AptechProjectS4
GO



if exists (select * from dbo.sysobjects where id = object_id(N'Customer_Insert') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Customer_Insert
GO
CREATE PROCEDURE Customer_Insert
	@Address nvarchar(255) = null ,
	@CreatedAt datetime = null ,
	@CreatedBy int ,
	@Email varchar(255) = null ,
	@Id int ,
	@Name nvarchar(255) ,
	@Phone varchar(255) = null ,
	@UpdatedAt datetime = null ,
	@UpdatedBy int 

AS

INSERT [dbo].[Customer]
(
	[Address],
	[CreatedAt],
	[CreatedBy],
	[Email],
	[Id],
	[Name],
	[Phone],
	[UpdatedAt],
	[UpdatedBy]

)
VALUES
(
	@Address,
	@CreatedAt,
	@CreatedBy,
	@Email,
	@Id,
	@Name,
	@Phone,
	@UpdatedAt,
	@UpdatedBy

)


GO

if exists (select * from dbo.sysobjects where id = object_id(N'Customer_Update') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Customer_Update
GO
CREATE PROCEDURE Customer_Update
	@Address nvarchar(255) = null,
	@CreatedAt datetime = null,
	@CreatedBy int,
	@Email varchar(255) = null,
	@Id int,
	@Name nvarchar(255),
	@Phone varchar(255) = null,
	@UpdatedAt datetime = null,
	@UpdatedBy int

AS

UPDATE [dbo].[Customer]
SET
	[Address] = @Address,
	[CreatedAt] = @CreatedAt,
	[CreatedBy] = @CreatedBy,
	[Email] = @Email,
	[Id] = @Id,
	[Name] = @Name,
	[Phone] = @Phone,
	[UpdatedAt] = @UpdatedAt,
	[UpdatedBy] = @UpdatedBy
 WHERE 
	[Id] = @Id

GO

if exists (select * from dbo.sysobjects where id = object_id(N'Customer_SelectAll') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Customer_SelectAll
GO
CREATE PROCEDURE Customer_SelectAll
AS

	SELECT 
		[Address], [CreatedAt], [CreatedBy], [Email], [Id], [Name], [Phone], [UpdatedAt], [UpdatedBy]
	FROM [dbo].[Customer]

GO

if exists (select * from dbo.sysobjects where id = object_id(N'Customer_DeleteByPrimaryKey') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Customer_DeleteByPrimaryKey
GO
CREATE PROCEDURE Customer_DeleteByPrimaryKey
	@Id int
AS

DELETE FROM [dbo].[Customer]
 WHERE 
	[Id] = @Id

GO

if exists (select * from dbo.sysobjects where id = object_id(N'Image_Insert') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Image_Insert
GO
CREATE PROCEDURE Image_Insert
	@CreatedAt datetime = null ,
	@CreatedBy int ,
	@Id int ,
	@ImageUrl varchar(255) ,
	@UpdatedAt datetime = null ,
	@UpdatedBy int 

AS

INSERT [dbo].[Image]
(
	[CreatedAt],
	[CreatedBy],
	[Id],
	[ImageUrl],
	[UpdatedAt],
	[UpdatedBy]

)
VALUES
(
	@CreatedAt,
	@CreatedBy,
	@Id,
	@ImageUrl,
	@UpdatedAt,
	@UpdatedBy

)


GO

if exists (select * from dbo.sysobjects where id = object_id(N'Image_Update') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Image_Update
GO
CREATE PROCEDURE Image_Update
	@CreatedAt datetime = null,
	@CreatedBy int,
	@Id int,
	@ImageUrl varchar(255),
	@UpdatedAt datetime = null,
	@UpdatedBy int

AS

UPDATE [dbo].[Image]
SET
	[CreatedAt] = @CreatedAt,
	[CreatedBy] = @CreatedBy,
	[Id] = @Id,
	[ImageUrl] = @ImageUrl,
	[UpdatedAt] = @UpdatedAt,
	[UpdatedBy] = @UpdatedBy
 WHERE 
	[Id] = @Id

GO

if exists (select * from dbo.sysobjects where id = object_id(N'Image_SelectAll') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Image_SelectAll
GO
CREATE PROCEDURE Image_SelectAll
AS

	SELECT 
		[CreatedAt], [CreatedBy], [Id], [ImageUrl], [UpdatedAt], [UpdatedBy]
	FROM [dbo].[Image]

GO

if exists (select * from dbo.sysobjects where id = object_id(N'Image_DeleteByPrimaryKey') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Image_DeleteByPrimaryKey
GO
CREATE PROCEDURE Image_DeleteByPrimaryKey
	@Id int
AS

DELETE FROM [dbo].[Image]
 WHERE 
	[Id] = @Id

GO

if exists (select * from dbo.sysobjects where id = object_id(N'ImportReceipt_Insert') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure ImportReceipt_Insert
GO
CREATE PROCEDURE ImportReceipt_Insert
	@CreatedAt datetime = null ,
	@CreatedBy int ,
	@Date datetime ,
	@Id int ,
	@IdEmployee int ,
	@IdProvider int ,
	@ListProductId varchar(255) = null ,
	@TotalPrice decimal(19,4) ,
	@UpdatedAt datetime = null ,
	@UpdatedBy int 

AS

INSERT [dbo].[ImportReceipt]
(
	[CreatedAt],
	[CreatedBy],
	[Date],
	[Id],
	[IdEmployee],
	[IdProvider],
	[ListProductId],
	[TotalPrice],
	[UpdatedAt],
	[UpdatedBy]

)
VALUES
(
	@CreatedAt,
	@CreatedBy,
	@Date,
	@Id,
	@IdEmployee,
	@IdProvider,
	@ListProductId,
	@TotalPrice,
	@UpdatedAt,
	@UpdatedBy

)


GO

if exists (select * from dbo.sysobjects where id = object_id(N'ImportReceipt_Update') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure ImportReceipt_Update
GO
CREATE PROCEDURE ImportReceipt_Update
	@CreatedAt datetime = null,
	@CreatedBy int,
	@Date datetime,
	@Id int,
	@IdEmployee int,
	@IdProvider int,
	@ListProductId varchar(255) = null,
	@TotalPrice decimal(19,4),
	@UpdatedAt datetime = null,
	@UpdatedBy int

AS

UPDATE [dbo].[ImportReceipt]
SET
	[CreatedAt] = @CreatedAt,
	[CreatedBy] = @CreatedBy,
	[Date] = @Date,
	[Id] = @Id,
	[IdEmployee] = @IdEmployee,
	[IdProvider] = @IdProvider,
	[ListProductId] = @ListProductId,
	[TotalPrice] = @TotalPrice,
	[UpdatedAt] = @UpdatedAt,
	[UpdatedBy] = @UpdatedBy
 WHERE 
	[Id] = @Id

GO

if exists (select * from dbo.sysobjects where id = object_id(N'ImportReceipt_SelectAll') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure ImportReceipt_SelectAll
GO
CREATE PROCEDURE ImportReceipt_SelectAll
AS

	SELECT 
		[CreatedAt], [CreatedBy], [Date], [Id], [IdEmployee], [IdProvider], [ListProductId], [TotalPrice], [UpdatedAt], [UpdatedBy]
	FROM [dbo].[ImportReceipt]

GO

if exists (select * from dbo.sysobjects where id = object_id(N'ImportReceipt_DeleteByPrimaryKey') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure ImportReceipt_DeleteByPrimaryKey
GO
CREATE PROCEDURE ImportReceipt_DeleteByPrimaryKey
	@Id int
AS

DELETE FROM [dbo].[ImportReceipt]
 WHERE 
	[Id] = @Id

GO

if exists (select * from dbo.sysobjects where id = object_id(N'Manufacturer_Insert') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Manufacturer_Insert
GO
CREATE PROCEDURE Manufacturer_Insert
	@CreatedAt datetime = null ,
	@CreatedBy int ,
	@Id int ,
	@ImageId nvarchar(255) = null ,
	@Name nvarchar(255) ,
	@UpdatedAt datetime = null ,
	@UpdatedBy int 

AS

INSERT [dbo].[Manufacturer]
(
	[CreatedAt],
	[CreatedBy],
	[Id],
	[ImageId],
	[Name],
	[UpdatedAt],
	[UpdatedBy]

)
VALUES
(
	@CreatedAt,
	@CreatedBy,
	@Id,
	@ImageId,
	@Name,
	@UpdatedAt,
	@UpdatedBy

)


GO

if exists (select * from dbo.sysobjects where id = object_id(N'Manufacturer_Update') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Manufacturer_Update
GO
CREATE PROCEDURE Manufacturer_Update
	@CreatedAt datetime = null,
	@CreatedBy int,
	@Id int,
	@ImageId nvarchar(255) = null,
	@Name nvarchar(255),
	@UpdatedAt datetime = null,
	@UpdatedBy int

AS

UPDATE [dbo].[Manufacturer]
SET
	[CreatedAt] = @CreatedAt,
	[CreatedBy] = @CreatedBy,
	[Id] = @Id,
	[ImageId] = @ImageId,
	[Name] = @Name,
	[UpdatedAt] = @UpdatedAt,
	[UpdatedBy] = @UpdatedBy
 WHERE 
	[Id] = @Id

GO

if exists (select * from dbo.sysobjects where id = object_id(N'Manufacturer_SelectAll') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Manufacturer_SelectAll
GO
CREATE PROCEDURE Manufacturer_SelectAll
AS

	SELECT 
		[CreatedAt], [CreatedBy], [Id], [ImageId], [Name], [UpdatedAt], [UpdatedBy]
	FROM [dbo].[Manufacturer]

GO

if exists (select * from dbo.sysobjects where id = object_id(N'Manufacturer_DeleteByPrimaryKey') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Manufacturer_DeleteByPrimaryKey
GO
CREATE PROCEDURE Manufacturer_DeleteByPrimaryKey
	@Id int
AS

DELETE FROM [dbo].[Manufacturer]
 WHERE 
	[Id] = @Id

GO

if exists (select * from dbo.sysobjects where id = object_id(N'OrderDetail_Insert') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure OrderDetail_Insert
GO
CREATE PROCEDURE OrderDetail_Insert
	@Address nvarchar(255) = null ,
	@CreatedAt datetime = null ,
	@CreatedBy int ,
	@Date datetime ,
	@Id int ,
	@IdCustomer int = null ,
	@IdEmployee int ,
	@ListProductId varchar(255) = null ,
	@Name varchar(255) = null ,
	@OrderStatus varchar(255) = null ,
	@Phone varchar(255) = null ,
	@TotalPrice decimal(19,4) ,
	@UpdatedAt datetime = null ,
	@UpdatedBy int 

AS

INSERT [dbo].[OrderDetail]
(
	[Address],
	[CreatedAt],
	[CreatedBy],
	[Date],
	[Id],
	[IdCustomer],
	[IdEmployee],
	[ListProductId],
	[Name],
	[OrderStatus],
	[Phone],
	[TotalPrice],
	[UpdatedAt],
	[UpdatedBy]

)
VALUES
(
	@Address,
	@CreatedAt,
	@CreatedBy,
	@Date,
	@Id,
	@IdCustomer,
	@IdEmployee,
	@ListProductId,
	@Name,
	@OrderStatus,
	@Phone,
	@TotalPrice,
	@UpdatedAt,
	@UpdatedBy

)


GO

if exists (select * from dbo.sysobjects where id = object_id(N'OrderDetail_Update') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure OrderDetail_Update
GO
CREATE PROCEDURE OrderDetail_Update
	@Address nvarchar(255) = null,
	@CreatedAt datetime = null,
	@CreatedBy int,
	@Date datetime,
	@Id int,
	@IdCustomer int = null,
	@IdEmployee int,
	@ListProductId varchar(255) = null,
	@Name varchar(255) = null,
	@OrderStatus varchar(255) = null,
	@Phone varchar(255) = null,
	@TotalPrice decimal(19,4),
	@UpdatedAt datetime = null,
	@UpdatedBy int

AS

UPDATE [dbo].[OrderDetail]
SET
	[Address] = @Address,
	[CreatedAt] = @CreatedAt,
	[CreatedBy] = @CreatedBy,
	[Date] = @Date,
	[Id] = @Id,
	[IdCustomer] = @IdCustomer,
	[IdEmployee] = @IdEmployee,
	[ListProductId] = @ListProductId,
	[Name] = @Name,
	[OrderStatus] = @OrderStatus,
	[Phone] = @Phone,
	[TotalPrice] = @TotalPrice,
	[UpdatedAt] = @UpdatedAt,
	[UpdatedBy] = @UpdatedBy
 WHERE 
	[Id] = @Id

GO

if exists (select * from dbo.sysobjects where id = object_id(N'OrderDetail_SelectAll') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure OrderDetail_SelectAll
GO
CREATE PROCEDURE OrderDetail_SelectAll
AS

	SELECT 
		[Address], [CreatedAt], [CreatedBy], [Date], [Id], [IdCustomer], [IdEmployee], [ListProductId], [Name], [OrderStatus], [Phone], [TotalPrice], [UpdatedAt], [UpdatedBy]
	FROM [dbo].[OrderDetail]

GO

if exists (select * from dbo.sysobjects where id = object_id(N'OrderDetail_DeleteByPrimaryKey') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure OrderDetail_DeleteByPrimaryKey
GO
CREATE PROCEDURE OrderDetail_DeleteByPrimaryKey
	@Id int
AS

DELETE FROM [dbo].[OrderDetail]
 WHERE 
	[Id] = @Id

GO

if exists (select * from dbo.sysobjects where id = object_id(N'Permission_Insert') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Permission_Insert
GO
CREATE PROCEDURE Permission_Insert
	@CreatedAt datetime = null ,
	@CreatedBy int ,
	@Description nvarchar(255) ,
	@Id int ,
	@Name nvarchar(255) ,
	@UpdatedAt datetime = null ,
	@UpdatedBy int 

AS

INSERT [dbo].[Permission]
(
	[CreatedAt],
	[CreatedBy],
	[Description],
	[Id],
	[Name],
	[UpdatedAt],
	[UpdatedBy]

)
VALUES
(
	@CreatedAt,
	@CreatedBy,
	@Description,
	@Id,
	@Name,
	@UpdatedAt,
	@UpdatedBy

)


GO

if exists (select * from dbo.sysobjects where id = object_id(N'Permission_Update') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Permission_Update
GO
CREATE PROCEDURE Permission_Update
	@CreatedAt datetime = null,
	@CreatedBy int,
	@Description nvarchar(255),
	@Id int,
	@Name nvarchar(255),
	@UpdatedAt datetime = null,
	@UpdatedBy int

AS

UPDATE [dbo].[Permission]
SET
	[CreatedAt] = @CreatedAt,
	[CreatedBy] = @CreatedBy,
	[Description] = @Description,
	[Id] = @Id,
	[Name] = @Name,
	[UpdatedAt] = @UpdatedAt,
	[UpdatedBy] = @UpdatedBy
 WHERE 
	[Id] = @Id

GO

if exists (select * from dbo.sysobjects where id = object_id(N'Permission_SelectAll') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Permission_SelectAll
GO
CREATE PROCEDURE Permission_SelectAll
AS

	SELECT 
		[CreatedAt], [CreatedBy], [Description], [Id], [Name], [UpdatedAt], [UpdatedBy]
	FROM [dbo].[Permission]

GO

if exists (select * from dbo.sysobjects where id = object_id(N'Permission_DeleteByPrimaryKey') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Permission_DeleteByPrimaryKey
GO
CREATE PROCEDURE Permission_DeleteByPrimaryKey
	@Id int
AS

DELETE FROM [dbo].[Permission]
 WHERE 
	[Id] = @Id

GO

if exists (select * from dbo.sysobjects where id = object_id(N'Post_Insert') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Post_Insert
GO
CREATE PROCEDURE Post_Insert
	@Content text = null ,
	@CreatedAt datetime = null ,
	@CreatedBy int ,
	@Id int ,
	@Tittle varchar(255) ,
	@UpdatedAt datetime = null ,
	@UpdatedBy int 

AS

INSERT [dbo].[Post]
(
	[Content],
	[CreatedAt],
	[CreatedBy],
	[Id],
	[Tittle],
	[UpdatedAt],
	[UpdatedBy]

)
VALUES
(
	@Content,
	@CreatedAt,
	@CreatedBy,
	@Id,
	@Tittle,
	@UpdatedAt,
	@UpdatedBy

)


GO

if exists (select * from dbo.sysobjects where id = object_id(N'Post_Update') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Post_Update
GO
CREATE PROCEDURE Post_Update
	@Content text = null,
	@CreatedAt datetime = null,
	@CreatedBy int,
	@Id int,
	@Tittle varchar(255),
	@UpdatedAt datetime = null,
	@UpdatedBy int

AS

UPDATE [dbo].[Post]
SET
	[Content] = @Content,
	[CreatedAt] = @CreatedAt,
	[CreatedBy] = @CreatedBy,
	[Id] = @Id,
	[Tittle] = @Tittle,
	[UpdatedAt] = @UpdatedAt,
	[UpdatedBy] = @UpdatedBy
 WHERE 
	[Id] = @Id

GO

if exists (select * from dbo.sysobjects where id = object_id(N'Post_SelectAll') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Post_SelectAll
GO
CREATE PROCEDURE Post_SelectAll
AS

	SELECT 
		[Content], [CreatedAt], [CreatedBy], [Id], [Tittle], [UpdatedAt], [UpdatedBy]
	FROM [dbo].[Post]

GO

if exists (select * from dbo.sysobjects where id = object_id(N'Post_DeleteByPrimaryKey') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Post_DeleteByPrimaryKey
GO
CREATE PROCEDURE Post_DeleteByPrimaryKey
	@Id int
AS

DELETE FROM [dbo].[Post]
 WHERE 
	[Id] = @Id

GO

if exists (select * from dbo.sysobjects where id = object_id(N'Product_Insert') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Product_Insert
GO
CREATE PROCEDURE Product_Insert
	@CreatedAt datetime = null ,
	@CreatedBy int ,
	@Description nvarchar(255) = null ,
	@Id int ,
	@IdDisplay varchar(50) ,
	@IdManufacturer int ,
	@IdProductType int ,
	@ImageId nvarchar(255) = null ,
	@Name nvarchar(255) ,
	@Quantity int = null ,
	@SupportDuration int = null ,
	@UnitPrice decimal(19,4) = null ,
	@UpdatedAt datetime = null ,
	@UpdatedBy int 

AS

INSERT [dbo].[Product]
(
	[CreatedAt],
	[CreatedBy],
	[Description],
	[Id],
	[IdDisplay],
	[IdManufacturer],
	[IdProductType],
	[ImageId],
	[Name],
	[Quantity],
	[SupportDuration],
	[UnitPrice],
	[UpdatedAt],
	[UpdatedBy]

)
VALUES
(
	@CreatedAt,
	@CreatedBy,
	@Description,
	@Id,
	@IdDisplay,
	@IdManufacturer,
	@IdProductType,
	@ImageId,
	@Name,
	@Quantity,
	@SupportDuration,
	@UnitPrice,
	@UpdatedAt,
	@UpdatedBy

)


GO

if exists (select * from dbo.sysobjects where id = object_id(N'Product_Update') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Product_Update
GO
CREATE PROCEDURE Product_Update
	@CreatedAt datetime = null,
	@CreatedBy int,
	@Description nvarchar(255) = null,
	@Id int,
	@IdDisplay varchar(50),
	@IdManufacturer int,
	@IdProductType int,
	@ImageId nvarchar(255) = null,
	@Name nvarchar(255),
	@Quantity int = null,
	@SupportDuration int = null,
	@UnitPrice decimal(19,4) = null,
	@UpdatedAt datetime = null,
	@UpdatedBy int

AS

UPDATE [dbo].[Product]
SET
	[CreatedAt] = @CreatedAt,
	[CreatedBy] = @CreatedBy,
	[Description] = @Description,
	[Id] = @Id,
	[IdDisplay] = @IdDisplay,
	[IdManufacturer] = @IdManufacturer,
	[IdProductType] = @IdProductType,
	[ImageId] = @ImageId,
	[Name] = @Name,
	[Quantity] = @Quantity,
	[SupportDuration] = @SupportDuration,
	[UnitPrice] = @UnitPrice,
	[UpdatedAt] = @UpdatedAt,
	[UpdatedBy] = @UpdatedBy
 WHERE 
	[Id] = @Id

GO

if exists (select * from dbo.sysobjects where id = object_id(N'Product_SelectAll') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Product_SelectAll
GO
CREATE PROCEDURE Product_SelectAll
AS

	SELECT 
		[CreatedAt], [CreatedBy], [Description], [Id], [IdDisplay], [IdManufacturer], [IdProductType], [ImageId], [Name], [Quantity], [SupportDuration], [UnitPrice], [UpdatedAt], [UpdatedBy]
	FROM [dbo].[Product]

GO

if exists (select * from dbo.sysobjects where id = object_id(N'Product_DeleteByPrimaryKey') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Product_DeleteByPrimaryKey
GO
CREATE PROCEDURE Product_DeleteByPrimaryKey
	@Id int
AS

DELETE FROM [dbo].[Product]
 WHERE 
	[Id] = @Id

GO

if exists (select * from dbo.sysobjects where id = object_id(N'ProductType_Insert') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure ProductType_Insert
GO
CREATE PROCEDURE ProductType_Insert
	@CreatedAt datetime = null ,
	@CreatedBy int ,
	@Id int ,
	@ImageId nvarchar(255) = null ,
	@Name nvarchar(255) ,
	@UpdatedAt datetime = null ,
	@UpdatedBy int 

AS

INSERT [dbo].[ProductType]
(
	[CreatedAt],
	[CreatedBy],
	[Id],
	[ImageId],
	[Name],
	[UpdatedAt],
	[UpdatedBy]

)
VALUES
(
	@CreatedAt,
	@CreatedBy,
	@Id,
	@ImageId,
	@Name,
	@UpdatedAt,
	@UpdatedBy

)


GO

if exists (select * from dbo.sysobjects where id = object_id(N'ProductType_Update') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure ProductType_Update
GO
CREATE PROCEDURE ProductType_Update
	@CreatedAt datetime = null,
	@CreatedBy int,
	@Id int,
	@ImageId nvarchar(255) = null,
	@Name nvarchar(255),
	@UpdatedAt datetime = null,
	@UpdatedBy int

AS

UPDATE [dbo].[ProductType]
SET
	[CreatedAt] = @CreatedAt,
	[CreatedBy] = @CreatedBy,
	[Id] = @Id,
	[ImageId] = @ImageId,
	[Name] = @Name,
	[UpdatedAt] = @UpdatedAt,
	[UpdatedBy] = @UpdatedBy
 WHERE 
	[Id] = @Id

GO

if exists (select * from dbo.sysobjects where id = object_id(N'ProductType_SelectAll') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure ProductType_SelectAll
GO
CREATE PROCEDURE ProductType_SelectAll
AS

	SELECT 
		[CreatedAt], [CreatedBy], [Id], [ImageId], [Name], [UpdatedAt], [UpdatedBy]
	FROM [dbo].[ProductType]

GO

if exists (select * from dbo.sysobjects where id = object_id(N'ProductType_DeleteByPrimaryKey') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure ProductType_DeleteByPrimaryKey
GO
CREATE PROCEDURE ProductType_DeleteByPrimaryKey
	@Id int
AS

DELETE FROM [dbo].[ProductType]
 WHERE 
	[Id] = @Id

GO

if exists (select * from dbo.sysobjects where id = object_id(N'Property_Insert') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Property_Insert
GO
CREATE PROCEDURE Property_Insert
	@CreatedAt datetime = null ,
	@CreatedBy int ,
	@Data nvarchar(255) ,
	@Id int ,
	@IdProduct int ,
	@ImageId nvarchar(255) = null ,
	@Name nvarchar(255) ,
	@UpdatedAt datetime = null ,
	@UpdatedBy int 

AS

INSERT [dbo].[Property]
(
	[CreatedAt],
	[CreatedBy],
	[Data],
	[Id],
	[IdProduct],
	[ImageId],
	[Name],
	[UpdatedAt],
	[UpdatedBy]

)
VALUES
(
	@CreatedAt,
	@CreatedBy,
	@Data,
	@Id,
	@IdProduct,
	@ImageId,
	@Name,
	@UpdatedAt,
	@UpdatedBy

)


GO

if exists (select * from dbo.sysobjects where id = object_id(N'Property_Update') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Property_Update
GO
CREATE PROCEDURE Property_Update
	@CreatedAt datetime = null,
	@CreatedBy int,
	@Data nvarchar(255),
	@Id int,
	@IdProduct int,
	@ImageId nvarchar(255) = null,
	@Name nvarchar(255),
	@UpdatedAt datetime = null,
	@UpdatedBy int

AS

UPDATE [dbo].[Property]
SET
	[CreatedAt] = @CreatedAt,
	[CreatedBy] = @CreatedBy,
	[Data] = @Data,
	[Id] = @Id,
	[IdProduct] = @IdProduct,
	[ImageId] = @ImageId,
	[Name] = @Name,
	[UpdatedAt] = @UpdatedAt,
	[UpdatedBy] = @UpdatedBy
 WHERE 
	[Id] = @Id

GO

if exists (select * from dbo.sysobjects where id = object_id(N'Property_SelectAll') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Property_SelectAll
GO
CREATE PROCEDURE Property_SelectAll
AS

	SELECT 
		[CreatedAt], [CreatedBy], [Data], [Id], [IdProduct], [ImageId], [Name], [UpdatedAt], [UpdatedBy]
	FROM [dbo].[Property]

GO

if exists (select * from dbo.sysobjects where id = object_id(N'Property_DeleteByPrimaryKey') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Property_DeleteByPrimaryKey
GO
CREATE PROCEDURE Property_DeleteByPrimaryKey
	@Id int
AS

DELETE FROM [dbo].[Property]
 WHERE 
	[Id] = @Id

GO

if exists (select * from dbo.sysobjects where id = object_id(N'Provider_Insert') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Provider_Insert
GO
CREATE PROCEDURE Provider_Insert
	@Address nvarchar(255) = null ,
	@CreatedAt datetime = null ,
	@CreatedBy int ,
	@Email varchar(255) = null ,
	@Id int ,
	@ImageId nvarchar(255) = null ,
	@Name nvarchar(255) ,
	@Phone varchar(255) = null ,
	@UpdatedAt datetime = null ,
	@UpdatedBy int 

AS

INSERT [dbo].[Provider]
(
	[Address],
	[CreatedAt],
	[CreatedBy],
	[Email],
	[Id],
	[ImageId],
	[Name],
	[Phone],
	[UpdatedAt],
	[UpdatedBy]

)
VALUES
(
	@Address,
	@CreatedAt,
	@CreatedBy,
	@Email,
	@Id,
	@ImageId,
	@Name,
	@Phone,
	@UpdatedAt,
	@UpdatedBy

)


GO

if exists (select * from dbo.sysobjects where id = object_id(N'Provider_Update') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Provider_Update
GO
CREATE PROCEDURE Provider_Update
	@Address nvarchar(255) = null,
	@CreatedAt datetime = null,
	@CreatedBy int,
	@Email varchar(255) = null,
	@Id int,
	@ImageId nvarchar(255) = null,
	@Name nvarchar(255),
	@Phone varchar(255) = null,
	@UpdatedAt datetime = null,
	@UpdatedBy int

AS

UPDATE [dbo].[Provider]
SET
	[Address] = @Address,
	[CreatedAt] = @CreatedAt,
	[CreatedBy] = @CreatedBy,
	[Email] = @Email,
	[Id] = @Id,
	[ImageId] = @ImageId,
	[Name] = @Name,
	[Phone] = @Phone,
	[UpdatedAt] = @UpdatedAt,
	[UpdatedBy] = @UpdatedBy
 WHERE 
	[Id] = @Id

GO

if exists (select * from dbo.sysobjects where id = object_id(N'Provider_SelectAll') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Provider_SelectAll
GO
CREATE PROCEDURE Provider_SelectAll
AS

	SELECT 
		[Address], [CreatedAt], [CreatedBy], [Email], [Id], [ImageId], [Name], [Phone], [UpdatedAt], [UpdatedBy]
	FROM [dbo].[Provider]

GO

if exists (select * from dbo.sysobjects where id = object_id(N'Provider_DeleteByPrimaryKey') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Provider_DeleteByPrimaryKey
GO
CREATE PROCEDURE Provider_DeleteByPrimaryKey
	@Id int
AS

DELETE FROM [dbo].[Provider]
 WHERE 
	[Id] = @Id

GO

if exists (select * from dbo.sysobjects where id = object_id(N'Role_Insert') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Role_Insert
GO
CREATE PROCEDURE Role_Insert
	@CreatedAt datetime = null ,
	@CreatedBy int ,
	@Description nvarchar(255) ,
	@Id int ,
	@Name nvarchar(255) ,
	@UpdatedAt datetime = null ,
	@UpdatedBy int 

AS

INSERT [dbo].[Role]
(
	[CreatedAt],
	[CreatedBy],
	[Description],
	[Id],
	[Name],
	[UpdatedAt],
	[UpdatedBy]

)
VALUES
(
	@CreatedAt,
	@CreatedBy,
	@Description,
	@Id,
	@Name,
	@UpdatedAt,
	@UpdatedBy

)


GO

if exists (select * from dbo.sysobjects where id = object_id(N'Role_Update') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Role_Update
GO
CREATE PROCEDURE Role_Update
	@CreatedAt datetime = null,
	@CreatedBy int,
	@Description nvarchar(255),
	@Id int,
	@Name nvarchar(255),
	@UpdatedAt datetime = null,
	@UpdatedBy int

AS

UPDATE [dbo].[Role]
SET
	[CreatedAt] = @CreatedAt,
	[CreatedBy] = @CreatedBy,
	[Description] = @Description,
	[Id] = @Id,
	[Name] = @Name,
	[UpdatedAt] = @UpdatedAt,
	[UpdatedBy] = @UpdatedBy
 WHERE 
	[Id] = @Id

GO

if exists (select * from dbo.sysobjects where id = object_id(N'Role_SelectAll') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Role_SelectAll
GO
CREATE PROCEDURE Role_SelectAll
AS

	SELECT 
		[CreatedAt], [CreatedBy], [Description], [Id], [Name], [UpdatedAt], [UpdatedBy]
	FROM [dbo].[Role]

GO

if exists (select * from dbo.sysobjects where id = object_id(N'Role_DeleteByPrimaryKey') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure Role_DeleteByPrimaryKey
GO
CREATE PROCEDURE Role_DeleteByPrimaryKey
	@Id int
AS

DELETE FROM [dbo].[Role]
 WHERE 
	[Id] = @Id

GO

if exists (select * from dbo.sysobjects where id = object_id(N'RolePermission_Insert') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure RolePermission_Insert
GO
CREATE PROCEDURE RolePermission_Insert
	@IdPermission int ,
	@IdRole int 

AS

INSERT [dbo].[RolePermission]
(
	[IdPermission],
	[IdRole]

)
VALUES
(
	@IdPermission,
	@IdRole

)


GO

if exists (select * from dbo.sysobjects where id = object_id(N'RolePermission_Update') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure RolePermission_Update
GO
CREATE PROCEDURE RolePermission_Update
	@IdPermission int,
	@IdRole int

AS

UPDATE [dbo].[RolePermission]
SET
	[IdPermission] = @IdPermission,
	[IdRole] = @IdRole
 WHERE 
	[IdPermission] = @IdPermission AND 
	[IdRole] = @IdRole

GO

if exists (select * from dbo.sysobjects where id = object_id(N'RolePermission_SelectAll') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure RolePermission_SelectAll
GO
CREATE PROCEDURE RolePermission_SelectAll
AS

	SELECT 
		[IdPermission], [IdRole]
	FROM [dbo].[RolePermission]

GO

if exists (select * from dbo.sysobjects where id = object_id(N'RolePermission_DeleteByPrimaryKey') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure RolePermission_DeleteByPrimaryKey
GO
CREATE PROCEDURE RolePermission_DeleteByPrimaryKey
	@IdPermission int,
	@IdRole int
AS

DELETE FROM [dbo].[RolePermission]
 WHERE 
	[IdPermission] = @IdPermission AND 
	[IdRole] = @IdRole

GO

if exists (select * from dbo.sysobjects where id = object_id(N'UserInfo_Insert') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure UserInfo_Insert
GO
CREATE PROCEDURE UserInfo_Insert
	@Address nvarchar(255) = null ,
	@CreatedAt datetime = null ,
	@CreatedBy int ,
	@Email varchar(255) = null ,
	@IdUserLogin int ,
	@ImageId nvarchar(255) = null ,
	@Name nvarchar(255) ,
	@Phone varchar(255) = null ,
	@UpdatedAt datetime = null ,
	@UpdatedBy int 

AS

INSERT [dbo].[UserInfo]
(
	[Address],
	[CreatedAt],
	[CreatedBy],
	[Email],
	[IdUserLogin],
	[ImageId],
	[Name],
	[Phone],
	[UpdatedAt],
	[UpdatedBy]

)
VALUES
(
	@Address,
	@CreatedAt,
	@CreatedBy,
	@Email,
	@IdUserLogin,
	@ImageId,
	@Name,
	@Phone,
	@UpdatedAt,
	@UpdatedBy

)


GO

if exists (select * from dbo.sysobjects where id = object_id(N'UserInfo_Update') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure UserInfo_Update
GO
CREATE PROCEDURE UserInfo_Update
	@Address nvarchar(255) = null,
	@CreatedAt datetime = null,
	@CreatedBy int,
	@Email varchar(255) = null,
	@IdUserLogin int,
	@ImageId nvarchar(255) = null,
	@Name nvarchar(255),
	@Phone varchar(255) = null,
	@UpdatedAt datetime = null,
	@UpdatedBy int

AS

UPDATE [dbo].[UserInfo]
SET
	[Address] = @Address,
	[CreatedAt] = @CreatedAt,
	[CreatedBy] = @CreatedBy,
	[Email] = @Email,
	[IdUserLogin] = @IdUserLogin,
	[ImageId] = @ImageId,
	[Name] = @Name,
	[Phone] = @Phone,
	[UpdatedAt] = @UpdatedAt,
	[UpdatedBy] = @UpdatedBy
 WHERE 
	[IdUserLogin] = @IdUserLogin

GO

if exists (select * from dbo.sysobjects where id = object_id(N'UserInfo_SelectAll') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure UserInfo_SelectAll
GO
CREATE PROCEDURE UserInfo_SelectAll
AS

	SELECT 
		[Address], [CreatedAt], [CreatedBy], [Email], [IdUserLogin], [ImageId], [Name], [Phone], [UpdatedAt], [UpdatedBy]
	FROM [dbo].[UserInfo]

GO

if exists (select * from dbo.sysobjects where id = object_id(N'UserInfo_DeleteByPrimaryKey') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure UserInfo_DeleteByPrimaryKey
GO
CREATE PROCEDURE UserInfo_DeleteByPrimaryKey
	@IdUserLogin int
AS

DELETE FROM [dbo].[UserInfo]
 WHERE 
	[IdUserLogin] = @IdUserLogin

GO

if exists (select * from dbo.sysobjects where id = object_id(N'UserLogin_Insert') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure UserLogin_Insert
GO
CREATE PROCEDURE UserLogin_Insert
	@Id int ,
	@Password varchar(255) ,
	@Username varchar(255) ,
	@UserStatus int 

AS

INSERT [dbo].[UserLogin]
(
	[Id],
	[Password],
	[Username],
	[UserStatus]

)
VALUES
(
	@Id,
	@Password,
	@Username,
	@UserStatus

)


GO

if exists (select * from dbo.sysobjects where id = object_id(N'UserLogin_Update') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure UserLogin_Update
GO
CREATE PROCEDURE UserLogin_Update
	@Id int,
	@Password varchar(255),
	@Username varchar(255),
	@UserStatus int

AS

UPDATE [dbo].[UserLogin]
SET
	[Id] = @Id,
	[Password] = @Password,
	[Username] = @Username,
	[UserStatus] = @UserStatus
 WHERE 
	[Id] = @Id

GO

if exists (select * from dbo.sysobjects where id = object_id(N'UserLogin_SelectAll') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure UserLogin_SelectAll
GO
CREATE PROCEDURE UserLogin_SelectAll
AS

	SELECT 
		[Id], [Password], [Username], [UserStatus]
	FROM [dbo].[UserLogin]

GO

if exists (select * from dbo.sysobjects where id = object_id(N'UserLogin_DeleteByPrimaryKey') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure UserLogin_DeleteByPrimaryKey
GO
CREATE PROCEDURE UserLogin_DeleteByPrimaryKey
	@Id int
AS

DELETE FROM [dbo].[UserLogin]
 WHERE 
	[Id] = @Id

GO

if exists (select * from dbo.sysobjects where id = object_id(N'UserRole_Insert') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure UserRole_Insert
GO
CREATE PROCEDURE UserRole_Insert
	@IdRole int ,
	@IdUserLogin int 

AS

INSERT [dbo].[UserRole]
(
	[IdRole],
	[IdUserLogin]

)
VALUES
(
	@IdRole,
	@IdUserLogin

)


GO

if exists (select * from dbo.sysobjects where id = object_id(N'UserRole_Update') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure UserRole_Update
GO
CREATE PROCEDURE UserRole_Update
	@IdRole int,
	@IdUserLogin int

AS

UPDATE [dbo].[UserRole]
SET
	[IdRole] = @IdRole,
	[IdUserLogin] = @IdUserLogin
 WHERE 
	[IdRole] = @IdRole AND 
	[IdUserLogin] = @IdUserLogin

GO

if exists (select * from dbo.sysobjects where id = object_id(N'UserRole_SelectAll') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure UserRole_SelectAll
GO
CREATE PROCEDURE UserRole_SelectAll
AS

	SELECT 
		[IdRole], [IdUserLogin]
	FROM [dbo].[UserRole]

GO

if exists (select * from dbo.sysobjects where id = object_id(N'UserRole_DeleteByPrimaryKey') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure UserRole_DeleteByPrimaryKey
GO
CREATE PROCEDURE UserRole_DeleteByPrimaryKey
	@IdRole int,
	@IdUserLogin int
AS

DELETE FROM [dbo].[UserRole]
 WHERE 
	[IdRole] = @IdRole AND 
	[IdUserLogin] = @IdUserLogin

GO

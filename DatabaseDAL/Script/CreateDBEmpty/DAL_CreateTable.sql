USE AptechProjectS4
GO




IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Customer') AND type in (N'U')) DROP TABLE [dbo].[Customer]
GO

BEGIN
CREATE TABLE [dbo].[Customer](

	[Address] [nvarchar](255)  NULL  ,
	[CreatedAt] [datetime] NULL  ,
	[CreatedBy] [int] NOT NULL  ,
	[Id] [int] NOT NULL  ,
	[Name] [nvarchar](255)  NOT NULL  ,
	[Phone] [varchar](255)  NULL  ,
	[Email] [varchar](255)  NULL  ,
	[UpdatedAt] [datetime] NULL  ,
	[UpdatedBy] [int] NOT NULL  
CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Image') AND type in (N'U')) DROP TABLE [dbo].[Image]
GO

BEGIN
CREATE TABLE [dbo].[Image](

	[CreatedAt] [datetime] NULL  ,
	[CreatedBy] [int] NOT NULL  ,
	[Id] [int] NOT NULL  ,
	[ImageUrl] [varchar](255)  NOT NULL  ,
	[UpdatedAt] [datetime] NULL  ,
	[UpdatedBy] [int] NOT NULL  
CONSTRAINT [PK_Image] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ImportReceipt') AND type in (N'U')) DROP TABLE [dbo].[ImportReceipt]
GO

BEGIN
CREATE TABLE [dbo].[ImportReceipt](

	[CreatedAt] [datetime] NULL  ,
	[CreatedBy] [int] NOT NULL  ,
	[Date] [datetime] NOT NULL  ,
	[Id] [int] NOT NULL  ,
	[IdEmployee] [int] NOT NULL  ,
	[IdProvider] [int] NOT NULL  ,
	[ListProductId] [varchar](255)  NULL  ,
	[TotalPrice] [decimal](19,4) NOT NULL  ,
	[UpdatedAt] [datetime] NULL  ,
	[UpdatedBy] [int] NOT NULL  
CONSTRAINT [PK_ImportReceipt] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Manufacturer') AND type in (N'U')) DROP TABLE [dbo].[Manufacturer]
GO

BEGIN
CREATE TABLE [dbo].[Manufacturer](

	[CreatedAt] [datetime] NULL  ,
	[CreatedBy] [int] NOT NULL  ,
	[Id] [int] NOT NULL  ,
	[ImageId] [nvarchar](255)  NULL  ,
	[Name] [nvarchar](255)  NOT NULL  ,
	[UpdatedAt] [datetime] NULL  ,
	[UpdatedBy] [int] NOT NULL  
CONSTRAINT [PK_Manufacturer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'OrderDetail') AND type in (N'U')) DROP TABLE [dbo].[OrderDetail]
GO

BEGIN
CREATE TABLE [dbo].[OrderDetail](

	[Address] [nvarchar](255)  NULL  ,
	[CreatedAt] [datetime] NULL  ,
	[CreatedBy] [int] NOT NULL  ,
	[Date] [datetime] NOT NULL  ,
	[Id] [int] NOT NULL  ,
	[IdCustomer] [int] NULL  ,
	[IdEmployee] [int] NOT NULL  ,
	[ListProductId] [varchar](255)  NULL  ,
	[Name] [varchar](255)  NULL  ,
	[OrderStatus] [varchar](255)  NULL  ,
	[Phone] [varchar](255)  NULL  ,
	[TotalPrice] [decimal](19,4) NOT NULL  ,
	[UpdatedAt] [datetime] NULL  ,
	[UpdatedBy] [int] NOT NULL  
CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Permission') AND type in (N'U')) DROP TABLE [dbo].[Permission]
GO

BEGIN
CREATE TABLE [dbo].[Permission](

	[CreatedAt] [datetime] NULL  ,
	[CreatedBy] [int] NOT NULL  ,
	[Description] [nvarchar](255)  NOT NULL  ,
	[Id] [int] NOT NULL  ,
	[Name] [nvarchar](255)  NOT NULL  ,
	[UpdatedAt] [datetime] NULL  ,
	[UpdatedBy] [int] NOT NULL  
CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Post') AND type in (N'U')) DROP TABLE [dbo].[Post]
GO

BEGIN
CREATE TABLE [dbo].[Post](

	[Content] [text] NULL  ,
	[CreatedAt] [datetime] NULL  ,
	[CreatedBy] [int] NOT NULL  ,
	[Id] [int] NOT NULL  ,
	[Tittle] [varchar](255)  NOT NULL  ,
	[UpdatedAt] [datetime] NULL  ,
	[UpdatedBy] [int] NOT NULL  
CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Product') AND type in (N'U')) DROP TABLE [dbo].[Product]
GO

BEGIN
CREATE TABLE [dbo].[Product](

	[CreatedAt] [datetime] NULL  ,
	[CreatedBy] [int] NOT NULL  ,
	[Description] [nvarchar](255)  NULL  ,
	[Id] [int] NOT NULL  ,
	[IdDisplay] [varchar](50)  NOT NULL  ,
	[IdManufacturer] [int] NOT NULL  ,
	[IdProductType] [int] NOT NULL  ,
	[ImageId] [nvarchar](255)  NULL  ,
	[Name] [nvarchar](255)  NOT NULL  ,
	[Quantity] [int] NULL  ,
	[SupportDuration] [int] NULL  ,
	[UnitPrice] [decimal](19,4) NULL  ,
	[UpdatedAt] [datetime] NULL  ,
	[UpdatedBy] [int] NOT NULL  
CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ProductType') AND type in (N'U')) DROP TABLE [dbo].[ProductType]
GO

BEGIN
CREATE TABLE [dbo].[ProductType](

	[CreatedAt] [datetime] NULL  ,
	[CreatedBy] [int] NOT NULL  ,
	[Id] [int] NOT NULL  ,
	[ImageId] [nvarchar](255)  NULL  ,
	[Name] [nvarchar](255)  NOT NULL  ,
	[UpdatedAt] [datetime] NULL  ,
	[UpdatedBy] [int] NOT NULL  
CONSTRAINT [PK_ProductType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Property') AND type in (N'U')) DROP TABLE [dbo].[Property]
GO

BEGIN
CREATE TABLE [dbo].[Property](

	[CreatedAt] [datetime] NULL  ,
	[CreatedBy] [int] NOT NULL  ,
	[Data] [nvarchar](255)  NOT NULL  ,
	[Id] [int] NOT NULL  ,
	[IdProduct] [int] NOT NULL  ,
	[ImageId] [nvarchar](255)  NULL  ,
	[Name] [nvarchar](255)  NOT NULL  ,
	[UpdatedAt] [datetime] NULL  ,
	[UpdatedBy] [int] NOT NULL  
CONSTRAINT [PK_Property] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Provider') AND type in (N'U')) DROP TABLE [dbo].[Provider]
GO

BEGIN
CREATE TABLE [dbo].[Provider](

	[Address] [nvarchar](255)  NULL  ,
	[CreatedAt] [datetime] NULL  ,
	[CreatedBy] [int] NOT NULL  ,
	[Email] [varchar](255)  NULL  ,
	[Id] [int] NOT NULL  ,
	[ImageId] [nvarchar](255)  NULL  ,
	[Name] [nvarchar](255)  NOT NULL  ,
	[Phone] [varchar](255)  NULL  ,
	[UpdatedAt] [datetime] NULL  ,
	[UpdatedBy] [int] NOT NULL  
CONSTRAINT [PK_Provider] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Role') AND type in (N'U')) DROP TABLE [dbo].[Role]
GO

BEGIN
CREATE TABLE [dbo].[Role](

	[CreatedAt] [datetime] NULL  ,
	[CreatedBy] [int] NOT NULL  ,
	[Description] [nvarchar](255)  NOT NULL  ,
	[Id] [int] NOT NULL  ,
	[Name] [nvarchar](255)  NOT NULL  ,
	[UpdatedAt] [datetime] NULL  ,
	[UpdatedBy] [int] NOT NULL  
CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'RolePermission') AND type in (N'U')) DROP TABLE [dbo].[RolePermission]
GO

BEGIN
CREATE TABLE [dbo].[RolePermission](

	[IdPermission] [int] NOT NULL  ,
	[IdRole] [int] NOT NULL  
CONSTRAINT [PK_RolePermission] PRIMARY KEY CLUSTERED 
(
	[IdPermission] ASC,
	[IdRole] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'UserInfo') AND type in (N'U')) DROP TABLE [dbo].[UserInfo]
GO

BEGIN
CREATE TABLE [dbo].[UserInfo](

	[Address] [nvarchar](255)  NULL  ,
	[CreatedAt] [datetime] NULL  ,
	[CreatedBy] [int] NOT NULL  ,
	[Email] [varchar](255)  NULL  ,
	[IdUserLogin] [int] NOT NULL  ,
	[ImageId] [nvarchar](255)  NULL  ,
	[Name] [nvarchar](255)  NOT NULL  ,
	[Phone] [varchar](255)  NULL  ,
	[UpdatedAt] [datetime] NULL  ,
	[UpdatedBy] [int] NOT NULL  
CONSTRAINT [PK_UserInfo] PRIMARY KEY CLUSTERED 
(
	[IdUserLogin] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'UserLogin') AND type in (N'U')) DROP TABLE [dbo].[UserLogin]
GO

BEGIN
CREATE TABLE [dbo].[UserLogin](

	[Id] [int] NOT NULL  ,
	[Password] [varchar](255)  NOT NULL  ,
	[UserStatus] [int] NOT NULL  ,
	[Username] [varchar](255)  NOT NULL  
CONSTRAINT [PK_UserLogin] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'UserRole') AND type in (N'U')) DROP TABLE [dbo].[UserRole]
GO

BEGIN
CREATE TABLE [dbo].[UserRole](

	[IdRole] [int] NOT NULL  ,
	[IdUserLogin] [int] NOT NULL  
CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[IdRole] ASC,
	[IdUserLogin] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO


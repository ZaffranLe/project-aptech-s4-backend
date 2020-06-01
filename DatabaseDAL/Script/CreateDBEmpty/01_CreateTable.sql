USE DatabaseWorkingNVC
GO




IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Account') AND type in (N'U')) DROP TABLE [dbo].[Account]
GO

BEGIN
CREATE TABLE [dbo].[Account](

	[AccountId] [varchar](50)  NOT NULL  ,
	[AccountLevel] [int] NULL  ,
	[AccountType] [int] NULL  ,
	[ActorChanged] [bigint] NOT NULL  ,
	[AffectAreaId] [int] NULL  ,
	[AffectBranchId] [bigint] NULL  ,
	[AffectBrandId] [int] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[Amount] [decimal](28,6) NULL  ,
	[Currency] [varchar](10)  NULL  ,
	[DisplayAccount] [varchar](30)  NULL  ,
	[Hold] [decimal](28,6) NULL  ,
	[IsPendingChanged] [bit] NULL  ,
	[MemberId] [bigint] NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'AccountInfo') AND type in (N'U')) DROP TABLE [dbo].[AccountInfo]
GO

BEGIN
CREATE TABLE [dbo].[AccountInfo](

	[AccountId] [varchar](50)  NOT NULL  ,
	[ActorChanged] [bigint] NOT NULL  ,
	[AffectAreaId] [int] NULL  ,
	[AffectBranchId] [bigint] NULL  ,
	[AffectBrandId] [int] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[AmountHold] [decimal](28,6) NULL  ,
	[Currency] [varchar](10)  NULL  ,
	[IM] [decimal](28,6) NULL  ,
	[IsPendingChanged] [bit] NULL  ,
	[MarginDeficitInterestRate] [decimal](28,6) NULL  ,
	[MarginSurplusInterestRate] [decimal](28,6) NULL  ,
	[MemberId] [bigint] NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[UnPnL] [decimal](28,6) NULL  
CONSTRAINT [PK_AccountInfo] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'AccountTransaction') AND type in (N'U')) DROP TABLE [dbo].[AccountTransaction]
GO

BEGIN
CREATE TABLE [dbo].[AccountTransaction](

	[Actor] [bigint] NULL  ,
	[ActorChanged] [bigint] NOT NULL  ,
	[AffectAreaId] [int] NULL  ,
	[AffectBrandId] [int] NULL  ,
	[AffectMemberActor] [bigint] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[Amount] [decimal](28,6) NULL  ,
	[AmountCR] [decimal](28,6) NULL  ,
	[AmountDR] [decimal](28,6) NULL  ,
	[AmountOrginal] [decimal](28,6) NULL  ,
	[BrokerId] [int] NULL  ,
	[BrokerOrderId] [varchar](50)  NULL  ,
	[CR] [varchar](50)  NULL  ,
	[Currency] [varchar](10)  NULL  ,
	[CurrencyOrginal] [varchar](10)  NULL  ,
	[DR] [varchar](50)  NULL  ,
	[DnOrverDraft] [decimal](28,6) NULL  ,
	[HoldCR] [decimal](28,6) NULL  ,
	[HoldDR] [decimal](28,6) NULL  ,
	[HoldOverDraft] [decimal](28,6) NULL  ,
	[MemberIdCR] [bigint] NULL  ,
	[MemberIdDR] [bigint] NULL  ,
	[Note] [nvarchar](250)  NULL  ,
	[OrderTransactionBuyId] [bigint] NULL  ,
	[OrderTransactionSellId] [bigint] NULL  ,
	[OverDraftLimit] [decimal](28,6) NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[TransTime] [datetime] NULL  ,
	[TransTypeId] [bigint] NULL  ,
	[TransactionId] [bigint] NOT NULL  
CONSTRAINT [PK_AccountTransaction] PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'MemberAccountMapping') AND type in (N'U')) DROP TABLE [dbo].[MemberAccountMapping]
GO

BEGIN
CREATE TABLE [dbo].[MemberAccountMapping](
	[MemberId] [bigint] NOT NULL  ,
	[MemberParent] [bigint] NOT NULL ,
	[AccountName] [nvarchar](100) NULL ,
	[TimeChanged] [datetime] NOT NULL ,
 CONSTRAINT [PK_MemberAccountMapping] PRIMARY KEY CLUSTERED 
(
	[MemberId] ASC 
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'MarginConfig') AND type in (N'U')) DROP TABLE [dbo].[MarginConfig]
GO

BEGIN
CREATE TABLE [dbo].[MarginConfig](

	[ActorChanged] [bigint] NOT NULL  ,
	[MemberId] [bigint] NOT NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[Value] [bit] NULL  
CONSTRAINT [PK_MarginConfig] PRIMARY KEY CLUSTERED 
(
	[MemberId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'AccountTransactionTemp') AND type in (N'U')) DROP TABLE [dbo].[AccountTransactionTemp]
GO

BEGIN
CREATE TABLE [dbo].[AccountTransactionTemp](

	[AccountTransactionTempId] [varchar](50)  NOT NULL  ,
	[Actor] [bigint] NULL  ,
	[ActorChanged] [bigint] NOT NULL  ,
	[AffectAreaId] [int] NULL  ,
	[AffectBranchId] [bigint] NULL  ,
	[AffectBrandId] [int] NULL  ,
	[AffectMemberActor] [bigint] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[Amount] [decimal](28,6) NULL  ,
	[AmountOrginal] [decimal](28,6) NULL  ,
	[BrokerId] [int] NULL  ,
	[CR] [varchar](50)  NULL  ,
	[Currency] [varchar](10)  NULL  ,
	[CurrencyOrginal] [varchar](10)  NULL  ,
	[DR] [varchar](50)  NULL  ,
	[MemberIdCR] [bigint] NULL  ,
	[MemberIdDR] [bigint] NULL  ,
	[Note] [nvarchar](250)  NULL  ,
	[OrderTransactionBuyId] [bigint] NULL  ,
	[OrderTransactionSellId] [bigint] NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[TransTime] [datetime] NULL  ,
	[TransTypeId] [bigint] NULL  
CONSTRAINT [PK_AccountTransactionTemp] PRIMARY KEY CLUSTERED 
(
	[AccountTransactionTempId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'AccountTransactionView') AND type in (N'U')) DROP TABLE [dbo].[AccountTransactionView]
GO

BEGIN
CREATE TABLE [dbo].[AccountTransactionView](

	[Actor] [bigint] NULL  ,
	[ActorChanged] [bigint] NOT NULL  ,
	[AffectAreaId] [int] NULL  ,
	[AffectBrandId] [int] NULL  ,
	[AffectMemberActor] [bigint] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[Amount] [decimal](28,6) NULL  ,
	[AmountCR] [decimal](28,6) NULL  ,
	[AmountDR] [decimal](28,6) NULL  ,
	[AmountOrginal] [decimal](28,6) NULL  ,
	[BrokerId] [int] NULL  ,
	[BrokerOrderId] [varchar](50)  NULL  ,
	[CR] [varchar](50)  NULL  ,
	[Currency] [varchar](10)  NULL  ,
	[CurrencyOrginal] [varchar](10)  NULL  ,
	[DR] [varchar](50)  NULL  ,
	[DnOrverDraft] [decimal](28,6) NULL  ,
	[HoldCR] [decimal](28,6) NULL  ,
	[HoldDR] [decimal](28,6) NULL  ,
	[HoldOverDraft] [decimal](28,6) NULL  ,
	[MemberIdCR] [bigint] NULL  ,
	[MemberIdDR] [bigint] NULL  ,
	[Note] [nvarchar](250)  NULL  ,
	[OrderTransactionBuyId] [bigint] NULL  ,
	[OrderTransactionSellId] [bigint] NULL  ,
	[OverDraftLimit] [decimal](28,6) NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[TransTime] [datetime] NULL  ,
	[TransTypeId] [bigint] NULL  ,
	[TransactionViewId] [bigint] NOT NULL  
CONSTRAINT [PK_AccountTransactionView] PRIMARY KEY CLUSTERED 
(
	[TransactionViewId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'AlertEmail') AND type in (N'U')) DROP TABLE [dbo].[AlertEmail]
GO

BEGIN
CREATE TABLE [dbo].[AlertEmail](

	[ActorChanged] [bigint] NOT NULL  ,
	[Address] [nvarchar](MAX)  NOT NULL  ,
	[AlertKey] [ntext] NULL  ,
	[AttachData] [varbinary](MAX)  NULL  ,
	[AttachName] [nvarchar](250)  NULL  ,
	[Content] [ntext] NOT NULL  ,
	[ContentType] [nvarchar](50)  NOT NULL  ,
	[CountError] [int] NOT NULL  ,
	[EmailStatus] [int] NULL  ,
	[Id] [bigint] NOT NULL IDENTITY(1, 1) ,
	[IsSuccess] [bit] NULL  ,
	[IsWorking] [bit] NOT NULL  ,
	[ObjectAlert] [nvarchar](250)  NULL  ,
	[ParentSessionId] [bigint] NULL  ,
	[Priority] [int] NOT NULL  ,
	[ReceiverID] [nvarchar](50)  NOT NULL  ,
	[Sender] [nvarchar](50)  NOT NULL  ,
	[SendingTime] [datetime] NULL  ,
	[Subject] [ntext] NOT NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_AlertEmail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'AlertSms') AND type in (N'U')) DROP TABLE [dbo].[AlertSms]
GO

BEGIN
CREATE TABLE [dbo].[AlertSms](

	[ActorChanged] [bigint] NOT NULL  ,
	[AlertKey] [ntext] NULL  ,
	[Content] [ntext] NULL  ,
	[ContentType] [nvarchar](50)  NOT NULL  ,
	[CountError] [int] NOT NULL  ,
	[Id] [bigint] NOT NULL IDENTITY(1, 1) ,
	[IsSuccess] [bit] NULL  ,
	[IsWorking] [bit] NOT NULL  ,
	[ParentSessionId] [bigint] NULL  ,
	[Phone] [varchar](50)  NOT NULL  ,
	[Priority] [int] NOT NULL  ,
	[ReceiverID] [nvarchar](50)  NOT NULL  ,
	[Sender] [nvarchar](50)  NOT NULL  ,
	[SendingTime] [datetime] NOT NULL  ,
	[SmsStatus] [int] NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_AlertSms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ApprovalAccount') AND type in (N'U')) DROP TABLE [dbo].[ApprovalAccount]
GO

BEGIN
CREATE TABLE [dbo].[ApprovalAccount](

	[ActorChanged] [bigint] NOT NULL  ,
	[AffectAreaId] [int] NULL  ,
	[AffectBrandId] [int] NULL  ,
	[AffectMemberActor] [bigint] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[ApprovalAccountId] [bigint] NOT NULL  ,
	[ApprovalBy] [bigint] NULL  ,
	[ApprovalDate] [datetime] NULL  ,
	[ApprovalType] [int] NULL  ,
	[CreateBy] [bigint] NULL  ,
	[CreateDate] [datetime] NULL  ,
	[CurrentValue] [ntext] NULL  ,
	[NewValue] [ntext] NULL  ,
	[Note] [ntext] NULL  ,
	[ObjectId] [varchar](50)  NULL  ,
	[ObjectName] [varchar](50)  NULL  ,
	[Reason] [ntext] NULL  ,
	[Status] [int] NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_ApprovalAccount] PRIMARY KEY CLUSTERED 
(
	[ApprovalAccountId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ApprovalDealing') AND type in (N'U')) DROP TABLE [dbo].[ApprovalDealing]
GO

BEGIN
CREATE TABLE [dbo].[ApprovalDealing](

	[ActorChanged] [bigint] NOT NULL  ,
	[AffectAreaId] [int] NULL  ,
	[AffectBrandId] [int] NULL  ,
	[AffectMemberActor] [bigint] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[ApprovalBy] [bigint] NULL  ,
	[ApprovalDate] [datetime] NULL  ,
	[ApprovalDealingId] [bigint] NOT NULL  ,
	[ApprovalType] [int] NULL  ,
	[CreateBy] [bigint] NULL  ,
	[CreateDate] [datetime] NULL  ,
	[CurrentValue] [ntext] NULL  ,
	[NewValue] [ntext] NULL  ,
	[Note] [nvarchar](500)  NULL  ,
	[ObjectId] [nvarchar](50)  NULL  ,
	[ObjectName] [nvarchar](50)  NULL  ,
	[Reason] [nvarchar](500)  NULL  ,
	[Status] [int] NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_ApprovalDealing] PRIMARY KEY CLUSTERED 
(
	[ApprovalDealingId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ApprovalMember') AND type in (N'U')) DROP TABLE [dbo].[ApprovalMember]
GO

BEGIN
CREATE TABLE [dbo].[ApprovalMember](

	[ActorChanged] [bigint] NOT NULL  ,
	[AffectAreaId] [int] NULL  ,
	[AffectBrandId] [int] NULL  ,
	[AffectMemberActor] [bigint] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[ApprovalBy] [bigint] NULL  ,
	[ApprovalDate] [datetime] NULL  ,
	[ApprovalMemberId] [bigint] NOT NULL  ,
	[ApprovalType] [int] NULL  ,
	[CreateBy] [bigint] NULL  ,
	[CreateDate] [datetime] NULL  ,
	[CurrentValue] [ntext] NULL  ,
	[NewValue] [ntext] NULL  ,
	[Note] [nvarchar](500)  NULL  ,
	[ObjectId] [nvarchar](50)  NULL  ,
	[ObjectName] [nvarchar](50)  NULL  ,
	[Reason] [nvarchar](500)  NULL  ,
	[Status] [int] NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_ApprovalMember] PRIMARY KEY CLUSTERED 
(
	[ApprovalMemberId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ApprovalOrder') AND type in (N'U')) DROP TABLE [dbo].[ApprovalOrder]
GO

BEGIN
CREATE TABLE [dbo].[ApprovalOrder](

	[ActorChanged] [bigint] NOT NULL  ,
	[AffectAreaId] [int] NULL  ,
	[AffectBrandId] [int] NULL  ,
	[AffectMemberActor] [bigint] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[ApprovalBy] [bigint] NULL  ,
	[ApprovalDate] [datetime] NULL  ,
	[ApprovalOrderId] [bigint] NOT NULL  ,
	[ApprovalType] [int] NULL  ,
	[CreateBy] [bigint] NULL  ,
	[CreateDate] [datetime] NULL  ,
	[CurrentValue] [ntext] NULL  ,
	[IsReject] [bit] NULL  ,
	[NewValue] [ntext] NULL  ,
	[Note] [nvarchar](500)  NULL  ,
	[ObjectId] [nvarchar](50)  NULL  ,
	[ObjectName] [nvarchar](50)  NULL  ,
	[Reason] [nvarchar](500)  NULL  ,
	[Status] [int] NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_ApprovalOrder] PRIMARY KEY CLUSTERED 
(
	[ApprovalOrderId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ApprovalPreRisk') AND type in (N'U')) DROP TABLE [dbo].[ApprovalPreRisk]
GO

BEGIN
CREATE TABLE [dbo].[ApprovalPreRisk](

	[ActorChanged] [bigint] NOT NULL  ,
	[AffectAreaId] [int] NULL  ,
	[AffectBrandId] [int] NULL  ,
	[AffectMemberActor] [bigint] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[ApprovalBy] [bigint] NULL  ,
	[ApprovalDate] [datetime] NULL  ,
	[ApprovalPreRiskId] [bigint] NOT NULL  ,
	[ApprovalType] [int] NULL  ,
	[CreateBy] [bigint] NULL  ,
	[CreateDate] [datetime] NULL  ,
	[CurrentValue] [ntext] NULL  ,
	[NewValue] [ntext] NULL  ,
	[Note] [nvarchar](500)  NULL  ,
	[ObjectId] [nvarchar](50)  NULL  ,
	[ObjectName] [nvarchar](50)  NULL  ,
	[Reason] [nvarchar](500)  NULL  ,
	[Status] [int] NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_ApprovalPreRisk] PRIMARY KEY CLUSTERED 
(
	[ApprovalPreRiskId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ApprovalSystem') AND type in (N'U')) DROP TABLE [dbo].[ApprovalSystem]
GO

BEGIN
CREATE TABLE [dbo].[ApprovalSystem](

	[ActorChanged] [bigint] NOT NULL  ,
	[AffectAreaId] [int] NULL  ,
	[AffectBrandId] [int] NULL  ,
	[AffectMemberActor] [bigint] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[ApprovalBy] [bigint] NULL  ,
	[ApprovalDate] [datetime] NULL  ,
	[ApprovalSystemId] [bigint] NOT NULL  ,
	[ApprovalType] [int] NULL  ,
	[CreateBy] [bigint] NULL  ,
	[CreateDate] [datetime] NULL  ,
	[CurrentValue] [ntext] NULL  ,
	[NewValue] [ntext] NULL  ,
	[Note] [nvarchar](500)  NULL  ,
	[ObjectId] [nvarchar](50)  NULL  ,
	[ObjectName] [nvarchar](50)  NULL  ,
	[Reason] [nvarchar](500)  NULL  ,
	[Status] [int] NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_ApprovalSystem] PRIMARY KEY CLUSTERED 
(
	[ApprovalSystemId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'BaseSymbol') AND type in (N'U')) DROP TABLE [dbo].[BaseSymbol]
GO

BEGIN
CREATE TABLE [dbo].[BaseSymbol](

	[ActorChanged] [bigint] NOT NULL  ,
	[AuditType] [int] NULL  ,
	[BaseSymbolId] [bigint] NOT NULL  ,
	[Currency] [nvarchar](50)  NULL  ,
	[ExchangeId] [int] NULL  ,
	[FirstNotifyDayAjust] [int] NULL  ,
	[FullName] [nvarchar](50)  NULL  ,
	[GroupProductId] [int] NULL  ,
	[InitialMargin] [decimal](28,6) NULL  ,
	[InitialMarginMXV] [decimal](28,6) NULL  ,
	[MaintainMargin] [decimal](28,6) NULL  ,
	[MarginRefSource] [int] NULL  ,
	[MarginType] [int] NULL  ,
	[MinPriceIncrement] [decimal](28,6) NULL  ,
	[MinUnitIncrement] [decimal](28,6) NULL  ,
	[NumbreDecimal] [int] NULL  ,
	[OrderBookingType] [int] NULL  ,
	[PriceMultiplier] [decimal](28,6) NULL  ,
	[ProductType] [int] NULL  ,
	[ShortName] [nvarchar](50)  NULL  ,
	[SpreadInitialMargin] [decimal](28,6) NULL  ,
	[SpreadMaintainMargin] [decimal](28,6) NULL  ,
	[SymbolStatus] [int] NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[TradePriceOfMeasure] [nvarchar](50)  NULL  ,
	[TradeUnitOfMeasure] [nvarchar](50)  NULL  ,
	[Unit] [nvarchar](50)  NULL  ,
	[UnitMultiplier] [decimal](28,6) NULL  
CONSTRAINT [PK_BaseSymbol] PRIMARY KEY CLUSTERED 
(
	[BaseSymbolId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'BaseSymbolCQG') AND type in (N'U')) DROP TABLE [dbo].[BaseSymbolCQG]
GO

BEGIN
CREATE TABLE [dbo].[BaseSymbolCQG](

	[Actor] [bigint] NOT NULL  ,
	[ActorChanged] [bigint] NOT NULL  ,
	[BaseSymbolCQGId] [bigint] NOT NULL  ,
	[ContractUnit] [nvarchar](50)  NULL  ,
	[CountryCode] [varchar](50)  NULL  ,
	[CreatDate] [datetime] NOT NULL  ,
	[Currency] [nvarchar](50)  NULL  ,
	[ExchangeId] [int] NULL  ,
	[FullName] [nvarchar](200)  NULL  ,
	[GroupProductId] [int] NULL  ,
	[InitialMargin] [decimal](28,6) NULL  ,
	[InitialMarginMXV] [decimal](28,6) NULL  ,
	[IsPendingChange] [bit] NULL  ,
	[IsPendingChangeMargin] [bit] NULL  ,
	[MaintainMargin] [decimal](28,6) NULL  ,
	[MinPriceIncrement] [decimal](28,6) NULL  ,
	[MinUnitIncrement] [decimal](28,6) NULL  ,
	[Note] [nvarchar](200)  NULL  ,
	[NumbreDecimal] [int] NULL  ,
	[PriceMultiplier] [decimal](28,6) NULL  ,
	[ProductType] [int] NULL  ,
	[PromptMonthInitialMargin] [decimal](28,6) NULL  ,
	[ShortName] [nvarchar](200)  NULL  ,
	[SpreadInitialMargin] [decimal](28,6) NULL  ,
	[SpreadMaintainMargin] [decimal](28,6) NULL  ,
	[SymbolCQGId] [nvarchar](50)  NOT NULL  ,
	[SymbolCQGName] [nvarchar](50)  NOT NULL  ,
	[SymbolCQGStatus] [int] NULL  ,
	[SymbolMonths] [nvarchar](50)  NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[UnitMultiplier] [decimal](28,6) NULL  
CONSTRAINT [PK_BaseSymbolCQG] PRIMARY KEY CLUSTERED 
(
	[BaseSymbolCQGId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'BaseSymbolMapping') AND type in (N'U')) DROP TABLE [dbo].[BaseSymbolMapping]
GO

BEGIN
CREATE TABLE [dbo].[BaseSymbolMapping](

	[ActorChanged] [bigint] NOT NULL  ,
	[BaseSymbolId] [bigint] NOT NULL  ,
	[BaseSymbolName] [nvarchar](50)  NULL  ,
	[BrokerId] [int] NOT NULL  ,
	[ExchangeName] [nvarchar](50)  NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_BaseSymbolMapping] PRIMARY KEY CLUSTERED 
(
	[BaseSymbolId] ASC,
	[BrokerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'BaseSymbolReportMapping') AND type in (N'U')) DROP TABLE [dbo].[BaseSymbolReportMapping]
GO

BEGIN
CREATE TABLE [dbo].[BaseSymbolReportMapping](

	[ActorChanged] [bigint] NOT NULL  ,
	[BaseSymbolPartner] [nvarchar](50)  NOT NULL  ,
	[BaseSymbolReportMappingId] [bigint] NOT NULL  ,
	[BaseSymbolVc] [nvarchar](50)  NOT NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_BaseSymbolReportMapping] PRIMARY KEY CLUSTERED 
(
	[BaseSymbolReportMappingId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'BrokerOrder') AND type in (N'U')) DROP TABLE [dbo].[BrokerOrder]
GO

BEGIN
CREATE TABLE [dbo].[BrokerOrder](

	[Actor] [bigint] NULL  ,
	[ActorChanged] [bigint] NOT NULL  ,
	[AffectAreaId] [int] NULL  ,
	[AffectBrandId] [int] NULL  ,
	[AffectMemberActor] [bigint] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[AvgPx] [decimal](28,6) NULL  ,
	[BaseSymbolId] [bigint] NULL  ,
	[BrokerId] [int] NULL  ,
	[BrokerOrderId] [varchar](50)  NOT NULL  ,
	[ClientOrderId] [varchar](50)  NULL  ,
	[CumQty] [decimal](28,6) NULL  ,
	[Currency] [varchar](50)  NULL  ,
	[DoneTime] [datetime] NULL  ,
	[EffectiveTime] [datetime] NULL  ,
	[ExchangeOrderId] [varchar](50)  NULL  ,
	[ExpireDate] [datetime] NULL  ,
	[ExpireTime] [datetime] NULL  ,
	[InitTime] [datetime] NULL  ,
	[IsBuy] [bit] NULL  ,
	[LeavesQty] [decimal](28,6) NULL  ,
	[MemberId] [bigint] NULL  ,
	[Note] [nvarchar](100)  NULL  ,
	[OrderLink] [varchar](50)  NULL  ,
	[OrderStatus] [int] NULL  ,
	[OrderType] [nvarchar](50)  NULL  ,
	[Price] [decimal](28,6) NULL  ,
	[Quantity] [decimal](28,6) NULL  ,
	[StopPx] [decimal](28,6) NULL  ,
	[SymbolId] [bigint] NULL  ,
	[SymbolName] [varchar](50)  NULL  ,
	[Text] [nvarchar](200)  NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[TimeInForce] [varchar](10)  NULL  ,
	[TradingDealType] [int] NULL  ,
	[TransactTime] [datetime] NULL  
CONSTRAINT [PK_BrokerOrder] PRIMARY KEY CLUSTERED 
(
	[BrokerOrderId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ChangedPasswordHist') AND type in (N'U')) DROP TABLE [dbo].[ChangedPasswordHist]
GO

BEGIN
CREATE TABLE [dbo].[ChangedPasswordHist](

	[ActorChanged] [bigint] NOT NULL  ,
	[Id] [bigint] NOT NULL IDENTITY(1, 1) ,
	[Password] [nvarchar](500)  NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[UserId] [bigint] NOT NULL  
CONSTRAINT [PK_ChangedPasswordHist] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ClientOrderMapping') AND type in (N'U')) DROP TABLE [dbo].[ClientOrderMapping]
GO

BEGIN
CREATE TABLE [dbo].[ClientOrderMapping](

	[Account] [varchar](100)  NULL  ,
	[ChainOrderId] [varchar](100)  NULL  ,
	[ClientOrderId] [varchar](50)  NOT NULL  ,
	[CustomerId] [bigint] NULL  ,
	[OriClientOrderId] [varchar](50)  NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_ClientOrderMapping] PRIMARY KEY CLUSTERED 
(
	[ClientOrderId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ConfigApprovalTransaction') AND type in (N'U')) DROP TABLE [dbo].[ConfigApprovalTransaction]
GO

BEGIN
CREATE TABLE [dbo].[ConfigApprovalTransaction](

	[ActorChanged] [bigint] NULL  ,
	[ConfigName] [nvarchar](50)  NOT NULL  ,
	[ConfigType] [int] NOT NULL  ,
	[IsAuto] [bit] NULL  ,
	[TimeApproval] [datetime] NULL  ,
	[TimeChanged] [datetime] NULL  
CONSTRAINT [PK_ConfigApprovalTransaction] PRIMARY KEY CLUSTERED 
(
	[ConfigType] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ContractNoticeDate') AND type in (N'U')) DROP TABLE [dbo].[ContractNoticeDate]
GO

BEGIN
CREATE TABLE [dbo].[ContractNoticeDate](

	[ActorChanged] [bigint] NOT NULL  ,
	[AffectSession] [bigint] NULL  ,
	[ContractNoticeType] [int] NOT NULL  ,
	[CurrentNotifyDay] [datetime] NOT NULL  ,
	[Note] [nvarchar](300)  NULL  ,
	[NoticeStatus] [int] NULL  ,
	[NotifyDay] [datetime] NOT NULL  ,
	[NotifyMsg] [nvarchar](300)  NULL  ,
	[SmsNotifyMsg] [nvarchar](300)  NULL  ,
	[SmsRequest] [bit] NULL  ,
	[SymbolId] [bigint] NOT NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[UserId] [bigint] NOT NULL  
CONSTRAINT [PK_ContractNoticeDate] PRIMARY KEY CLUSTERED 
(
	[ContractNoticeType] ASC,
	[CurrentNotifyDay] ASC,
	[SymbolId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ContractUnit') AND type in (N'U')) DROP TABLE [dbo].[ContractUnit]
GO

BEGIN
CREATE TABLE [dbo].[ContractUnit](

	[ActorChanged] [bigint] NOT NULL  ,
	[ContractUnitId] [int] NOT NULL  ,
	[Description] [nvarchar](200)  NULL  ,
	[FullName] [nvarchar](100)  NULL  ,
	[ShortName] [nvarchar](50)  NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_ContractUnit] PRIMARY KEY CLUSTERED 
(
	[ContractUnitId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'CurencyInfo') AND type in (N'U')) DROP TABLE [dbo].[CurencyInfo]
GO

BEGIN
CREATE TABLE [dbo].[CurencyInfo](

	[Actor] [bigint] NULL  ,
	[ActorChanged] [bigint] NOT NULL  ,
	[ApprovalBy] [bigint] NULL  ,
	[ApprovalDate] [datetime] NULL  ,
	[CreateDate] [datetime] NULL  ,
	[Curency] [nvarchar](50)  NOT NULL  ,
	[CurencyName] [nvarchar](100)  NOT NULL  ,
	[FormatNumber] [int] NULL  ,
	[IsBasic] [bit] NULL  ,
	[IsOrverDraft] [bit] NULL  ,
	[IsPendingChange] [bit] NULL  ,
	[Note] [ntext] NULL  ,
	[Priority] [int] NULL  ,
	[Status] [int] NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_CurencyInfo] PRIMARY KEY CLUSTERED 
(
	[Curency] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Exchange') AND type in (N'U')) DROP TABLE [dbo].[Exchange]
GO

BEGIN
CREATE TABLE [dbo].[Exchange](

	[ActorChanged] [bigint] NOT NULL  ,
	[Address] [nvarchar](200)  NULL  ,
	[ExchangeId] [int] NOT NULL  ,
	[ExchangeStatus] [int] NULL  ,
	[FullName] [nvarchar](500)  NULL  ,
	[MarginType] [int] NULL  ,
	[ShortName] [nvarchar](50)  NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[TimeZoneUtc] [int] NULL  ,
	[UpdateUrl] [nvarchar](200)  NULL  
CONSTRAINT [PK_Exchange] PRIMARY KEY CLUSTERED 
(
	[ExchangeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ExecutionReport') AND type in (N'U')) DROP TABLE [dbo].[ExecutionReport]
GO

BEGIN
CREATE TABLE [dbo].[ExecutionReport](

	[Account] [nvarchar](100)  NULL  ,
	[AccountName] [nvarchar](100)  NULL  ,
	[ActorChanged] [bigint] NOT NULL  ,
	[ActorOrder] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[BaseSymbolId] [bigint] NULL  ,
	[ChainOrderId] [varchar](50)  NULL  ,
	[ClOrdID] [nvarchar](100)  NULL  ,
	[CustomerId] [bigint] NULL  ,
	[ExecId] [nvarchar](50)  NULL  ,
	[ExecValue] [ntext] NULL  ,
	[ExecutionReportId] [bigint] NOT NULL  ,
	[LastStatus] [nvarchar](50)  NULL  ,
	[MemberId] [bigint] NULL  ,
	[Note] [nvarchar](100)  NULL  ,
	[OrdStatus] [nvarchar](50)  NULL  ,
	[OrderID] [nvarchar](100)  NULL  ,
	[OrigClOrdID] [nvarchar](100)  NULL  ,
	[ProcessStatus] [nvarchar](50)  NULL  ,
	[SymbolId] [bigint] NULL  ,
	[SymbolName] [nvarchar](50)  NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[TransactionTime] [datetime] NULL  
CONSTRAINT [PK_ExecutionReport] PRIMARY KEY CLUSTERED 
(
	[ExecutionReportId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'FeeLevel') AND type in (N'U')) DROP TABLE [dbo].[FeeLevel]
GO

BEGIN
CREATE TABLE [dbo].[FeeLevel](

	[ActorChanged] [bigint] NOT NULL  ,
	[BaseSymbolId] [bigint] NULL  ,
	[Fee] [decimal](28,6) NULL  ,
	[FeeLevelId] [bigint] NOT NULL  ,
	[FromQuantity] [decimal](28,6) NULL  ,
	[IsPendingChange] [bit] NULL  ,
	[MemberId] [bigint] NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[ToQuantity] [decimal](28,6) NULL  
CONSTRAINT [PK_FeeLevel] PRIMARY KEY CLUSTERED 
(
	[FeeLevelId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'FeeLevelOther') AND type in (N'U')) DROP TABLE [dbo].[FeeLevelOther]
GO

BEGIN
CREATE TABLE [dbo].[FeeLevelOther](

	[ActorChanged] [bigint] NOT NULL  ,
	[BaseSymbolId] [bigint] NULL  ,
	[Fee] [decimal](28,6) NULL  ,
	[FeeLevelOtherId] [bigint] NOT NULL  ,
	[FeeOtherName] [nvarchar](200)  NULL  ,
	[IsPendingChange] [bit] NULL  ,
	[MemberId] [bigint] NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_FeeLevelOther] PRIMARY KEY CLUSTERED 
(
	[FeeLevelOtherId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'FeeLevelType') AND type in (N'U')) DROP TABLE [dbo].[FeeLevelType]
GO

BEGIN
CREATE TABLE [dbo].[FeeLevelType](

	[ActorChanged] [bigint] NOT NULL  ,
	[BaseSymbolId] [bigint] NOT NULL  ,
	[FeeLevelTypeEnum] [int] NULL  ,
	[IsPendingChange] [bit] NULL  ,
	[MemberId] [bigint] NOT NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_FeeLevelType] PRIMARY KEY CLUSTERED 
(
	[BaseSymbolId] ASC,
	[MemberId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'FeeRefundMonth') AND type in (N'U')) DROP TABLE [dbo].[FeeRefundMonth]
GO

BEGIN
CREATE TABLE [dbo].[FeeRefundMonth](

	[ActorChanged] [bigint] NOT NULL  ,
	[AffectAreaId] [int] NULL  ,
	[AffectBranchId] [bigint] NULL  ,
	[AffectBrandId] [int] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[BaseSymbolId] [bigint] NULL  ,
	[DateCreate] [datetime] NULL  ,
	[FeeActual] [decimal](28,6) NULL  ,
	[FeeCalculator] [decimal](28,6) NULL  ,
	[FeePartner] [decimal](28,6) NULL  ,
	[FeePartnerRefund] [decimal](28,6) NULL  ,
	[FeeRefund] [decimal](28,6) NULL  ,
	[FeeRefundMonthId] [bigint] NOT NULL  ,
	[FromDate] [datetime] NULL  ,
	[MemberId] [bigint] NULL  ,
	[MonthRefund] [datetime] NULL  ,
	[Note] [ntext] NULL  ,
	[Rate] [decimal](28,6) NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[ToDate] [datetime] NULL  ,
	[TotalLot] [decimal](28,6) NULL  
CONSTRAINT [PK_FeeRefundMonth] PRIMARY KEY CLUSTERED 
(
	[FeeRefundMonthId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ForexRate') AND type in (N'U')) DROP TABLE [dbo].[ForexRate]
GO

BEGIN
CREATE TABLE [dbo].[ForexRate](

	[ActorChanged] [bigint] NOT NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[ApprovalBy] [bigint] NULL  ,
	[ApprovalDate] [datetime] NULL  ,
	[CreateBy] [bigint] NOT NULL  ,
	[CreateDate] [datetime] NOT NULL  ,
	[ForexRateId] [int] NOT NULL  ,
	[FromCurrency] [varchar](10)  NULL  ,
	[IsPendingChange] [bit] NULL  ,
	[RateBuy] [decimal](28,6) NULL  ,
	[RateSell] [decimal](28,6) NULL  ,
	[Source] [varchar](50)  NULL  ,
	[Status] [int] NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[ToCurrency] [varchar](10)  NULL  ,
	[ValueDate] [datetime] NULL  
CONSTRAINT [PK_ForexRate] PRIMARY KEY CLUSTERED 
(
	[ForexRateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ForexRateInternal') AND type in (N'U')) DROP TABLE [dbo].[ForexRateInternal]
GO

BEGIN
CREATE TABLE [dbo].[ForexRateInternal](

	[ActorChanged] [bigint] NOT NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[ApprovalBy] [bigint] NULL  ,
	[ApprovalDate] [datetime] NULL  ,
	[CreateBy] [bigint] NOT NULL  ,
	[CreateDate] [datetime] NOT NULL  ,
	[ForexRateInternalId] [bigint] NOT NULL  ,
	[FromCurrency] [varchar](10)  NULL  ,
	[IsPendingChange] [bit] NULL  ,
	[RateBuy] [decimal](28,6) NULL  ,
	[RateSell] [decimal](28,6) NULL  ,
	[Source] [varchar](50)  NULL  ,
	[Status] [int] NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[ToCurrency] [varchar](10)  NULL  ,
	[ValueDate] [datetime] NULL  
CONSTRAINT [PK_ForexRateInternal] PRIMARY KEY CLUSTERED 
(
	[ForexRateInternalId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'HolidayException') AND type in (N'U')) DROP TABLE [dbo].[HolidayException]
GO

BEGIN
CREATE TABLE [dbo].[HolidayException](

	[ActorChanged] [bigint] NOT NULL  ,
	[Comment] [nvarchar](250)  NULL  ,
	[HolidayDate] [datetime] NOT NULL  ,
	[HolidayTypeId] [bigint] NOT NULL  ,
	[IsWorkingDay] [bit] NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_HolidayException] PRIMARY KEY CLUSTERED 
(
	[HolidayDate] ASC,
	[HolidayTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'HolidayType') AND type in (N'U')) DROP TABLE [dbo].[HolidayType]
GO

BEGIN
CREATE TABLE [dbo].[HolidayType](

	[ActorChanged] [bigint] NOT NULL  ,
	[Comment] [nvarchar](250)  NULL  ,
	[HolidayTypeId] [bigint] NOT NULL  ,
	[HolidayTypeName] [nvarchar](250)  NOT NULL  ,
	[Status] [bit] NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[Value] [nvarchar](50)  NULL  
CONSTRAINT [PK_HolidayType] PRIMARY KEY CLUSTERED 
(
	[HolidayTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'LinkedTransaction') AND type in (N'U')) DROP TABLE [dbo].[LinkedTransaction]
GO

BEGIN
CREATE TABLE [dbo].[LinkedTransaction](

	[AccountPartner] [varchar](50)  NULL  ,
	[ActorChanged] [bigint] NOT NULL  ,
	[AffectBranchId] [bigint] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[Amount] [decimal](28,6) NOT NULL  ,
	[BaseSymbolId] [bigint] NULL  ,
	[ChainOrderIdBuy] [varchar](50)  NULL  ,
	[ChainOrderIdSell] [varchar](50)  NULL  ,
	[ExecIdBuy] [varchar](50)  NULL  ,
	[ExecIdSell] [varchar](50)  NULL  ,
	[IndexRow] [bigint] NOT NULL IDENTITY(1, 1) ,
	[IsSuccess] [bit] NULL  ,
	[MemberId] [bigint] NULL  ,
	[OrderLinkedType] [varchar](50)  NULL  ,
	[OrderTransactionBuyId] [bigint] NOT NULL  ,
	[OrderTransactionSellId] [bigint] NOT NULL  ,
	[PartnerId] [bigint] NULL  ,
	[SessionDate] [datetime] NULL  ,
	[SymbolId] [bigint] NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_LinkedTransaction] PRIMARY KEY CLUSTERED 
(
	[OrderTransactionBuyId] ASC,
	[OrderTransactionSellId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ManualPrice') AND type in (N'U')) DROP TABLE [dbo].[ManualPrice]
GO

BEGIN
CREATE TABLE [dbo].[ManualPrice](

	[ActorChanged] [bigint] NOT NULL  ,
	[BaseSymbolId] [bigint] NOT NULL  ,
	[CreateBy] [bigint] NOT NULL  ,
	[CreateDate] [datetime] NOT NULL  ,
	[IsPendingChanged] [bit] NULL  ,
	[Price] [decimal](28,6) NOT NULL  ,
	[SessionId] [bigint] NOT NULL  ,
	[SymbolId] [bigint] NOT NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_ManualPrice] PRIMARY KEY CLUSTERED 
(
	[SymbolId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ManualTradingDeal') AND type in (N'U')) DROP TABLE [dbo].[ManualTradingDeal]
GO

BEGIN
CREATE TABLE [dbo].[ManualTradingDeal](

	[AccountName] [varchar](100)  NULL  ,
	[AccountPartner] [varchar](50)  NULL  ,
	[Actor] [bigint] NULL  ,
	[ActorChanged] [bigint] NULL  ,
	[AffectBranchId] [bigint] NULL  ,
	[AffectMemberActor] [bigint] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[AvgPx] [decimal](28,6) NULL  ,
	[BaseSymbolId] [bigint] NULL  ,
	[BuySell] [varchar](50)  NULL  ,
	[ChainOrderId] [varchar](50)  NULL  ,
	[CumQty] [decimal](28,6) NULL  ,
	[Currency] [varchar](30)  NULL  ,
	[CurrentState] [nvarchar](100)  NULL  ,
	[CustomerId] [bigint] NULL  ,
	[DateCreate] [datetime] NULL  ,
	[DeliverToCompId] [nvarchar](50)  NULL  ,
	[DeliverToSubId] [nvarchar](50)  NULL  ,
	[DoneTime] [datetime] NULL  ,
	[ExecId] [nvarchar](50)  NULL  ,
	[FCM] [varchar](100)  NULL  ,
	[FCMAccountNumber] [nvarchar](100)  NULL  ,
	[FeatureOrder] [nvarchar](50)  NULL  ,
	[FillPrice] [decimal](28,6) NULL  ,
	[LeavesQty] [decimal](28,6) NULL  ,
	[LimitPrice] [decimal](28,6) NULL  ,
	[ManualTradingDealId] [bigint] NOT NULL  ,
	[Note] [nvarchar](200)  NULL  ,
	[OrderPrice] [decimal](28,6) NULL  ,
	[OrderSource] [varchar](50)  NULL  ,
	[OrderType] [varchar](50)  NULL  ,
	[PartnerId] [bigint] NULL  ,
	[PlaceTime] [datetime] NULL  ,
	[Quantity] [decimal](28,6) NULL  ,
	[SecondaryOrderId] [varchar](50)  NULL  ,
	[StopPx] [decimal](28,6) NULL  ,
	[SymbolId] [bigint] NULL  ,
	[SymbolName] [varchar](100)  NULL  ,
	[Text] [varchar](200)  NULL  ,
	[TimeChanged] [datetime] NULL  ,
	[TimeInForce] [varchar](50)  NULL  
CONSTRAINT [PK_ManualTradingDeal] PRIMARY KEY CLUSTERED 
(
	[ManualTradingDealId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'MarginManualStatus') AND type in (N'U')) DROP TABLE [dbo].[MarginManualStatus]
GO

BEGIN
CREATE TABLE [dbo].[MarginManualStatus](

	[ActorChanged] [bigint] NOT NULL  ,
	[BaseSymbolId] [bigint] NOT NULL  ,
	[Currency] [nvarchar](50)  NULL  ,
	[DoneSession] [bigint] NULL  ,
	[DoneTime] [datetime] NULL  ,
	[IsAuto] [bit] NULL  ,
	[IsDone] [bit] NULL  ,
	[MemberId] [bigint] NOT NULL  ,
	[ParentSessionId] [bigint] NOT NULL  ,
	[PartnerId] [bigint] NULL  ,
	[SymbolId] [bigint] NOT NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[TotalBuy] [decimal](18,0) NULL  ,
	[TotalSell] [decimal](18,0) NULL  
CONSTRAINT [PK_MarginManualStatus] PRIMARY KEY CLUSTERED 
(
	[BaseSymbolId] ASC,
	[MemberId] ASC,
	[ParentSessionId] ASC,
	[SymbolId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'MarginProcess') AND type in (N'U')) DROP TABLE [dbo].[MarginProcess]
GO

BEGIN
CREATE TABLE [dbo].[MarginProcess](

	[ActorChanged] [bigint] NOT NULL  ,
	[Currency] [nvarchar](50)  NOT NULL  ,
	[MemberId] [bigint] NOT NULL  ,
	[ProcessStatus] [int] NULL  ,
	[SessionId] [bigint] NOT NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_MarginProcess] PRIMARY KEY CLUSTERED 
(
	[MemberId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'MarginTradePrice') AND type in (N'U')) DROP TABLE [dbo].[MarginTradePrice]
GO

BEGIN
CREATE TABLE [dbo].[MarginTradePrice](

	[ActorChanged] [bigint] NOT NULL  ,
	[SymbolId] [bigint] NOT NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[TradePrice] [decimal](28,6) NOT NULL  
CONSTRAINT [PK_MarginTradePrice] PRIMARY KEY CLUSTERED 
(
	[SymbolId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'MemberInfo') AND type in (N'U')) DROP TABLE [dbo].[MemberInfo]
GO

BEGIN
CREATE TABLE [dbo].[MemberInfo](

	[AccountName] [nvarchar](50)  NULL  ,
	[ActorChanged] [bigint] NOT NULL  ,
	[ActorCreater] [bigint] NULL  ,
	[BrandId] [int] NULL  ,
	[ClosedTime] [datetime] NULL  ,
	[CreatedTime] [datetime] NULL  ,
	[CustomerGroup] [int] NULL  ,
	[DisplayMemberName] [nvarchar](50)  NULL  ,
	[FCMAccountNumber] [nvarchar](50)  NULL  ,
	[IsPendingChange] [bit] NULL  ,
	[MemberId] [bigint] NOT NULL  ,
	[MemberName] [nvarchar](50)  NULL  ,
	[MemberParent] [bigint] NULL  ,
	[MemberType] [int] NULL  ,
	[Note] [nvarchar](500)  NULL  ,
	[Status] [int] NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[WithdrawRuleType] [int] NULL  
CONSTRAINT [PK_MemberInfo] PRIMARY KEY CLUSTERED 
(
	[MemberId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'MemberInfoAdditional') AND type in (N'U')) DROP TABLE [dbo].[MemberInfoAdditional]
GO

BEGIN
CREATE TABLE [dbo].[MemberInfoAdditional](

	[ActorChanged] [bigint] NOT NULL  ,
	[Address] [nvarchar](200)  NULL  ,
	[Birthday] [datetime] NULL  ,
	[BussinessNumber] [varchar](50)  NULL  ,
	[CardNo] [nvarchar](100)  NULL  ,
	[CompanyName] [nvarchar](100)  NULL  ,
	[Email] [nvarchar](100)  NULL  ,
	[EmailRepresent] [nvarchar](100)  NULL  ,
	[Fax] [nvarchar](50)  NULL  ,
	[ImageSignature] [varbinary](MAX)  NULL  ,
	[ImageTaxNo] [varbinary](MAX)  NULL  ,
	[IsPendingChange] [bit] NULL  ,
	[IsPersonal] [bit] NULL  ,
	[MemberId] [bigint] NOT NULL  ,
	[Mobile] [nvarchar](50)  NULL  ,
	[MobileRepresent] [nvarchar](50)  NULL  ,
	[ObjectPersonType] [int] NULL  ,
	[ObjectTypeEnum] [int] NULL  ,
	[PersonalRepresent] [nvarchar](100)  NULL  ,
	[ProvidedAddress] [nvarchar](200)  NULL  ,
	[ProvidedDate] [datetime] NULL  ,
	[ProvidedRepresent] [nvarchar](200)  NULL  ,
	[TaxCode] [nvarchar](50)  NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_MemberInfoAdditional] PRIMARY KEY CLUSTERED 
(
	[MemberId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'MemberLimit') AND type in (N'U')) DROP TABLE [dbo].[MemberLimit]
GO

BEGIN
CREATE TABLE [dbo].[MemberLimit](

	[ActorChanged] [bigint] NOT NULL  ,
	[Curency] [nvarchar](50)  NULL  ,
	[DetailMarginCreditLimit] [decimal](28,6) NULL  ,
	[ExpiredDayAlert] [int] NULL  ,
	[FeeOther] [decimal](28,6) NULL  ,
	[IMLimit] [decimal](28,6) NULL  ,
	[IsPendingChange] [bit] NULL  ,
	[IsPendingChangeFee] [bit] NULL  ,
	[MarginCalculationMethod] [int] NULL  ,
	[MarginCreditInterestRate] [decimal](28,6) NULL  ,
	[MarginCreditMaturityDate] [datetime] NULL  ,
	[MarginMultiplier] [decimal](28,6) NULL  ,
	[MemberId] [bigint] NOT NULL  ,
	[PositionLimit] [decimal](28,6) NULL  ,
	[PositionLiquidationMethod] [int] NULL  ,
	[PositionTradingLimit] [decimal](28,6) NULL  ,
	[SplitTKKQ] [decimal](28,6) NULL  ,
	[StatusRatio] [decimal](28,6) NULL  ,
	[StoplossRatio] [decimal](28,6) NULL  ,
	[TckqRate] [decimal](28,6) NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[TotalMarginCreditLimit] [decimal](28,6) NULL  ,
	[WarningRatio] [decimal](28,6) NULL  
CONSTRAINT [PK_MemberLimit] PRIMARY KEY CLUSTERED 
(
	[MemberId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'MemberOtherInfo') AND type in (N'U')) DROP TABLE [dbo].[MemberOtherInfo]
GO

BEGIN
CREATE TABLE [dbo].[MemberOtherInfo](

	[ActorChanged] [bigint] NOT NULL  ,
	[AlertEmail] [bit] NULL  ,
	[AlertSms] [bit] NULL  ,
	[BankAccountName] [nvarchar](100)  NULL  ,
	[BankName] [nvarchar](100)  NULL  ,
	[CIFName] [nvarchar](100)  NULL  ,
	[CIFNo] [nvarchar](50)  NULL  ,
	[ContractId] [nvarchar](100)  NULL  ,
	[IsPendingChange] [bit] NULL  ,
	[LicenseCode] [nvarchar](50)  NULL  ,
	[LicenseDate] [datetime] NULL  ,
	[LicensePlace] [nvarchar](300)  NULL  ,
	[LicenseType] [nvarchar](200)  NULL  ,
	[MasterAgreementNo] [nvarchar](100)  NULL  ,
	[MemberId] [bigint] NOT NULL  ,
	[Mobile] [varchar](20)  NULL  ,
	[ShareFeeRate] [decimal](28,6) NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_MemberOtherInfo] PRIMARY KEY CLUSTERED 
(
	[MemberId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'MemberSymbolMapping') AND type in (N'U')) DROP TABLE [dbo].[MemberSymbolMapping]
GO

BEGIN
CREATE TABLE [dbo].[MemberSymbolMapping](

	[AccountPartner] [varchar](50)  NULL  ,
	[ActorChanged] [bigint] NOT NULL  ,
	[BaseSymbolId] [bigint] NOT NULL  ,
	[MemberId] [bigint] NOT NULL  ,
	[MemberPartner] [bigint] NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_MemberSymbolMapping] PRIMARY KEY CLUSTERED 
(
	[BaseSymbolId] ASC,
	[MemberId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'MemberTradingLimit') AND type in (N'U')) DROP TABLE [dbo].[MemberTradingLimit]
GO

BEGIN
CREATE TABLE [dbo].[MemberTradingLimit](

	[ActorChanged] [bigint] NOT NULL  ,
	[Id] [bigint] NOT NULL  ,
	[MemberId] [bigint] NOT NULL  ,
	[MemberTradingLimitName] [nvarchar](50)  NULL  ,
	[MemberTradingLimitType] [int] NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_MemberTradingLimit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'OpenPosition') AND type in (N'U')) DROP TABLE [dbo].[OpenPosition]
GO

BEGIN
CREATE TABLE [dbo].[OpenPosition](

	[ActorChanged] [bigint] NOT NULL  ,
	[AffectAreaId] [bigint] NULL  ,
	[AffectBranchId] [bigint] NULL  ,
	[AffectBrandId] [bigint] NULL  ,
	[AffectMemberId] [bigint] NOT NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[AvgBuy] [decimal](28,6) NULL  ,
	[AvgBuyNet] [decimal](28,6) NULL  ,
	[AvgPrice] [decimal](28,6) NULL  ,
	[AvgPriceNet] [decimal](28,6) NULL  ,
	[AvgSell] [decimal](28,6) NULL  ,
	[AvgSellNet] [decimal](28,6) NULL  ,
	[BaseSymbolId] [bigint] NOT NULL  ,
	[Currency] [varchar](50)  NULL  ,
	[IsPartner] [bit] NULL  ,
	[MemberId] [bigint] NOT NULL  ,
	[SymbolId] [bigint] NOT NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[TotalBuy] [decimal](28,6) NULL  ,
	[TotalSell] [decimal](28,6) NULL  
CONSTRAINT [PK_OpenPosition] PRIMARY KEY CLUSTERED 
(
	[BaseSymbolId] ASC,
	[MemberId] ASC,
	[SymbolId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'OpenPositionDetail') AND type in (N'U')) DROP TABLE [dbo].[OpenPositionDetail]
GO

BEGIN
CREATE TABLE [dbo].[OpenPositionDetail](

	[AccountPartner] [varchar](50)  NULL  ,
	[ActorChanged] [bigint] NOT NULL  ,
	[AffectAreaId] [bigint] NULL  ,
	[AffectBranchId] [bigint] NULL  ,
	[AffectBrandId] [bigint] NULL  ,
	[AffectMemberId] [bigint] NOT NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[BaseSymbolId] [bigint] NOT NULL  ,
	[ChainOrderId] [varchar](50)  NULL  ,
	[Currency] [varchar](50)  NULL  ,
	[FilledQty] [decimal](28,6) NULL  ,
	[InitialMargin] [decimal](28,6) NULL  ,
	[InitialMarginMXV] [decimal](28,6) NULL  ,
	[IsBuy] [bit] NULL  ,
	[MaintainMargin] [decimal](28,6) NULL  ,
	[MemberId] [bigint] NOT NULL  ,
	[OpenQty] [decimal](28,6) NULL  ,
	[OrderTransactionId] [bigint] NOT NULL  ,
	[PartnerId] [bigint] NULL  ,
	[PriceFill] [decimal](28,6) NULL  ,
	[QuantityFill] [decimal](28,6) NULL  ,
	[SpreadInitialMargin] [decimal](28,6) NULL  ,
	[SpreadMaintainMargin] [decimal](28,6) NULL  ,
	[SymbolId] [bigint] NOT NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[TransactionTime] [datetime] NULL  
CONSTRAINT [PK_OpenPositionDetail] PRIMARY KEY CLUSTERED 
(
	[BaseSymbolId] ASC,
	[MemberId] ASC,
	[OrderTransactionId] ASC,
	[SymbolId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'OrderTransaction') AND type in (N'U')) DROP TABLE [dbo].[OrderTransaction]
GO

BEGIN
CREATE TABLE [dbo].[OrderTransaction](

	[AccountId] [varchar](30)  NULL  ,
	[AccountPartner] [varchar](50)  NULL  ,
	[Actor] [bigint] NULL  ,
	[ActorChanged] [bigint] NULL  ,
	[ActorUpdate] [bigint] NULL  ,
	[AffectBranchId] [bigint] NULL  ,
	[AffectMemberActor] [bigint] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[ApprovalSession] [bigint] NULL  ,
	[ApprovalStatus] [varchar](100)  NULL  ,
	[AvgPx] [decimal](28,6) NULL  ,
	[BaseSymbolId] [bigint] NULL  ,
	[BrokerId] [int] NULL  ,
	[BrokerOrderId] [varchar](50)  NULL  ,
	[ChainOrderId] [varchar](50)  NULL  ,
	[ClientOrderId] [varchar](50)  NULL  ,
	[CumQty] [decimal](28,6) NULL  ,
	[Currency] [varchar](30)  NULL  ,
	[CustomerId] [bigint] NULL  ,
	[DeliverToCompId] [nvarchar](50)  NULL  ,
	[DeliverToSubId] [nvarchar](50)  NULL  ,
	[DoneTime] [datetime] NULL  ,
	[ExecId] [nvarchar](50)  NULL  ,
	[ExpiredDate] [datetime] NULL  ,
	[FeatureOrder] [nvarchar](50)  NULL  ,
	[FeeCustomer] [decimal](28,6) NULL  ,
	[FeeMG] [decimal](28,6) NULL  ,
	[FeePartner] [decimal](28,6) NULL  ,
	[FeeTV] [decimal](28,6) NULL  ,
	[InitTime] [datetime] NULL  ,
	[IsBuy] [bit] NULL  ,
	[IsManualOrder] [bit] NULL  ,
	[IsUpdateTradingDeal] [bit] NULL  ,
	[MaturityDate] [datetime] NULL  ,
	[Note] [nvarchar](200)  NULL  ,
	[OrderLink] [varchar](50)  NULL  ,
	[OrderTransactionId] [bigint] NOT NULL  ,
	[OrderType] [varchar](50)  NULL  ,
	[OrderTypeEnum] [nvarchar](100)  NULL  ,
	[PartnerId] [bigint] NULL  ,
	[Price] [decimal](28,6) NULL  ,
	[Quantity] [decimal](28,6) NULL  ,
	[StopPx] [decimal](28,6) NULL  ,
	[SymbolId] [bigint] NULL  ,
	[SymbolName] [varchar](100)  NULL  ,
	[Text] [varchar](200)  NULL  ,
	[TimeChanged] [datetime] NULL  ,
	[TimeInForce] [varchar](50)  NULL  
CONSTRAINT [PK_OrderTransaction] PRIMARY KEY CLUSTERED 
(
	[OrderTransactionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Representative') AND type in (N'U')) DROP TABLE [dbo].[Representative]
GO

BEGIN
CREATE TABLE [dbo].[Representative](

	[ActorChanged] [bigint] NOT NULL  ,
	[Address] [nvarchar](200)  NULL  ,
	[Birthday] [datetime] NULL  ,
	[CardNo] [nvarchar](100)  NULL  ,
	[Email] [nvarchar](200)  NULL  ,
	[ExpiredDate] [datetime] NULL  ,
	[FullName] [nvarchar](50)  NULL  ,
	[Gender] [nvarchar](50)  NULL  ,
	[IdNoTypeId] [int] NULL  ,
	[IsPendingChange] [bit] NULL  ,
	[IssueDate] [datetime] NULL  ,
	[IssuedOrg] [nvarchar](150)  NULL  ,
	[Nationality] [nvarchar](50)  NULL  ,
	[Note] [nvarchar](500)  NULL  ,
	[Phone] [nvarchar](20)  NULL  ,
	[RepresentativeId] [bigint] NOT NULL  ,
	[RepresentativeStatus] [int] NULL  ,
	[RepresentativeType] [nvarchar](150)  NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[UserId] [bigint] NULL  
CONSTRAINT [PK_Representative] PRIMARY KEY CLUSTERED 
(
	[RepresentativeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Role') AND type in (N'U')) DROP TABLE [dbo].[Role]
GO

BEGIN
CREATE TABLE [dbo].[Role](

	[ActorChanged] [bigint] NOT NULL  ,
	[Description] [nvarchar](500)  NULL  ,
	[Enable] [bit] NULL  ,
	[Name] [nvarchar](200)  NULL  ,
	[RoleId] [int] NOT NULL IDENTITY(1, 1) ,
	[RoleType] [int] NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'RoleGroup') AND type in (N'U')) DROP TABLE [dbo].[RoleGroup]
GO

BEGIN
CREATE TABLE [dbo].[RoleGroup](

	[ActorChanged] [bigint] NOT NULL  ,
	[Description] [nvarchar](500)  NULL  ,
	[IsPendingChange] [bit] NULL  ,
	[Name] [nvarchar](100)  NULL  ,
	[RoleGroupId] [int] NOT NULL  ,
	[RoleGroupType] [int] NULL  ,
	[Status] [int] NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_RoleGroup] PRIMARY KEY CLUSTERED 
(
	[RoleGroupId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'RoleGroupRef') AND type in (N'U')) DROP TABLE [dbo].[RoleGroupRef]
GO

BEGIN
CREATE TABLE [dbo].[RoleGroupRef](

	[ActorChanged] [bigint] NOT NULL  ,
	[IsPendingChange] [bit] NULL  ,
	[RoleGroupId] [int] NOT NULL  ,
	[RoleId] [int] NOT NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_RoleGroupRef] PRIMARY KEY CLUSTERED 
(
	[RoleGroupId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SessionConfig') AND type in (N'U')) DROP TABLE [dbo].[SessionConfig]
GO

BEGIN
CREATE TABLE [dbo].[SessionConfig](

	[ActorChanged] [bigint] NOT NULL  ,
	[Desciption] [varchar](100)  NULL  ,
	[SessionConfigId] [int] NOT NULL  ,
	[SessionInUse] [bit] NULL  ,
	[SessionName] [varchar](50)  NULL  ,
	[SessionNumber] [int] NULL  ,
	[SessionType] [nvarchar](50)  NULL  ,
	[StartTime] [datetime] NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_SessionConfig] PRIMARY KEY CLUSTERED 
(
	[SessionConfigId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SessionHistory') AND type in (N'U')) DROP TABLE [dbo].[SessionHistory]
GO

BEGIN
CREATE TABLE [dbo].[SessionHistory](

	[ActiveStatus] [int] NULL  ,
	[ActorChanged] [bigint] NOT NULL  ,
	[BusinessDate] [datetime] NULL  ,
	[CurrentState] [nvarchar](100)  NULL  ,
	[Description] [nvarchar](100)  NULL  ,
	[EndTime] [datetime] NULL  ,
	[IsSessionWorking] [bit] NULL  ,
	[LastState] [nvarchar](100)  NULL  ,
	[SessionId] [bigint] NOT NULL  ,
	[SessionParentId] [bigint] NULL  ,
	[SessionType] [nvarchar](50)  NULL  ,
	[StartTime] [datetime] NULL  ,
	[TaskNumber] [int] NULL  ,
	[TaskStatus] [nvarchar](100)  NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_SessionHistory] PRIMARY KEY CLUSTERED 
(
	[SessionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SpecAccounting') AND type in (N'U')) DROP TABLE [dbo].[SpecAccounting]
GO

BEGIN
CREATE TABLE [dbo].[SpecAccounting](

	[AccountCR] [varchar](50)  NULL  ,
	[AccountDR] [varchar](50)  NULL  ,
	[ActorChanged] [bigint] NOT NULL  ,
	[Amount] [decimal](28,6) NULL  ,
	[AmountOrginal] [decimal](28,6) NULL  ,
	[BaseSymbolId] [bigint] NULL  ,
	[ContractName] [nvarchar](50)  NULL  ,
	[Currency] [varchar](50)  NULL  ,
	[CurrencyOrginal] [varchar](50)  NULL  ,
	[DescriptionCR] [nvarchar](500)  NULL  ,
	[DescriptionDR] [nvarchar](500)  NULL  ,
	[DescriptionRef] [nvarchar](500)  NULL  ,
	[DescriptionTrading] [nvarchar](250)  NULL  ,
	[DisplayMemberName] [nvarchar](50)  NULL  ,
	[ExecId] [varchar](50)  NULL  ,
	[IsBuy] [bit] NULL  ,
	[IsReversal] [bit] NULL  ,
	[Note] [ntext] NULL  ,
	[OrderTransactionId] [bigint] NULL  ,
	[OrderTransactionIdBuy] [bigint] NULL  ,
	[OrderTransactionIdSell] [bigint] NULL  ,
	[ParentSessionId] [bigint] NULL  ,
	[PartnerCode] [nvarchar](50)  NULL  ,
	[PostDate] [datetime] NULL  ,
	[QuantityLinked] [decimal](28,6) NULL  ,
	[Remark] [nvarchar](250)  NULL  ,
	[SessionId] [bigint] NULL  ,
	[SourceBranchCode] [int] NULL  ,
	[SpecAccountingEnum] [varchar](100)  NULL  ,
	[SpecAccountingId] [bigint] NOT NULL  ,
	[SymbolId] [bigint] NULL  ,
	[SyncStatus] [int] NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[TransBranchCode] [int] NULL  ,
	[ValueDate] [datetime] NULL  
CONSTRAINT [PK_SpecAccounting] PRIMARY KEY CLUSTERED 
(
	[SpecAccountingId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Symbol') AND type in (N'U')) DROP TABLE [dbo].[Symbol]
GO

BEGIN
CREATE TABLE [dbo].[Symbol](

	[ActorChanged] [bigint] NOT NULL  ,
	[BaseSymbolId] [bigint] NULL  ,
	[ContractName] [nvarchar](100)  NULL  ,
	[FirstNotifyDay] [datetime] NULL  ,
	[InitialMargin] [decimal](28,6) NULL  ,
	[InitialMarginMXV] [decimal](28,6) NULL  ,
	[LastTradingDay] [datetime] NULL  ,
	[MaintainMargin] [decimal](28,6) NULL  ,
	[MarginRefSource] [int] NULL  ,
	[SpreadInitialMargin] [decimal](28,6) NULL  ,
	[SpreadMaintainMargin] [decimal](28,6) NULL  ,
	[SymbolId] [bigint] NOT NULL  ,
	[SymbolStatus] [int] NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[TradingType] [int] NULL  ,
	[ValueDay] [int] NULL  
CONSTRAINT [PK_Symbol] PRIMARY KEY CLUSTERED 
(
	[SymbolId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SymbolCQG') AND type in (N'U')) DROP TABLE [dbo].[SymbolCQG]
GO

BEGIN
CREATE TABLE [dbo].[SymbolCQG](

	[ActorChanged] [bigint] NOT NULL  ,
	[BaseSymbolCQGId] [bigint] NULL  ,
	[ContractName] [nvarchar](100)  NULL  ,
	[FirstNotifyDay] [datetime] NULL  ,
	[FullName] [nvarchar](200)  NULL  ,
	[InitialMargin] [decimal](28,6) NULL  ,
	[InitialMarginMXV] [decimal](28,6) NULL  ,
	[IsPendingChange] [bit] NULL  ,
	[LastTradingDay] [datetime] NULL  ,
	[MaintainMargin] [decimal](28,6) NULL  ,
	[MarginRefSource] [int] NULL  ,
	[MothExpiryCode] [nvarchar](50)  NULL  ,
	[Note] [nvarchar](200)  NULL  ,
	[ShortName] [nvarchar](150)  NULL  ,
	[SpreadInitialMargin] [decimal](28,6) NULL  ,
	[SpreadMaintainMargin] [decimal](28,6) NULL  ,
	[SymbolCQGId] [bigint] NOT NULL  ,
	[SymbolExpiry] [nvarchar](50)  NULL  ,
	[SymbolName] [nvarchar](100)  NULL  ,
	[SymbolStatus] [int] NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[TradingType] [int] NULL  ,
	[YearExpiryCode] [nvarchar](50)  NULL  
CONSTRAINT [PK_SymbolCQG] PRIMARY KEY CLUSTERED 
(
	[SymbolCQGId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SymbolMapping') AND type in (N'U')) DROP TABLE [dbo].[SymbolMapping]
GO

BEGIN
CREATE TABLE [dbo].[SymbolMapping](

	[ActorChanged] [bigint] NOT NULL  ,
	[BrokerId] [int] NOT NULL  ,
	[InitialMargin] [decimal](28,6) NULL  ,
	[InitialMarginMXV] [decimal](28,6) NULL  ,
	[MaintainMargin] [decimal](28,6) NULL  ,
	[MaturityMonthYear] [nvarchar](50)  NULL  ,
	[SpreadInitialMargin] [decimal](28,6) NULL  ,
	[SpreadMaintainMargin] [decimal](28,6) NULL  ,
	[SymbolId] [bigint] NOT NULL  ,
	[SymbolName] [nvarchar](50)  NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_SymbolMapping] PRIMARY KEY CLUSTERED 
(
	[BrokerId] ASC,
	[SymbolId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SymbolSettlementPrice') AND type in (N'U')) DROP TABLE [dbo].[SymbolSettlementPrice]
GO

BEGIN
CREATE TABLE [dbo].[SymbolSettlementPrice](

	[ActorChanged] [bigint] NOT NULL  ,
	[ApprovalBy] [bigint] NULL  ,
	[ApprovalDate] [datetime] NULL  ,
	[BusinessDate] [datetime] NULL  ,
	[CreateBy] [bigint] NULL  ,
	[CreateDate] [datetime] NULL  ,
	[ParentSessionId] [bigint] NOT NULL  ,
	[SettlementPrice] [decimal](28,6) NULL  ,
	[SymbolId] [bigint] NOT NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[YesterdaySettlement] [decimal](28,6) NULL  
CONSTRAINT [PK_SymbolSettlementPrice] PRIMARY KEY CLUSTERED 
(
	[SymbolId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SystemAction') AND type in (N'U')) DROP TABLE [dbo].[SystemAction]
GO

BEGIN
CREATE TABLE [dbo].[SystemAction](

	[ActionTime] [datetime] NULL  ,
	[ActorChanged] [bigint] NOT NULL  ,
	[DateApply] [datetime] NULL  ,
	[Description] [nvarchar](250)  NULL  ,
	[IsManual] [bit] NULL  ,
	[ParentSessionId] [bigint] NULL  ,
	[SessionId] [bigint] NULL  ,
	[Status] [bit] NULL  ,
	[SystemActionId] [bigint] NOT NULL IDENTITY(1, 1) ,
	[SystemActionType] [nvarchar](50)  NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_SystemAction] PRIMARY KEY CLUSTERED 
(
	[SystemActionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SystemConfig') AND type in (N'U')) DROP TABLE [dbo].[SystemConfig]
GO

BEGIN
CREATE TABLE [dbo].[SystemConfig](

	[ActorChanged] [bigint] NOT NULL  ,
	[Description] [nvarchar](250)  NULL  ,
	[Name] [varchar](250)  NULL  ,
	[SystemConfigId] [bigint] NOT NULL  ,
	[SystemConfigType] [int] NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[Value] [ntext] NULL  
CONSTRAINT [PK_SystemConfig] PRIMARY KEY CLUSTERED 
(
	[SystemConfigId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'TradingDeal') AND type in (N'U')) DROP TABLE [dbo].[TradingDeal]
GO

BEGIN
CREATE TABLE [dbo].[TradingDeal](

	[AcceptOrder] [bigint] NULL  ,
	[AcceptOrderMember] [bigint] NULL  ,
	[AccountPartner] [nvarchar](50)  NULL  ,
	[Actor] [bigint] NULL  ,
	[ActorChanged] [bigint] NOT NULL  ,
	[ActorUpdate] [bigint] NULL  ,
	[AffectAreaId] [int] NULL  ,
	[AffectBranchId] [bigint] NULL  ,
	[AffectBrandId] [int] NULL  ,
	[AffectMemberActor] [bigint] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[AvgPx] [decimal](28,6) NULL  ,
	[BaseSymbolId] [bigint] NULL  ,
	[BrokerId] [int] NULL  ,
	[ChainOrderId] [varchar](50)  NULL  ,
	[ClientOrderId] [varchar](50)  NOT NULL  ,
	[Commission] [decimal](28,6) NULL  ,
	[CostPrice] [decimal](28,6) NULL  ,
	[CumQty] [decimal](28,6) NULL  ,
	[Currency] [varchar](50)  NULL  ,
	[CurrentState] [nvarchar](100)  NULL  ,
	[CustomerId] [bigint] NULL  ,
	[DeliverToCompId] [nvarchar](50)  NULL  ,
	[DeliverToSubId] [nvarchar](50)  NULL  ,
	[DoneTime] [datetime] NULL  ,
	[EffectiveTime] [datetime] NULL  ,
	[ExpireDate] [datetime] NULL  ,
	[ExpireTime] [datetime] NULL  ,
	[FeatureOrder] [nvarchar](50)  NULL  ,
	[FeeCustomer] [decimal](28,6) NULL  ,
	[FeeMG] [decimal](28,6) NULL  ,
	[FeePartner] [decimal](28,6) NULL  ,
	[FeeTV] [decimal](28,6) NULL  ,
	[InitTime] [datetime] NULL  ,
	[InitialMargin] [decimal](28,6) NULL  ,
	[InitialMarginMXV] [decimal](28,6) NULL  ,
	[IsBuy] [bit] NULL  ,
	[IsManualOrder] [bit] NULL  ,
	[IsUpdateTradingDeal] [bit] NULL  ,
	[LastState] [nvarchar](100)  NULL  ,
	[LastTrigger] [nvarchar](100)  NULL  ,
	[LeavesQty] [decimal](28,6) NULL  ,
	[MainBrokerOrderId] [varchar](50)  NULL  ,
	[MainExchangeOrderId] [varchar](50)  NULL  ,
	[MaintainMargin] [decimal](28,6) NULL  ,
	[Note] [nvarchar](100)  NULL  ,
	[OrderScrope] [nvarchar](100)  NULL  ,
	[OrderSource] [varchar](50)  NULL  ,
	[OrderType] [nvarchar](50)  NULL  ,
	[OrderTypeEnum] [nvarchar](100)  NULL  ,
	[PartnerId] [bigint] NULL  ,
	[Price] [decimal](28,6) NULL  ,
	[PriceCustomer] [decimal](28,6) NULL  ,
	[Quantity] [decimal](28,6) NULL  ,
	[SecondaryOrderId] [varchar](50)  NULL  ,
	[SenderCompId] [nvarchar](50)  NULL  ,
	[SpreadInitialMargin] [decimal](28,6) NULL  ,
	[SpreadMaintainMargin] [decimal](28,6) NULL  ,
	[StopPx] [decimal](28,6) NULL  ,
	[SymbolId] [bigint] NULL  ,
	[SymbolName] [nvarchar](50)  NULL  ,
	[TargetCompId] [nvarchar](50)  NULL  ,
	[TargetSubId] [nvarchar](50)  NULL  ,
	[TaskNameString] [nvarchar](100)  NULL  ,
	[Text] [nvarchar](200)  NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[TimeInForce] [varchar](50)  NULL  ,
	[TransactTime] [datetime] NULL  
CONSTRAINT [PK_TradingDeal] PRIMARY KEY CLUSTERED 
(
	[ClientOrderId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'UserInfo') AND type in (N'U')) DROP TABLE [dbo].[UserInfo]
GO

BEGIN
CREATE TABLE [dbo].[UserInfo](

	[ActorChanged] [bigint] NOT NULL  ,
	[ActorCreater] [bigint] NULL  ,
	[Address] [nvarchar](200)  NULL  ,
	[BankAccount] [nvarchar](50)  NULL  ,
	[BankName] [nvarchar](100)  NULL  ,
	[Birthday] [datetime] NULL  ,
	[CardNo] [nvarchar](100)  NULL  ,
	[ClosedTime] [datetime] NULL  ,
	[ContactAddress] [nvarchar](200)  NULL  ,
	[CreatedTime] [datetime] NULL  ,
	[DepartmentId] [nvarchar](200)  NULL  ,
	[DisplayId] [nvarchar](50)  NULL  ,
	[Email] [nvarchar](100)  NULL  ,
	[ExpiredDate] [datetime] NULL  ,
	[Fax] [nvarchar](50)  NULL  ,
	[FullName] [nvarchar](100)  NULL  ,
	[Gender] [nvarchar](50)  NULL  ,
	[IdNoTypeId] [int] NULL  ,
	[IsBlackList] [bit] NULL  ,
	[IsNotifySms] [bit] NULL  ,
	[IsNotifySmsOrder] [bit] NULL  ,
	[IsNotityfyEmail] [bit] NULL  ,
	[IsPendingChange] [bit] NULL  ,
	[IssueDate] [datetime] NULL  ,
	[IssuedOrg] [nvarchar](150)  NULL  ,
	[MemberId] [bigint] NULL  ,
	[Mobile] [nvarchar](50)  NULL  ,
	[Note] [nvarchar](200)  NULL  ,
	[Status] [int] NULL  ,
	[TaxIdNo] [nvarchar](50)  NULL  ,
	[Tel] [nvarchar](50)  NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[UserId] [bigint] NOT NULL  
CONSTRAINT [PK_UserInfo] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'UserLayout') AND type in (N'U')) DROP TABLE [dbo].[UserLayout]
GO

BEGIN
CREATE TABLE [dbo].[UserLayout](

	[ActorChanged] [bigint] NOT NULL  ,
	[CurrentLayout] [ntext] NULL  ,
	[DefaultLayout] [ntext] NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[UserId] [bigint] NOT NULL  
CONSTRAINT [PK_UserLayout] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'UserLogin') AND type in (N'U')) DROP TABLE [dbo].[UserLogin]
GO

BEGIN
CREATE TABLE [dbo].[UserLogin](

	[ActorChanged] [bigint] NOT NULL  ,
	[ActorCreated] [bigint] NULL  ,
	[ExpiredDay] [bigint] NULL  ,
	[ExpiredDayAlert] [bigint] NULL  ,
	[FailCount] [bigint] NULL  ,
	[FailNumber] [int] NULL  ,
	[IsExpriedChanged] [bit] NULL  ,
	[IsExpriedCheck] [bit] NULL  ,
	[IsPendingChange] [bit] NULL  ,
	[LastestLogin] [datetime] NULL  ,
	[LoginCount] [bigint] NULL  ,
	[OtpPass] [nvarchar](500)  NULL  ,
	[PassChangedDate] [datetime] NULL  ,
	[Password] [nvarchar](500)  NULL  ,
	[Status] [int] NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[UserId] [bigint] NOT NULL  ,
	[UserName] [nvarchar](100)  NULL  ,
	[WorkingQueue] [nvarchar](500)  NULL  
CONSTRAINT [PK_UserLogin] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'UserOtherInfo') AND type in (N'U')) DROP TABLE [dbo].[UserOtherInfo]
GO

BEGIN
CREATE TABLE [dbo].[UserOtherInfo](

	[ActorChanged] [bigint] NOT NULL  ,
	[IsPendingChange] [bit] NULL  ,
	[Note] [nvarchar](200)  NULL  ,
	[Photo] [varbinary](MAX)  NULL  ,
	[PhotoLink] [ntext] NULL  ,
	[Signature1] [varbinary](MAX)  NULL  ,
	[Signature2] [varbinary](MAX)  NULL  ,
	[Signature3] [varbinary](MAX)  NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[UserId] [bigint] NOT NULL  
CONSTRAINT [PK_UserOtherInfo] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'UserRoleGroup') AND type in (N'U')) DROP TABLE [dbo].[UserRoleGroup]
GO

BEGIN
CREATE TABLE [dbo].[UserRoleGroup](

	[ActorChanged] [bigint] NOT NULL  ,
	[IsPendingChange] [bit] NULL  ,
	[RoleGroupId] [int] NOT NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[UserId] [bigint] NOT NULL  
CONSTRAINT [PK_UserRoleGroup] PRIMARY KEY CLUSTERED 
(
	[RoleGroupId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO

USE DatabaseHistNVC
GO




IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'AccountDaily') AND type in (N'U')) DROP TABLE [dbo].[AccountDaily]
GO

BEGIN
CREATE TABLE [dbo].[AccountDaily](

	[AccountDailyId] [bigint] NOT NULL  ,
	[AccountId] [varchar](50)  NOT NULL  ,
	[AccountLevel] [int] NULL  ,
	[AccountType] [int] NULL  ,
	[ActorChanged] [bigint] NOT NULL  ,
	[AffectAreaId] [int] NULL  ,
	[AffectBranchId] [bigint] NULL  ,
	[AffectBrandId] [int] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[Amount] [decimal](28,6) NULL  ,
	[BusinessDate] [datetime] NULL  ,
	[Currency] [varchar](10)  NULL  ,
	[DisplayAccount] [varchar](30)  NULL  ,
	[Hold] [decimal](28,6) NULL  ,
	[MemberId] [bigint] NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_AccountDaily] PRIMARY KEY CLUSTERED 
(
	[AccountDailyId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'AccountInfoDaily') AND type in (N'U')) DROP TABLE [dbo].[AccountInfoDaily]
GO

BEGIN
CREATE TABLE [dbo].[AccountInfoDaily](

	[AccountId] [varchar](50)  NOT NULL  ,
	[AccountInfoDailyId] [bigint] NOT NULL  ,
	[ActorChanged] [bigint] NOT NULL  ,
	[AffectAreaId] [int] NULL  ,
	[AffectBranchId] [bigint] NULL  ,
	[AffectBrandId] [int] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[AmountHold] [decimal](28,6) NULL  ,
	[BusinessDate] [datetime] NULL  ,
	[Currency] [varchar](10)  NULL  ,
	[IM] [decimal](28,6) NULL  ,
	[IsPendingChanged] [bit] NULL  ,
	[MarginDeficitInterestRate] [decimal](28,6) NULL  ,
	[MarginSurplusInterestRate] [decimal](28,6) NULL  ,
	[MemberId] [bigint] NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[UnPnL] [decimal](28,6) NULL  
CONSTRAINT [PK_AccountInfoDaily] PRIMARY KEY CLUSTERED 
(
	[AccountInfoDailyId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'AccountTransactionHist') AND type in (N'U')) DROP TABLE [dbo].[AccountTransactionHist]
GO

BEGIN
CREATE TABLE [dbo].[AccountTransactionHist](

	[AccountTransactionHistId] [bigint] NOT NULL IDENTITY(1, 1) ,
	[Actor] [bigint] NULL  ,
	[ActorChanged] [bigint] NOT NULL  ,
	[AffectAreaId] [int] NULL  ,
	[AffectBrandId] [int] NULL  ,
	[AffectMemberActor] [bigint] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[Amount] [decimal](28,6) NULL  ,
	[AmountCR] [decimal](28,6) NULL  ,
	[AmountDR] [decimal](28,6) NULL  ,
	[AmountOrginal] [decimal](28,6) NULL  ,
	[BrokerId] [int] NULL  ,
	[BrokerOrderId] [varchar](50)  NULL  ,
	[CR] [varchar](50)  NULL  ,
	[Currency] [varchar](10)  NULL  ,
	[CurrencyOrginal] [varchar](10)  NULL  ,
	[DR] [varchar](50)  NULL  ,
	[DnOrverDraft] [decimal](28,6) NULL  ,
	[HoldCR] [decimal](28,6) NULL  ,
	[HoldDR] [decimal](28,6) NULL  ,
	[HoldOverDraft] [decimal](28,6) NULL  ,
	[MemberIdCR] [bigint] NULL  ,
	[MemberIdDR] [bigint] NULL  ,
	[Note] [nvarchar](250)  NULL  ,
	[OrderTransactionBuyId] [bigint] NULL  ,
	[OrderTransactionSellId] [bigint] NULL  ,
	[OverDraftLimit] [decimal](28,6) NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[TransTime] [datetime] NULL  ,
	[TransTypeId] [bigint] NULL  ,
	[TransactionId] [bigint] NOT NULL  
CONSTRAINT [PK_AccountTransactionHist] PRIMARY KEY CLUSTERED 
(
	[AccountTransactionHistId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ActionLog') AND type in (N'U')) DROP TABLE [dbo].[ActionLog]
GO

BEGIN
CREATE TABLE [dbo].[ActionLog](

	[ActionLogId] [bigint] NOT NULL IDENTITY(1, 1) ,
	[ActionTime] [datetime] NULL  ,
	[ActionType] [nvarchar](50)  NULL  ,
	[ActionValue] [nvarchar](50)  NULL  ,
	[ActorChanged] [bigint] NOT NULL  ,
	[Description] [ntext] NULL  ,
	[FromActor] [bigint] NULL  ,
	[IsMemberLog] [bit] NULL  ,
	[IsSystemEffect] [bit] NULL  ,
	[SessionId] [bigint] NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[ToActor] [bigint] NULL  
CONSTRAINT [PK_ActionLog] PRIMARY KEY CLUSTERED 
(
	[ActionLogId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'AlertEmailHis') AND type in (N'U')) DROP TABLE [dbo].[AlertEmailHis]
GO

BEGIN
CREATE TABLE [dbo].[AlertEmailHis](

	[ActorChanged] [bigint] NOT NULL  ,
	[Address] [nvarchar](MAX)  NOT NULL  ,
	[AlertKey] [ntext] NULL  ,
	[AttachData] [varbinary](MAX)  NULL  ,
	[AttachName] [nvarchar](250)  NULL  ,
	[Content] [ntext] NOT NULL  ,
	[ContentType] [nvarchar](50)  NOT NULL  ,
	[CountError] [int] NOT NULL  ,
	[EmailStatus] [int] NULL  ,
	[Id] [bigint] NOT NULL IDENTITY(1, 1) ,
	[IsSuccess] [bit] NULL  ,
	[IsWorking] [bit] NOT NULL  ,
	[ObjectAlert] [nvarchar](250)  NULL  ,
	[ParentSessionId] [bigint] NULL  ,
	[Priority] [int] NOT NULL  ,
	[ReceiverID] [nvarchar](50)  NOT NULL  ,
	[Sender] [nvarchar](50)  NOT NULL  ,
	[SendingTime] [datetime] NULL  ,
	[Subject] [ntext] NOT NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_AlertEmailHis] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'AlertSmsHis') AND type in (N'U')) DROP TABLE [dbo].[AlertSmsHis]
GO

BEGIN
CREATE TABLE [dbo].[AlertSmsHis](

	[ActorChanged] [bigint] NOT NULL  ,
	[AlertKey] [ntext] NULL  ,
	[Content] [ntext] NULL  ,
	[ContentType] [nvarchar](50)  NOT NULL  ,
	[CountError] [int] NOT NULL  ,
	[Id] [bigint] NOT NULL IDENTITY(1, 1) ,
	[IsSuccess] [bit] NULL  ,
	[IsWorking] [bit] NOT NULL  ,
	[ParentSessionId] [bigint] NULL  ,
	[Phone] [varchar](50)  NOT NULL  ,
	[Priority] [int] NOT NULL  ,
	[ReceiverID] [nvarchar](50)  NOT NULL  ,
	[Sender] [nvarchar](50)  NOT NULL  ,
	[SendingTime] [datetime] NOT NULL  ,
	[SmsStatus] [int] NULL  ,
	[TimeChanged] [datetime] NULL  
CONSTRAINT [PK_AlertSmsHis] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ApprovalAccountHist') AND type in (N'U')) DROP TABLE [dbo].[ApprovalAccountHist]
GO

BEGIN
CREATE TABLE [dbo].[ApprovalAccountHist](

	[ActorChanged] [bigint] NOT NULL  ,
	[AffectAreaId] [int] NULL  ,
	[AffectBrandId] [int] NULL  ,
	[AffectMemberActor] [bigint] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[ApprovalAccountHistId] [bigint] NOT NULL IDENTITY(1, 1) ,
	[ApprovalAccountId] [bigint] NOT NULL  ,
	[ApprovalBy] [bigint] NULL  ,
	[ApprovalDate] [datetime] NULL  ,
	[ApprovalType] [int] NULL  ,
	[CreateBy] [bigint] NULL  ,
	[CreateDate] [datetime] NULL  ,
	[CurrentValue] [ntext] NULL  ,
	[NewValue] [ntext] NULL  ,
	[Note] [ntext] NULL  ,
	[ObjectId] [varchar](50)  NULL  ,
	[ObjectName] [varchar](50)  NULL  ,
	[Reason] [ntext] NULL  ,
	[Status] [int] NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_ApprovalAccountHist] PRIMARY KEY CLUSTERED 
(
	[ApprovalAccountHistId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ApprovalDealingHist') AND type in (N'U')) DROP TABLE [dbo].[ApprovalDealingHist]
GO

BEGIN
CREATE TABLE [dbo].[ApprovalDealingHist](

	[ActorChanged] [bigint] NOT NULL  ,
	[AffectAreaId] [int] NULL  ,
	[AffectBrandId] [int] NULL  ,
	[AffectMemberActor] [bigint] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[ApprovalBy] [bigint] NULL  ,
	[ApprovalDate] [datetime] NULL  ,
	[ApprovalDealingHistId] [bigint] NOT NULL IDENTITY(1, 1) ,
	[ApprovalDealingId] [bigint] NOT NULL  ,
	[ApprovalType] [int] NULL  ,
	[CreateBy] [bigint] NULL  ,
	[CreateDate] [datetime] NULL  ,
	[CurrentValue] [ntext] NULL  ,
	[NewValue] [ntext] NULL  ,
	[Note] [nvarchar](500)  NULL  ,
	[ObjectId] [nvarchar](50)  NULL  ,
	[ObjectName] [nvarchar](50)  NULL  ,
	[Reason] [nvarchar](500)  NULL  ,
	[Status] [int] NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_ApprovalDealingHist] PRIMARY KEY CLUSTERED 
(
	[ApprovalDealingHistId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ApprovalMemberHist') AND type in (N'U')) DROP TABLE [dbo].[ApprovalMemberHist]
GO

BEGIN
CREATE TABLE [dbo].[ApprovalMemberHist](

	[ActorChanged] [bigint] NOT NULL  ,
	[AffectAreaId] [int] NULL  ,
	[AffectBrandId] [int] NULL  ,
	[AffectMemberActor] [bigint] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[ApprovalBy] [bigint] NULL  ,
	[ApprovalDate] [datetime] NULL  ,
	[ApprovalMemberHistId] [bigint] NOT NULL IDENTITY(1, 1) ,
	[ApprovalMemberId] [bigint] NOT NULL  ,
	[ApprovalType] [int] NULL  ,
	[CreateBy] [bigint] NULL  ,
	[CreateDate] [datetime] NULL  ,
	[CurrentValue] [ntext] NULL  ,
	[NewValue] [ntext] NULL  ,
	[Note] [nvarchar](500)  NULL  ,
	[ObjectId] [nvarchar](50)  NULL  ,
	[ObjectName] [nvarchar](50)  NULL  ,
	[Reason] [nvarchar](500)  NULL  ,
	[Status] [int] NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_ApprovalMemberHist] PRIMARY KEY CLUSTERED 
(
	[ApprovalMemberHistId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ApprovalOrderHist') AND type in (N'U')) DROP TABLE [dbo].[ApprovalOrderHist]
GO

BEGIN
CREATE TABLE [dbo].[ApprovalOrderHist](

	[ActorChanged] [bigint] NOT NULL  ,
	[AffectAreaId] [int] NULL  ,
	[AffectBrandId] [int] NULL  ,
	[AffectMemberActor] [bigint] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[ApprovalBy] [bigint] NULL  ,
	[ApprovalDate] [datetime] NULL  ,
	[ApprovalOrderHistId] [bigint] NOT NULL IDENTITY(1, 1) ,
	[ApprovalOrderId] [bigint] NOT NULL  ,
	[ApprovalType] [int] NULL  ,
	[CreateBy] [bigint] NULL  ,
	[CreateDate] [datetime] NULL  ,
	[CurrentValue] [ntext] NULL  ,
	[IsReject] [bit] NULL  ,
	[NewValue] [ntext] NULL  ,
	[Note] [nvarchar](500)  NULL  ,
	[ObjectId] [nvarchar](50)  NULL  ,
	[ObjectName] [nvarchar](50)  NULL  ,
	[Reason] [nvarchar](500)  NULL  ,
	[Status] [int] NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_ApprovalOrderHist] PRIMARY KEY CLUSTERED 
(
	[ApprovalOrderHistId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ApprovalPreRiskHist') AND type in (N'U')) DROP TABLE [dbo].[ApprovalPreRiskHist]
GO

BEGIN
CREATE TABLE [dbo].[ApprovalPreRiskHist](

	[ActorChanged] [bigint] NOT NULL  ,
	[AffectAreaId] [int] NULL  ,
	[AffectBrandId] [int] NULL  ,
	[AffectMemberActor] [bigint] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[ApprovalBy] [bigint] NULL  ,
	[ApprovalDate] [datetime] NULL  ,
	[ApprovalPreRiskHistId] [bigint] NOT NULL IDENTITY(1, 1) ,
	[ApprovalPreRiskId] [bigint] NOT NULL  ,
	[ApprovalType] [int] NULL  ,
	[CreateBy] [bigint] NULL  ,
	[CreateDate] [datetime] NULL  ,
	[CurrentValue] [ntext] NULL  ,
	[NewValue] [ntext] NULL  ,
	[Note] [nvarchar](500)  NULL  ,
	[ObjectId] [nvarchar](50)  NULL  ,
	[ObjectName] [nvarchar](50)  NULL  ,
	[Reason] [nvarchar](500)  NULL  ,
	[Status] [int] NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_ApprovalPreRiskHist] PRIMARY KEY CLUSTERED 
(
	[ApprovalPreRiskHistId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ApprovalSystemHist') AND type in (N'U')) DROP TABLE [dbo].[ApprovalSystemHist]
GO

BEGIN
CREATE TABLE [dbo].[ApprovalSystemHist](

	[ActorChanged] [bigint] NOT NULL  ,
	[AffectAreaId] [int] NULL  ,
	[AffectBrandId] [int] NULL  ,
	[AffectMemberActor] [bigint] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[ApprovalBy] [bigint] NULL  ,
	[ApprovalDate] [datetime] NULL  ,
	[ApprovalSystemHistId] [bigint] NOT NULL IDENTITY(1, 1) ,
	[ApprovalSystemId] [bigint] NOT NULL  ,
	[ApprovalType] [int] NULL  ,
	[CreateBy] [bigint] NULL  ,
	[CreateDate] [datetime] NULL  ,
	[CurrentValue] [ntext] NULL  ,
	[NewValue] [ntext] NULL  ,
	[Note] [nvarchar](500)  NULL  ,
	[ObjectId] [nvarchar](50)  NULL  ,
	[ObjectName] [nvarchar](50)  NULL  ,
	[Reason] [nvarchar](500)  NULL  ,
	[Status] [int] NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_ApprovalSystemHist] PRIMARY KEY CLUSTERED 
(
	[ApprovalSystemHistId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'BaseSymbolAction') AND type in (N'U')) DROP TABLE [dbo].[BaseSymbolAction]
GO

BEGIN
CREATE TABLE [dbo].[BaseSymbolAction](

	[ActorChanged] [bigint] NOT NULL  ,
	[AuditType] [int] NULL  ,
	[BaseSymbolActionId] [bigint] NOT NULL IDENTITY(1, 1) ,
	[BaseSymbolId] [bigint] NULL  ,
	[BrokerId] [int] NOT NULL  ,
	[Currency] [nvarchar](50)  NULL  ,
	[ExchangeId] [int] NULL  ,
	[FirstNotifyDayAjust] [int] NULL  ,
	[FullName] [nvarchar](50)  NULL  ,
	[GroupProductId] [int] NULL  ,
	[InitialMargin] [decimal](28,6) NULL  ,
	[InitialMarginMXV] [decimal](28,6) NULL  ,
	[MaintainMargin] [decimal](28,6) NULL  ,
	[MarginRefSource] [int] NULL  ,
	[MarginType] [int] NULL  ,
	[MinPriceIncrement] [decimal](28,6) NULL  ,
	[MinUnitIncrement] [decimal](28,6) NULL  ,
	[NumbreDecimal] [int] NULL  ,
	[OrderBookingType] [int] NULL  ,
	[PriceMultiplier] [decimal](28,6) NULL  ,
	[ProductType] [int] NULL  ,
	[ShortName] [nvarchar](50)  NULL  ,
	[SpreadInitialMargin] [decimal](28,6) NULL  ,
	[SpreadMaintainMargin] [decimal](28,6) NULL  ,
	[SymbolStatus] [int] NOT NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[TradePriceOfMeasure] [nvarchar](50)  NULL  ,
	[TradeUnitOfMeasure] [nvarchar](50)  NULL  ,
	[Unit] [nvarchar](50)  NULL  ,
	[UnitMultiplier] [decimal](28,6) NULL  
CONSTRAINT [PK_BaseSymbolAction] PRIMARY KEY CLUSTERED 
(
	[BaseSymbolActionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'BaseSymbolCQGAction') AND type in (N'U')) DROP TABLE [dbo].[BaseSymbolCQGAction]
GO

BEGIN
CREATE TABLE [dbo].[BaseSymbolCQGAction](

	[Actor] [bigint] NOT NULL  ,
	[ActorChanged] [bigint] NOT NULL  ,
	[BaseSymbolCQGActionId] [bigint] NOT NULL IDENTITY(1, 1) ,
	[BaseSymbolCQGId] [bigint] NULL  ,
	[ContractUnit] [nvarchar](50)  NULL  ,
	[CreatDate] [datetime] NOT NULL  ,
	[Currency] [nvarchar](50)  NULL  ,
	[InitialMargin] [decimal](28,6) NULL  ,
	[InitialMarginMXV] [decimal](28,6) NULL  ,
	[IsPendingChange] [bit] NULL  ,
	[IsPendingChangeMargin] [bit] NULL  ,
	[MaintainMargin] [decimal](28,6) NULL  ,
	[MinPriceIncrement] [decimal](28,6) NULL  ,
	[MinUnitIncrement] [decimal](28,6) NULL  ,
	[Note] [nvarchar](200)  NULL  ,
	[NumbreDecimal] [int] NULL  ,
	[PriceMultiplier] [decimal](28,6) NULL  ,
	[PromptMonthInitialMargin] [decimal](28,6) NULL  ,
	[SpreadInitialMargin] [decimal](28,6) NULL  ,
	[SpreadMaintainMargin] [decimal](28,6) NULL  ,
	[SymbolCQGId] [nvarchar](50)  NOT NULL  ,
	[SymbolCQGName] [nvarchar](50)  NOT NULL  ,
	[SymbolCQGStatus] [int] NULL  ,
	[SymbolMonths] [nvarchar](50)  NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[UnitMultiplier] [decimal](28,6) NULL  
CONSTRAINT [PK_BaseSymbolCQGAction] PRIMARY KEY CLUSTERED 
(
	[BaseSymbolCQGActionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'BaseSymbolDaily') AND type in (N'U')) DROP TABLE [dbo].[BaseSymbolDaily]
GO

BEGIN
CREATE TABLE [dbo].[BaseSymbolDaily](

	[ActorChanged] [bigint] NOT NULL  ,
	[AuditType] [int] NULL  ,
	[BaseSymbolDailyId] [bigint] NOT NULL  ,
	[BaseSymbolId] [bigint] NOT NULL  ,
	[Currency] [nvarchar](50)  NULL  ,
	[ExchangeId] [int] NULL  ,
	[FirstNotifyDayAjust] [int] NULL  ,
	[FullName] [nvarchar](50)  NULL  ,
	[GroupProductId] [int] NULL  ,
	[InitialMargin] [decimal](28,6) NULL  ,
	[InitialMarginMXV] [decimal](28,6) NULL  ,
	[MaintainMargin] [decimal](28,6) NULL  ,
	[MarginRefSource] [int] NULL  ,
	[MarginType] [int] NULL  ,
	[MinPriceIncrement] [decimal](28,6) NULL  ,
	[MinUnitIncrement] [decimal](28,6) NULL  ,
	[NumbreDecimal] [int] NULL  ,
	[OrderBookingType] [int] NULL  ,
	[PriceMultiplier] [decimal](28,6) NULL  ,
	[ProductType] [int] NULL  ,
	[SessionId] [bigint] NOT NULL  ,
	[ShortName] [nvarchar](50)  NULL  ,
	[SpreadInitialMargin] [decimal](28,6) NULL  ,
	[SpreadMaintainMargin] [decimal](28,6) NULL  ,
	[SymbolStatus] [int] NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[TradePriceOfMeasure] [nvarchar](50)  NULL  ,
	[TradeUnitOfMeasure] [nvarchar](50)  NULL  ,
	[Unit] [nvarchar](50)  NULL  ,
	[UnitMultiplier] [decimal](28,6) NULL  
CONSTRAINT [PK_BaseSymbolDaily] PRIMARY KEY CLUSTERED 
(
	[BaseSymbolDailyId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'BrokerOrderHist') AND type in (N'U')) DROP TABLE [dbo].[BrokerOrderHist]
GO

BEGIN
CREATE TABLE [dbo].[BrokerOrderHist](

	[Actor] [bigint] NULL  ,
	[ActorChanged] [bigint] NOT NULL  ,
	[AffectAreaId] [int] NULL  ,
	[AffectBrandId] [int] NULL  ,
	[AffectMemberActor] [bigint] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[AvgPx] [decimal](28,6) NULL  ,
	[BaseSymbolId] [bigint] NULL  ,
	[BrokerId] [int] NULL  ,
	[BrokerOrderHistId] [bigint] NOT NULL IDENTITY(1, 1) ,
	[BrokerOrderId] [varchar](50)  NOT NULL  ,
	[ClientOrderId] [varchar](50)  NULL  ,
	[CumQty] [decimal](28,6) NULL  ,
	[Currency] [varchar](50)  NULL  ,
	[DoneTime] [datetime] NULL  ,
	[EffectiveTime] [datetime] NULL  ,
	[ExchangeOrderId] [varchar](50)  NULL  ,
	[ExpireDate] [datetime] NULL  ,
	[ExpireTime] [datetime] NULL  ,
	[InitTime] [datetime] NULL  ,
	[IsBuy] [bit] NULL  ,
	[LeavesQty] [decimal](28,6) NULL  ,
	[MemberId] [bigint] NULL  ,
	[Note] [nvarchar](100)  NULL  ,
	[OrderLink] [varchar](50)  NULL  ,
	[OrderStatus] [int] NULL  ,
	[OrderType] [nvarchar](50)  NULL  ,
	[Price] [decimal](28,6) NULL  ,
	[Quantity] [decimal](28,6) NULL  ,
	[StopPx] [decimal](28,6) NULL  ,
	[SymbolId] [bigint] NULL  ,
	[SymbolName] [varchar](50)  NULL  ,
	[Text] [nvarchar](200)  NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[TimeInForce] [varchar](10)  NULL  ,
	[TradingDealType] [int] NULL  ,
	[TransactTime] [datetime] NULL  
CONSTRAINT [PK_BrokerOrderHist] PRIMARY KEY CLUSTERED 
(
	[BrokerOrderHistId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ConnectionAction') AND type in (N'U')) DROP TABLE [dbo].[ConnectionAction]
GO

BEGIN
CREATE TABLE [dbo].[ConnectionAction](

	[ActorChanged] [bigint] NOT NULL  ,
	[ConnectionActionId] [bigint] NOT NULL IDENTITY(1, 1) ,
	[ConnectionStatus] [bit] NULL  ,
	[ConnectionType] [nvarchar](50)  NULL  ,
	[SessionId] [bigint] NOT NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_ConnectionAction] PRIMARY KEY CLUSTERED 
(
	[ConnectionActionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ContractNoticeDateHist') AND type in (N'U')) DROP TABLE [dbo].[ContractNoticeDateHist]
GO

BEGIN
CREATE TABLE [dbo].[ContractNoticeDateHist](

	[ActorChanged] [bigint] NOT NULL  ,
	[AffectSession] [bigint] NULL  ,
	[ContractNoticeDateHistId] [bigint] NOT NULL IDENTITY(1, 1) ,
	[ContractNoticeType] [int] NOT NULL  ,
	[CurrentNotifyDay] [datetime] NOT NULL  ,
	[Note] [nvarchar](300)  NULL  ,
	[NoticeStatus] [int] NULL  ,
	[NotifyDay] [datetime] NOT NULL  ,
	[NotifyMsg] [nvarchar](300)  NULL  ,
	[SmsNotifyMsg] [nvarchar](300)  NULL  ,
	[SmsRequest] [bit] NULL  ,
	[SymbolId] [bigint] NOT NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[UserId] [bigint] NOT NULL  
CONSTRAINT [PK_ContractNoticeDateHist] PRIMARY KEY CLUSTERED 
(
	[ContractNoticeDateHistId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ExecutionReportHist') AND type in (N'U')) DROP TABLE [dbo].[ExecutionReportHist]
GO

BEGIN
CREATE TABLE [dbo].[ExecutionReportHist](

	[Account] [nvarchar](100)  NULL  ,
	[AccountName] [nvarchar](100)  NULL  ,
	[ActorChanged] [bigint] NOT NULL  ,
	[ActorOrder] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[BaseSymbolId] [bigint] NULL  ,
	[ChainOrderId] [varchar](50)  NULL  ,
	[ClOrdID] [nvarchar](100)  NULL  ,
	[CustomerId] [bigint] NULL  ,
	[ExecId] [nvarchar](50)  NULL  ,
	[ExecValue] [ntext] NULL  ,
	[ExecutionReportHistId] [bigint] NOT NULL IDENTITY(1, 1) ,
	[ExecutionReportId] [bigint] NOT NULL  ,
	[LastStatus] [nvarchar](50)  NULL  ,
	[MemberId] [bigint] NULL  ,
	[Note] [nvarchar](100)  NULL  ,
	[OrdStatus] [nvarchar](50)  NULL  ,
	[OrderID] [nvarchar](100)  NULL  ,
	[OrigClOrdID] [nvarchar](100)  NULL  ,
	[ProcessStatus] [nvarchar](50)  NULL  ,
	[SymbolId] [bigint] NULL  ,
	[SymbolName] [nvarchar](50)  NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[TransactionTime] [datetime] NULL  
CONSTRAINT [PK_ExecutionReportHist] PRIMARY KEY CLUSTERED 
(
	[ExecutionReportHistId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ForexRateAction') AND type in (N'U')) DROP TABLE [dbo].[ForexRateAction]
GO

BEGIN
CREATE TABLE [dbo].[ForexRateAction](

	[ActorChanged] [bigint] NOT NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[ApprovalBy] [bigint] NULL  ,
	[ApprovalDate] [datetime] NULL  ,
	[CreateBy] [bigint] NOT NULL  ,
	[CreateDate] [datetime] NOT NULL  ,
	[ForexRateActionId] [bigint] NOT NULL IDENTITY(1, 1) ,
	[ForexRateId] [int] NOT NULL  ,
	[FromCurrency] [varchar](10)  NULL  ,
	[RateBuy] [decimal](28,6) NULL  ,
	[RateSell] [decimal](28,6) NULL  ,
	[Source] [varchar](50)  NULL  ,
	[Status] [int] NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[ToCurrency] [varchar](10)  NULL  ,
	[ValueDate] [datetime] NULL  
CONSTRAINT [PK_ForexRateAction] PRIMARY KEY CLUSTERED 
(
	[ForexRateActionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ForexRateDaily') AND type in (N'U')) DROP TABLE [dbo].[ForexRateDaily]
GO

BEGIN
CREATE TABLE [dbo].[ForexRateDaily](

	[ActorChanged] [bigint] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[ApprovalBy] [bigint] NULL  ,
	[ApprovalDate] [datetime] NULL  ,
	[CreateBy] [bigint] NULL  ,
	[CreateDate] [datetime] NULL  ,
	[ForexRateDailyId] [bigint] NOT NULL  ,
	[ForexRateId] [int] NULL  ,
	[FromCurrency] [varchar](10)  NULL  ,
	[IsPendingChange] [bit] NULL  ,
	[RateBuy] [decimal](28,6) NULL  ,
	[RateSell] [decimal](28,6) NULL  ,
	[SessionParentId] [bigint] NULL  ,
	[Source] [varchar](50)  NULL  ,
	[Status] [int] NULL  ,
	[TimeChanged] [datetime] NULL  ,
	[ToCurrency] [varchar](10)  NULL  ,
	[ValueDate] [datetime] NULL  
CONSTRAINT [PK_ForexRateDaily] PRIMARY KEY CLUSTERED 
(
	[ForexRateDailyId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ForexRateInternalAction') AND type in (N'U')) DROP TABLE [dbo].[ForexRateInternalAction]
GO

BEGIN
CREATE TABLE [dbo].[ForexRateInternalAction](

	[ActorChanged] [bigint] NOT NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[ApprovalBy] [bigint] NULL  ,
	[ApprovalDate] [datetime] NULL  ,
	[CreateBy] [bigint] NOT NULL  ,
	[CreateDate] [datetime] NOT NULL  ,
	[ForexRateId] [bigint] NULL  ,
	[ForexRateInternalActionId] [bigint] NOT NULL  ,
	[FromCurrency] [varchar](10)  NULL  ,
	[RateBuy] [decimal](28,6) NULL  ,
	[RateSell] [decimal](28,6) NULL  ,
	[Source] [varchar](50)  NULL  ,
	[Status] [int] NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[ToCurrency] [varchar](10)  NULL  ,
	[ValueDate] [datetime] NULL  
CONSTRAINT [PK_ForexRateInternalAction] PRIMARY KEY CLUSTERED 
(
	[ForexRateInternalActionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ForexRateInternalDaily') AND type in (N'U')) DROP TABLE [dbo].[ForexRateInternalDaily]
GO

BEGIN
CREATE TABLE [dbo].[ForexRateInternalDaily](

	[ActorChanged] [bigint] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[ApprovalBy] [bigint] NULL  ,
	[ApprovalDate] [datetime] NULL  ,
	[CreateBy] [bigint] NULL  ,
	[CreateDate] [datetime] NULL  ,
	[ForexRateId] [bigint] NULL  ,
	[ForexRateInternalDailyId] [bigint] NOT NULL  ,
	[FromCurrency] [varchar](10)  NULL  ,
	[IsPendingChange] [bit] NULL  ,
	[RateBuy] [decimal](28,6) NULL  ,
	[RateSell] [decimal](28,6) NULL  ,
	[SessionParentId] [bigint] NULL  ,
	[Source] [varchar](50)  NULL  ,
	[Status] [int] NULL  ,
	[TimeChanged] [datetime] NULL  ,
	[ToCurrency] [varchar](10)  NULL  ,
	[ValueDate] [datetime] NULL  
CONSTRAINT [PK_ForexRateInternalDaily] PRIMARY KEY CLUSTERED 
(
	[ForexRateInternalDailyId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'LinkedTransactionHist') AND type in (N'U')) DROP TABLE [dbo].[LinkedTransactionHist]
GO

BEGIN
CREATE TABLE [dbo].[LinkedTransactionHist](

	[AccountPartner] [varchar](50)  NULL  ,
	[ActorChanged] [bigint] NOT NULL  ,
	[AffectBranchId] [bigint] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[Amount] [decimal](28,6) NOT NULL  ,
	[BaseSymbolId] [bigint] NULL  ,
	[ChainOrderIdBuy] [varchar](50)  NULL  ,
	[ChainOrderIdSell] [varchar](50)  NULL  ,
	[ExecIdBuy] [varchar](50)  NULL  ,
	[ExecIdSell] [varchar](50)  NULL  ,
	[IndexRow] [bigint] NOT NULL  ,
	[IsSuccess] [bit] NULL  ,
	[LinkedTransactionHistId] [bigint] NOT NULL  ,
	[MemberId] [bigint] NULL  ,
	[OrderLinkedType] [varchar](50)  NULL  ,
	[OrderTransactionBuyId] [bigint] NULL  ,
	[OrderTransactionSellId] [bigint] NULL  ,
	[PartnerId] [bigint] NULL  ,
	[SessionDate] [datetime] NULL  ,
	[SymbolId] [bigint] NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_LinkedTransactionHist] PRIMARY KEY CLUSTERED 
(
	[LinkedTransactionHistId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ManualPriceAction') AND type in (N'U')) DROP TABLE [dbo].[ManualPriceAction]
GO

BEGIN
CREATE TABLE [dbo].[ManualPriceAction](

	[ActorChanged] [bigint] NOT NULL  ,
	[BaseSymbolId] [bigint] NOT NULL  ,
	[CreateBy] [bigint] NULL  ,
	[CreateDate] [datetime] NULL  ,
	[ManualPriceActionId] [bigint] NOT NULL IDENTITY(1, 1) ,
	[Price] [decimal](28,6) NOT NULL  ,
	[SessionId] [bigint] NOT NULL  ,
	[SymbolId] [bigint] NOT NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_ManualPriceAction] PRIMARY KEY CLUSTERED 
(
	[ManualPriceActionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'MemberLimitDaily') AND type in (N'U')) DROP TABLE [dbo].[MemberLimitDaily]
GO

BEGIN
CREATE TABLE [dbo].[MemberLimitDaily](

	[ActorChanged] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[Curency] [nvarchar](50)  NULL  ,
	[DetailMarginCreditLimit] [decimal](28,6) NULL  ,
	[ExpiredDayAlert] [int] NULL  ,
	[FeeOther] [decimal](28,6) NULL  ,
	[IMLimit] [decimal](28,6) NULL  ,
	[IsPendingChange] [bit] NULL  ,
	[MarginCalculationMethod] [int] NULL  ,
	[MarginCreditInterestRate] [decimal](28,6) NULL  ,
	[MarginCreditMaturityDate] [datetime] NULL  ,
	[MarginMultiplier] [decimal](28,6) NULL  ,
	[MemberId] [bigint] NULL  ,
	[MemberLimitDailyId] [bigint] NOT NULL  ,
	[PositionLimit] [decimal](28,6) NULL  ,
	[PositionLiquidationMethod] [int] NULL  ,
	[PositionTradingLimit] [decimal](28,6) NULL  ,
	[SplitTKKQ] [decimal](28,6) NULL  ,
	[StatusRatio] [decimal](28,6) NULL  ,
	[StoplossRatio] [decimal](28,6) NULL  ,
	[TckqRate] [decimal](28,6) NULL  ,
	[TimeChanged] [datetime] NULL  ,
	[TotalMarginCreditLimit] [decimal](28,6) NULL  ,
	[WarningRatio] [decimal](28,6) NULL  
CONSTRAINT [PK_MemberLimitDaily] PRIMARY KEY CLUSTERED 
(
	[MemberLimitDailyId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'OpenPositionDaily') AND type in (N'U')) DROP TABLE [dbo].[OpenPositionDaily]
GO

BEGIN
CREATE TABLE [dbo].[OpenPositionDaily](

	[ActorChanged] [bigint] NULL  ,
	[AffectAreaId] [bigint] NULL  ,
	[AffectBranchId] [bigint] NULL  ,
	[AffectBrandId] [bigint] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[AvgBuy] [decimal](28,6) NULL  ,
	[AvgBuyNet] [decimal](28,6) NULL  ,
	[AvgPrice] [decimal](28,6) NULL  ,
	[AvgPriceNet] [decimal](28,6) NULL  ,
	[AvgSell] [decimal](28,6) NULL  ,
	[AvgSellNet] [decimal](28,6) NULL  ,
	[BaseSymbolId] [bigint] NULL  ,
	[Currency] [varchar](10)  NULL  ,
	[IsPartner] [bit] NULL  ,
	[MemberId] [bigint] NULL  ,
	[OpenPositionDailyId] [bigint] NOT NULL  ,
	[PriceAction] [decimal](28,6) NULL  ,
	[SymbolId] [bigint] NULL  ,
	[TimeChanged] [datetime] NULL  ,
	[TotalBuy] [decimal](28,6) NULL  ,
	[TotalSell] [decimal](28,6) NULL  
CONSTRAINT [PK_OpenPositionDaily] PRIMARY KEY CLUSTERED 
(
	[OpenPositionDailyId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'OpenPositionDetailDaily') AND type in (N'U')) DROP TABLE [dbo].[OpenPositionDetailDaily]
GO

BEGIN
CREATE TABLE [dbo].[OpenPositionDetailDaily](

	[AccountPartner] [varchar](50)  NULL  ,
	[ActorChanged] [bigint] NOT NULL  ,
	[AffectAreaId] [bigint] NULL  ,
	[AffectBranchId] [bigint] NULL  ,
	[AffectBrandId] [bigint] NULL  ,
	[AffectMemberId] [bigint] NOT NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[BaseSymbolId] [bigint] NULL  ,
	[ChainOrderId] [varchar](50)  NULL  ,
	[Currency] [varchar](50)  NULL  ,
	[FilledQty] [decimal](28,6) NULL  ,
	[InitialMargin] [decimal](28,6) NULL  ,
	[InitialMarginMXV] [decimal](28,6) NULL  ,
	[IsBuy] [bit] NULL  ,
	[MaintainMargin] [decimal](28,6) NULL  ,
	[MemberId] [bigint] NULL  ,
	[OpenPositionDetailDailyId] [bigint] NOT NULL  ,
	[OpenQty] [decimal](28,6) NULL  ,
	[OrderTransactionId] [bigint] NULL  ,
	[PartnerId] [bigint] NULL  ,
	[PriceFill] [decimal](28,6) NULL  ,
	[QuantityFill] [decimal](28,6) NULL  ,
	[SpreadInitialMargin] [decimal](28,6) NULL  ,
	[SpreadMaintainMargin] [decimal](28,6) NULL  ,
	[SymbolId] [bigint] NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[TransactionTime] [datetime] NULL  
CONSTRAINT [PK_OpenPositionDetailDaily] PRIMARY KEY CLUSTERED 
(
	[OpenPositionDetailDailyId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'OpenPositionDetailHist') AND type in (N'U')) DROP TABLE [dbo].[OpenPositionDetailHist]
GO

BEGIN
CREATE TABLE [dbo].[OpenPositionDetailHist](

	[AccountPartner] [varchar](50)  NULL  ,
	[ActorChanged] [bigint] NOT NULL  ,
	[AffectAreaId] [bigint] NULL  ,
	[AffectBranchId] [bigint] NULL  ,
	[AffectBrandId] [bigint] NULL  ,
	[AffectMemberId] [bigint] NOT NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[BaseSymbolId] [bigint] NULL  ,
	[ChainOrderId] [varchar](50)  NULL  ,
	[Currency] [varchar](50)  NULL  ,
	[FilledQty] [decimal](28,6) NULL  ,
	[InitialMargin] [decimal](28,6) NULL  ,
	[InitialMarginMXV] [decimal](28,6) NULL  ,
	[IsBuy] [bit] NULL  ,
	[MaintainMargin] [decimal](28,6) NULL  ,
	[MemberId] [bigint] NULL  ,
	[OpenPositionDetailHistId] [bigint] NOT NULL IDENTITY(1, 1) ,
	[OpenQty] [decimal](28,6) NULL  ,
	[OrderTransactionId] [bigint] NULL  ,
	[PartnerId] [bigint] NULL  ,
	[PriceFill] [decimal](28,6) NULL  ,
	[QuantityFill] [decimal](28,6) NULL  ,
	[SpreadInitialMargin] [decimal](28,6) NULL  ,
	[SpreadMaintainMargin] [decimal](28,6) NULL  ,
	[SymbolId] [bigint] NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[TransactionTime] [datetime] NULL  
CONSTRAINT [PK_OpenPositionDetailHist] PRIMARY KEY CLUSTERED 
(
	[OpenPositionDetailHistId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'OpenPositionHist') AND type in (N'U')) DROP TABLE [dbo].[OpenPositionHist]
GO

BEGIN
CREATE TABLE [dbo].[OpenPositionHist](

	[ActorChanged] [bigint] NOT NULL  ,
	[AffectAreaId] [bigint] NULL  ,
	[AffectBranchId] [bigint] NULL  ,
	[AffectBrandId] [bigint] NULL  ,
	[AffectMemberId] [bigint] NOT NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[AvgBuy] [decimal](28,6) NULL  ,
	[AvgBuyNet] [decimal](28,6) NULL  ,
	[AvgPrice] [decimal](28,6) NULL  ,
	[AvgPriceNet] [decimal](28,6) NULL  ,
	[AvgSell] [decimal](28,6) NULL  ,
	[AvgSellNet] [decimal](28,6) NULL  ,
	[BaseSymbolId] [bigint] NULL  ,
	[Currency] [varchar](50)  NULL  ,
	[IsPartner] [bit] NULL  ,
	[MemberId] [bigint] NULL  ,
	[OpenPositionHistId] [bigint] NOT NULL  ,
	[SymbolId] [bigint] NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[TotalBuy] [decimal](28,6) NULL  ,
	[TotalSell] [decimal](28,6) NULL  
CONSTRAINT [PK_OpenPositionHist] PRIMARY KEY CLUSTERED 
(
	[OpenPositionHistId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'OrderTransactionAction') AND type in (N'U')) DROP TABLE [dbo].[OrderTransactionAction]
GO

BEGIN
CREATE TABLE [dbo].[OrderTransactionAction](

	[AccountId] [varchar](30)  NULL  ,
	[AccountPartner] [varchar](50)  NULL  ,
	[Actor] [bigint] NULL  ,
	[ActorChanged] [bigint] NULL  ,
	[ActorUpdate] [bigint] NULL  ,
	[AffectBranchId] [bigint] NULL  ,
	[AffectMemberActor] [bigint] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[ApprovalSession] [bigint] NULL  ,
	[ApprovalStatus] [varchar](100)  NULL  ,
	[AvgPx] [decimal](28,6) NULL  ,
	[BaseSymbolId] [bigint] NULL  ,
	[BrokerId] [int] NULL  ,
	[BrokerOrderId] [varchar](50)  NULL  ,
	[ChainOrderId] [varchar](50)  NULL  ,
	[ClientOrderId] [varchar](50)  NULL  ,
	[CumQty] [decimal](28,6) NULL  ,
	[Currency] [varchar](30)  NULL  ,
	[CustomerId] [bigint] NULL  ,
	[DeliverToCompId] [nvarchar](50)  NULL  ,
	[DeliverToSubId] [nvarchar](50)  NULL  ,
	[DoneTime] [datetime] NULL  ,
	[ExecId] [nvarchar](50)  NULL  ,
	[ExpiredDate] [datetime] NULL  ,
	[FeatureOrder] [nvarchar](50)  NULL  ,
	[FeeCustomer] [decimal](28,6) NULL  ,
	[FeeMG] [decimal](28,6) NULL  ,
	[FeePartner] [decimal](28,6) NULL  ,
	[FeeTV] [decimal](28,6) NULL  ,
	[InitTime] [datetime] NULL  ,
	[IsBuy] [bit] NULL  ,
	[IsManualOrder] [bit] NULL  ,
	[MaturityDate] [datetime] NULL  ,
	[Note] [nvarchar](200)  NULL  ,
	[OrderLink] [varchar](50)  NULL  ,
	[OrderTransactionActionId] [bigint] NOT NULL IDENTITY(1, 1) ,
	[OrderTransactionId] [bigint] NULL  ,
	[OrderType] [varchar](50)  NULL  ,
	[OrderTypeEnum] [nvarchar](100)  NULL  ,
	[PartnerId] [bigint] NULL  ,
	[Price] [decimal](28,6) NULL  ,
	[Quantity] [decimal](28,6) NULL  ,
	[StopPx] [decimal](28,6) NULL  ,
	[SymbolId] [bigint] NULL  ,
	[SymbolName] [varchar](100)  NULL  ,
	[Text] [varchar](200)  NULL  ,
	[TimeChanged] [datetime] NULL  ,
	[TimeInForce] [varchar](50)  NULL  
CONSTRAINT [PK_OrderTransactionAction] PRIMARY KEY CLUSTERED 
(
	[OrderTransactionActionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'OrderTransactionDaily') AND type in (N'U')) DROP TABLE [dbo].[OrderTransactionDaily]
GO

BEGIN
CREATE TABLE [dbo].[OrderTransactionDaily](

	[AccountId] [varchar](30)  NULL  ,
	[AccountPartner] [varchar](50)  NULL  ,
	[Actor] [bigint] NULL  ,
	[ActorChanged] [bigint] NULL  ,
	[ActorUpdate] [bigint] NULL  ,
	[AffectBranchId] [bigint] NULL  ,
	[AffectMemberActor] [bigint] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[ApprovalSession] [bigint] NULL  ,
	[ApprovalStatus] [varchar](100)  NULL  ,
	[AvgPx] [decimal](28,6) NULL  ,
	[BaseSymbolId] [bigint] NULL  ,
	[BrokerId] [int] NULL  ,
	[BrokerOrderId] [varchar](50)  NULL  ,
	[ChainOrderId] [varchar](50)  NULL  ,
	[ClientOrderId] [varchar](50)  NULL  ,
	[CumQty] [decimal](28,6) NULL  ,
	[Currency] [varchar](30)  NULL  ,
	[CustomerId] [bigint] NULL  ,
	[DeliverToCompId] [nvarchar](50)  NULL  ,
	[DeliverToSubId] [nvarchar](50)  NULL  ,
	[DoneTime] [datetime] NULL  ,
	[ExecId] [nvarchar](50)  NULL  ,
	[ExpiredDate] [datetime] NULL  ,
	[FeatureOrder] [nvarchar](50)  NULL  ,
	[FeeCustomer] [decimal](28,6) NULL  ,
	[FeeMG] [decimal](28,6) NULL  ,
	[FeePartner] [decimal](28,6) NULL  ,
	[FeeTV] [decimal](28,6) NULL  ,
	[InitTime] [datetime] NULL  ,
	[IsBuy] [bit] NULL  ,
	[IsManualOrder] [bit] NULL  ,
	[IsUpdateTradingDeal] [bit] NULL  ,
	[MaturityDate] [datetime] NULL  ,
	[Note] [nvarchar](200)  NULL  ,
	[OrderLink] [varchar](50)  NULL  ,
	[OrderTransactionDailyId] [bigint] NOT NULL  ,
	[OrderTransactionId] [bigint] NULL  ,
	[OrderType] [varchar](50)  NULL  ,
	[OrderTypeEnum] [nvarchar](100)  NULL  ,
	[PartnerId] [bigint] NULL  ,
	[Price] [decimal](28,6) NULL  ,
	[Quantity] [decimal](28,6) NULL  ,
	[StopPx] [decimal](28,6) NULL  ,
	[SymbolId] [bigint] NULL  ,
	[SymbolName] [varchar](100)  NULL  ,
	[Text] [varchar](200)  NULL  ,
	[TimeChanged] [datetime] NULL  ,
	[TimeInForce] [varchar](50)  NULL  
CONSTRAINT [PK_OrderTransactionDaily] PRIMARY KEY CLUSTERED 
(
	[OrderTransactionDailyId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'OrderTransactionHist') AND type in (N'U')) DROP TABLE [dbo].[OrderTransactionHist]
GO

BEGIN
CREATE TABLE [dbo].[OrderTransactionHist](

	[AccountId] [varchar](30)  NULL  ,
	[AccountPartner] [varchar](50)  NULL  ,
	[Actor] [bigint] NULL  ,
	[ActorChanged] [bigint] NULL  ,
	[ActorUpdate] [bigint] NULL  ,
	[AffectBranchId] [bigint] NULL  ,
	[AffectMemberActor] [bigint] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[ApprovalSession] [bigint] NULL  ,
	[ApprovalStatus] [varchar](100)  NULL  ,
	[AvgPx] [decimal](28,6) NULL  ,
	[BaseSymbolId] [bigint] NULL  ,
	[BrokerId] [int] NULL  ,
	[BrokerOrderId] [varchar](50)  NULL  ,
	[ChainOrderId] [varchar](50)  NULL  ,
	[ClientOrderId] [varchar](50)  NULL  ,
	[CumQty] [decimal](28,6) NULL  ,
	[Currency] [varchar](30)  NULL  ,
	[CustomerId] [bigint] NULL  ,
	[DeliverToCompId] [nvarchar](50)  NULL  ,
	[DeliverToSubId] [nvarchar](50)  NULL  ,
	[DoneTime] [datetime] NULL  ,
	[ExecId] [nvarchar](50)  NULL  ,
	[ExpiredDate] [datetime] NULL  ,
	[FeatureOrder] [nvarchar](50)  NULL  ,
	[FeeCustomer] [decimal](28,6) NULL  ,
	[FeeMG] [decimal](28,6) NULL  ,
	[FeePartner] [decimal](28,6) NULL  ,
	[FeeTV] [decimal](28,6) NULL  ,
	[InitTime] [datetime] NULL  ,
	[IsBuy] [bit] NULL  ,
	[IsManualOrder] [bit] NULL  ,
	[MaturityDate] [datetime] NULL  ,
	[Note] [nvarchar](200)  NULL  ,
	[OrderLink] [varchar](50)  NULL  ,
	[OrderTransactionHistId] [bigint] NOT NULL  ,
	[OrderTransactionId] [bigint] NULL  ,
	[OrderType] [varchar](50)  NULL  ,
	[OrderTypeEnum] [nvarchar](100)  NULL  ,
	[PartnerId] [bigint] NULL  ,
	[Price] [decimal](28,6) NULL  ,
	[Quantity] [decimal](28,6) NULL  ,
	[StopPx] [decimal](28,6) NULL  ,
	[SymbolId] [bigint] NULL  ,
	[SymbolName] [varchar](100)  NULL  ,
	[Text] [varchar](200)  NULL  ,
	[TimeChanged] [datetime] NULL  ,
	[TimeInForce] [varchar](50)  NULL  
CONSTRAINT [PK_OrderTransactionHist] PRIMARY KEY CLUSTERED 
(
	[OrderTransactionHistId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SessionHistoryMove') AND type in (N'U')) DROP TABLE [dbo].[SessionHistoryMove]
GO

BEGIN
CREATE TABLE [dbo].[SessionHistoryMove](

	[ActiveStatus] [int] NULL  ,
	[ActorChanged] [bigint] NOT NULL  ,
	[BusinessDate] [datetime] NULL  ,
	[CurrentState] [nvarchar](100)  NULL  ,
	[Description] [nvarchar](100)  NULL  ,
	[EndTime] [datetime] NULL  ,
	[IsSessionWorking] [bit] NULL  ,
	[LastState] [nvarchar](100)  NULL  ,
	[SessionId] [bigint] NOT NULL  ,
	[SessionParentId] [bigint] NULL  ,
	[SessionType] [nvarchar](50)  NULL  ,
	[StartTime] [datetime] NULL  ,
	[TaskNumber] [int] NULL  ,
	[TaskStatus] [nvarchar](100)  NULL  ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_SessionHistoryMove] PRIMARY KEY CLUSTERED 
(
	[SessionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SpecAccountingHist') AND type in (N'U')) DROP TABLE [dbo].[SpecAccountingHist]
GO

BEGIN
CREATE TABLE [dbo].[SpecAccountingHist](

	[AccountCR] [varchar](50)  NULL  ,
	[AccountDR] [varchar](50)  NULL  ,
	[ActorChanged] [bigint] NOT NULL  ,
	[Amount] [decimal](28,6) NULL  ,
	[AmountOrginal] [decimal](28,6) NULL  ,
	[BaseSymbolId] [bigint] NULL  ,
	[ContractName] [nvarchar](50)  NULL  ,
	[Currency] [varchar](50)  NULL  ,
	[CurrencyOrginal] [varchar](50)  NULL  ,
	[DescriptionCR] [nvarchar](500)  NULL  ,
	[DescriptionDR] [nvarchar](500)  NULL  ,
	[DescriptionRef] [nvarchar](500)  NULL  ,
	[DescriptionTrading] [nvarchar](250)  NULL  ,
	[DisplayMemberName] [nvarchar](50)  NULL  ,
	[ExecId] [varchar](50)  NULL  ,
	[IsBuy] [bit] NULL  ,
	[IsReversal] [bit] NULL  ,
	[Note] [ntext] NULL  ,
	[OrderTransactionId] [bigint] NULL  ,
	[OrderTransactionIdBuy] [bigint] NULL  ,
	[OrderTransactionIdSell] [bigint] NULL  ,
	[ParentSessionId] [bigint] NULL  ,
	[PartnerCode] [nvarchar](50)  NULL  ,
	[PostDate] [datetime] NULL  ,
	[QuantityLinked] [decimal](28,6) NULL  ,
	[Remark] [nvarchar](250)  NULL  ,
	[SessionId] [bigint] NULL  ,
	[SourceBranchCode] [int] NULL  ,
	[SpecAccountingEnum] [varchar](100)  NULL  ,
	[SpecAccountingHistId] [bigint] NOT NULL IDENTITY(1, 1) ,
	[SpecAccountingId] [bigint] NOT NULL  ,
	[SymbolId] [bigint] NULL  ,
	[SyncStatus] [int] NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[TransBranchCode] [int] NULL  ,
	[ValueDate] [datetime] NULL  
CONSTRAINT [PK_SpecAccountingHist] PRIMARY KEY CLUSTERED 
(
	[SpecAccountingHistId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'StateChangedAccount') AND type in (N'U')) DROP TABLE [dbo].[StateChangedAccount]
GO

BEGIN
CREATE TABLE [dbo].[StateChangedAccount](

	[AccountId] [nvarchar](50)  NULL  ,
	[ActorChanged] [bigint] NOT NULL  ,
	[Amount] [decimal](28,6) NULL  ,
	[Description] [nvarchar](250)  NULL  ,
	[Hold] [decimal](28,6) NULL  ,
	[Id] [bigint] NOT NULL IDENTITY(1, 1) ,
	[TimeChanged] [datetime] NOT NULL  
CONSTRAINT [PK_StateChangedAccount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SymbolCQGAction') AND type in (N'U')) DROP TABLE [dbo].[SymbolCQGAction]
GO

BEGIN
CREATE TABLE [dbo].[SymbolCQGAction](

	[ActorChanged] [bigint] NOT NULL  ,
	[BaseSymbolCQGId] [bigint] NULL  ,
	[ContractName] [nvarchar](100)  NULL  ,
	[FirstNotifyDay] [datetime] NULL  ,
	[InitialMargin] [decimal](28,6) NULL  ,
	[InitialMarginMXV] [decimal](28,6) NULL  ,
	[IsPendingChange] [bit] NULL  ,
	[LastTradingDay] [datetime] NULL  ,
	[MaintainMargin] [decimal](28,6) NULL  ,
	[MarginRefSource] [int] NULL  ,
	[MothExpiryCode] [nvarchar](50)  NULL  ,
	[Note] [nvarchar](200)  NULL  ,
	[SpreadInitialMargin] [decimal](28,6) NULL  ,
	[SpreadMaintainMargin] [decimal](28,6) NULL  ,
	[SymbolCQGActionId] [bigint] NOT NULL IDENTITY(1, 1) ,
	[SymbolCQGId] [bigint] NULL  ,
	[SymbolExpiry] [nvarchar](50)  NULL  ,
	[SymbolName] [nvarchar](100)  NULL  ,
	[SymbolStatus] [int] NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[TradingType] [int] NULL  ,
	[YearExpiryCode] [nvarchar](50)  NULL  
CONSTRAINT [PK_SymbolCQGAction] PRIMARY KEY CLUSTERED 
(
	[SymbolCQGActionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SymbolSettlementPriceAction') AND type in (N'U')) DROP TABLE [dbo].[SymbolSettlementPriceAction]
GO

BEGIN
CREATE TABLE [dbo].[SymbolSettlementPriceAction](

	[ActorChanged] [bigint] NOT NULL  ,
	[ApprovalBy] [bigint] NULL  ,
	[ApprovalDate] [datetime] NULL  ,
	[BusinessDate] [datetime] NULL  ,
	[CreateBy] [bigint] NULL  ,
	[CreateDate] [datetime] NULL  ,
	[ParentSessionId] [bigint] NULL  ,
	[SettlementPrice] [decimal](28,6) NULL  ,
	[SymbolId] [bigint] NULL  ,
	[SymbolSettlementPriceActionId] [bigint] NOT NULL IDENTITY(1, 1) ,
	[TimeChanged] [datetime] NOT NULL  ,
	[YesterdaySettlement] [decimal](28,6) NULL  
CONSTRAINT [PK_SymbolSettlementPriceAction] PRIMARY KEY CLUSTERED 
(
	[SymbolSettlementPriceActionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SymbolSettlementPriceDaily') AND type in (N'U')) DROP TABLE [dbo].[SymbolSettlementPriceDaily]
GO

BEGIN
CREATE TABLE [dbo].[SymbolSettlementPriceDaily](

	[ActorChanged] [bigint] NOT NULL  ,
	[ApprovalBy] [bigint] NULL  ,
	[ApprovalDate] [datetime] NULL  ,
	[BusinessDate] [datetime] NULL  ,
	[CreateBy] [bigint] NULL  ,
	[CreateDate] [datetime] NULL  ,
	[ParentSessionId] [bigint] NOT NULL  ,
	[SettlementPrice] [decimal](28,6) NULL  ,
	[SymbolId] [bigint] NOT NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[YesterdaySettlement] [decimal](28,6) NULL  
CONSTRAINT [PK_SymbolSettlementPriceDaily] PRIMARY KEY CLUSTERED 
(
	[ParentSessionId] ASC,
	[SymbolId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SystemConfigAction') AND type in (N'U')) DROP TABLE [dbo].[SystemConfigAction]
GO

BEGIN
CREATE TABLE [dbo].[SystemConfigAction](

	[ActorChanged] [bigint] NOT NULL  ,
	[Description] [nvarchar](250)  NULL  ,
	[Name] [varchar](250)  NULL  ,
	[SystemConfigActionId] [bigint] NOT NULL IDENTITY(1, 1) ,
	[SystemConfigType] [int] NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[Value] [ntext] NULL  
CONSTRAINT [PK_SystemConfigAction] PRIMARY KEY CLUSTERED 
(
	[SystemConfigActionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'TradingDealAction') AND type in (N'U')) DROP TABLE [dbo].[TradingDealAction]
GO

BEGIN
CREATE TABLE [dbo].[TradingDealAction](

	[AcceptOrder] [bigint] NULL  ,
	[AcceptOrderMember] [bigint] NULL  ,
	[AccountPartner] [nvarchar](50)  NULL  ,
	[Actor] [bigint] NULL  ,
	[ActorChanged] [bigint] NOT NULL  ,
	[ActorUpdate] [bigint] NULL  ,
	[AffectAreaId] [int] NULL  ,
	[AffectBranchId] [bigint] NULL  ,
	[AffectBrandId] [int] NULL  ,
	[AffectMemberActor] [bigint] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[AvgPx] [decimal](28,6) NULL  ,
	[BaseSymbolId] [bigint] NULL  ,
	[BrokerId] [int] NULL  ,
	[ChainOrderId] [varchar](50)  NULL  ,
	[ClientOrderId] [varchar](50)  NOT NULL  ,
	[Commission] [decimal](28,6) NULL  ,
	[CostPrice] [decimal](28,6) NULL  ,
	[CumQty] [decimal](28,6) NULL  ,
	[Currency] [varchar](50)  NULL  ,
	[CurrentState] [nvarchar](100)  NULL  ,
	[CustomerId] [bigint] NULL  ,
	[DeliverToCompId] [nvarchar](50)  NULL  ,
	[DeliverToSubId] [nvarchar](50)  NULL  ,
	[DoneTime] [datetime] NULL  ,
	[EffectiveTime] [datetime] NULL  ,
	[ExpireDate] [datetime] NULL  ,
	[ExpireTime] [datetime] NULL  ,
	[FeatureOrder] [nvarchar](50)  NULL  ,
	[FeeCustomer] [decimal](28,6) NULL  ,
	[FeeMG] [decimal](28,6) NULL  ,
	[FeePartner] [decimal](28,6) NULL  ,
	[FeeTV] [decimal](28,6) NULL  ,
	[InitTime] [datetime] NULL  ,
	[InitialMargin] [decimal](28,6) NULL  ,
	[InitialMarginMXV] [decimal](28,6) NULL  ,
	[IsBuy] [bit] NULL  ,
	[IsManualOrder] [bit] NULL  ,
	[LastState] [nvarchar](100)  NULL  ,
	[LastTrigger] [nvarchar](100)  NULL  ,
	[LeavesQty] [decimal](28,6) NULL  ,
	[MainBrokerOrderId] [varchar](50)  NULL  ,
	[MainExchangeOrderId] [varchar](50)  NULL  ,
	[MaintainMargin] [decimal](28,6) NULL  ,
	[Note] [nvarchar](100)  NULL  ,
	[OrderScrope] [nvarchar](100)  NULL  ,
	[OrderSource] [varchar](50)  NULL  ,
	[OrderType] [nvarchar](50)  NULL  ,
	[OrderTypeEnum] [nvarchar](100)  NULL  ,
	[PartnerId] [bigint] NULL  ,
	[Price] [decimal](28,6) NULL  ,
	[PriceCustomer] [decimal](28,6) NULL  ,
	[Quantity] [decimal](28,6) NULL  ,
	[SecondaryOrderId] [varchar](50)  NULL  ,
	[SenderCompId] [nvarchar](50)  NULL  ,
	[SpreadInitialMargin] [decimal](28,6) NULL  ,
	[SpreadMaintainMargin] [decimal](28,6) NULL  ,
	[StopPx] [decimal](28,6) NULL  ,
	[SymbolId] [bigint] NULL  ,
	[SymbolName] [nvarchar](50)  NULL  ,
	[TargetCompId] [nvarchar](50)  NULL  ,
	[TargetSubId] [nvarchar](50)  NULL  ,
	[TaskNameString] [nvarchar](100)  NULL  ,
	[Text] [nvarchar](200)  NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[TimeInForce] [varchar](50)  NULL  ,
	[TradingDealActionId] [bigint] NOT NULL IDENTITY(1, 1) ,
	[TransactTime] [datetime] NULL  
CONSTRAINT [PK_TradingDealAction] PRIMARY KEY CLUSTERED 
(
	[TradingDealActionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'TradingDealHist') AND type in (N'U')) DROP TABLE [dbo].[TradingDealHist]
GO

BEGIN
CREATE TABLE [dbo].[TradingDealHist](

	[AcceptOrder] [bigint] NULL  ,
	[AcceptOrderMember] [bigint] NULL  ,
	[AccountPartner] [nvarchar](50)  NULL  ,
	[Actor] [bigint] NULL  ,
	[ActorChanged] [bigint] NOT NULL  ,
	[ActorUpdate] [bigint] NULL  ,
	[AffectAreaId] [int] NULL  ,
	[AffectBranchId] [bigint] NULL  ,
	[AffectBrandId] [int] NULL  ,
	[AffectMemberActor] [bigint] NULL  ,
	[AffectMemberId] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[AvgPx] [decimal](28,6) NULL  ,
	[BaseSymbolId] [bigint] NULL  ,
	[BrokerId] [int] NULL  ,
	[ChainOrderId] [varchar](50)  NULL  ,
	[ClientOrderId] [varchar](50)  NOT NULL  ,
	[Commission] [decimal](28,6) NULL  ,
	[CostPrice] [decimal](28,6) NULL  ,
	[CumQty] [decimal](28,6) NULL  ,
	[Currency] [varchar](50)  NULL  ,
	[CurrentState] [nvarchar](100)  NULL  ,
	[CustomerId] [bigint] NULL  ,
	[DeliverToCompId] [nvarchar](50)  NULL  ,
	[DeliverToSubId] [nvarchar](50)  NULL  ,
	[DoneTime] [datetime] NULL  ,
	[EffectiveTime] [datetime] NULL  ,
	[ExpireDate] [datetime] NULL  ,
	[ExpireTime] [datetime] NULL  ,
	[FeatureOrder] [nvarchar](50)  NULL  ,
	[FeeCustomer] [decimal](28,6) NULL  ,
	[FeeMG] [decimal](28,6) NULL  ,
	[FeePartner] [decimal](28,6) NULL  ,
	[FeeTV] [decimal](28,6) NULL  ,
	[InitTime] [datetime] NULL  ,
	[InitialMargin] [decimal](28,6) NULL  ,
	[InitialMarginMXV] [decimal](28,6) NULL  ,
	[IsBuy] [bit] NULL  ,
	[IsManualOrder] [bit] NULL  ,
	[LastState] [nvarchar](100)  NULL  ,
	[LastTrigger] [nvarchar](100)  NULL  ,
	[LeavesQty] [decimal](28,6) NULL  ,
	[MainBrokerOrderId] [varchar](50)  NULL  ,
	[MainExchangeOrderId] [varchar](50)  NULL  ,
	[MaintainMargin] [decimal](28,6) NULL  ,
	[Note] [nvarchar](100)  NULL  ,
	[OrderScrope] [nvarchar](100)  NULL  ,
	[OrderSource] [varchar](50)  NULL  ,
	[OrderType] [nvarchar](50)  NULL  ,
	[OrderTypeEnum] [nvarchar](100)  NULL  ,
	[PartnerId] [bigint] NULL  ,
	[Price] [decimal](28,6) NULL  ,
	[PriceCustomer] [decimal](28,6) NULL  ,
	[Quantity] [decimal](28,6) NULL  ,
	[SecondaryOrderId] [varchar](50)  NULL  ,
	[SenderCompId] [nvarchar](50)  NULL  ,
	[SpreadInitialMargin] [decimal](28,6) NULL  ,
	[SpreadMaintainMargin] [decimal](28,6) NULL  ,
	[StopPx] [decimal](28,6) NULL  ,
	[SymbolId] [bigint] NULL  ,
	[SymbolName] [varchar](50)  NULL  ,
	[TargetCompId] [nvarchar](50)  NULL  ,
	[TargetSubId] [nvarchar](50)  NULL  ,
	[TaskNameString] [nvarchar](100)  NULL  ,
	[Text] [nvarchar](200)  NULL  ,
	[TimeChanged] [datetime] NOT NULL  ,
	[TimeInForce] [varchar](50)  NULL  ,
	[TradingDealHistId] [bigint] NOT NULL IDENTITY(1, 1) ,
	[TransactTime] [datetime] NULL  
CONSTRAINT [PK_TradingDealHist] PRIMARY KEY CLUSTERED 
(
	[TradingDealHistId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SystemMessage') AND type in (N'U')) DROP TABLE [dbo].[SystemMessage]
GO

BEGIN
CREATE TABLE [dbo].[SystemMessage](

	[ActorChanged] [bigint] NULL  ,
	[AffectSessionId] [bigint] NULL  ,
	[AttachName] [varchar](250)  NULL  ,
	[Content] [ntext] NULL  ,
	[FromUser] [bigint] NULL  ,
	[SystemMessageId] [bigint] NOT NULL IDENTITY(1, 1) ,
	[TimeChanged] [datetime] NOT NULL  ,
	[ToUsers] [nvarchar](MAX)  NULL  
CONSTRAINT [PK_SystemMessage] PRIMARY KEY CLUSTERED 
(
	[SystemMessageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY] 
END
GO




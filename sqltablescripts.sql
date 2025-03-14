USE [db_aa6a53_pcnet]
GO
/****** Object:  Table [dbo].[Bo]    Script Date: 11/09/2024 6:55:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bo](
	[OID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Branch] [int] NULL,
	[BoDate] [datetime] NULL,
	[Supplier] [int] NULL,
	[Remark] [nvarchar](100) NULL,
	[BoStatus] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[LastModifiedDate] [datetime] NULL,
	[LastModifiedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL,
 CONSTRAINT [PK_Bo] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BoDetail]    Script Date: 11/09/2024 6:55:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BoDetail](
	[OID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Bo] [int] NULL,
	[Stock] [int] NULL,
	[Quantity] [int] NULL,
	[Remarks] [nvarchar](100) NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL,
	[Branch] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[LastModifiedDate] [datetime] NULL,
	[LastModifiedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[ItemName] [nvarchar](100) NULL,
 CONSTRAINT [PK_BoDetail] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BoSerial]    Script Date: 11/09/2024 6:55:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BoSerial](
	[OID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[SerialNumber] [nvarchar](100) NULL,
	[Remark] [nvarchar](100) NULL,
	[BoDetail] [int] NULL,
	[LastModifiedDate] [datetime] NULL,
	[LastModifiedBy] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL,
 CONSTRAINT [PK_BoSerial] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Branch]    Script Date: 11/09/2024 6:55:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Branch](
	[OID] [int] NOT NULL,
	[BranchName] [nvarchar](100) NULL,
	[Address] [nvarchar](100) NULL,
	[ContactNo] [nvarchar](100) NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL,
 CONSTRAINT [PK_Branch] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Branches]    Script Date: 11/09/2024 6:55:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Branches](
	[OID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[BranchId] [int] NULL,
	[BranchName] [nvarchar](100) NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL,
 CONSTRAINT [PK_Branches] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BranchStock]    Script Date: 11/09/2024 6:55:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BranchStock](
	[OID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Branch] [int] NULL,
	[Stock] [int] NULL,
	[Quantity] [int] NULL,
	[Cost] [float] NULL,
	[Price] [float] NULL,
	[Active] [bit] NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL,
 CONSTRAINT [PK_BranchStock] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Brand]    Script Date: 11/09/2024 6:55:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brand](
	[OID] [int] IDENTITY(1,1) NOT NULL,
	[BrandName] [nvarchar](100) NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL,
 CONSTRAINT [PK_Brand] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 11/09/2024 6:55:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[OID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](100) NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 11/09/2024 6:55:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[OID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Address] [nvarchar](100) NULL,
	[ContactNumber] [nvarchar](100) NULL,
	[Email] [nvarchar](100) NULL,
	[Balance] [money] NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[LastModifiedDate] [datetime] NULL,
	[LastModifiedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerPayment]    Script Date: 11/09/2024 6:55:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerPayment](
	[OID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[PaymentDate] [datetime] NULL,
	[Amount] [float] NULL,
	[Remark] [nvarchar](100) NULL,
	[SaleHeader] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[LastModifiedDate] [datetime] NULL,
	[LastModifiedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL,
 CONSTRAINT [PK_CustomerPayment] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeliveryDetail]    Script Date: 11/09/2024 6:55:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeliveryDetail](
	[OID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[DeliveryHeader] [int] NULL,
	[Stock] [int] NULL,
	[Cost] [float] NULL,
	[QtyDelivered] [int] NULL,
	[Discount] [float] NULL,
	[Remark] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[LastModifiedDate] [datetime] NULL,
	[LastModifiedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Branch] [int] NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL,
	[ItemName] [nvarchar](100) NULL,
 CONSTRAINT [PK_DeliveryDetail] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeliveryHeader]    Script Date: 11/09/2024 6:55:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeliveryHeader](
	[OID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Branch] [int] NULL,
	[DateDelivered] [datetime] NULL,
	[InvoiceNumber] [nvarchar](15) NULL,
	[Supplier] [int] NULL,
	[Discount] [float] NULL,
	[Remarks] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[LastModifiedDate] [datetime] NULL,
	[LastModifiedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[DeliveryStatus] [int] NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL,
 CONSTRAINT [PK_DeliveryHeader] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeliverySerial]    Script Date: 11/09/2024 6:55:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeliverySerial](
	[OID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[SerialNumber] [nvarchar](100) NULL,
	[Remark] [nvarchar](100) NULL,
	[DeliveryDetail] [int] NULL,
	[LastModifiedDate] [datetime] NULL,
	[LastModifiedBy] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL,
 CONSTRAINT [PK_DeliverySerial] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 11/09/2024 6:55:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[OID] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [nvarchar](100) NULL,
	[Branch] [int] NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemLedger]    Script Date: 11/09/2024 6:55:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemLedger](
	[OID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[TransactionType] [nvarchar](100) NULL,
	[TransactionDate] [datetime] NULL,
	[UserID] [nvarchar](100) NULL,
	[BranchId] [int] NULL,
	[BranchName] [nvarchar](100) NULL,
	[InvoiceNumber] [nvarchar](100) NULL,
	[TransactionOid] [int] NULL,
	[StockId] [int] NULL,
	[ItemName] [nvarchar](100) NULL,
	[PreviousQty] [int] NULL,
	[CurrentQty] [nvarchar](100) NULL,
	[QtyChange] [int] NULL,
	[Remarks] [nvarchar](100) NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL,
	[PreviousEditedQty] [int] NULL,
	[CurrentEditedQty] [int] NULL,
 CONSTRAINT [PK_ItemLedger] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemSize]    Script Date: 11/09/2024 6:55:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemSize](
	[OID] [int] IDENTITY(1,1) NOT NULL,
	[SizeName] [nvarchar](100) NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL,
 CONSTRAINT [PK_ItemSize] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemType]    Script Date: 11/09/2024 6:55:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemType](
	[OID] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](100) NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL,
 CONSTRAINT [PK_ItemType] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ManualInventory]    Script Date: 11/09/2024 6:55:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ManualInventory](
	[OID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[InventoryDate] [datetime] NULL,
	[Branch] [int] NULL,
	[Remarks] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[LastModifiedDate] [datetime] NULL,
	[LastModifiedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL,
	[ManualInvStatus] [int] NULL,
 CONSTRAINT [PK_ManualInventory] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ManualInventoryDetail]    Script Date: 11/09/2024 6:55:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ManualInventoryDetail](
	[OID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[ManualInventory] [int] NULL,
	[Stock] [int] NULL,
	[Quantity] [int] NULL,
	[Remark] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[LastModifiedDate] [datetime] NULL,
	[LastModifiedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL,
	[Branch] [int] NULL,
 CONSTRAINT [PK_ManualInventoryDetail] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SaleDetail]    Script Date: 11/09/2024 6:55:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleDetail](
	[OID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[SaleHeader] [int] NULL,
	[Stock] [int] NULL,
	[Price] [money] NULL,
	[QtySold] [int] NULL,
	[SubTotal] [money] NULL,
	[Discount] [money] NULL,
	[Total] [money] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[LastModifiedDate] [datetime] NULL,
	[LastModifiedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL,
	[Cost] [float] NULL,
	[Remark] [nvarchar](100) NULL,
	[Branch] [int] NULL,
	[ItemName] [nvarchar](100) NULL,
 CONSTRAINT [PK_SaleDetail] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SaleHeader]    Script Date: 11/09/2024 6:55:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleHeader](
	[OID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Customer] [int] NULL,
	[SoldDate] [datetime] NULL,
	[InvoiceNumber] [nvarchar](100) NULL,
	[SubTotal] [money] NULL,
	[Discount] [money] NULL,
	[Total] [money] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[LastModifiedDate] [datetime] NULL,
	[LastModifiedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Branch] [int] NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL,
	[SaleStatus] [int] NULL,
	[Remarks] [nvarchar](100) NULL,
 CONSTRAINT [PK_SaleHeader] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SaleRefund]    Script Date: 11/09/2024 6:55:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleRefund](
	[OID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[SaleHeader] [int] NULL,
	[SaleRefNo] [int] NULL,
	[Branch] [int] NULL,
	[Stock] [int] NULL,
	[Price] [float] NULL,
	[Quantity] [float] NULL,
	[Remark] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[LastModifiedDate] [datetime] NULL,
	[LastModifiedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL,
	[TotalPurchase] [float] NULL,
	[RefundDate] [datetime] NULL,
 CONSTRAINT [PK_SaleRefund] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SaleSerial]    Script Date: 11/09/2024 6:55:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleSerial](
	[OID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[SerialNumber] [nvarchar](100) NULL,
	[Remark] [nvarchar](100) NULL,
	[SaleDetail] [int] NULL,
	[LastModifiedDate] [datetime] NULL,
	[LastModifiedBy] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL,
 CONSTRAINT [PK_SaleSerial] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceCharge]    Script Date: 11/09/2024 6:55:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceCharge](
	[OID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[DateCollected] [datetime] NULL,
	[ServiceType] [int] NULL,
	[Technician] [int] NULL,
	[JONumber] [nvarchar](15) NULL,
	[Amount] [float] NULL,
	[Branch] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[LastModifiedDate] [datetime] NULL,
	[LastModifiedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL,
 CONSTRAINT [PK_ServiceCharge] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceType]    Script Date: 11/09/2024 6:55:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceType](
	[OID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[ServiceTypeDescription] [nvarchar](100) NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL,
 CONSTRAINT [PK_ServiceType] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stock]    Script Date: 11/09/2024 6:55:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stock](
	[OID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[ItemCode] [nvarchar](100) NULL,
	[ItemName] [nvarchar](100) NULL,
	[StockDescription] [nvarchar](100) NULL,
	[Cost] [money] NULL,
	[Price] [money] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[LastModifiedDate] [datetime] NULL,
	[LastModifiedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL,
	[Active] [bit] NULL,
	[Category] [int] NULL,
	[Brand] [int] NULL,
	[ItemType] [int] NULL,
	[ItemSize] [int] NULL,
	[Supplier] [int] NULL,
	[LastDelivery] [datetime] NULL,
	[LastSupplier] [nvarchar](100) NULL,
	[LastQtyDelivered] [float] NULL,
	[LastCost] [float] NULL,
	[LatestCost] [float] NULL,
 CONSTRAINT [PK_Stock] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 11/09/2024 6:55:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier](
	[OID] [int] IDENTITY(1,1) NOT NULL,
	[SupplierName] [nvarchar](100) NULL,
	[Address] [nvarchar](100) NULL,
	[ContactNo] [nvarchar](11) NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[LastModifiedDate] [datetime] NULL,
	[LastModifiedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Technician]    Script Date: 11/09/2024 6:55:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Technician](
	[OID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Name] [nvarchar](100) NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL,
 CONSTRAINT [PK_Technician] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transfer]    Script Date: 11/09/2024 6:55:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transfer](
	[OID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[DateTransferred] [datetime] NULL,
	[FromBranch] [int] NULL,
	[Remarks] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[LastModifiedDate] [datetime] NULL,
	[LastModifiedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL,
	[TransferStatus] [int] NULL,
	[Branches] [int] NULL,
	[ToBranch] [int] NULL,
 CONSTRAINT [PK_Transfer] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransferDetail]    Script Date: 11/09/2024 6:55:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransferDetail](
	[OID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Transfer] [int] NULL,
	[Stock] [int] NULL,
	[Quantity] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[LastModifiedDate] [datetime] NULL,
	[LastModifiedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL,
	[Remark] [nvarchar](100) NULL,
	[Branch] [int] NULL,
	[ItemName] [nvarchar](100) NULL,
 CONSTRAINT [PK_TransferDetail] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UpdatedPrice]    Script Date: 11/09/2024 6:55:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UpdatedPrice](
	[Branch] [nvarchar](255) NULL,
	[Item Code] [nvarchar](255) NULL,
	[Item Name] [nvarchar](255) NULL,
	[Supplier Name] [nvarchar](255) NULL,
	[Category Name] [nvarchar](255) NULL,
	[Brand Name] [nvarchar](255) NULL,
	[Available] [float] NULL,
	[ Cost ] [float] NULL,
	[ Price ] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Warranty]    Script Date: 11/09/2024 6:55:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Warranty](
	[OID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[WarrantyDate] [datetime] NULL,
	[Customer] [int] NULL,
	[Branch] [int] NULL,
	[WarrantyStatus] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[LastModifiedDate] [datetime] NULL,
	[LastModifiedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL,
 CONSTRAINT [PK_Warranty] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WarrantyDetail]    Script Date: 11/09/2024 6:55:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WarrantyDetail](
	[OID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Warranty] [int] NULL,
	[Stock] [int] NULL,
	[Quantity] [int] NULL,
	[Remark] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[LastModifiedDate] [datetime] NULL,
	[LastModifiedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL,
	[Branch] [int] NULL,
	[ItemName] [nvarchar](100) NULL,
 CONSTRAINT [PK_WarrantyDetail] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WarrantySerial]    Script Date: 11/09/2024 6:55:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WarrantySerial](
	[OID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[WarrantyDetail] [int] NULL,
	[SerialNumber] [nvarchar](100) NULL,
	[Remarks] [nvarchar](100) NULL,
	[OptimisticLockField] [int] NULL,
	[GCRecord] [int] NULL,
 CONSTRAINT [PK_WarrantySerial] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bo]  WITH NOCHECK ADD  CONSTRAINT [FK_Bo_Branch] FOREIGN KEY([Branch])
REFERENCES [dbo].[Branch] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Bo] CHECK CONSTRAINT [FK_Bo_Branch]
GO
ALTER TABLE [dbo].[Bo]  WITH NOCHECK ADD  CONSTRAINT [FK_Bo_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Bo] CHECK CONSTRAINT [FK_Bo_CreatedBy]
GO
ALTER TABLE [dbo].[Bo]  WITH NOCHECK ADD  CONSTRAINT [FK_Bo_DeletedBy] FOREIGN KEY([DeletedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Bo] CHECK CONSTRAINT [FK_Bo_DeletedBy]
GO
ALTER TABLE [dbo].[Bo]  WITH NOCHECK ADD  CONSTRAINT [FK_Bo_LastModifiedBy] FOREIGN KEY([LastModifiedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Bo] CHECK CONSTRAINT [FK_Bo_LastModifiedBy]
GO
ALTER TABLE [dbo].[Bo]  WITH NOCHECK ADD  CONSTRAINT [FK_Bo_Supplier] FOREIGN KEY([Supplier])
REFERENCES [dbo].[Supplier] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Bo] CHECK CONSTRAINT [FK_Bo_Supplier]
GO
ALTER TABLE [dbo].[BoDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_BoDetail_Bo] FOREIGN KEY([Bo])
REFERENCES [dbo].[Bo] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[BoDetail] CHECK CONSTRAINT [FK_BoDetail_Bo]
GO
ALTER TABLE [dbo].[BoDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_BoDetail_Branch] FOREIGN KEY([Branch])
REFERENCES [dbo].[Branch] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[BoDetail] CHECK CONSTRAINT [FK_BoDetail_Branch]
GO
ALTER TABLE [dbo].[BoDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_BoDetail_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[BoDetail] CHECK CONSTRAINT [FK_BoDetail_CreatedBy]
GO
ALTER TABLE [dbo].[BoDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_BoDetail_DeletedBy] FOREIGN KEY([DeletedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[BoDetail] CHECK CONSTRAINT [FK_BoDetail_DeletedBy]
GO
ALTER TABLE [dbo].[BoDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_BoDetail_LastModifiedBy] FOREIGN KEY([LastModifiedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[BoDetail] CHECK CONSTRAINT [FK_BoDetail_LastModifiedBy]
GO
ALTER TABLE [dbo].[BoDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_BoDetail_Stock] FOREIGN KEY([Stock])
REFERENCES [dbo].[Stock] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[BoDetail] CHECK CONSTRAINT [FK_BoDetail_Stock]
GO
ALTER TABLE [dbo].[BoSerial]  WITH NOCHECK ADD  CONSTRAINT [FK_BoSerial_BoDetail] FOREIGN KEY([BoDetail])
REFERENCES [dbo].[BoDetail] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[BoSerial] CHECK CONSTRAINT [FK_BoSerial_BoDetail]
GO
ALTER TABLE [dbo].[BoSerial]  WITH NOCHECK ADD  CONSTRAINT [FK_BoSerial_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[BoSerial] CHECK CONSTRAINT [FK_BoSerial_CreatedBy]
GO
ALTER TABLE [dbo].[BoSerial]  WITH NOCHECK ADD  CONSTRAINT [FK_BoSerial_LastModifiedBy] FOREIGN KEY([LastModifiedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[BoSerial] CHECK CONSTRAINT [FK_BoSerial_LastModifiedBy]
GO
ALTER TABLE [dbo].[Branch]  WITH NOCHECK ADD  CONSTRAINT [FK_Branch_OID] FOREIGN KEY([OID])
REFERENCES [dbo].[CustomBaseObject] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Branch] CHECK CONSTRAINT [FK_Branch_OID]
GO
ALTER TABLE [dbo].[BranchStock]  WITH NOCHECK ADD  CONSTRAINT [FK_BranchStock_Branch] FOREIGN KEY([Branch])
REFERENCES [dbo].[Branch] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[BranchStock] CHECK CONSTRAINT [FK_BranchStock_Branch]
GO
ALTER TABLE [dbo].[BranchStock]  WITH NOCHECK ADD  CONSTRAINT [FK_BranchStock_Stock] FOREIGN KEY([Stock])
REFERENCES [dbo].[Stock] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[BranchStock] CHECK CONSTRAINT [FK_BranchStock_Stock]
GO
ALTER TABLE [dbo].[Customer]  WITH NOCHECK ADD  CONSTRAINT [FK_Customer_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_CreatedBy]
GO
ALTER TABLE [dbo].[Customer]  WITH NOCHECK ADD  CONSTRAINT [FK_Customer_DeletedBy] FOREIGN KEY([DeletedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_DeletedBy]
GO
ALTER TABLE [dbo].[Customer]  WITH NOCHECK ADD  CONSTRAINT [FK_Customer_LastModifiedBy] FOREIGN KEY([LastModifiedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_LastModifiedBy]
GO
ALTER TABLE [dbo].[CustomerPayment]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerPayment_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[CustomerPayment] CHECK CONSTRAINT [FK_CustomerPayment_CreatedBy]
GO
ALTER TABLE [dbo].[CustomerPayment]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerPayment_DeletedBy] FOREIGN KEY([DeletedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[CustomerPayment] CHECK CONSTRAINT [FK_CustomerPayment_DeletedBy]
GO
ALTER TABLE [dbo].[CustomerPayment]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerPayment_LastModifiedBy] FOREIGN KEY([LastModifiedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[CustomerPayment] CHECK CONSTRAINT [FK_CustomerPayment_LastModifiedBy]
GO
ALTER TABLE [dbo].[CustomerPayment]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerPayment_SaleHeader] FOREIGN KEY([SaleHeader])
REFERENCES [dbo].[SaleHeader] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[CustomerPayment] CHECK CONSTRAINT [FK_CustomerPayment_SaleHeader]
GO
ALTER TABLE [dbo].[DeliveryDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_DeliveryDetail_Branch] FOREIGN KEY([Branch])
REFERENCES [dbo].[Branch] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[DeliveryDetail] CHECK CONSTRAINT [FK_DeliveryDetail_Branch]
GO
ALTER TABLE [dbo].[DeliveryDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_DeliveryDetail_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[DeliveryDetail] CHECK CONSTRAINT [FK_DeliveryDetail_CreatedBy]
GO
ALTER TABLE [dbo].[DeliveryDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_DeliveryDetail_DeletedBy] FOREIGN KEY([DeletedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[DeliveryDetail] CHECK CONSTRAINT [FK_DeliveryDetail_DeletedBy]
GO
ALTER TABLE [dbo].[DeliveryDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_DeliveryDetail_DeliveryHeader] FOREIGN KEY([DeliveryHeader])
REFERENCES [dbo].[DeliveryHeader] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[DeliveryDetail] CHECK CONSTRAINT [FK_DeliveryDetail_DeliveryHeader]
GO
ALTER TABLE [dbo].[DeliveryDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_DeliveryDetail_LastModifiedBy] FOREIGN KEY([LastModifiedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[DeliveryDetail] CHECK CONSTRAINT [FK_DeliveryDetail_LastModifiedBy]
GO
ALTER TABLE [dbo].[DeliveryDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_DeliveryDetail_Stock] FOREIGN KEY([Stock])
REFERENCES [dbo].[Stock] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[DeliveryDetail] CHECK CONSTRAINT [FK_DeliveryDetail_Stock]
GO
ALTER TABLE [dbo].[DeliveryHeader]  WITH NOCHECK ADD  CONSTRAINT [FK_DeliveryHeader_Branch] FOREIGN KEY([Branch])
REFERENCES [dbo].[Branch] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[DeliveryHeader] CHECK CONSTRAINT [FK_DeliveryHeader_Branch]
GO
ALTER TABLE [dbo].[DeliveryHeader]  WITH NOCHECK ADD  CONSTRAINT [FK_DeliveryHeader_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[DeliveryHeader] CHECK CONSTRAINT [FK_DeliveryHeader_CreatedBy]
GO
ALTER TABLE [dbo].[DeliveryHeader]  WITH NOCHECK ADD  CONSTRAINT [FK_DeliveryHeader_DeletedBy] FOREIGN KEY([DeletedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[DeliveryHeader] CHECK CONSTRAINT [FK_DeliveryHeader_DeletedBy]
GO
ALTER TABLE [dbo].[DeliveryHeader]  WITH NOCHECK ADD  CONSTRAINT [FK_DeliveryHeader_LastModifiedBy] FOREIGN KEY([LastModifiedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[DeliveryHeader] CHECK CONSTRAINT [FK_DeliveryHeader_LastModifiedBy]
GO
ALTER TABLE [dbo].[DeliveryHeader]  WITH NOCHECK ADD  CONSTRAINT [FK_DeliveryHeader_Supplier] FOREIGN KEY([Supplier])
REFERENCES [dbo].[Supplier] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[DeliveryHeader] CHECK CONSTRAINT [FK_DeliveryHeader_Supplier]
GO
ALTER TABLE [dbo].[DeliverySerial]  WITH NOCHECK ADD  CONSTRAINT [FK_DeliverySerial_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[DeliverySerial] CHECK CONSTRAINT [FK_DeliverySerial_CreatedBy]
GO
ALTER TABLE [dbo].[DeliverySerial]  WITH NOCHECK ADD  CONSTRAINT [FK_DeliverySerial_DeliveryDetail] FOREIGN KEY([DeliveryDetail])
REFERENCES [dbo].[DeliveryDetail] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[DeliverySerial] CHECK CONSTRAINT [FK_DeliverySerial_DeliveryDetail]
GO
ALTER TABLE [dbo].[DeliverySerial]  WITH NOCHECK ADD  CONSTRAINT [FK_DeliverySerial_LastModifiedBy] FOREIGN KEY([LastModifiedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[DeliverySerial] CHECK CONSTRAINT [FK_DeliverySerial_LastModifiedBy]
GO
ALTER TABLE [dbo].[Department]  WITH NOCHECK ADD  CONSTRAINT [FK_Department_Branch] FOREIGN KEY([Branch])
REFERENCES [dbo].[Branch] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_Department_Branch]
GO
ALTER TABLE [dbo].[Department]  WITH NOCHECK ADD  CONSTRAINT [FK_Department_OID] FOREIGN KEY([OID])
REFERENCES [dbo].[CustomBaseObject] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_Department_OID]
GO
ALTER TABLE [dbo].[ItemType]  WITH NOCHECK ADD  CONSTRAINT [FK_ItemType_OID] FOREIGN KEY([OID])
REFERENCES [dbo].[CustomBaseObject] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[ItemType] CHECK CONSTRAINT [FK_ItemType_OID]
GO
ALTER TABLE [dbo].[ManualInventory]  WITH NOCHECK ADD  CONSTRAINT [FK_ManualInventory_Branch] FOREIGN KEY([Branch])
REFERENCES [dbo].[Branch] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[ManualInventory] CHECK CONSTRAINT [FK_ManualInventory_Branch]
GO
ALTER TABLE [dbo].[ManualInventory]  WITH NOCHECK ADD  CONSTRAINT [FK_ManualInventory_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[ManualInventory] CHECK CONSTRAINT [FK_ManualInventory_CreatedBy]
GO
ALTER TABLE [dbo].[ManualInventory]  WITH NOCHECK ADD  CONSTRAINT [FK_ManualInventory_DeletedBy] FOREIGN KEY([DeletedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[ManualInventory] CHECK CONSTRAINT [FK_ManualInventory_DeletedBy]
GO
ALTER TABLE [dbo].[ManualInventory]  WITH NOCHECK ADD  CONSTRAINT [FK_ManualInventory_LastModifiedBy] FOREIGN KEY([LastModifiedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[ManualInventory] CHECK CONSTRAINT [FK_ManualInventory_LastModifiedBy]
GO
ALTER TABLE [dbo].[ManualInventoryDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_ManualInventoryDetail_Branch] FOREIGN KEY([Branch])
REFERENCES [dbo].[Branch] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[ManualInventoryDetail] CHECK CONSTRAINT [FK_ManualInventoryDetail_Branch]
GO
ALTER TABLE [dbo].[ManualInventoryDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_ManualInventoryDetail_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[ManualInventoryDetail] CHECK CONSTRAINT [FK_ManualInventoryDetail_CreatedBy]
GO
ALTER TABLE [dbo].[ManualInventoryDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_ManualInventoryDetail_DeletedBy] FOREIGN KEY([DeletedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[ManualInventoryDetail] CHECK CONSTRAINT [FK_ManualInventoryDetail_DeletedBy]
GO
ALTER TABLE [dbo].[ManualInventoryDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_ManualInventoryDetail_LastModifiedBy] FOREIGN KEY([LastModifiedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[ManualInventoryDetail] CHECK CONSTRAINT [FK_ManualInventoryDetail_LastModifiedBy]
GO
ALTER TABLE [dbo].[ManualInventoryDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_ManualInventoryDetail_ManualInventory] FOREIGN KEY([ManualInventory])
REFERENCES [dbo].[ManualInventory] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[ManualInventoryDetail] CHECK CONSTRAINT [FK_ManualInventoryDetail_ManualInventory]
GO
ALTER TABLE [dbo].[ManualInventoryDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_ManualInventoryDetail_Stock] FOREIGN KEY([Stock])
REFERENCES [dbo].[Stock] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[ManualInventoryDetail] CHECK CONSTRAINT [FK_ManualInventoryDetail_Stock]
GO
ALTER TABLE [dbo].[SaleDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_SaleDetail_Branch] FOREIGN KEY([Branch])
REFERENCES [dbo].[Branch] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[SaleDetail] CHECK CONSTRAINT [FK_SaleDetail_Branch]
GO
ALTER TABLE [dbo].[SaleDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_SaleDetail_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[SaleDetail] CHECK CONSTRAINT [FK_SaleDetail_CreatedBy]
GO
ALTER TABLE [dbo].[SaleDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_SaleDetail_DeletedBy] FOREIGN KEY([DeletedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[SaleDetail] CHECK CONSTRAINT [FK_SaleDetail_DeletedBy]
GO
ALTER TABLE [dbo].[SaleDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_SaleDetail_LastModifiedBy] FOREIGN KEY([LastModifiedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[SaleDetail] CHECK CONSTRAINT [FK_SaleDetail_LastModifiedBy]
GO
ALTER TABLE [dbo].[SaleDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_SaleDetail_SaleHeader] FOREIGN KEY([SaleHeader])
REFERENCES [dbo].[SaleHeader] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[SaleDetail] CHECK CONSTRAINT [FK_SaleDetail_SaleHeader]
GO
ALTER TABLE [dbo].[SaleDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_SaleDetail_Stock] FOREIGN KEY([Stock])
REFERENCES [dbo].[Stock] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[SaleDetail] CHECK CONSTRAINT [FK_SaleDetail_Stock]
GO
ALTER TABLE [dbo].[SaleHeader]  WITH NOCHECK ADD  CONSTRAINT [FK_SaleHeader_Branch] FOREIGN KEY([Branch])
REFERENCES [dbo].[Branch] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[SaleHeader] CHECK CONSTRAINT [FK_SaleHeader_Branch]
GO
ALTER TABLE [dbo].[SaleHeader]  WITH NOCHECK ADD  CONSTRAINT [FK_SaleHeader_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[SaleHeader] CHECK CONSTRAINT [FK_SaleHeader_CreatedBy]
GO
ALTER TABLE [dbo].[SaleHeader]  WITH NOCHECK ADD  CONSTRAINT [FK_SaleHeader_Customer] FOREIGN KEY([Customer])
REFERENCES [dbo].[Customer] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[SaleHeader] CHECK CONSTRAINT [FK_SaleHeader_Customer]
GO
ALTER TABLE [dbo].[SaleHeader]  WITH NOCHECK ADD  CONSTRAINT [FK_SaleHeader_DeletedBy] FOREIGN KEY([DeletedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[SaleHeader] CHECK CONSTRAINT [FK_SaleHeader_DeletedBy]
GO
ALTER TABLE [dbo].[SaleHeader]  WITH NOCHECK ADD  CONSTRAINT [FK_SaleHeader_LastModifiedBy] FOREIGN KEY([LastModifiedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[SaleHeader] CHECK CONSTRAINT [FK_SaleHeader_LastModifiedBy]
GO
ALTER TABLE [dbo].[SaleRefund]  WITH NOCHECK ADD  CONSTRAINT [FK_SaleRefund_Branch] FOREIGN KEY([Branch])
REFERENCES [dbo].[Branch] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[SaleRefund] CHECK CONSTRAINT [FK_SaleRefund_Branch]
GO
ALTER TABLE [dbo].[SaleRefund]  WITH NOCHECK ADD  CONSTRAINT [FK_SaleRefund_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[SaleRefund] CHECK CONSTRAINT [FK_SaleRefund_CreatedBy]
GO
ALTER TABLE [dbo].[SaleRefund]  WITH NOCHECK ADD  CONSTRAINT [FK_SaleRefund_DeletedBy] FOREIGN KEY([DeletedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[SaleRefund] CHECK CONSTRAINT [FK_SaleRefund_DeletedBy]
GO
ALTER TABLE [dbo].[SaleRefund]  WITH NOCHECK ADD  CONSTRAINT [FK_SaleRefund_LastModifiedBy] FOREIGN KEY([LastModifiedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[SaleRefund] CHECK CONSTRAINT [FK_SaleRefund_LastModifiedBy]
GO
ALTER TABLE [dbo].[SaleRefund]  WITH NOCHECK ADD  CONSTRAINT [FK_SaleRefund_SaleHeader] FOREIGN KEY([SaleHeader])
REFERENCES [dbo].[SaleHeader] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[SaleRefund] CHECK CONSTRAINT [FK_SaleRefund_SaleHeader]
GO
ALTER TABLE [dbo].[SaleRefund]  WITH NOCHECK ADD  CONSTRAINT [FK_SaleRefund_Stock] FOREIGN KEY([Stock])
REFERENCES [dbo].[Stock] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[SaleRefund] CHECK CONSTRAINT [FK_SaleRefund_Stock]
GO
ALTER TABLE [dbo].[SaleSerial]  WITH NOCHECK ADD  CONSTRAINT [FK_SaleSerial_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[SaleSerial] CHECK CONSTRAINT [FK_SaleSerial_CreatedBy]
GO
ALTER TABLE [dbo].[SaleSerial]  WITH NOCHECK ADD  CONSTRAINT [FK_SaleSerial_LastModifiedBy] FOREIGN KEY([LastModifiedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[SaleSerial] CHECK CONSTRAINT [FK_SaleSerial_LastModifiedBy]
GO
ALTER TABLE [dbo].[SaleSerial]  WITH NOCHECK ADD  CONSTRAINT [FK_SaleSerial_SaleDetail] FOREIGN KEY([SaleDetail])
REFERENCES [dbo].[SaleDetail] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[SaleSerial] CHECK CONSTRAINT [FK_SaleSerial_SaleDetail]
GO
ALTER TABLE [dbo].[ServiceCharge]  WITH NOCHECK ADD  CONSTRAINT [FK_ServiceCharge_Branch] FOREIGN KEY([Branch])
REFERENCES [dbo].[Branch] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[ServiceCharge] CHECK CONSTRAINT [FK_ServiceCharge_Branch]
GO
ALTER TABLE [dbo].[ServiceCharge]  WITH NOCHECK ADD  CONSTRAINT [FK_ServiceCharge_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[ServiceCharge] CHECK CONSTRAINT [FK_ServiceCharge_CreatedBy]
GO
ALTER TABLE [dbo].[ServiceCharge]  WITH NOCHECK ADD  CONSTRAINT [FK_ServiceCharge_DeletedBy] FOREIGN KEY([DeletedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[ServiceCharge] CHECK CONSTRAINT [FK_ServiceCharge_DeletedBy]
GO
ALTER TABLE [dbo].[ServiceCharge]  WITH NOCHECK ADD  CONSTRAINT [FK_ServiceCharge_LastModifiedBy] FOREIGN KEY([LastModifiedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[ServiceCharge] CHECK CONSTRAINT [FK_ServiceCharge_LastModifiedBy]
GO
ALTER TABLE [dbo].[ServiceCharge]  WITH NOCHECK ADD  CONSTRAINT [FK_ServiceCharge_ServiceType] FOREIGN KEY([ServiceType])
REFERENCES [dbo].[ServiceType] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[ServiceCharge] CHECK CONSTRAINT [FK_ServiceCharge_ServiceType]
GO
ALTER TABLE [dbo].[ServiceCharge]  WITH NOCHECK ADD  CONSTRAINT [FK_ServiceCharge_Technician] FOREIGN KEY([Technician])
REFERENCES [dbo].[Technician] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[ServiceCharge] CHECK CONSTRAINT [FK_ServiceCharge_Technician]
GO
ALTER TABLE [dbo].[Stock]  WITH NOCHECK ADD  CONSTRAINT [FK_Stock_Brand] FOREIGN KEY([Brand])
REFERENCES [dbo].[Brand] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_Brand]
GO
ALTER TABLE [dbo].[Stock]  WITH NOCHECK ADD  CONSTRAINT [FK_Stock_Category] FOREIGN KEY([Category])
REFERENCES [dbo].[Category] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_Category]
GO
ALTER TABLE [dbo].[Stock]  WITH NOCHECK ADD  CONSTRAINT [FK_Stock_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_CreatedBy]
GO
ALTER TABLE [dbo].[Stock]  WITH NOCHECK ADD  CONSTRAINT [FK_Stock_DeletedBy] FOREIGN KEY([DeletedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_DeletedBy]
GO
ALTER TABLE [dbo].[Stock]  WITH NOCHECK ADD  CONSTRAINT [FK_Stock_ItemSize] FOREIGN KEY([ItemSize])
REFERENCES [dbo].[ItemSize] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_ItemSize]
GO
ALTER TABLE [dbo].[Stock]  WITH NOCHECK ADD  CONSTRAINT [FK_Stock_ItemType] FOREIGN KEY([ItemType])
REFERENCES [dbo].[ItemType] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_ItemType]
GO
ALTER TABLE [dbo].[Stock]  WITH NOCHECK ADD  CONSTRAINT [FK_Stock_LastModifiedBy] FOREIGN KEY([LastModifiedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_LastModifiedBy]
GO
ALTER TABLE [dbo].[Stock]  WITH NOCHECK ADD  CONSTRAINT [FK_Stock_Supplier] FOREIGN KEY([Supplier])
REFERENCES [dbo].[Supplier] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_Supplier]
GO
ALTER TABLE [dbo].[Supplier]  WITH NOCHECK ADD  CONSTRAINT [FK_Supplier_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Supplier] CHECK CONSTRAINT [FK_Supplier_CreatedBy]
GO
ALTER TABLE [dbo].[Supplier]  WITH NOCHECK ADD  CONSTRAINT [FK_Supplier_DeletedBy] FOREIGN KEY([DeletedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Supplier] CHECK CONSTRAINT [FK_Supplier_DeletedBy]
GO
ALTER TABLE [dbo].[Supplier]  WITH NOCHECK ADD  CONSTRAINT [FK_Supplier_LastModifiedBy] FOREIGN KEY([LastModifiedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Supplier] CHECK CONSTRAINT [FK_Supplier_LastModifiedBy]
GO
ALTER TABLE [dbo].[Transfer]  WITH NOCHECK ADD  CONSTRAINT [FK_Transfer_Branches] FOREIGN KEY([Branches])
REFERENCES [dbo].[Branches] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Transfer] CHECK CONSTRAINT [FK_Transfer_Branches]
GO
ALTER TABLE [dbo].[Transfer]  WITH NOCHECK ADD  CONSTRAINT [FK_Transfer_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Transfer] CHECK CONSTRAINT [FK_Transfer_CreatedBy]
GO
ALTER TABLE [dbo].[Transfer]  WITH NOCHECK ADD  CONSTRAINT [FK_Transfer_DeletedBy] FOREIGN KEY([DeletedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Transfer] CHECK CONSTRAINT [FK_Transfer_DeletedBy]
GO
ALTER TABLE [dbo].[Transfer]  WITH NOCHECK ADD  CONSTRAINT [FK_Transfer_FromBranch] FOREIGN KEY([FromBranch])
REFERENCES [dbo].[Branch] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Transfer] CHECK CONSTRAINT [FK_Transfer_FromBranch]
GO
ALTER TABLE [dbo].[Transfer]  WITH NOCHECK ADD  CONSTRAINT [FK_Transfer_LastModifiedBy] FOREIGN KEY([LastModifiedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Transfer] CHECK CONSTRAINT [FK_Transfer_LastModifiedBy]
GO
ALTER TABLE [dbo].[TransferDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_TransferDetail_Branch] FOREIGN KEY([Branch])
REFERENCES [dbo].[Branch] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[TransferDetail] CHECK CONSTRAINT [FK_TransferDetail_Branch]
GO
ALTER TABLE [dbo].[TransferDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_TransferDetail_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[TransferDetail] CHECK CONSTRAINT [FK_TransferDetail_CreatedBy]
GO
ALTER TABLE [dbo].[TransferDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_TransferDetail_DeletedBy] FOREIGN KEY([DeletedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[TransferDetail] CHECK CONSTRAINT [FK_TransferDetail_DeletedBy]
GO
ALTER TABLE [dbo].[TransferDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_TransferDetail_LastModifiedBy] FOREIGN KEY([LastModifiedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[TransferDetail] CHECK CONSTRAINT [FK_TransferDetail_LastModifiedBy]
GO
ALTER TABLE [dbo].[TransferDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_TransferDetail_Stock] FOREIGN KEY([Stock])
REFERENCES [dbo].[Stock] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[TransferDetail] CHECK CONSTRAINT [FK_TransferDetail_Stock]
GO
ALTER TABLE [dbo].[TransferDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_TransferDetail_Transfer] FOREIGN KEY([Transfer])
REFERENCES [dbo].[Transfer] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[TransferDetail] CHECK CONSTRAINT [FK_TransferDetail_Transfer]
GO
ALTER TABLE [dbo].[Warranty]  WITH NOCHECK ADD  CONSTRAINT [FK_Warranty_Branch] FOREIGN KEY([Branch])
REFERENCES [dbo].[Branch] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Warranty] CHECK CONSTRAINT [FK_Warranty_Branch]
GO
ALTER TABLE [dbo].[Warranty]  WITH NOCHECK ADD  CONSTRAINT [FK_Warranty_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Warranty] CHECK CONSTRAINT [FK_Warranty_CreatedBy]
GO
ALTER TABLE [dbo].[Warranty]  WITH NOCHECK ADD  CONSTRAINT [FK_Warranty_Customer] FOREIGN KEY([Customer])
REFERENCES [dbo].[Customer] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Warranty] CHECK CONSTRAINT [FK_Warranty_Customer]
GO
ALTER TABLE [dbo].[Warranty]  WITH NOCHECK ADD  CONSTRAINT [FK_Warranty_DeletedBy] FOREIGN KEY([DeletedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Warranty] CHECK CONSTRAINT [FK_Warranty_DeletedBy]
GO
ALTER TABLE [dbo].[Warranty]  WITH NOCHECK ADD  CONSTRAINT [FK_Warranty_LastModifiedBy] FOREIGN KEY([LastModifiedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Warranty] CHECK CONSTRAINT [FK_Warranty_LastModifiedBy]
GO
ALTER TABLE [dbo].[WarrantyDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_WarrantyDetail_Branch] FOREIGN KEY([Branch])
REFERENCES [dbo].[Branch] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[WarrantyDetail] CHECK CONSTRAINT [FK_WarrantyDetail_Branch]
GO
ALTER TABLE [dbo].[WarrantyDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_WarrantyDetail_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[WarrantyDetail] CHECK CONSTRAINT [FK_WarrantyDetail_CreatedBy]
GO
ALTER TABLE [dbo].[WarrantyDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_WarrantyDetail_DeletedBy] FOREIGN KEY([DeletedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[WarrantyDetail] CHECK CONSTRAINT [FK_WarrantyDetail_DeletedBy]
GO
ALTER TABLE [dbo].[WarrantyDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_WarrantyDetail_LastModifiedBy] FOREIGN KEY([LastModifiedBy])
REFERENCES [dbo].[PermissionPolicyUser] ([Oid])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[WarrantyDetail] CHECK CONSTRAINT [FK_WarrantyDetail_LastModifiedBy]
GO
ALTER TABLE [dbo].[WarrantyDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_WarrantyDetail_Stock] FOREIGN KEY([Stock])
REFERENCES [dbo].[Stock] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[WarrantyDetail] CHECK CONSTRAINT [FK_WarrantyDetail_Stock]
GO
ALTER TABLE [dbo].[WarrantyDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_WarrantyDetail_Warranty] FOREIGN KEY([Warranty])
REFERENCES [dbo].[Warranty] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[WarrantyDetail] CHECK CONSTRAINT [FK_WarrantyDetail_Warranty]
GO
ALTER TABLE [dbo].[WarrantySerial]  WITH NOCHECK ADD  CONSTRAINT [FK_WarrantySerial_WarrantyDetail] FOREIGN KEY([WarrantyDetail])
REFERENCES [dbo].[WarrantyDetail] ([OID])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[WarrantySerial] CHECK CONSTRAINT [FK_WarrantySerial_WarrantyDetail]
GO

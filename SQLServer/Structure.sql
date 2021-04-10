USE [SampleProject]
GO
/****** Object:  Table [dbo].[AccountInfos]    Script Date: 2021/4/10 上午 10:32:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountInfos](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Phone] [varchar](50) NULL,
	[Title] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_AccountInfos] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 2021/4/10 上午 10:32:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[PWD] [varchar](50) NOT NULL,
	[UserLevel] [int] NOT NULL,
	[Email] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MediaFIles]    Script Date: 2021/4/10 上午 10:32:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MediaFIles](
	[ID] [uniqueidentifier] NOT NULL,
	[ReferenceID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Folder] [varchar](50) NOT NULL,
	[FileName] [varchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[Creator] [uniqueidentifier] NOT NULL,
	[ModifyDate] [datetime] NULL,
	[Modifier] [uniqueidentifier] NULL,
 CONSTRAINT [PK_MediaFIles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[News]    Script Date: 2021/4/10 上午 10:32:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News](
	[ID] [uniqueidentifier] NOT NULL,
	[Caption] [nvarchar](50) NOT NULL,
	[Body] [nvarchar](4000) NOT NULL,
	[IsEnabled] [bit] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[CreateDate] [datetime] NOT NULL,
	[Creator] [uniqueidentifier] NOT NULL,
	[ModifyDate] [datetime] NULL,
	[Modifier] [uniqueidentifier] NULL,
 CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 2021/4/10 上午 10:32:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ID] [uniqueidentifier] NOT NULL,
	[ProductType] [int] NOT NULL,
	[Caption] [nvarchar](50) NOT NULL,
	[Price] [decimal](10, 4) NOT NULL,
	[Body] [nvarchar](4000) NOT NULL,
	[IsEnabled] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[Creator] [uniqueidentifier] NOT NULL,
	[ModifyDate] [datetime] NULL,
	[Modifier] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SaleNoteDetails]    Script Date: 2021/4/10 上午 10:32:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleNoteDetails](
	[ID] [uniqueidentifier] NOT NULL,
	[SaleNoteID] [uniqueidentifier] NOT NULL,
	[StockID] [uniqueidentifier] NOT NULL,
	[RequiredQty] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[Creator] [uniqueidentifier] NOT NULL,
	[ModifyDate] [datetime] NULL,
	[Modifier] [uniqueidentifier] NULL,
 CONSTRAINT [PK_SaleNoteDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SaleNotes]    Script Date: 2021/4/10 上午 10:32:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleNotes](
	[ID] [uniqueidentifier] NOT NULL,
	[Status] [int] NOT NULL,
	[Body] [nvarchar](4000) NOT NULL,
	[ConfirmUserID] [uniqueidentifier] NULL,
	[ConfirmDate] [datetime] NULL,
	[CreateDate] [datetime] NOT NULL,
	[Creator] [uniqueidentifier] NOT NULL,
	[ModifyDate] [datetime] NULL,
	[Modifier] [uniqueidentifier] NULL,
 CONSTRAINT [PK_SaleNotes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stocks]    Script Date: 2021/4/10 上午 10:32:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stocks](
	[ID] [uniqueidentifier] NOT NULL,
	[ProductID] [uniqueidentifier] NOT NULL,
	[CurrentQty] [int] NOT NULL,
	[LockedQty] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[Creator] [uniqueidentifier] NOT NULL,
	[ModifyDate] [datetime] NULL,
	[Modifier] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Stocks] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

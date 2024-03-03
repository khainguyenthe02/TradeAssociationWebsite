use TradeAssociationWebsite;
go

-- Users--
CREATE TABLE [dbo].[USER](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[FullName] [nvarchar](50) NULL,
	[Password] [varchar](50) NOT NULL,
	[Birthday] [date] NULL,
	[Address] [nvarchar](100) NULL,
	[Phone] [varchar](10) NULL,
	[UserPicture] [varchar](max) NULL,
	[UserType] [int] NULL,
	[CreatedAt] [date] NULL
) ON [PRIMARY]
GO
select * from [dbo].[USER]

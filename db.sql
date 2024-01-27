use TradeAssociationWebsite;
go

-- Users--
CREATE TABLE [dbo].[USER](
	[Id] [int] primary key identity(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[FullName] [nvarchar](50) NULL,
	[Password] [varchar](50) NOT NULL,
	[Birthday] [date] NULL,
	[Address] [nvarchar](100) NULL
) ON [PRIMARY]
GO
select * from [dbo].[USER]
alter table [dbo].[USER] add  Phone varchar(10)
use TradeAssociationWebsite;
go

-- Tạo bảng người dùng hội viên
  CREATE TABLE [dbo].[USER](
	[Id] [int] primary key IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[FullName] [nvarchar](50) NULL,
	[Password] [varchar](50) NULL,
	[Birthday] [date] NULL,
	[Address] [nvarchar](100) NULL,
	[Phone] [varchar](10) NULL,
	[UserPicture] [varchar](max) NULL,
	[UserType] [nvarchar](100) NULL,
	[CreatedAt] [date] NULL
) 
GO
select * from [dbo].[USER]

INSERT INTO [dbo].[USER] ([UserName], [FullName], [Password], [Birthday], [Address], [Phone], [UserPicture], [UserType], [CreatedAt])
VALUES ('khainguyen02', N'Nguyễn Thế Khải', 'khaibro02!', '2002/07/14', N'xã Trung Mỹ, huyện Bình Xuyên, tỉnh Vĩnh Phúc', '0962887203', null, 'Admin', GETDATE())
GO
INSERT INTO [dbo].[USER] ([UserName], [FullName], [Password], [Birthday], [Address], [Phone], [UserPicture], [UserType], [CreatedAt])
VALUES ('namnguyen02', N'Nguyễn Công Nam', 'congnam02!', '2002/08/11', N'thị trấn Thanh Lãng, huyện Bình Xuyên, tỉnh Vĩnh Phúc', '0962887203', null, 'Admin', GETDATE())
GO
INSERT INTO [dbo].[USER] ([UserName], [FullName], [Password], [Birthday], [Address], [Phone], [UserPicture], [UserType], [CreatedAt])
VALUES ('hoanguyen02', N'Nguyễn Thị Hoa', 'thihoa02!', '2002/08/11', N'thị trấn Thanh Lãng, huyện Bình Xuyên, tỉnh Vĩnh Phúc', '0962887203', null, 'Admin', GETDATE())
GO

 -- Tạo bảng tin tức sự kiện
CREATE TABLE [dbo].[NEWS](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Title] [nvarchar](max) NOT NULL,
    [Description] [nvarchar](max) NULL,
    [Contents] [nvarchar](max) NOT NULL,
    [CreatedAt] [datetime] NULL,
    [Type] [nvarchar](50) NULL,
    [UserId] [int] NOT NULL,
    [UserName] [nvarchar](50) NULL,
    [ViewCount] [int] NULL,
	[ImageEvent] [varchar](max) NULL,
    CONSTRAINT [PK_NEWS] PRIMARY KEY CLUSTERED (
        [Id] ASC
    ),
    CONSTRAINT [FK_NEWS_USER] FOREIGN KEY ([UserId]) REFERENCES [dbo].[USER]([Id])
)
GO


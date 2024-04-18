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

INSERT INTO [dbo].[NEWS]
           ([Title]
           ,[Description]
           ,[Contents]
           ,[CreatedAt]
           ,[Type]
           ,[UserId]
           ,[UserName]
           ,[ViewCount]
           ,[ImageEvent])
     VALUES
           (N'TEAMZ WEB3 / AI SUMMIT 2024'
           , N'Hội nghị Web3/AI lớn nhất Nhật Bản “TEAMZ WEB3/AI SUMMIT 2024”.'
           ,N'VBA hân hạnh trở thành đối tác của Hội nghị Web3/AI lớn nhất Nhật Bản “TEAMZ WEB3/AI SUMMIT 2024”.
				TEAMZ WEB3 / AI SUMMIT 2024 là hội nghị thượng đỉnh ngành công nghiệp Web3 và AI tại Nhật Bản. Nơi hội tụ nhiều chuyên gia hàng đầu trong ngành nhằm chia sẻ kinh nghiệm chuyên môn, cũng như đánh giá bối cảnh kinh doanh trong tương lai.
				Hội nghị sẽ mang đến cho người tham dự những góc nhìn mới về công nghệ Web3 và AI, đồng thời là cơ hội kết nối với những người tiên phong.
				Hội nghị sẽ được tổ chức tại Toranomon Hills vào Thứ Bảy, ngày 13/04 và Chủ Nhật, ngày 14/04/2024.'
           ,GETDATE()
           ,N'Hội nghị'
           ,1
           ,N'Nguyễn Thế Khải'
           ,0
           ,'f5dda843-1624-4971-bbe7-91e01243d586.png'),

		   (N'UniTour – Khai phá tiềm năng ứng dụng của AI và Blockchain'
		   , N' Ngày 11/03/2024, Hiệp hội Blockchain Việt Nam, Viện Blockchain và Trí tuệ nhân tạo (ABAII), 
				SHE Blockchain  và Câu lạc bộ ChainCohort đã phối hợp cùng Khoa Tài chính – Thương mại, 
				Trường Đại học Công nghệ TP.HCM (HUTECH) tổ chức hội thảo “Khai phá tiềm năng ứng dụng của AI và 
				Blockchain trong nền Kinh tế số và Tài chính”.'
			, N'Chương trình được tổ chức cho gần 500 sinh viên và giảng viên về các vấn đề đang rất được quan tâm hiện nay là ứng dụng thực tiễn của AI và Blockchain trong nền kinh tế số và tài chính.
				Chương trình bao gồm các chia sẻ xoay quanh chủ đề AI & Blockchain trong tài chính và nền kinh tế số. Trong chương trình, các bạn sinh viên đã lắng nghe phần chia sẻ đến từ phiên thảo luận chủ đề “AI và Blockchain đang chuyển đổi trong tài chính và kinh doanh số như thế nào?” với sự tham gia của ThS. Lê Quyết Tâm – Trưởng ngành Thương mại điện tử, Trường ĐH Công nghệ TP.HCM (HUTECH), ông Hàng Minh Lợi – Phó Giám Đốc Trung Tâm Sáng Tạo AI, Viện Blockchain và Trí tuệ nhân tạo (ABAII), bà Phạm Thùy Linh – Nhà Sáng Lập & CEO SHE Blockchain, ông Hoàng Đặng – Đồng sáng lập & CEO Dizim.
				Về phía VBA có đại diện là ông Trần Dinh – Chủ Nhiệm Ủy ban Ứng Dụng Fintech, Hiệp hội Blockchain Việt Nam chia sẻ chủ đề “Xây dựng một tương lai an toàn: Vai trò của Blockchain trong định danh số và tăng cường an ninh mạng”.
				Ông Trần Dinh chia sẻ: “Blockchain không chỉ là công nghệ của tương lai mà còn là chìa khóa để mở ra một thế giới số an toàn hơn. Trong bối cảnh định danh số và an ninh mạng ngày càng trở nên quan trọng, việc áp dụng Blockchain sẽ là bước ngoặt, đảm bảo tính minh bạch, bảo mật và toàn vẹn dữ liệu trong kỷ nguyên số”. 
				Unitour sẽ bao gồm các buổi workshop, hội thảo với nội dung phù hợp với sinh viên chưa có kiến thức về công nghệ. 
				Bên cạnh đó, chương trình cũng sẽ tập trung vào việc chia sẻ các trải nghiệm thực tế qua kinh nghiệm thực chiến
				từ đại diện các doanh nghiệp công nghệ, qua đó giúp sinh viên có cái nhìn toàn diện và thực tiễn về ngành công nghiệp blockchain 
				và AI. Sinh viên cũng sẽ được mở ra cơ hội nghề nghiệp qua việc kết nối với các doanh nghiệp có nhu cầu về nguồn nhân lực.'
			, GETDATE()
			, N'Hội thảo'
			, 1
			, N'Nguyễn Thế Khải'
			,0
			,'8b7bad96-0cc2-4a27-a1e6-6bfe22f83bee.jpg')
	GO


CREATE PROCEDURE SearchNewsByTitle
    @SearchTerm NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * 
    FROM NEWS
    WHERE Title LIKE N'%' + @SearchTerm + '%'
END
GO

-- Xóa tin tức
CREATE PROCEDURE DeleteNews
    @newsId INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM NEWS
    WHERE Id = @newsId;
END;
GO
-- Tin tức mới nhất
CREATE PROCEDURE GetLastNews
AS
BEGIN
    SELECT TOP 4 *
    FROM News
    ORDER BY CreatedAt DESC; 
END;
go

-- Tin tức hot nhất
 CREATE PROCEDURE GetMostViewedNews
AS
BEGIN
    SELECT TOP 2 *
    FROM News
    ORDER BY ViewCount DESC; -- Sắp xếp theo lượt xem giảm dần
END;
go
CREATE TABLE [dbo].[COMMENT](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UserId] [int] NOT NULL,
	[FullName] [nvarchar](100) NULL,
	[NewsId] [int] NOT NULL,
 CONSTRAINT [PK_COMMENT] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[COMMENT]  WITH CHECK ADD  CONSTRAINT [FK_COMMENT_NEWS] FOREIGN KEY([NewsId])
REFERENCES [dbo].[NEWS] ([Id])
GO

ALTER TABLE [dbo].[COMMENT] CHECK CONSTRAINT [FK_COMMENT_NEWS]
GO

ALTER TABLE [dbo].[COMMENT]  WITH CHECK ADD  CONSTRAINT [FK_COMMENT_USER] FOREIGN KEY([UserId])
REFERENCES [dbo].[USER] ([Id])
GO

ALTER TABLE [dbo].[COMMENT] CHECK CONSTRAINT [FK_COMMENT_USER]
GO

--CREATE TABLE [dbo].[NewsStatus](
--    [NewsId] [int] NOT NULL, -- Khóa ngoại đến bảng News
--    [IsEnabled] [bit] NOT NULL DEFAULT 1,
--    [UpdatedAt] [datetime] NOT NULL DEFAULT GETDATE(),
--    CONSTRAINT [FK_NewsStatus_News] FOREIGN KEY ([NewsId]) REFERENCES [dbo].[News]([Id])
--)
--<!-- Checkbox để cho admin chọn trạng thái của sản phẩm -->
--<td>
--    <input type="checkbox" id="newsStatusCheckbox_@news.Id" @(news.IsEnabled ? "checked" : "") onchange="toggleNewsStatus(@news.Id)">
--    <label for="newsStatusCheckbox_@news.Id">Enable/Disable</label>
--</td>

--[HttpPost]
--public IActionResult ToggleNewsStatus(int newsId, bool isEnabled)
--{
--    try
--    {
--        var news = _newsRepository.GetNewsById(newsId);
--        if (news == null)
--        {
--            return NotFound(); // Trả về mã lỗi 404 nếu không tìm thấy sự kiện
--        }

--        // Chuyển đổi trạng thái của sự kiện
--        news.IsEnabled = !news.IsEnabled;
--        _newsRepository.UpdateNews(news);

--        return Ok(); // Trả về mã thành công 200 nếu cập nhật thành công
--    }
--    catch (Exception ex)
--    {
--        return BadRequest(ex.Message); // Trả về mã lỗi 400 nếu có lỗi xảy ra
--    }
--}


--<script>
--    function toggleNewsStatus(newsId) {
--        var checkbox = document.getElementById("newsStatusCheckbox_" + newsId);
--        var isEnabled = checkbox.checked;

--        // Gửi yêu cầu AJAX để cập nhật trạng thái của sự kiện
--        var xhr = new XMLHttpRequest();
--        xhr.open("POST", "/News/ToggleNewsStatus?newsId=" + newsId + "&isEnabled=" + isEnabled, true);
--        xhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
--        xhr.onreadystatechange = function () {
--            if (xhr.readyState == 4 && xhr.status == 200) {
--                // Xử lý kết quả nếu cần
--                console.log(xhr.responseText);
--            }
--        };
--        xhr.send();
--    }
--</script>

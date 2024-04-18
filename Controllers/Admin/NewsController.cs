using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TradeAssociationWebsite.Models.Admin;
using TradeAssociationWebsite.Repositories;
using TradeAssociationWebsite.Repositories.Interfaces;

namespace TradeAssociationWebsite.Controllers.Admin
{
    public class NewsController : Controller
    {
        private readonly INewsRepository _newsRepository;
        private readonly IUserRepository _userRepository;
        public NewsController(INewsRepository newsRepository, IUserRepository userRepository)
        {
            _newsRepository = newsRepository;  
            _userRepository = userRepository;
        }
        [Route("ListOfNews")]
        public IActionResult NewsList()
        {
            int userId = 0;
            string userType = string.Empty;
            var username = Request.Cookies["username"];
            ViewBag.Username = username;         
            if(username == null)
            {
                return RedirectToAction("LoginUser", "User");
            }
            else
            {
                List<News> eventsList = new List<News>();
                var user = _userRepository.GetByUserName(username);
                userId = (int) user.Id;
                userType = user.UserType;
                if (userType.Equals("Admin"))
                {
                    eventsList = _newsRepository.GetAll();                    
                }
                else
                {
                    eventsList = _newsRepository.GetAll().Where(x => x.UserId == userId).ToList();
                }
                if (eventsList != null)
                {
                    return View(eventsList);
                }
                return BadRequest("Lỗi");

            }           
            
        }
        [HttpGet]
        [Route("CreateNews")]
        public IActionResult CreateNews()
        {
            var username = Request.Cookies["username"];
            ViewBag.Username = username;
            // here we create the model directly inside the view
            return View("CreateNews");
        }

        // Thêm mới event
        [HttpPost]
        [Route("CreateNews")]
        [ValidateAntiForgeryToken] // Prevents Cross Site Request Forgery
        public IActionResult CreateNews(News news, IFormFile eventPictureFile)
        
        {
            var username = Request.Cookies["username"];
            ViewBag.Username = username;
            if (username == null)
            {
                return RedirectToAction("LoginUser", "User");
            }
            else
            {
                User user = _userRepository.GetByUserName(username);
                news.UserName = user.FullName;
                news.UserId = user.Id;
            }
            try
            {
                if (ModelState.IsValid)
                {
                    _newsRepository.Create(news, eventPictureFile);
                    return RedirectToAction("NewsList");
                }
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

		public IActionResult UpdateNews(int id)
		{
            var username = Request.Cookies["username"];
            ViewBag.Username = username;
            var news = _newsRepository.GetById(id);

			if (news == null)
			{
				return NotFound();
			}
			return View(news);
		}

		// Cập nhật tin tức
		[HttpPost]
		public IActionResult UpdateNews(News news, IFormFile eventPictureFile)
		{

            var username = Request.Cookies["username"];
            ViewBag.Username = username;
            
			try
			{
                News userResult = _newsRepository.GetById((int)news.Id);
                news.UserId = userResult.UserId;
                news.UserName = userResult.UserName;
                news.ViewCount = userResult.ViewCount;
                
                if (eventPictureFile == null)
				{
					news.ImageEvent = userResult.ImageEvent;
					_newsRepository.Update(news, eventPictureFile);
					return RedirectToAction("NewsList");
				}
				else
				{
					_newsRepository.Update(news, eventPictureFile);
					return RedirectToAction("NewsList");
				}
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message + "Unable to save changes. Try again, and if the problem persists see your system administrator.");

			}
			return View(news);
		}


        [HttpGet]
        [Route("News/Search")]
        public IActionResult SearchNewsList( string searchTerm)
        {
            int userId = 0;
            string userType = string.Empty;
            var username = Request.Cookies["username"];
            ViewBag.Username = username;
            if (username == null)
            {
                return RedirectToAction("LoginUser", "User");
            }
            else
            {
                List<News> eventsList = new List<News>();
                var user = _userRepository.GetByUserName(username);
                userId = (int)user.Id;
                userType = user.UserType;
                var result = _newsRepository.Search(searchTerm);
                if (userType.Equals("Admin"))
                {
                    eventsList = result;
                }
                else
                {
                    eventsList = result.Where(x => x.UserId == userId).ToList();
                }
                
                if (result.Count > 0)
                {
                    return Json(eventsList);
                }
                else
                {
                    return NotFound();
                }
            }
                
        }
        [HttpPost]
        [Route("/News/DeleteNews/{newsid:int}")]
        //[ValidateAntiForgeryToken]
        public IActionResult DeleteNews(int newsid)
        {
            try
            {
                _newsRepository.Delete(newsid);
                // Trả về một phản hồi JSON cho client
                return Json(new { success = true, message = "Xóa bài viết thành công" });
            }
            catch (Exception ex)
            {
                // Trả về một phản hồi JSON với thông báo lỗi nếu có lỗi xảy ra
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}

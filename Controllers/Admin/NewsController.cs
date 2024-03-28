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
            var eventsList = _newsRepository.GetAll();
            if(eventsList != null)
            {
                return View(eventsList);
            }
            return BadRequest("Lỗi") ;
        }
        [HttpGet]
        [Route("CreateNews")]
        public IActionResult CreateNews()
        {
            // here we create the model directly inside the view
            return View("CreateNews");
        }

        // Thêm mới event
        [HttpPost]
        [Route("CreateNews")]
        [ValidateAntiForgeryToken] // Prevents Cross Site Request Forgery
        public IActionResult CreateNews(News news, IFormFile eventPictureFile)
        
        {
            // Fix cứng người đăng tin 
            if (news.UserId == null)
            {
                string username = "Nguyễn Thế Khải";
                int userId = (int) _userRepository.GetAll()
                              .Where(user => user.FullName.Equals("Nguyễn Thế Khải")) 
                              .Select(user => user.Id)
                              .FirstOrDefault();
                news.UserId = userId;
                news.UserName = username;
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
			var news = _newsRepository.GetById(id);

			if (news == null)
			{
				return NotFound();
			}
			return View(news);
		}

		// Cập nhật hội viên
		[HttpPost]
		public IActionResult UpdateNews(News news, IFormFile eventPictureFile)
		{
			News userResult = _newsRepository.GetById((int)news.Id);
			// Fix cứng người đăng tin 
			if (news.UserId == null)
			{
				string username = "Nguyễn Thế Khải";
				int userId = (int)_userRepository.GetAll()
							  .Where(user => user.FullName.Equals("Nguyễn Thế Khải"))
							  .Select(user => user.Id)
							  .FirstOrDefault();
				news.UserId = userId;
				news.UserName = username;
			}
			try
			{
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
        [Route("News/SearchNewsByTitle")]
        public IActionResult SearchNewsList( string searchTerm)
        {
            var result = _newsRepository.Search(searchTerm);
            if (result.Count > 0)
            {
                return Json(result);
            }
            else
            {
                return NotFound();
            }
        }
	}
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TradeAssociationWebsite.DB;
using TradeAssociationWebsite.Models.Admin;
using TradeAssociationWebsite.Repositories.Interfaces;

namespace TradeAssociationWebsite.Controllers.Admin
{
    public class AdminController : Controller
    {
        //create dbContext variable
        private readonly IUserRepository _userRepository;
        public AdminController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

		public IActionResult Manager()
		{
            var username = Request.Cookies["username"];
            ViewBag.Username = username;
            return View();
		}
        [Route("ListOfUser")]
        public IActionResult UserList()
        {
            var username = Request.Cookies["username"];
            ViewBag.Username = username;
            var users = _userRepository.GetAll();
            if(users != null)
            {
                return View(users);
            }
            return BadRequest("Lỗi!");
        }


        // Chi tiết hội viên
		//[HttpGet]
		//[Route("DetailsUser/{id:int}")]
		//public IActionResult DetailsUser(int id)
  //      {
  //          var user = _userRepository.GetById(id);
  //          if (user == null)
  //          {
  //              return NotFound();
  //          }
  //          return View(user);
  //      }



		public IActionResult UpdateUser(int id)
		{
            var username = Request.Cookies["username"];
            ViewBag.Username = username;
            var user = _userRepository.GetById(id);

			if (user == null)
			{
				return NotFound();
			}
			return View(user);
		}

		// Cập nhật hội viên
		[HttpPost]
		public IActionResult UpdateUser(User user, IFormFile userPictureFile)
		{
            var username = Request.Cookies["username"];
            ViewBag.Username = username;
            User userResult = _userRepository.GetById((int)user.Id);
			if(userResult.Password != null)
			{
				user.Password = userResult.Password;
			}
			try
			{
				if (userPictureFile == null)
				{
					user.UserPicture = userResult.UserPicture;
					_userRepository.Update(user, userPictureFile);
					return RedirectToAction("UserList");
				}
				else
				{	
					_userRepository.Update(user, userPictureFile);
					return RedirectToAction("UserList");
				}
			}catch (Exception ex)
			{
				ModelState.AddModelError("",ex.Message + "Unable to save changes. Try again, and if the problem persists see your system administrator.");

			}
			return View(user);
		}

		[HttpGet]
		[Route("CreateUser")]
		public IActionResult CreateUser()
		{
            var username = Request.Cookies["username"];
            ViewBag.Username = username;
            // here we create the model directly inside the view
            return View("CreateUser");
		}

		// Thêm mới hội viên
		[HttpPost]
		[ValidateAntiForgeryToken] // Prevents Cross Site Request Forgery
		[Route("CreateUser")]
		public IActionResult CreateUser(User user, IFormFile userPictureFile)
        {           
			try
			{
				if (ModelState.IsValid)
				{
					_userRepository.Create(user, userPictureFile);
					return RedirectToAction("UserList");
				}
				return View();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		public IActionResult DeleteUser(int id)
		{
			if( id == 0)
			{
				return BadRequest(id.ToString());

			}
			else
			{
				User user = _userRepository.GetById(id);
				if (user == null) return NotFound();
				return View(user);
			}

			
		}

		//Xóa hội viên
		[HttpPost]
		[Route("DeleteUser/{id:int}")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteUser(int id, User user)
		{
			try
			{
				if (ModelState.IsValid)
				{
					if (id == 0)
					{
						return BadRequest("Id người dùng không hợp lệ");
					}

					_userRepository.Delete(id);
					return RedirectToAction("UserList");
					
				}
				return View();
			}catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		


	}
}

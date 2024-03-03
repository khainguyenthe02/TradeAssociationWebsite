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
        [Route("ListOfUser")]
        public IActionResult UserList()
        {
            var users = _userRepository.GetAll();
            if(users != null)
            {
                return View(users);
            }
            return BadRequest("Lỗi!");
        }


        // Chi tiết hội viên
		[HttpGet]
		[Route("DetailsUser/{id:int}")]
		public IActionResult DetailsUser(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }


        // Cập nhật hội viên
		[HttpGet]
		[Route("UpdateUser/{id:int}")]
		public IActionResult UpdateUser(int id)
		{
			var user = _userRepository.GetById(id);
			if (user == null)
			{
				return NotFound();
			}
			return View(user);
		}

		[HttpGet]
		[Route("CreateUser")]
		// Method invoked when pressing the Add New Movie Button
		public IActionResult CreateUser()
		{
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
					return RedirectToAction("ListOfUser");
				}
				return View();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}

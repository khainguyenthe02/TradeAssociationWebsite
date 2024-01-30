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

	}
}

using Microsoft.AspNetCore.Mvc;
using TradeAssociationWebsite.DB;
using TradeAssociationWebsite.Repositories;
using TradeAssociationWebsite.Repositories.Interfaces;

namespace TradeAssociationWebsite.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [Route("LoginUser")]
        public IActionResult LoginUser()
        {
            return View("LoginUser");
        }
        public IActionResult Login(string username, string password)
        {
            if( ModelState.IsValid )
            {
                var user = _userRepository.Login(username, password);
                if( user != null )
                {
                    // Tạo một cookie chứa thông tin đăng nhập
                    var cookieOptions = new CookieOptions
                    {
                        // Thiết lập thời gian sống của cookie (ví dụ: 7 phút)
                        Expires = DateTime.Now.AddMinutes(7),
                        // Đảm bảo rằng cookie chỉ được gửi qua HTTPS nếu bạn đang sử dụng HTTPS
                        Secure = true,
                        // Đảm bảo rằng cookie không thể truy cập bằng JavaScript
                        HttpOnly = true
                    };
                    Response.Cookies.Append("username", username, cookieOptions);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return NotFound("Người dùng không tồn tại");
                }
            }
            else
            {
                return BadRequest("Vui lòng nhập đầy đủ thông tin");
            }
        }
    }
}

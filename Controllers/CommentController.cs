using Microsoft.AspNetCore.Mvc;
using TradeAssociationWebsite.Models.Admin;
using TradeAssociationWebsite.Repositories.Interfaces;

namespace TradeAssociationWebsite.Controllers
{
    public class CommentController : Controller
    {
        private readonly INewsRepository _newsRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICommentRepository _commentRepository;
        public CommentController(INewsRepository newsRepository, IUserRepository userRepository, ICommentRepository commentRepository)
        {
            _newsRepository = newsRepository;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
        }
        [HttpPost]
        public IActionResult CreateComment(int newsid, string content)
        {
            int userId;
            string fullName = "";
            var username = Request.Cookies["username"];
            if (username == null)
            {
                return RedirectToAction("LoginUser", "User");
            }
            else
            {
                ViewBag.Username = username;
                userId = (int)_userRepository.GetByUserName(username).Id;
                if(userId > 0)
                {
                    fullName = _userRepository.GetById(userId).FullName;
                    Comment comment = new Comment
                    {
                        UserId = userId,
                        FullName = fullName,
                        Content = content,
                        NewsId = newsid
                    };
                    _commentRepository.Create(comment);
                    
                }
                return RedirectToAction("NewsDetail", "Home",  new { newid = newsid });
            }
            
        }
    }
}

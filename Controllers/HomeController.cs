using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TradeAssociationWebsite.Models;
using TradeAssociationWebsite.Models.Admin;
using TradeAssociationWebsite.Repositories.Interfaces;

namespace TradeAssociationWebsite.Controllers;

public class HomeController : Controller
{
    public void GetUserName()
    {
        GetUserName();
        //var username = Request.Cookies["username"];
        //ViewBag.Username = username;
    }
    private readonly INewsRepository _newsRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICommentRepository _commentRepository;
    public HomeController(INewsRepository newsRepository, IUserRepository userRepository, ICommentRepository commentRepository)
    {
        _newsRepository = newsRepository;
        _userRepository = userRepository;
        _commentRepository = commentRepository;
    }

    public IActionResult Index()
    {
        var username = Request.Cookies["username"];
        ViewBag.Username = username;
        var mostViewedNews = _newsRepository.GetMostViewedNews(); // Lấy danh sách tin tức có lượt xem nhiều nhất
        var latestNews = _newsRepository.GetLastNews(); // Lấy danh sách tin tức mới nhất
        var viewModel = new NewsIndexModel
        {
            MostViewedNews = mostViewedNews,
            LatestNews = latestNews
        };
        return View(viewModel); // Truyền hai danh sách vào view
    }

    // Load list
    [HttpGet]
    [Route("/Home/SearchListEnginee")]
    public IActionResult SearchListEnginee(string searchTerm)
    {
        return View("SearchListEnginee");
    }
    [HttpGet]
    [Route("/Home/SearchListEnginee1")]
    public ActionResult SearchListEnginee1(string searchTerm,  int page = 1, int pageSize = 5)
    {
        var username = Request.Cookies["username"];
        ViewBag.Username = username;
        var newList = new List<News>();
        newList = _newsRepository.SearchByTitle(searchTerm);
        // Tính toán chỉ mục bắt đầu và chỉ mục kết thúc của phân trang
        int startIndex = (page - 1) * pageSize;
        int endIndex = startIndex + pageSize;
        // Lấy ra danh sách tin tức cho trang hiện tại
        var pagedNewsList = newList.Skip(startIndex).Take(pageSize).ToList();
        if (pagedNewsList.Count > 0)
        {
            return Json(pagedNewsList);
        }
        else
        {
            return NotFound();
        }
        //
    }

    [HttpGet]
    [Route("Home/NewsDetail/{newid:int}")]
    public IActionResult NewsDetail(int newid)
    {
        var user = new User();
        var username = Request.Cookies["username"];
        if(username == null)
        {
            return RedirectToAction("LoginUser", "User");
        }
        else
        {
            ViewBag.Username = username;
            user = _userRepository.GetByUserName(username);
        }        
        var news = _newsRepository.GetById(newid);        
        if (news != null)
        {
           _newsRepository.UpdateForViewCount(news);
            var commentsList = _commentRepository.GetCommentsByNews(newid);
            ViewNewsModel newsDetail = new ViewNewsModel{
                News = news,
                User = user,
                CommentList = commentsList            
            };
            return View(newsDetail);
        }
        else { return NotFound(); }
        
    }

    //Profile User
    [HttpGet]
    public IActionResult ProfileDetail(string username)
    {
        username = Request.Cookies["username"];
        ViewBag.Username = username;
        if (username != null)
        {
            User userProfile = _userRepository.GetByUserName(username);
            return View(userProfile);
        }
        else
        {
            return RedirectToAction("LoginUser", "User");
        }
        
    }

    [HttpPost]
    public IActionResult CreateComment(string username, int newsid)
    {
        username = Request.Cookies["username"];
        if (!string.IsNullOrEmpty(username) && newsid > 0)
        {
            var userId = _userRepository.GetByUserName(username).Id;
            Comment comment = new Comment
            {
                UserId = userId,
                NewsId = newsid
            };
            _commentRepository.Create(comment);
            return View("NewsDetail");
        }
        else
        {
            return RedirectToAction("LoginUser", "User");
        }

    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

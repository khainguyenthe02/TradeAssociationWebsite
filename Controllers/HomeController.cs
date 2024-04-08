using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TradeAssociationWebsite.Models;
using TradeAssociationWebsite.Models.Admin;
using TradeAssociationWebsite.Repositories.Interfaces;

namespace TradeAssociationWebsite.Controllers;

public class HomeController : Controller
{

    private readonly INewsRepository _newsRepository;
    private readonly IUserRepository _userRepository;
    public HomeController(INewsRepository newsRepository, IUserRepository userRepository)
    {
        _newsRepository = newsRepository;
        _userRepository = userRepository;
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
        var news = _newsRepository.GetById(newid);
        if (news != null)
        {
           _newsRepository.UpdateForViewCount(news);
            ViewNewsModel newsDetail = new ViewNewsModel{
                News = news
            };
            return View(newsDetail);
        }
        else { return NotFound(); }
        
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

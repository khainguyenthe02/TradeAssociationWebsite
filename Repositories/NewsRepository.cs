using Microsoft.EntityFrameworkCore;
using TradeAssociationWebsite.DB;
using TradeAssociationWebsite.Models.Admin;
using TradeAssociationWebsite.Repositories.Interfaces;

namespace TradeAssociationWebsite.Repositories
{
    public class NewsRepository : INewsRepository
    {
        //create dbContext variable
        private readonly AppDBContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public NewsRepository(AppDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }
        public void Create(News news, IFormFile eventPictureFile)
        {

            // Lưu trữ hình ảnh vào thư mục Images nếu có
            if (eventPictureFile != null && eventPictureFile.Length > 0)
            {
                string imagesFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Events");
                if (!Directory.Exists(imagesFolderPath))
                {
                    Directory.CreateDirectory(imagesFolderPath);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(eventPictureFile.FileName);
                string filePath = Path.Combine(imagesFolderPath, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    eventPictureFile.CopyTo(stream);
                }

                news.ImageEvent = uniqueFileName;
            }
            _context.News.Add(news);
            _context.SaveChanges();
        }

        public bool Delete(int id)
        {
            var news = _context.News.Find(id);
            if (news != null)
            {
                _context.News.Remove(news);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<News> GetAll()
        {
            var newsList = _context.News.ToList();
            return newsList;
        }

        public News GetById(int id)
        {
            var news = _context.News.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (news != null)
            {
                return news;
            }
            return new News();
        }

        public List<News> Search(string searchTerm)
        {
            var newsList = new List<News>();
            //var newsList = _context.SearchNewsByTitle(searchTerm).ToList();
            if (string.IsNullOrEmpty(searchTerm))
            {
                newsList = _context.News.ToList();
            }
            else
            {
                newsList = _context.News.Where(u => u.Title.Contains(searchTerm)).ToList();
            }
            
            return newsList;
        }

        public bool Update(News news, IFormFile eventPictureFile)
        {
            string eventOldPicture = news.ImageEvent;
            // Lưu trữ hình ảnh vào thư mục Images nếu có
            if (eventPictureFile != null && eventPictureFile.Length > 0)
            {
                string imagesFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Events");
                if (!Directory.Exists(imagesFolderPath))
                {
                    Directory.CreateDirectory(imagesFolderPath);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(eventPictureFile.FileName);
                string filePath = Path.Combine(imagesFolderPath, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    eventPictureFile.CopyTo(stream);
                }
                news.ImageEvent = uniqueFileName;
            }
            else
            {
                news.ImageEvent = eventOldPicture;
            }

            _context.News.Update(news);
            _context.SaveChanges();
            return true;
        }
 
    }
}

using TradeAssociationWebsite.Models.Admin;

namespace TradeAssociationWebsite.Repositories.Interfaces
{
    public interface INewsRepository
    {
        void Create(News news, IFormFile fromFile);
        List<News> GetAll();
        bool Update(News news, IFormFile fromFile);
        bool UpdateForViewCount(News news);
        bool Delete(int id);
        News GetById(int id);
        List<News> GetMostViewedNews();
        List<News> GetLastNews();
        List<News> Search( string searchTerm);
        List<News> SearchByTitle(string titleSearch);
    }
}

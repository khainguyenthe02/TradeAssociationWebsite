using TradeAssociationWebsite.Models.Admin;

namespace TradeAssociationWebsite.Repositories.Interfaces
{
    public interface INewsRepository
    {
        void Create(News news, IFormFile fromFile);
        List<News> GetAll();
        bool Update(News news, IFormFile fromFile);
        bool Delete(int id);
        News GetById(int id);
        List<News> Search();
    }
}

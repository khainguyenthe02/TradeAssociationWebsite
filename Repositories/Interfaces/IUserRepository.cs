using Microsoft.AspNetCore.Http;
using TradeAssociationWebsite.Models.Admin;

namespace TradeAssociationWebsite.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void Create(User user, IFormFile fromFile);
        List<User> GetAll();
        bool Update(User user, IFormFile fromFile);
        bool Delete(int id);
        User GetById(int id);
        List<User> Search();
    }
}

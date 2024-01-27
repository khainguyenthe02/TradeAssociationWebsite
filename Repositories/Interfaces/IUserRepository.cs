using TradeAssociationWebsite.Models.Admin;

namespace TradeAssociationWebsite.Repositories.Interfaces
{
    public interface IUserRepository
    {
        string Create(User user);
        List<User> GetAll();
        bool Update(User user);
        bool Delete(int id);
        User GetById(int id);
        List<User> Search();
    }
}

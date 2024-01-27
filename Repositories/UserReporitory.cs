using Microsoft.AspNetCore.Mvc;
using TradeAssociationWebsite.DB;
using TradeAssociationWebsite.Models.Admin;
using TradeAssociationWebsite.Repositories.Interfaces;

namespace TradeAssociationWebsite.Repositories
{
    public class UserReporitory : IUserRepository
    {
        //create dbContext variable
        private readonly AppDBContext _context;

        public UserReporitory(AppDBContext context)
        {
            _context = context;
        }
        public string Create(User user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            var user = _context.User.Find(id);
            if (user != null)
            {
                _context.User.Remove(user);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public  List<User> GetAll()
        {
            var users = _context.User.ToList();
            return users;
        }

        public User GetById(int id)
        {
            var user = _context.User.FirstOrDefault(x => x.Id == id);
            if( user != null)
            {
                return user;
            }
            return null;
        }

        public List<User> Search()
        {
            throw new NotImplementedException();
        }

        public bool Update(User user)
        {
            throw new NotImplementedException();
        }


    }
}

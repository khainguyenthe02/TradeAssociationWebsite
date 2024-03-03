using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TradeAssociationWebsite.DB;
using TradeAssociationWebsite.Models.Admin;
using TradeAssociationWebsite.Repositories.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace TradeAssociationWebsite.Repositories
{
    public class UserReporitory : IUserRepository
    {
        //create dbContext variable
        private readonly AppDBContext _context;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public UserReporitory(AppDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
			_webHostEnvironment = webHostEnvironment;

		}
        public void Create(User user, IFormFile userPictureFile)
        {
			// Generate a random password
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			var random = new Random();
			user.Password = new string(Enumerable.Repeat(chars, 8) // Change 8 to the desired length of the password
				.Select(s => s[random.Next(s.Length)]).ToArray());
			user.CreatedAt = DateTime.Now;
			// Lưu trữ hình ảnh vào thư mục Images nếu có
			if (userPictureFile != null && userPictureFile.Length > 0)
			{
				//string imagesFolderPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images");
				string imagesFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
				if (!Directory.Exists(imagesFolderPath))
				{
					Directory.CreateDirectory(imagesFolderPath);
				}

				string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(userPictureFile.FileName);
				string filePath = Path.Combine(imagesFolderPath, uniqueFileName);

				using (var stream = new FileStream(filePath, FileMode.Create))
				{
				     userPictureFile.CopyToAsync(stream);
				}

				user.UserPicture = uniqueFileName;
			}
			_context.User.Add(user);
            _context.SaveChanges();

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
            return user;
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

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using TradeAssociationWebsite.DB;
using TradeAssociationWebsite.Models.Admin;
using TradeAssociationWebsite.Repositories.Interfaces;

namespace TradeAssociationWebsite.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        //create dbContext variable
        private readonly AppDBContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CommentRepository(AppDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }
        public void Create(Comment comment)
        {
            _context.Comment.Add(comment);
            _context.SaveChanges();
        }

        public List<Comment> GetCommentsByNews(int newsId)
        {
            var comments = _context.Comment.Where(cmt => cmt.NewsId == newsId).ToList();
            return comments;
        }
    }
}

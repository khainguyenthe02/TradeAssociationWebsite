
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TradeAssociationWebsite.Models.Admin;

namespace TradeAssociationWebsite.DB
{
    public class AppDBContext: DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
                : base(options)
        { }
        public DbSet<User> User { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Comment> Comment { get; set; }

        public IQueryable<News> SearchNewsByTitle(string searchTerm)
        {
            SqlParameter psearchTerm = new SqlParameter("@searchTerm", searchTerm);
            return this.News.FromSqlRaw("EXECUTE SearchNewsByTitle @searchTerm", psearchTerm);
        }

        public IQueryable<News> DeleteNews(int newsId)
        {
            return this.News.FromSqlRaw("EXECUTE DeleteNews @newsId", new SqlParameter("@newsId", newsId));
        }
        public IQueryable<News> GetMostViewedNews()
        {
            return this.News.FromSqlRaw("EXECUTE GetMostViewedNews");
        }

        public IQueryable<News> GetLastNews()
        {
            return this.News.FromSqlRaw("EXECUTE GetLastNews");
        }

    }
}

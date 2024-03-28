
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


        // Proc search
        public DbSet<News> SearchNewsByTitle(string searchTerm)
        {
            return (DbSet<News>)News.FromSqlInterpolated($"EXEC SearchNewsByTitle {searchTerm}");
        }
    }
}

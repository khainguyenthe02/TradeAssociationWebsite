using TradeAssociationWebsite.Models.Admin;

namespace TradeAssociationWebsite.Models
{
    public class NewsIndexModel
    {
        public List<News>? MostViewedNews { get; set; }
        public List<News>? LatestNews { get; set; }
    }
}

using TradeAssociationWebsite.Models.Admin;

namespace TradeAssociationWebsite.Models
{
    public class ViewNewsModel
    {
        public News? News { get; set; }
        public User? User { get; set; }
        public Comment? CommentItem { get; set; }
        public List<Comment>? CommentList { get; set; }
    }
}

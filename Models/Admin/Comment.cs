namespace TradeAssociationWebsite.Models.Admin
{
    public class Comment
    {
        public int? Id { get; set; }
        public string? Content { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public int? UserId { get; set; }
        public int? NewsId { get; set; }
        public string? FullName { get; set; }
    }
}

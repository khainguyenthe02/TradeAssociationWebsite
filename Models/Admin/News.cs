namespace TradeAssociationWebsite.Models.Admin
{
    public class News
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Contents { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public string? Type { get; set; }
        public int? UserId { get; set; }
        public string? UserName { get; set; }
        public string? ImageEvent { get; set; }
        public int? ViewCount { get; set; }

    }
}

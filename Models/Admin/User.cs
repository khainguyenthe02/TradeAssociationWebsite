using System.ComponentModel.DataAnnotations;

namespace TradeAssociationWebsite.Models.Admin
{
    public class User
    {
        public int? Id { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? UserPicture { get; set; }
        public int? UserType { get; set; }
        public DateTime? CreatedAt { get; set; }

    }
}

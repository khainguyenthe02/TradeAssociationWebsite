using TradeAssociationWebsite.Models.Admin;

namespace TradeAssociationWebsite.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        void Create(Comment comment);
        List<Comment> GetCommentsByNews( int newsId);
    }
}

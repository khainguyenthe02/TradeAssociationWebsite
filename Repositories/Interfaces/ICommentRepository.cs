using TradeAssociationWebsite.Models.Admin;

namespace TradeAssociationWebsite.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        void Create(Comment comment);
        Comment GetById(int id);
        bool Delete(int id);
        List<Comment> GetCommentsByNews( int newsId);
    }
}

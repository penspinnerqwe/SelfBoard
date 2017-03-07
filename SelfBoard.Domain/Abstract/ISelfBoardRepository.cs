using System.Linq;
using SelfBoard.Domain.Entities;

namespace SelfBoard.Domain.Abstract
{
    public interface ISelfBoardRepository
    {
        IQueryable<Message> Messages { get; }
        IQueryable<User> Users { get; }
        IQueryable<AuthUser> AuthUsers { get; }
        IQueryable<Frend> Frends { get; }
        IQueryable<Photo> Photos { get; }
        IQueryable<Like> Likes { get; }
        IQueryable<Comment> Comments { get; }

        void AddMessage(Message Item);
        void AddUser(User Item);
        void AddAuthUser(AuthUser Item);
        void AddFrend(Frend Item);
        void AddPhoto(Photo Item);
        void AddLike(Like Item);
        void AddComment(Comment Item);

        void DeleteFrend(Frend Item);
        void DeleteLike(Like Item);
        void DeleteComment(Comment Item);
        void DeletePhoto(Photo Item);

        void SaveContextChanges();      
    }
}

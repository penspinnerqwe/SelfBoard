using System.Linq;
using SelfBoard.Domain.Entities;

using SelfBoard.Domain.Abstract;

namespace SelfBoard.Domain.Concrete
{
    public class EFSelfBoardRepository : ISelfBoardRepository
    {
        EFSelfBoardContext Context = new EFSelfBoardContext();
        public IQueryable<Message> Messages { get { return Context.Messages; } }
        public IQueryable<User> Users { get { return Context.Users; } }
        public IQueryable<AuthUser> AuthUsers { get { return Context.AuthUsers; } }
        public IQueryable<Frend> Frends { get { return Context.Frends; } }
        public IQueryable<Photo> Photos { get { return Context.Photos; } }
        public IQueryable<Like> Likes { get { return Context.Likes; } }
        public IQueryable<Comment> Comments { get { return Context.Comments; } }

        public void AddMessage(Message Item) { Context.Messages.Add(Item); }
        public void AddUser(User Item) { Context.Users.Add(Item); }
        public void AddAuthUser(AuthUser Item) { Context.AuthUsers.Add(Item); }
        public void AddFrend(Frend Item) { Context.Frends.Add(Item); }
        public void AddPhoto(Photo Item) { Context.Photos.Add(Item); }
        public void AddLike(Like Item) { Context.Likes.Add(Item); }
        public void AddComment(Comment Item) { Context.Comments.Add(Item); }

        public void DeleteFrend(Frend Item) { Context.Frends.Remove(Item); }
        public void DeleteLike(Like Item) { Context.Likes.Remove(Item); }
        public void DeleteComment(Comment Item) { Context.Comments.Remove(Item); }
        public void DeletePhoto(Photo Item) { Context.Photos.Remove(Item); }

        public void SaveContextChanges()
        {
            Context.SaveChanges();
        }
    }
}

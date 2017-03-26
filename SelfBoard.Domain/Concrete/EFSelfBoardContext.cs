using System.Data.Entity;
using SelfBoard.Domain.Entities;

namespace SelfBoard.Domain.Concrete
{
    public class EFSelfBoardContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AuthUser> AuthUsers { get; set; }
        public DbSet<Frend> Frends { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}

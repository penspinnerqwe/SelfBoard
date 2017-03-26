using Microsoft.AspNet.Identity.EntityFramework;
using SelfBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfBoard.Domain.Concrete
{
    public class EFSelfBoardContext : IdentityDbContext<ApplicationUser>
    {
        public EFSelfBoardContext() : base("EFSelfBoardContext") { }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Frend> Frends { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public static EFSelfBoardContext Create()
        {
            return new EFSelfBoardContext();
        }
    }
}

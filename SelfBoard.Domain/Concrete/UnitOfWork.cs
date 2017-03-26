using SelfBoard.Domain.EntityRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfBoard.Domain.Concrete
{
    public class UnitOfWork : IDisposable
    {
        private EFSelfBoardContext DBContext = new EFSelfBoardContext();

        private ApplicationUserRepo applicationUserRepo;
        private CommentRepo commentRepo;
        private FrendRepo frendRepo;
        private LikeRepo likeRepo;
        private MessageRepo messageRepo;
        private PhotoRepo photoRepo;

        public ApplicationUserRepo ApplicationUsers
        {
            get
            {
                if (applicationUserRepo == null)
                    applicationUserRepo = new ApplicationUserRepo(DBContext);
                return applicationUserRepo;
            }
        }
        public CommentRepo Comments
        {
            get
            {
                if (commentRepo == null)
                    commentRepo = new CommentRepo(DBContext);
                return commentRepo;
            }
        }
        public FrendRepo Frends
        {
            get
            {
                if (frendRepo == null)
                    frendRepo = new FrendRepo(DBContext);
                return frendRepo;
            }
        }
        public LikeRepo Likes
        {
            get
            {
                if (likeRepo == null)
                    likeRepo = new LikeRepo(DBContext);
                return likeRepo;
            }
        }
        public MessageRepo Messages
        {
            get
            {
                if (messageRepo == null)
                    messageRepo = new MessageRepo(DBContext);
                return messageRepo;
            }
        }
        public PhotoRepo Photos
        {
            get
            {
                if (photoRepo == null)
                    photoRepo = new PhotoRepo(DBContext);
                return photoRepo;
            }
        }

        public void Save()
        {
            DBContext.SaveChanges();
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DBContext.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

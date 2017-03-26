using SelfBoard.Domain.Abstract;
using SelfBoard.Domain.Concrete;
using SelfBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfBoard.Domain.EntityRepositories
{
    public class LikeRepo : IObjectRepository<Like>
    {
        private EFSelfBoardContext DBContext;

        public LikeRepo(EFSelfBoardContext context)
        {
            this.DBContext = context;
        }
        public IEnumerable<Like> GetObjects()
        {
            return DBContext.Likes;
        }
        public Like GetObjectByID(int ObjectId)
        {
            return DBContext.Likes.Find(ObjectId);
        }
        public void InsertObject(Like Object)
        {
            DBContext.Likes.Add(Object);
        }
        public void DeleteObject(int ObjectID)
        {
            Like like = DBContext.Likes.Find(ObjectID);
            if (like != null)
                DBContext.Likes.Remove(like);
        }
        public void UpdateObject(Like Object)
        {
            DBContext.Entry(Object).State = EntityState.Modified;
        }
    }
}

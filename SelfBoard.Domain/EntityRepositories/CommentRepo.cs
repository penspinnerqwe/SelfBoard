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
    public class CommentRepo : IObjectRepository<Comment>
    {
        private EFSelfBoardContext DBContext;

        public CommentRepo(EFSelfBoardContext context)
        {
            this.DBContext = context;
        }
        public IEnumerable<Comment> GetObjects()
        {
            return DBContext.Comments;
        }
        public Comment GetObjectByID(int ObjectId)
        {
            return DBContext.Comments.Find(ObjectId);
        }
        public void InsertObject(Comment Object)
        {
            DBContext.Comments.Add(Object);
        }
        public void DeleteObject(int ObjectID)
        {
            Comment comment = DBContext.Comments.Find(ObjectID);
            if (comment != null)
                DBContext.Comments.Remove(comment);
        }
        public void UpdateObject(Comment Object)
        {
            DBContext.Entry(Object).State = EntityState.Modified;
        }
    }
}

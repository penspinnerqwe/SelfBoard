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
    public class MessageRepo : IObjectRepository<Message>
    {
        private EFSelfBoardContext DBContext;

        public MessageRepo(EFSelfBoardContext context)
        {
            this.DBContext = context;
        }
        public IEnumerable<Message> GetObjects()
        {
            return DBContext.Messages;
        }
        public Message GetObjectByID(int ObjectId)
        {
            return DBContext.Messages.Find(ObjectId);
        }
        public void InsertObject(Message Object)
        {
            DBContext.Messages.Add(Object);
        }
        public void DeleteObject(int ObjectID)
        {
            Message message = DBContext.Messages.Find(ObjectID);
            if (message != null)
                DBContext.Messages.Remove(message);
        }
        public void UpdateObject(Message Object)
        {
            DBContext.Entry(Object).State = EntityState.Modified;
        }
    }
}

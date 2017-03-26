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
    public class FrendRepo : IObjectRepository<Frend>
    {
        private EFSelfBoardContext DBContext;

        public FrendRepo(EFSelfBoardContext context)
        {
            this.DBContext = context;
        }
        public IEnumerable<Frend> GetObjects()
        {
            return DBContext.Frends;
        }
        public Frend GetObjectByID(int ObjectId)
        {
            return DBContext.Frends.Find(ObjectId);
        }
        public void InsertObject(Frend Object)
        {
            DBContext.Frends.Add(Object);
        }
        public void DeleteObject(int ObjectID)
        {
            Frend frend = DBContext.Frends.Find(ObjectID);
            if (frend != null)
                DBContext.Frends.Remove(frend);
        }
        public void UpdateObject(Frend Object)
        {
            DBContext.Entry(Object).State = EntityState.Modified;
        }

        public IEnumerable<ApplicationUser> GetAllFrends(string UserId)
        {
            var MessageStrings = DBContext.Frends
               .Where(x => (x.SenderId == UserId || x.ReceiverId == UserId) && x.State == 1)
               .Select(x => x.SenderId == UserId ? x.Receiver : x.Sender);

            return MessageStrings;
        }
        public IEnumerable<ApplicationUser> GetInFrendsRequest(string UserId)
        {
            var MessageStrings = DBContext.Frends
               .Where(x => x.ReceiverId == UserId && x.State == 0)
               .Select(x => x.Sender);

            return MessageStrings;
        }
        public IEnumerable<ApplicationUser> GetOutFrendsRequest(string UserId)
        {
            var MessageStrings = DBContext.Frends
               .Where(x => x.SenderId == UserId && x.State == 0)
               .Select(x => x.Receiver);

            return MessageStrings;
        }
    }
}

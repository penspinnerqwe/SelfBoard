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
    public class ApplicationUserRepo 
    {
        private EFSelfBoardContext DBContext;

        public ApplicationUserRepo(EFSelfBoardContext context)
        {
            this.DBContext = context;
        }
        public IEnumerable<ApplicationUser> GetObjects()
        {
            return DBContext.Users;
        }
        public ApplicationUser GetObjectByID(string ObjectId)
        {
            return DBContext.Users.Find(ObjectId);
        }
        public void InsertObject(ApplicationUser Object)
        {
            DBContext.Users.Add(Object);
        }
        public void DeleteObject(string ObjectID)
        {
            ApplicationUser user = DBContext.Users.Find(ObjectID);
            if (user != null)
                DBContext.Users.Remove(user);
        }
        public void UpdateObject(ApplicationUser Object)
        {
            DBContext.Entry(Object).State = EntityState.Modified;
        }
    }
}

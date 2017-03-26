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
    public class PhotoRepo : IObjectRepository<Photo>
    {
        private EFSelfBoardContext DBContext;

        public PhotoRepo(EFSelfBoardContext context)
        {
            this.DBContext = context;
        }
        public IEnumerable<Photo> GetObjects()
        {
            return DBContext.Photos;
        }
        public Photo GetObjectByID(int ObjectId)
        {
            return DBContext.Photos.Find(ObjectId);
        }
        public void InsertObject(Photo Object)
        {
            DBContext.Photos.Add(Object);
        }
        public void DeleteObject(int ObjectID)
        {
            Photo photo = DBContext.Photos.Find(ObjectID);
            if (photo != null)
                DBContext.Photos.Remove(photo);
        }
        public void UpdateObject(Photo Object)
        {
            DBContext.Entry(Object).State = EntityState.Modified;
        }

        public Photo GetUsersAvatar(string UserId)
        {
            var Avatar = DBContext.Users
                .Where(x => x.Id == UserId)
                .Select(x => x.Avatar)
                .FirstOrDefault();

            return Avatar;
        }
    }
}

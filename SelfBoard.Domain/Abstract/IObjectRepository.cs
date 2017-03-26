using System.Collections.Generic;

namespace SelfBoard.Domain.Abstract
{
    public interface IObjectRepository<Type> where Type : class
    {
        IEnumerable<Type> GetObjects();
        Type GetObjectByID(int ObjectId);
        void InsertObject(Type Object);
        void DeleteObject(int ObjectID);
        void UpdateObject(Type Object);
    }
}

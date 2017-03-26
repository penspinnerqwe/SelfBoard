using System.Collections.Generic;
using SelfBoard.Domain.Entities;

namespace SelfBoard.WebUI.Infrastructure
{
    public class MessageComparer : IEqualityComparer<Message>
    {
        public bool Equals(Message x, Message y)
        {
            if ((x.ReceiverId == y.ReceiverId && x.SenderId == y.SenderId) ||
                (x.ReceiverId == y.SenderId && x.SenderId == y.ReceiverId))
                return true;
            else
                return false;
        }

        public int GetHashCode(Message obj)
        {
            return obj.ReceiverId.GetHashCode() + obj.SenderId.GetHashCode();
        }
    }
}
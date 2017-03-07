using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfBoard.Domain.Entities
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Photo")]
        public int? PhotoId { get; set; }
        public Photo Photo { get; set; }
    }
}

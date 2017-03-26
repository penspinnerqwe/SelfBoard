using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfBoard.Domain.Entities
{
    public class Frend
    {
        [Key]
        public int FrendId { get; set; }

        [Range(0, 2)]
        public int State { get; set; }

        [ForeignKey("Receiver")]
        public int? ReceiverId { get; set; }
        public User Receiver { get; set; }

        [ForeignKey("Sender")]
        public int? SenderId { get; set; }
        public User Sender { get; set; }
    }
}

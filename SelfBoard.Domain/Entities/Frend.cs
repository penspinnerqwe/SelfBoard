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
        public string ReceiverId { get; set; }
        public ApplicationUser Receiver { get; set; }

        [ForeignKey("Sender")]
        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }
    }
}

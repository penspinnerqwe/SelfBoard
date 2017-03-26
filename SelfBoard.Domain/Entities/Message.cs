using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SelfBoard.Domain.Entities
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        [Required(ErrorMessage = "Введите ваше сообщение")]
        public string MessageString { get; set; }

        public DateTime SendDate { get; set; }

        [Range(0, 1)]
        public int State { get; set; }

        [ForeignKey("Receiver")]
        public string ReceiverId { get; set; }
        public ApplicationUser Receiver { get; set; }

        [ForeignKey("Sender")]
        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }
    }
}

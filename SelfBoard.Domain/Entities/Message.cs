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
        public int? ReceiverId { get; set; }
        public User Receiver { get; set; }

        [ForeignKey("Sender")]
        public int? SenderId { get; set; }
        public User Sender { get; set; }
    }
}

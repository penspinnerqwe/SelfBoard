using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SelfBoard.Domain.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Range(0, 1)]
        public int Sex { get; set; }

        [Required(ErrorMessage = "Проверьте дату рождения")]
        public DateTime BirthDay { get; set; }

        [Required(ErrorMessage = "Проверьте имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Проверьте фамилию")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Проверьте информацию о себе")]
        public string AboutMe { get; set; }

        [Range(0, 1)]
        public int Online { get; set; }

        [ForeignKey("Avatar")]
        public int? AvatarId { get; set; }
        public Photo Avatar { get; set; }

        public ICollection<Message> Messages { get; set; }
        public ICollection<Frend> Frends { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public User()
        {
            Messages = new List<Message>();
            Frends = new List<Frend>();
            Photos = new List<Photo>();
        }
    }
}

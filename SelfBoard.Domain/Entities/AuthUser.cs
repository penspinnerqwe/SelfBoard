using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfBoard.Domain.Entities
{
    public class AuthUser
    {
        [Key]
        [Required(ErrorMessage = "Проверьте логин")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Проверьте Пароль")]
        public string Password { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}

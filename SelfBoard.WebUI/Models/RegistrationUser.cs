using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SelfBoard.Domain.Entities;

namespace SelfBoard.WebUI.Models
{
    public class RegistrationUser
    {
        public AuthUser UserAuthData { get; set; }
        public User UserInfarmation { get; set; }
        public string StringSex { get; set; }
    }
}
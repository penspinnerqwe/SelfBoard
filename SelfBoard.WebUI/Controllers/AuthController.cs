using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SelfBoard.Domain.Abstract;
using SelfBoard.WebUI.Models;

namespace SelfBoard.WebUI.Controllers
{
    public class AuthController : Controller
    {
        private ISelfBoardRepository DBContext;
        public AuthController(ISelfBoardRepository DBContext)
        {
            this.DBContext = DBContext;
        }

        public PartialViewResult Registration ()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Registration(RegistrationUser obj)
        {
            obj.UserInfarmation.Sex = obj.StringSex == "Мужской" ? 0 : 1;
            if (DBContext.AuthUsers.FirstOrDefault(x => x.Login == obj.UserAuthData.Login) != null)
                ModelState.AddModelError("", "Логин занят");

            if (ModelState.IsValid)
            {
                DBContext.AddUser(obj.UserInfarmation);
                DBContext.SaveContextChanges();

                obj.UserAuthData.UserId = obj.UserInfarmation.UserId;
                DBContext.AddAuthUser(obj.UserAuthData);
                DBContext.SaveContextChanges();

                return RedirectToRoute(new { controller = "Person", action = "Login" });
            }
            else
                return PartialView();
        }
    }
}
using System.Linq;
using System.Web.Mvc;
using SelfBoard.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Web;
using SelfBoard.Domain.Entities;

namespace SelfBoard.WebUI.Controllers
{
    public class PersonController : Controller
    {
        private ISelfBoardRepository DBContext;
        public PersonController(ISelfBoardRepository DBContext)
        {  
            this.DBContext = DBContext;
        }

        [HttpPost]
        public ActionResult Login(AuthUser obj)
        {
            var AuthResult = DBContext.AuthUsers.FirstOrDefault(x => x.Login == obj.Login);

            if (AuthResult != null && AuthResult.Password != obj.Password)
                ModelState.AddModelError("", "Неверный пароль");

            if (AuthResult != null && AuthResult.Password == obj.Password)
                return RedirectToRoute(new
                {
                    controller = "Person",
                    action = "Index",
                    UserId = AuthResult.UserId,
                    SelectedCategory = "Моя страница"
                });
            else             
                return View();

        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Index(int UserId, string SelectedCategory)
        {
            HttpCookie cookie = new HttpCookie("SelfBoardCookie");
            cookie["UserId"] = UserId.ToString();
            Response.Cookies.Add(cookie);

            DBContext.Users.FirstOrDefault(x => x.UserId == UserId).Online = 1;
            DBContext.SaveContextChanges();

            ViewBag.SelectedCategory = SelectedCategory;
            return View(DBContext.Users.FirstOrDefault(x => x.UserId == UserId));
        }

        public ActionResult Home(int UserId, string SelectedCategory)
        {
            ViewBag.SelectedCategory = SelectedCategory;
            return View("Index", DBContext.Users.FirstOrDefault(x => x.UserId == UserId));
        }

        public ActionResult SetNewAvatar(int PhotoId)
        {
            HttpCookie cookieReq = Request.Cookies["SelfBoardCookie"];
            int CookieUser = Convert.ToInt32(cookieReq["UserId"]);

            DBContext.Users.FirstOrDefault(x => x.UserId == CookieUser).AvatarId = PhotoId;
            DBContext.SaveContextChanges();

            return View("Index", DBContext.Users.FirstOrDefault(x => x.UserId == CookieUser));
        }

        public RedirectToRouteResult SignOut(int UserId)
        {
            DBContext.Users.FirstOrDefault(x => x.UserId == UserId).Online = 0;
            DBContext.SaveContextChanges();

            HttpCookie cookie = new HttpCookie("SelfBoardCookie");
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookie);

            return RedirectToRoute(new { controller = "Person", action = "Login" });
        }
    }
}
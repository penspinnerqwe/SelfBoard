using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SelfBoard.Domain.Concrete;
using SelfBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelfBoard.WebUI.Controllers
{
    public class PersonController : Controller
    {
        private UnitOfWork DBContext = new UnitOfWork();

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public ActionResult Home(string UserId, string SelectedCategory)
        {
            ViewBag.SelectedCategory = SelectedCategory;
            if (UserId == null)
            { 
                UserId = User.Identity.GetUserId();
                DBContext.ApplicationUsers.GetObjectByID(UserId).Online = 1;
                DBContext.Save();
            }
         
            return View(DBContext.ApplicationUsers.GetObjectByID(UserId));
        }

        public ActionResult SetNewAvatar(int PhotoId)
        {
            string CookieUser = User.Identity.GetUserId();
            DBContext.ApplicationUsers.GetObjectByID(CookieUser).AvatarId = PhotoId;
            DBContext.Save();
            return View("Home", DBContext.ApplicationUsers.GetObjectByID(CookieUser));
        }

        public RedirectToRouteResult SignOut(string UserId)
        {
            DBContext.ApplicationUsers.GetObjectByID(UserId).Online = 0;
            DBContext.Save();

            AuthenticationManager.SignOut();
            return RedirectToRoute(new { controller = "Auth", action = "Login" });
        }
    }
}
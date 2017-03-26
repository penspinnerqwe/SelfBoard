using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SelfBoard.Domain.Entities;
using SelfBoard.Domain.Concrete;
using Microsoft.AspNet.Identity;

namespace SelfBoard.WebUI.Controllers
{
    public class FrendController : Controller
    {
        private UnitOfWork DBContext = new UnitOfWork();

        public ActionResult Frends(string UserId, string SelectedCategory)
        {
            ViewBag.SelectedCategory = SelectedCategory;
            return View(DBContext.Frends.GetAllFrends(UserId));
        }

        public ActionResult InFrendsRequest(string UserId, string SelectedCategory)
        {
            ViewBag.SelectedCategory = SelectedCategory;
            return View("Frends", DBContext.Frends.GetInFrendsRequest(UserId));
        }

        public ActionResult OutFrendsRequest(string UserId, string SelectedCategory)
        {
            ViewBag.SelectedCategory = SelectedCategory;
            return View("Frends", DBContext.Frends.GetOutFrendsRequest(UserId));
        }

        public RedirectToRouteResult DeleteFrend(string UserId, string SelectedCategory)
        {
            string CookieUser = User.Identity.GetUserId();

            ViewBag.SelectedCategory = SelectedCategory;

            DBContext.Frends.GetObjects()
                .FirstOrDefault(x => (x.SenderId == CookieUser && x.ReceiverId == UserId) ||
                (x.SenderId == UserId && x.ReceiverId == CookieUser)).State = 0;
            DBContext.Save();

            return RedirectToRoute(new
            {
                controller = "Person",
                action = "Home",
                UserId = UserId,
                SelectedCategory = "Друзья"
            });
        }

        public RedirectToRouteResult DeleteRequest(string UserId, string SelectedCategory)
        {
            string CookieUser = User.Identity.GetUserId();

            ViewBag.SelectedCategory = SelectedCategory;
            var deleteFrendReqest = DBContext.Frends.GetObjects()
                .FirstOrDefault(x => (x.SenderId == CookieUser && x.ReceiverId == UserId) ||
                (x.SenderId == UserId && x.ReceiverId == CookieUser)).FrendId;

            DBContext.Frends.DeleteObject(deleteFrendReqest);
            DBContext.Save();

            return RedirectToRoute(new
            {
                controller = "Person",
                action = "Home",
                UserId = UserId,
                SelectedCategory = "Друзья"
            });
        }

        public RedirectToRouteResult SendRequest(string UserId, string SelectedCategory)
        {
            string CookieUser = User.Identity.GetUserId();

            ViewBag.SelectedCategory = SelectedCategory;

            Frend newFrenfd = new Frend() { State = 0, SenderId = CookieUser, ReceiverId = UserId };
            DBContext.Frends.InsertObject(newFrenfd);
            DBContext.Save();

            return RedirectToRoute(new
            {
                controller = "Person",
                action = "Home",
                UserId = UserId,
                SelectedCategory = "Друзья"
            });
        }

        public RedirectToRouteResult AcceptRequest(string UserId, string SelectedCategory)
        {
            string CookieUser = User.Identity.GetUserId();

            ViewBag.SelectedCategory = SelectedCategory;

            DBContext.Frends.GetObjects()
                .FirstOrDefault(x => (x.SenderId == CookieUser && x.ReceiverId == UserId) ||
                (x.SenderId == UserId && x.ReceiverId == CookieUser)).State = 1;
            DBContext.Save();

            return RedirectToRoute(new
            {
                controller = "Person",
                action = "Home",
                UserId = UserId,
                SelectedCategory = "Друзья"
            });
        }
    }
}
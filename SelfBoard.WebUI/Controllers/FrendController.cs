using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SelfBoard.Domain.Abstract;
using SelfBoard.Domain.Entities;

namespace SelfBoard.WebUI.Controllers
{
    public class FrendController : Controller
    {
        private ISelfBoardRepository DBContext;
        public FrendController(ISelfBoardRepository DBContext)
        {
            this.DBContext = DBContext;
        }

        public ActionResult Frends(int UserId,string SelectedCategory)
        {
            ViewBag.SelectedCategory = SelectedCategory;

            var MessageStrings = DBContext.Frends
                .Where(x => (x.SenderId == UserId || x.ReceiverId == UserId) && x.State == 1)
                .Select(x => x.SenderId == UserId ? x.Receiver : x.Sender);
            return View(MessageStrings);
        }

        public ActionResult InFrendsRequest(int UserId, string SelectedCategory)
        {
            ViewBag.SelectedCategory = SelectedCategory;

            var MessageStrings = DBContext.Frends
                .Where(x => x.ReceiverId == UserId && x.State == 0)
                .Select(x => x.Sender);
            return View("Frends", MessageStrings);
        }

        public ActionResult OutFrendsRequest(int UserId, string SelectedCategory)
        {
            ViewBag.SelectedCategory = SelectedCategory;

            var MessageStrings = DBContext.Frends
                .Where(x => x.SenderId == UserId && x.State == 0)
                .Select(x => x.Receiver);
            return View("Frends", MessageStrings);
        }

        public RedirectToRouteResult DeleteFrend(int UserId, string SelectedCategory)
        {
            HttpCookie cookieReq = Request.Cookies["SelfBoardCookie"];
            int CookieUser = Convert.ToInt32(cookieReq["UserId"]);

            ViewBag.SelectedCategory = SelectedCategory;

            DBContext.Frends.FirstOrDefault(x => (x.SenderId == CookieUser && x.ReceiverId == UserId) ||
                (x.SenderId == UserId && x.ReceiverId == CookieUser)).State = 0;
            DBContext.SaveContextChanges();

            return RedirectToRoute(new
            {
                controller = "Person",
                action = "Home",
                UserId = UserId,
                SelectedCategory = "Друзья"
            });
        }

        public RedirectToRouteResult DeleteRequest(int UserId, string SelectedCategory)
        {
            HttpCookie cookieReq = Request.Cookies["SelfBoardCookie"];
            int CookieUser = Convert.ToInt32(cookieReq["UserId"]);

            ViewBag.SelectedCategory = SelectedCategory;

            DBContext.DeleteFrend(DBContext.Frends.FirstOrDefault(x => (x.SenderId == CookieUser && x.ReceiverId == UserId) ||
                (x.SenderId == UserId && x.ReceiverId == CookieUser)));
            DBContext.SaveContextChanges();

            return RedirectToRoute(new
            {
                controller = "Person",
                action = "Home",
                UserId = UserId,
                SelectedCategory = "Друзья"
            });
        }

        public RedirectToRouteResult SendRequest(int UserId, string SelectedCategory)
        {
            HttpCookie cookieReq = Request.Cookies["SelfBoardCookie"];
            int CookieUser = Convert.ToInt32(cookieReq["UserId"]);

            ViewBag.SelectedCategory = SelectedCategory;

            Frend newFrenfd = new Frend() { State = 0, SenderId = CookieUser, ReceiverId = UserId };
            DBContext.AddFrend(newFrenfd);
            DBContext.SaveContextChanges();

            return RedirectToRoute(new
            {
                controller = "Person",
                action = "Home",
                UserId = UserId,
                SelectedCategory = "Друзья"
            });
        }

        public RedirectToRouteResult AcceptRequest(int UserId, string SelectedCategory)
        {
            HttpCookie cookieReq = Request.Cookies["SelfBoardCookie"];
            int CookieUser = Convert.ToInt32(cookieReq["UserId"]);

            ViewBag.SelectedCategory = SelectedCategory;

            DBContext.Frends.FirstOrDefault(x => (x.SenderId == CookieUser && x.ReceiverId == UserId) ||
                (x.SenderId == UserId && x.ReceiverId == CookieUser)).State = 1;
            DBContext.SaveContextChanges();

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
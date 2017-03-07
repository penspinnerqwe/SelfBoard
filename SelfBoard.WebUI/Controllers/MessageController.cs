using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SelfBoard.Domain.Abstract;
using SelfBoard.Domain.Entities;
using SelfBoard.WebUI.Models;

namespace SelfBoard.WebUI.Controllers
{
    public class MessageController : Controller
    {
        private ISelfBoardRepository DBContext;
        public MessageController(ISelfBoardRepository DBContext)
        {
            this.DBContext = DBContext;
        }

        public ActionResult Messages(int UserId ,string SelectedCategory )
        {
            ViewBag.SelectedCategory = SelectedCategory;

            MessageModel.CurrentUserId = UserId;
            var MessageStrings = DBContext.Messages.Where(x => x.SenderId == UserId || x.ReceiverId == UserId)
                .OrderByDescending(x => x.SendDate)
                .ToList().Distinct(new MessageComparer())
                .Select(z => new MessageModel() {  
                    MessageObj = z,
                    IsSenderImgExist = DBContext.Users.FirstOrDefault(x => x.UserId == z.SenderId).AvatarId == null ? false : true,
                    IsReceiverImgExist = DBContext.Users.FirstOrDefault(x => x.UserId == z.ReceiverId).AvatarId == null ? false : true
                });
            return View(MessageStrings);
        }

        public ActionResult ConcreteMessages(int UserId)
        {
            HttpCookie cookieReq = Request.Cookies["SelfBoardCookie"];
            int CookieUser = Convert.ToInt32(cookieReq["UserId"]);

            MessageModel.CurrentUserId = UserId;
            var MessageStrings = DBContext.Messages
                .Where(x => (x.SenderId == UserId && x.ReceiverId == CookieUser) ||
                (x.SenderId == CookieUser && x.ReceiverId == UserId))
                .OrderByDescending(y => y.SendDate)
                .Select(z => new MessageModel() {
                    MessageObj = z,
                    IsSenderImgExist = DBContext.Users.FirstOrDefault(x => x.UserId == z.SenderId).AvatarId == null ? false : true,
                    IsReceiverImgExist = DBContext.Users.FirstOrDefault(x => x.UserId == z.ReceiverId).AvatarId == null ? false : true
                });

            return View(MessageStrings);
        }

        [HttpPost]
        public RedirectToRouteResult SendMessage(int CurrentUserId, string MessageString)
        {
            try
            {
                HttpCookie cookieReq = Request.Cookies["SelfBoardCookie"];

                Message NewMessageObj = new Message()
                {
                    MessageString = MessageString,
                    State = 0,
                    SendDate = DateTime.Now,
                    SenderId = Convert.ToInt32(cookieReq["UserId"]),
                    ReceiverId = CurrentUserId
                };

                DBContext.AddMessage(NewMessageObj);
                DBContext.SaveContextChanges();
            }
            catch
            {
                return RedirectToRoute(new
                {
                    controller = "Message",
                    action = "ConcreteMessages",
                    UserId = CurrentUserId,
                    SelectedCategory = "Cообщения"
                });
            }
            return RedirectToRoute(new
            {
                controller = "Message",
                action = "ConcreteMessages",
                UserId = CurrentUserId,
                SelectedCategory = "Cообщения"
            });
        }
    }
}
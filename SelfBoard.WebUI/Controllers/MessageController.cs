﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SelfBoard.Domain.Entities;
using SelfBoard.WebUI.Models;
using SelfBoard.Domain.Concrete;
using SelfBoard.WebUI.Infrastructure;
using Microsoft.AspNet.Identity;

namespace SelfBoard.WebUI.Controllers
{
    public class MessageController : Controller
    {
        private UnitOfWork DBContext = new UnitOfWork();

        public ActionResult Messages(string UserId, string SelectedCategory)
        {
            ViewBag.SelectedCategory = SelectedCategory;

            MessageModel.CurrentUserId = UserId;
            var MessageStrings = DBContext.Messages.GetObjects()
                .Where(x => x.SenderId == UserId || x.ReceiverId == UserId)
                .OrderByDescending(x => x.SendDate)
                .ToList().Distinct(new MessageComparer())
                .Select(z => new MessageModel() { MessageObj = z });

            return View(MessageStrings);
        }

        public ActionResult ConcreteMessages(string UserId)
        {
            string CookieUser = User.Identity.GetUserId();

            MessageModel.CurrentUserId = UserId;
            var MessageStrings = DBContext.Messages.GetObjects()
                .Where(x => (x.SenderId == UserId && x.ReceiverId == CookieUser) ||
                (x.SenderId == CookieUser && x.ReceiverId == UserId))
                .OrderByDescending(y => y.SendDate)
                .Select(z => new MessageModel() { MessageObj = z });

            return View(MessageStrings);
        }

        [HttpPost]
        public RedirectToRouteResult SendMessage(string CurrentUserId, string MessageString)
        {
            try
            {
                Message NewMessageObj = new Message()
                {
                    MessageString = MessageString,
                    State = 0,
                    SendDate = DateTime.Now,
                    SenderId = User.Identity.GetUserId(),
                    ReceiverId = CurrentUserId
                };

                DBContext.Messages.InsertObject(NewMessageObj);
                DBContext.Save();
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
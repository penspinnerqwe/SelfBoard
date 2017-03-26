using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SelfBoard.Domain.Entities;
using SelfBoard.WebUI.Models;
using SelfBoard.Domain.Concrete;
using Microsoft.AspNet.Identity;

namespace SelfBoard.WebUI.Controllers
{
    public class LikeController : Controller
    {
        private UnitOfWork DBContext = new UnitOfWork();

        public PartialViewResult LikeControll(int PhotoId)
        {
            return PartialView(new LikeControllModel
            {
                PhotoId = PhotoId,
                Count = DBContext.Likes.GetObjects()
                .Where(x => x.PhotoId == PhotoId).Count()
            });
        }

        public PartialViewResult AddLike(int PhotoId)
        {
            string UserId = User.Identity.GetUserId();

            var UsersLike = DBContext.Likes.GetObjects()
                .FirstOrDefault(x => x.PhotoId == PhotoId && x.UserId == UserId);

            if (UsersLike == null)
            {
                DBContext.Likes.InsertObject(new Like { PhotoId = PhotoId, UserId = UserId });
                DBContext.Save();
            }
            else
            {
                DBContext.Likes.DeleteObject(UsersLike.LikeId);
                DBContext.Save();
            }

            return PartialView("LikeControll", new LikeControllModel
            {
                PhotoId = PhotoId,
                Count = DBContext.Likes.GetObjects()
                .Where(x => x.PhotoId == PhotoId).Count()
            });
        }
    }
}
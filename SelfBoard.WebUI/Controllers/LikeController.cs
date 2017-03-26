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
    public class LikeController : Controller
    {
        private ISelfBoardRepository DBContext;
        public LikeController(ISelfBoardRepository DBContext)
        {
            this.DBContext = DBContext;
        }

        public PartialViewResult LikeControll(int PhotoId)
        {
            return PartialView(new LikeControllModel { PhotoId = PhotoId, Count = DBContext.Likes
                .Where(x => x.PhotoId == PhotoId).Count()
            });
        }

        public PartialViewResult AddLike(int PhotoId)
        {
            HttpCookie cookieReq = Request.Cookies["SelfBoardCookie"];
            int UserId = Convert.ToInt32(cookieReq["UserId"]);

            var UsersLike = DBContext.Likes
                .FirstOrDefault(x => x.PhotoId == PhotoId && x.UserId == UserId);

            if (UsersLike == null)
            {
                DBContext.AddLike(new Like { PhotoId = PhotoId, UserId = UserId });
                DBContext.SaveContextChanges();
            }
            else
            {
                DBContext.DeleteLike(UsersLike);
                DBContext.SaveContextChanges();
            }

            return PartialView("LikeControll", new LikeControllModel { PhotoId = PhotoId, Count = DBContext.Likes
                .Where(x => x.PhotoId == PhotoId).Count()
            });
        }
    }
}
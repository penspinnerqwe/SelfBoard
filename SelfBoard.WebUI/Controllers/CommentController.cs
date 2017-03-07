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
    public class CommentController : Controller
    {
        private ISelfBoardRepository DBContext;
        public CommentController(ISelfBoardRepository DBContext)
        {
            this.DBContext = DBContext;
        }

        public PartialViewResult GetComments(int PhotoId)
        {
            var result = DBContext.Comments
                .Where(x => x.PhotoId == PhotoId)
                .Select(x => new ComentModel() {
                    CommentObj = x,
                    IsImgExist = DBContext.Users.FirstOrDefault(y => y.UserId == x.UserId).AvatarId == null ? false : true
                });

            return PartialView(result);
        }

        public PartialViewResult GetAddedComments(int UserId, int PhotoId, string CommentString)
        {
            HttpCookie cookieReq = Request.Cookies["SelfBoardCookie"];

            Comment NewComent = new Comment()
            {
                UserId = Convert.ToInt32(cookieReq["UserId"]),
                PhotoId = PhotoId,
                CommentString = CommentString
            };
            DBContext.AddComment(NewComent);
            DBContext.SaveContextChanges();

            var result = DBContext.Comments
                .Where(x => x.PhotoId == PhotoId)
                .Select(x => new ComentModel()
                {
                    CommentObj = x,
                    IsImgExist = DBContext.Users.FirstOrDefault(y => y.UserId == x.UserId).AvatarId == null ? false : true
                });

            return PartialView("GetComments", result);
        }  
    }
}
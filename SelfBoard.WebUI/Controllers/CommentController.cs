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
    public class CommentController : Controller
    {
        private UnitOfWork DBContext = new UnitOfWork();

        public PartialViewResult GetComments(int PhotoId)
        {
            var result = DBContext.Comments.GetObjects()
                .Where(x => x.PhotoId == PhotoId)
                .Select(x => x);

            return PartialView(result);
        }

        public PartialViewResult GetAddedComments(string UserId, int PhotoId, string CommentString)
        {
            Comment NewComent = new Comment()
            {
                UserId = User.Identity.GetUserId(),
                PhotoId = PhotoId,
                CommentString = CommentString
            };
            DBContext.Comments.InsertObject(NewComent);
            DBContext.Save();

            var result = DBContext.Comments.GetObjects()
                .Where(x => x.PhotoId == PhotoId)
                .Select(x => x);

            return PartialView("GetComments", result);
        }
    }
}
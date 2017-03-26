using System.Web;
using System.Web.Mvc;
using SelfBoard.WebUI.Models;
using SelfBoard.Domain.Concrete;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;

namespace SelfBoard.WebUI.Controllers
{
    public class NewsController : Controller
    {
        private UnitOfWork DBContext = new UnitOfWork();

        public PartialViewResult GetNews(string UserId)
        {
            List<NewsModel> result = new List<NewsModel>();

            if (UserId != null)
                result.AddRange(DBContext.Photos.GetObjects()
                    .Where(x => x.UserId == UserId)
                    .Select(x => new NewsModel()
                    {
                        NewsObj = x,
                        Autor = x.User,
                        Comments = DBContext.Comments.GetObjects().Where(y => y.PhotoId == x.PhotoId).Select(y => y),
                        Likes = DBContext.Likes.GetObjects().Where(z => z.PhotoId == x.PhotoId).Select(z => z)
                    }));
            else
                result.AddRange(DBContext.Photos.GetObjects().Select(x => new NewsModel()
                {
                    NewsObj = x,
                    Autor = x.User,
                    Comments = DBContext.Comments.GetObjects().Where(y => y.PhotoId == x.PhotoId).Select(y => y),
                    Likes = DBContext.Likes.GetObjects().Where(z => z.PhotoId == x.PhotoId).Select(z => z)
                }));

            return PartialView(result);
        }

        public PartialViewResult GetDeleteNewsButton(int PhotoId)
        {
            ViewBag.CurrentUserId = User.Identity.GetUserId();
            return PartialView(DBContext.Photos.GetObjectByID(PhotoId));
        }

        public PartialViewResult DeleteNews(int PhotoId)
        {
            string CurrentUserId = User.Identity.GetUserId();

            var tempUser = DBContext.ApplicationUsers.GetObjects()
                .FirstOrDefault(x => x.Id == CurrentUserId && x.AvatarId == PhotoId);
            if (tempUser != null)
                tempUser.AvatarId = null;

            var deleteLikesList = DBContext.Likes.GetObjects()
                .Where(x => x.PhotoId == PhotoId).Select(x => x);
            foreach (var deleteItem in deleteLikesList)
                DBContext.Likes.DeleteObject(deleteItem.LikeId);

            var deleteCommentList = DBContext.Comments.GetObjects()
                .Where(x => x.PhotoId == PhotoId).Select(x => x);
            foreach (var deleteItem in deleteCommentList)
                DBContext.Comments.DeleteObject(deleteItem.CommentId);

            DBContext.Photos.DeleteObject(DBContext.Photos.GetObjectByID(PhotoId).PhotoId);
            DBContext.Save();

            return PartialView();
        }

        public ActionResult GetAllNews()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SelfBoard.Domain.Abstract;
using SelfBoard.WebUI.Models;

namespace SelfBoard.WebUI.Controllers
{
    public class NewsController : Controller
    {
        private ISelfBoardRepository DBContext;
        public NewsController(ISelfBoardRepository DBContext)
        {
            this.DBContext = DBContext;
        }

        public PartialViewResult GetNews(int UserId)
        {
            List<NewsModel> result = new List<NewsModel>();

            if(UserId != 0)
                result.AddRange(DBContext.Photos
                    .Where(x => x.UserId == UserId)
                    .Select(x => new NewsModel() {
                    NewsObj = x,
                    Autor = x.User,
                    Comments = DBContext.Comments.Where(y => y.PhotoId == x.PhotoId).Select(y => y),
                    Likes = DBContext.Likes.Where(z => z.PhotoId == x.PhotoId).Select(z => z)
                }));
            else
                result.AddRange(DBContext.Photos.Select(x => new NewsModel(){
                    NewsObj = x,
                    Autor = x.User,
                    Comments = DBContext.Comments.Where(y => y.PhotoId == x.PhotoId).Select(y => y),
                    Likes = DBContext.Likes.Where(z => z.PhotoId == x.PhotoId).Select(z => z)
                }));

            return PartialView(result);
        }

        public PartialViewResult GetDeleteNewsButton(int PhotoId)
        {
            HttpCookie cookieReq = Request.Cookies["SelfBoardCookie"];
            ViewBag.CurrentUserId = Convert.ToInt32(cookieReq["UserId"]);
            return PartialView(DBContext.Photos.FirstOrDefault(x => x.PhotoId == PhotoId));
        }

        public PartialViewResult DeleteNews(int PhotoId)
        {
            HttpCookie cookieReq = Request.Cookies["SelfBoardCookie"];
            int CurrentUserId = Convert.ToInt32(cookieReq["UserId"]);

            if (DBContext.Users.FirstOrDefault(x => x.UserId == CurrentUserId && x.AvatarId == PhotoId) != null)
                DBContext.Users.FirstOrDefault(x => x.UserId == CurrentUserId && x.AvatarId == PhotoId).AvatarId = null;
            DBContext.SaveContextChanges();

            var deleteLikesList = DBContext.Likes.Where(x => x.PhotoId == PhotoId).Select(x => x);
            foreach (var deleteItem in deleteLikesList)
                DBContext.DeleteLike(deleteItem);

            var deleteCommentList = DBContext.Comments.Where(x => x.PhotoId == PhotoId).Select(x => x);
            foreach (var deleteItem in deleteCommentList)
                DBContext.DeleteComment(deleteItem);
            DBContext.SaveContextChanges();

            DBContext.DeletePhoto(DBContext.Photos.FirstOrDefault(x => x.PhotoId == PhotoId));
            DBContext.SaveContextChanges();

            return PartialView();
        }

        public ActionResult GetAllNews()
        {
            return View();
        }
    }
}
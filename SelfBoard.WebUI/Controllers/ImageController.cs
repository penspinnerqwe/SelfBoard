using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SelfBoard.Domain.Abstract;
using SelfBoard.Domain.Entities;
using System.Drawing;

namespace SelfBoard.WebUI.Controllers
{
    public class ImageController : Controller
    {
        private String DefaultImagePath = "~/Content/nophoto.gif";
        private ISelfBoardRepository DBContext;
        public ImageController(ISelfBoardRepository DBContext)
        {
            this.DBContext = DBContext;
        }

        public ActionResult RedactImage(int UserId)
        {
            return View(DBContext.Users.FirstOrDefault(x => x.UserId == UserId));
        }

        public FileResult GetImage(int UserId)
        {
            var Avatar = DBContext.Users
                .Where(x => x.UserId == UserId)
                .Select(x => x.Avatar)
                .FirstOrDefault();

            if (Avatar != null)
                return File(Avatar.ImageData, Avatar.ImageMimeType);
            else
                return new FilePathResult(DefaultImagePath, "image/jpeg");
        }

        public FileResult GetNewsImage(int PhotoId)
        {
            var Avatar = DBContext.Photos
                .Where(x => x.PhotoId == PhotoId)
                .Select(x => x)
                .FirstOrDefault();

            if (Avatar != null)
                return File(Avatar.ImageData, Avatar.ImageMimeType);
            else
                return new FilePathResult(DefaultImagePath, "image/jpeg");
        }

        public FileResult GetIconImage(int UserId)
        {
            var Avatar = DBContext.Users
                .Where(x => x.UserId == UserId)
                .Select(x => x.Avatar)
                .FirstOrDefault();
                  
            if (Avatar != null)
                return File(Avatar.RedactImage, Avatar.RedactImageMimeType);
            else
                return new FilePathResult(DefaultImagePath, "image/jpeg");
        }

        [HttpPost]
        public ActionResult PostImsge(int UserId, HttpPostedFileBase image)
        {
            var User = DBContext.Users.FirstOrDefault(x => x.UserId == UserId);
            Photo newPhoto = new Photo();

            if (image != null)
            {
                newPhoto.ImageMimeType = image.ContentType;
                newPhoto.ImageData = new byte[image.ContentLength];
                image.InputStream.Read(newPhoto.ImageData, 0, image.ContentLength);

                newPhoto.RedactImageMimeType = newPhoto.ImageMimeType;
                newPhoto.RedactImage = newPhoto.ImageData;

                DBContext.AddPhoto(newPhoto);
                DBContext.SaveContextChanges();

                User.AvatarId = newPhoto.PhotoId;
                newPhoto.UserId = UserId;
                DBContext.SaveContextChanges();
            }

            return RedirectToRoute(new { controller = "Person", action = "Home", UserId = UserId });
        }

        public String GetSex(int UserId)
        {
            var User = DBContext.Users.FirstOrDefault(x => x.UserId == UserId);
            if (User != null)
                return User.Sex == 0 ? "Мужской" : "Женский";
            else
                return null;
        }

        public String GetOnline(int UserId)
        {
            var User = DBContext.Users.FirstOrDefault(x => x.UserId == UserId);
            if (User != null)
                return User.Online == 0 ? "Не в сети" : "В сети";
            else
                return null;
        }

        public Int32 GetAge(int UserId)
        {
            var User = DBContext.Users.FirstOrDefault(x => x.UserId == UserId);
            if (User != null)
            {
                DateTime dateNow = DateTime.Now;
                int year = dateNow.Year - User.BirthDay.Year;
                if (dateNow.Month < User.BirthDay.Month ||
                   (dateNow.Month == User.BirthDay.Month && dateNow.Day < User.BirthDay.Day))
                    year--;
                return year;
            }
            else
                return -1;
        }

        [HttpPost]
        public RedirectToRouteResult CutImage(FormCollection form)
        {
            HttpCookie cookieReq = Request.Cookies["SelfBoardCookie"];
            int CookieUser = Convert.ToInt32(cookieReq["UserId"]);

            try
            {
                var x = Convert.ToInt32(form["X"]);
                var y = Convert.ToInt32(form["Y"]);
                var w = Convert.ToInt32(form["W"]);
                var h = Convert.ToInt32(form["H"]);
                var redactPhoto = DBContext.Photos.FirstOrDefault(param => param.PhotoId ==
                DBContext.Users.FirstOrDefault(b => b.UserId == CookieUser).AvatarId);

                Image oImage = (Bitmap)((new ImageConverter()).ConvertFrom(redactPhoto.ImageData));

                var bmp = new Bitmap(w, h, oImage.PixelFormat);
                var g = Graphics.FromImage(bmp);
                g.DrawImage(oImage, new Rectangle(0, 0, w, h), new Rectangle(x, y, w, h), GraphicsUnit.Pixel);

                DBContext.Photos.FirstOrDefault(param => param.PhotoId ==
                DBContext.Users.FirstOrDefault(b => b.UserId == CookieUser).AvatarId)
                    .RedactImage = (byte[])((new ImageConverter()).ConvertTo(bmp, typeof(byte[])));

                DBContext.SaveContextChanges();
            }
            catch
            {
                return RedirectToRoute(new
                {
                    controller = "Person",
                    action = "Home",
                    UserId = CookieUser,
                    SelectedCategory = "Моя страница"
                });
            }

            return RedirectToRoute(new
            {
                controller = "Person",
                action = "Home",
                UserId = CookieUser,
                SelectedCategory = "Моя страница"
            });
        }

        public ActionResult CutImageRedactor(int UserId)
        {
            return View(DBContext.Photos.FirstOrDefault(x => x.PhotoId == 
            DBContext.Users.FirstOrDefault(y => y.UserId == UserId).AvatarId));
        }

        public ActionResult SelectLoadedImage(int UserId)
        {
            return View(DBContext.Photos.Where(x => x.UserId == UserId).Select(x => x));
        }
    }
}
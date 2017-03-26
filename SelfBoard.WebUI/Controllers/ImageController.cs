using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SelfBoard.Domain.Entities;
using System.Drawing;
using SelfBoard.Domain.Concrete;
using Microsoft.AspNet.Identity;

namespace SelfBoard.WebUI.Controllers
{
    public class ImageController : Controller
    {
        private String DefaultImagePath = "~/Content/nophoto.gif";
        private UnitOfWork DBContext = new UnitOfWork();

        public ActionResult RedactImage(string UserId)
        {
            return View(DBContext.ApplicationUsers.GetObjectByID(UserId));
        }

        public FileResult GetImage(string UserId)
        {
            var Avatar = DBContext.Photos.GetUsersAvatar(UserId);

            if (Avatar != null)
                return File(Avatar.ImageData, Avatar.ImageMimeType);
            else
                return new FilePathResult(DefaultImagePath, "image/jpeg");
        }

        public FileResult GetNewsImage(int PhotoId)
        {
            var Avatar = DBContext.Photos.GetObjectByID(PhotoId);

            if (Avatar != null)
                return File(Avatar.ImageData, Avatar.ImageMimeType);
            else
                return new FilePathResult(DefaultImagePath, "image/jpeg");
        }

        public FileResult GetIconImage(string UserId)
        {
            var Avatar = DBContext.Photos.GetUsersAvatar(UserId);

            if (Avatar != null)
                return File(Avatar.RedactImage, Avatar.RedactImageMimeType);
            else
                return new FilePathResult(DefaultImagePath, "image/jpeg");
        }

        [HttpPost]
        public ActionResult PostImsge(string UserId, HttpPostedFileBase image)
        {
            var User = DBContext.ApplicationUsers.GetObjectByID(UserId);
            Photo newPhoto = new Photo();

            if (image != null)
            {
                newPhoto.ImageMimeType = image.ContentType;
                newPhoto.ImageData = new byte[image.ContentLength];
                image.InputStream.Read(newPhoto.ImageData, 0, image.ContentLength);

                newPhoto.RedactImageMimeType = newPhoto.ImageMimeType;
                newPhoto.RedactImage = newPhoto.ImageData;

                DBContext.Photos.InsertObject(newPhoto);
                User.AvatarId = newPhoto.PhotoId;
                newPhoto.UserId = UserId;
                DBContext.Save();
            }

            return RedirectToRoute(new { controller = "Person", action = "Home", UserId = UserId });
        }

        public String GetSex(string UserId)
        {
            var User = DBContext.ApplicationUsers.GetObjectByID(UserId);
            if (User != null)
                return User.Sex == 0 ? "Мужской" : "Женский";
            else
                return null;
        }

        public String GetOnline(string UserId)
        {
            var User = DBContext.ApplicationUsers.GetObjectByID(UserId);
            if (User != null)
                return User.Online == 0 ? "Не в сети" : "В сети";
            else
                return null;
        }

        public Int32 GetAge(string UserId)
        {
            var User = DBContext.ApplicationUsers.GetObjectByID(UserId);
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
            string CookieUser = User.Identity.GetUserId();
            try
            {
                var x = Convert.ToInt32(form["X"]);
                var y = Convert.ToInt32(form["Y"]);
                var w = Convert.ToInt32(form["W"]);
                var h = Convert.ToInt32(form["H"]);
                var avatarId = DBContext.ApplicationUsers.GetObjectByID(CookieUser).AvatarId;

                var redactPhoto = DBContext.Photos.GetObjectByID((int)avatarId);

                Image oImage = (Bitmap)((new ImageConverter()).ConvertFrom(redactPhoto.ImageData));

                var bmp = new Bitmap(w, h, oImage.PixelFormat);
                var g = Graphics.FromImage(bmp);
                g.DrawImage(oImage, new Rectangle(0, 0, w, h), new Rectangle(x, y, w, h), GraphicsUnit.Pixel);

                DBContext.Photos.GetObjectByID((int)avatarId)
                    .RedactImage = (byte[])((new ImageConverter()).ConvertTo(bmp, typeof(byte[])));
                DBContext.Save();
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

        public ActionResult CutImageRedactor(string UserId)
        {
            var avatarId = DBContext.ApplicationUsers.GetObjectByID(UserId).AvatarId;
            return View(DBContext.Photos.GetObjectByID((int)avatarId));
        }

        public ActionResult SelectLoadedImage(string UserId)
        {
            return View(DBContext.Photos.GetObjects().Where(x => x.UserId == UserId).Select(x => x));
        }
    }
}
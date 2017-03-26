using Microsoft.AspNet.Identity;
using SelfBoard.Domain.Concrete;
using SelfBoard.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelfBoard.WebUI.Controllers
{
    public class NavigationController : Controller
    {
        private UnitOfWork DBContext = new UnitOfWork();

        public PartialViewResult Menu(string selectedItem = null)
        {
            string CookieUser = User.Identity.GetUserId();

            ViewBag.SelectedCategory = selectedItem;
            List<NavigationModel> ViewArgs = new List<NavigationModel>
            {
                new NavigationModel {
                    String = "Моя страница" ,
                    Action = "Home",
                    Controller = "Person",
                    CurrentId = CookieUser
                },
                new NavigationModel {
                    String = "Друзья",
                    Action = "Frends",
                    Controller = "Frend",
                    CurrentId = CookieUser
                },
                new NavigationModel {
                    String = "Сообщения" ,
                    Action = "Messages",
                    Controller = "Message",
                    CurrentId = CookieUser
                },
                new NavigationModel {
                    String = "Новости"  ,
                    Action = "GetAllNews",
                    Controller = "News",
                },
                new NavigationModel {
                    String = "Поиск" ,
                    Action = "Search",
                    Controller = "Search"
                }
            };
            return PartialView(ViewArgs);
        }

        public PartialViewResult TopMenu(string selectedItem = null)
        {
            string CookieUser = User.Identity.GetUserId();

            ViewBag.SelectedCategory = selectedItem;
            List<NavigationModel> ViewArgs = new List<NavigationModel>
            {
                new NavigationModel {
                    String = "Выход" ,
                    Action = "SignOut",
                    Controller = "Person",
                    CurrentId = CookieUser
                },
                new NavigationModel {
                    String = "О сайте" ,
                    Action = "Home",
                    Controller = "Person",
                    CurrentId = CookieUser
                }
            };
            return PartialView(ViewArgs);
        }

        public PartialViewResult SelfMenu(string UserId)
        {
            List<NavigationModel> ViewArgs = new List<NavigationModel>();

            string CookieUser = User.Identity.GetUserId();

            if (CookieUser == UserId)
                ViewArgs.Add(new NavigationModel()
                {
                    String = "Редактор",
                    Action = "RedactImage",
                    Controller = "Image",
                    CurrentId = CookieUser
                });
            else
            {
                var tempFrednItem = DBContext.Frends.GetObjects()
                    .FirstOrDefault(x => (x.ReceiverId == UserId && x.SenderId == CookieUser) ||
                    (x.SenderId == UserId && x.ReceiverId == CookieUser));

                if (tempFrednItem != null)
                    switch (tempFrednItem.State)
                    {
                        case 0:
                            if (tempFrednItem.SenderId == CookieUser)
                                ViewArgs.Add(new NavigationModel()
                                {
                                    String = "Отменить заявку в друзья",
                                    Action = "DeleteRequest",
                                    Controller = "Frend",
                                    CurrentId = UserId
                                });
                            else
                                ViewArgs.Add(new NavigationModel()
                                {
                                    String = "Принять заявку в друзья",
                                    Action = "AcceptRequest",
                                    Controller = "Frend",
                                    CurrentId = UserId
                                });
                            break;
                        case 1:
                            ViewArgs.Add(new NavigationModel()
                            {
                                String = "Убрать из друзей",
                                Action = "DeleteFrend",
                                Controller = "Frend",
                                CurrentId = UserId
                            }); break;
                        case 2:
                            ViewArgs.Add(new NavigationModel()
                            {
                                String = "Вы в чёрном списке",
                                Action = "Frends",
                                Controller = "Frend",
                                CurrentId = UserId
                            }); break;
                    }
                else
                    ViewArgs.Add(new NavigationModel()
                    {
                        String = "Добавить в друзья",
                        Action = "SendRequest",
                        Controller = "Frend",
                        CurrentId = UserId
                    });

                ViewArgs.Add(new NavigationModel()
                {
                    String = "Написать сообщение",
                    Action = "ConcreteMessages",
                    Controller = "Message",
                    CurrentId = UserId
                });
            }

            ViewArgs.Add(new NavigationModel()
            {
                String = "Просмотреть друзей",
                Action = "Frends",
                Controller = "Frend",
                CurrentId = UserId
            });

            return PartialView("TopMenu", ViewArgs);
        }

        public PartialViewResult FrendMenu(string UserId, string selectedItem = null)
        {
            ViewBag.SelectedCategory = selectedItem;
            List<NavigationModel> ViewArgs = new List<NavigationModel>
            {
                new NavigationModel {
                    String = "Посмотреть страницу" ,
                    Controller = "Person",
                    Action = "Home",
                    CurrentId = UserId
                },
                new NavigationModel {
                    String = "Написать сообщение",
                    Action = "ConcreteMessages",
                    Controller = "Message",
                    CurrentId = UserId
                }
            };
            return PartialView("TopMenu", ViewArgs);
        }

        public PartialViewResult FrendsRequestMenu()
        {
            string CookieUser = User.Identity.GetUserId();

            List<NavigationModel> ViewArgs = new List<NavigationModel>
            {
                new NavigationModel {
                    String = "Принятые заявки" ,
                    Controller = "Frend",
                    Action = "Frends",
                    CurrentId = CookieUser
                },
                new NavigationModel {
                    String = "Исходящие заявки",
                    Action = "OutFrendsRequest",
                    Controller = "Frend",
                    CurrentId = CookieUser
                },
                new NavigationModel {
                    String = "Входящие заявки",
                    Action = "InFrendsRequest",
                    Controller = "Frend",
                    CurrentId = CookieUser
                }
            };
            return PartialView("TopMenu", ViewArgs);
        }

        public PartialViewResult RedactImageMenu(string UserId)
        {
            string CookieUser = User.Identity.GetUserId();

            List<NavigationModel> ViewArgs = new List<NavigationModel>
            {
                new NavigationModel {
                    String = "Выбрать загруженное фото" ,
                    Controller = "Image",
                    Action = "SelectLoadedImage",
                    CurrentId = CookieUser
                },
                new NavigationModel {
                    String = "Выбрать миниатюру",
                    Action = "CutImageRedactor",
                    Controller = "Image",
                    CurrentId = CookieUser
                }
            };
            return PartialView("TopMenu", ViewArgs);
        }
    }
}
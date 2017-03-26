using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using SelfBoard.Domain.Entities;
using SelfBoard.Domain.Concrete;

namespace SelfBoard.WebUI.Controllers
{
    public class SearchController : Controller
    {
        private UnitOfWork DBContext = new UnitOfWork();

        public PartialViewResult SearchPeiole(string arg)
        {
            var SearchParam = new Regex(@"\w+").Match(arg);
            List<string> LastFirstName = new List<string>();
            while (SearchParam.Success)
            {
                LastFirstName.Add(SearchParam.Value);
                SearchParam = SearchParam.NextMatch();
            }

            List<ApplicationUser> result = new List<ApplicationUser>();
            if (LastFirstName.Count >= 2)
            {
                string firstParam = LastFirstName[0].ToLower();
                string lastParam = LastFirstName[1].ToLower();

                result.AddRange(DBContext.ApplicationUsers.GetObjects()
                    .Where(x => (x.FirstName.ToLower() == firstParam && x.LastName.ToLower() == lastParam) ||
                    (x.FirstName.ToLower() == lastParam && x.LastName.ToLower() == firstParam))
                    .Select(x => x));
            }
            else
            {
                string firstParam = LastFirstName[0].ToLower();

                result.AddRange(DBContext.ApplicationUsers.GetObjects()
                    .Where(x => x.FirstName.ToLower() == firstParam || x.LastName.ToLower() == firstParam)
                    .Select(x => x));
            }

            return PartialView(result);
        }

        public ActionResult Search()
        {
            return View();
        }
    }
}
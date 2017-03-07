using System;
using System.Web.Mvc;

namespace SelfBoard.WebUI.Infrastructure
{
    public static class CustomHelpers
    {
        public static MvcHtmlString GetDivNewId(this HtmlHelper html, string part1, string part2)
        {
            string result = String.Format("id='{0}'", part1 + part2);
            return new MvcHtmlString(result);
        }

        public static MvcHtmlString GetLikesCount(this HtmlHelper html, int count)
        {
            string result = String.Format("value='Мне нравится: {0}'", count.ToString());
            return new MvcHtmlString(result);
        }
    }
}
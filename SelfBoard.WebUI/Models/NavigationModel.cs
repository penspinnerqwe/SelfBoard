using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfBoard.WebUI.Models
{
    public class NavigationModel
    {
        public string String { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public int CurrentId { get; set; }
    }
}
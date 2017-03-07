using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SelfBoard.Domain.Entities;

namespace SelfBoard.WebUI.Models
{
    public class NewsModel
    {
        public Photo NewsObj { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<Like> Likes { get; set; }
        public User Autor { get; set; }
        public static int CurrentUserId { get; set; }

        public NewsModel()
        {
            Comments = new List<Comment>();
            Likes = new List<Like>();
        }
    }
}
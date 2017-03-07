using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfBoard.Domain.Entities
{
    public class Photo
    {
        public int PhotoId { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

        public byte[] RedactImage { get; set; }
        public string RedactImageMimeType { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }
        public User User { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
        public Photo()
        {
            Comments = new List<Comment>();
            Likes = new List<Like>();
        }
    }
}

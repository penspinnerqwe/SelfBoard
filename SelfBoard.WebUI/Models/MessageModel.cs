﻿using SelfBoard.Domain.Entities;

namespace SelfBoard.WebUI.Models
{
    public class MessageModel
    {
        public Message MessageObj { get; set; }
        public static int CurrentUserId { get; set; }
        public bool IsSenderImgExist { get; set; }
        public bool IsReceiverImgExist { get; set; }
    }
}
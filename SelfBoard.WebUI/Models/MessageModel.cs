﻿using SelfBoard.Domain.Entities;

namespace SelfBoard.WebUI.Models
{
    public class MessageModel
    {
        public Message MessageObj { get; set; }
        public static string CurrentUserId { get; set; }
    }
}
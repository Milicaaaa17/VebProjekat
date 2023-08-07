﻿namespace ProjekatVeb2.Models
{
    public class Message
    {
        public string Subject { get; set; }
        public string Content { get; set; }
        public List<string> To { get; set; }
        public List<string> Attachments { get; set; }

        public Message()
        {
            To = new List<string>();
            Attachments = new List<string>();
        }
    }
}

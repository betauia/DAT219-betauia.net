using System;

namespace betauia.Models
{
    public class BlogPost
    {       
        public BlogPost(string title, string content, DateTime postTime)
        {
            Title = title;
            Content = content;
            if (postTime == DateTime.MinValue)
            {
                postTime = DateTime.UtcNow;
            }
            PostTime = postTime;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }        
        public DateTime PostTime { get; set; }
    }
}
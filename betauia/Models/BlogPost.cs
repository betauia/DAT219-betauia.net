using System;

namespace betauia.Models
{
    public class BlogPost
    {       
        public BlogPost(string title, string content)
        {
            Title = title;
            Content = content;
            
            CreationDate = DateTime.UtcNow;
            LastEditDate = CreationDate;
        }

        public void UpdateEditTime()
        {
            LastEditDate = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }        
        public DateTime CreationDate { get; set; }
        public DateTime LastEditDate { get; set; }
    }
}
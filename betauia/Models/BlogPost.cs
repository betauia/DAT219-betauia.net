using System;

namespace betauia.Models
{
    public class BlogPost
    {       
        public BlogPost(string title,  string summary, string content, string image)
        {
            Title = title;
            Summary = summary;
            Content = content;
            Image = image;

            CreationDate = DateTime.UtcNow;
            LastEditDate = CreationDate;
        }

        public void UpdateEditTime()
        {
            LastEditDate = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        
        public string Image { get; set; }
        
        public string Summary { get; set; }
        public string Content { get; set; }        
        public DateTime CreationDate { get; set; }
        public DateTime LastEditDate { get; set; }
    }
}
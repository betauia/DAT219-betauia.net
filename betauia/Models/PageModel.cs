using System.Collections.Generic;
using System.Net.Mime;
using System;

namespace betauia.Models
{
    public class PageModel
    {
        public PageModel(string name, string title, string subTitle, string content)
        {
            Name = name;
            Title = title;
            SubTitle = subTitle;
            Content = content;
            CreationTime = DateTime.UtcNow;
            LastEditTime = CreationTime;
        }

        public void UpdateEditTime()
        {
            LastEditTime = DateTime.UtcNow;
        }
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        
        public string Content { get; set; }
        
        public DateTime CreationTime { get; set; }
        
        public DateTime LastEditTime { get; set; }
    }
}
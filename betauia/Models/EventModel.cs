using System;
using System.Net.Mime;

namespace betauia.Models
{
    public class EventModel
    {
        public EventModel(string title, string subTitle, string description, string content, DateTime eventTime, string author, string image, int atendees, bool isPublic, int eventPrice)
        {
            Title = title;
            SubTitle = subTitle;
            Description = description;
            Content = content;
            EventTime = eventTime;
            Author = author;
            Image = image;
            Atendees = atendees;
            IsPublic = isPublic;
            EventPrice = eventPrice;
        }
        
        
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public int Atendees { get; set; }
        public bool IsPublic { get; set; }
        public string Author { get; set; }
        public DateTime EventTime { get; set; }
        public int EventPrice { get; set; }
    }
}
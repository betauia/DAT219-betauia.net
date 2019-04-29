using System;
using System.Net.Mime;

namespace betauia.Models
{
    public class EventModel
    {
        public EventModel(string title, string subTitle, string description, string content, DateTime eventTime)
        {
            Title = title;
            SubTitle = subTitle;
            Description = description;
            Content = content;
            EventTime = eventTime;
        }
        
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime EventTime { get; set; }
    }
}
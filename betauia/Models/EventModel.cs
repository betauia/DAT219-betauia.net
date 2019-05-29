using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mime;

namespace betauia.Models
{
    public class EventModel
    {
        public EventModel()
        {
            Image = null;
            Atendees = 0;
            Author = null;
            EventTime = DateTime.MinValue;
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
        
        public string SeatMapId { get; set; }
        public EventSeatMap SeatMap { get; set; }
        
        public string SponsorId { get; set; }
        public SponsorModel Sponsor { get; set; }
    }
}
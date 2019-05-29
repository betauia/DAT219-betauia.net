using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace betauia.Models
{
    public class EventSeatMap
    {
        public string Id { get; set; }
        public int EventId { get; set; }
        public int NumSeats { get; set; }
        public int NumSeatsAvailable { get; set; }
    }
}
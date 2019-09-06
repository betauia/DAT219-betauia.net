using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace betauia.Models
{
    public class EventSeat
    {
        public EventSeat()
        {
            IsAvailable = true;
            IsReserved = false;
            ReserverId = "";
        }
        //[Key, Column(Order = 0)]
        public string Id { get; set; }

        public int Number { get; set; }

        //[Key, Column(Order =  1)]
        public string OwnerId { get; set; }

        //public float height { get; set; }
        //public float width { get; set; }

        public int x { get; set; }
        public int y { get; set; }

        public bool IsAvailable { get; set; }
        public bool IsReserved { get; set; }
        public string ReserverId { get; set; }

    }
}

using System.Collections.Generic;

namespace betauia.Models
{
    public class SeatMapModel
    {
        public SeatMapModel(int numSeats, float seatPrice)
        {
            NumSeats = numSeats;
            SeatPrice = seatPrice;
            NumSeatsAvailable = NumSeats;
        }
        
        public int Id { get; set; }
        
        // The amount of seats on the current event
        public int NumSeats { get; set; }
        public int NumSeatsAvailable { get; set; }
        
        // Seat price for the current event
        public float SeatPrice { get; set; }
    }
}
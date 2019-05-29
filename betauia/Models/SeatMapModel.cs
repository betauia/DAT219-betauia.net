namespace betauia.Models
{
    public class SeatMapModel
    {
        public SeatMapModel(int numSeats)
        {
            NumSeats = numSeats;
            NumSeatsAvailable = NumSeats;
        }
        
        public string Id { get; set; }
        
        // The amount of seats on the current event
        public int NumSeats { get; set; }
        public int NumSeatsAvailable { get; set; }
        //public float Height { get; set; }
        //public float Width { get; set; }
        //public string BackgroundImage { get; set; }
    }
}
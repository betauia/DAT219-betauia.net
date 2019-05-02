namespace betauia.Models
{
    public class SeatModel
    {
        public SeatModel(int seatNum)
        {
            SeatNum = seatNum;
        }
        
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public SeatMapModel Owner { get; set; }
        
        // Seat number
        public int SeatNum { get; set; }
    }
}
namespace betauia.Models
{
    public class SeatModel
    {
        public SeatModel()
        {
            IsAvailable = true;
        }
        
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public SeatMapModel Owner { get; set; }
        
        public bool IsAvailable { get; set; }
    }
}
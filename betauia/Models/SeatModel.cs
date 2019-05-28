namespace betauia.Models
{
    public class SeatModel
    {
        public SeatModel()
        {
            IsAvailable = true;
        }
        
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public SeatMapModel Owner { get; set; }
        
        public float height { get; set; }
        public float width { get; set; }
        
        public float x { get; set; }
        public float y { get; set; }
        
        public bool IsAvailable { get; set; }
    }
}
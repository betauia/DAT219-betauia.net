using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace betauia.Models
{
    public class SeatModel
    {
        public SeatModel()
        {
            IsAvailable = true;
        }
        //[Key, Column(Order = 0)]
        public int Id { get; set; }
        
        //[Key, Column(Order =  1)]
        public string OwnerId { get; set; }
        
        [ForeignKey("OwnerId")]
        public SeatMapModel Owner { get; set; }
        
        //public float height { get; set; }
        //public float width { get; set; }
        
        public int x { get; set; }
        public int y { get; set; }
        
        public bool IsAvailable { get; set; }
    }
}
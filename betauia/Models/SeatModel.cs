using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace betauia.Models
{
    public class SeatModel
    {
        public SeatModel()
        {
 
        }
        //[Key, Column(Order = 0)]
       public string Id { get; set; }
        
        public int Number { get; set; }
        
        //[Key, Column(Order =  1)]
        public string OwnerId { get; set; }
        
        //[ForeignKey("OwnerId")]
        public SeatMapModel Owner { get; set; }
        
        //public float height { get; set; }
        //public float width { get; set; }
        
        public int x { get; set; }
        public int y { get; set; }
    }
}
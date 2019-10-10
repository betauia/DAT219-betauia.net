using System.ComponentModel.DataAnnotations;
using System.Linq;
using betauia.Data;

namespace betauia.Models
{
    public class TicketModel
    {
        public TicketModel()
        {
            Verified = false;
        }

        public void CancelTicket(ApplicationDbContext dbContext)
        {
          Status = "CANCEL";
          QR = null;
          Verified = false;
          QRID = null;

          var seats = dbContext.EventSeats.Where(a => a.TicketId == Id.ToString()).ToList();
          foreach (var seat in seats)
          {
            seat.IsAvailable = true;
            seat.IsReserved = false;
            seat.ReserverId = null;
            seat.TicketId = null;
          }
          
                
          var eventmodel = dbContext.EventSeatMaps.Where(a => a.EventId == EventId).First();
          eventmodel.NumSeatsAvailable += seats.Count;
          dbContext.SaveChanges();
        }
        public int Id { get; set; }
        public string UserId { get; set; }
        public string TimePurchased { get; set; }
        public int Amount { get; set; }
        public string Status { get; set; }
        public string VippsOrderId { get; set; }
        [Required]
        public int EventId { get; set; }
        public string QR { get; set; }
        public string QRID { get; set; }
        public bool Verified { get; set; }
    }
}

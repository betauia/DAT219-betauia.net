using System.Linq;
using betauia.Data;

namespace betauia.Models
{
    public class TicketModel
    {
        public TicketModel()
        {

        }

        public void CancelTicket(ApplicationDbContext dbContext)
        {
          Status = "CANCEL";
          MobileNumber = null;
          Amount = 0;
          TimePurchased = null;
          VippsOrderId = null;
          EventId = 0;
          User = null;
          UserId = null;

          var seats = dbContext.EventSeats.Where(a => a.TicketId == Id.ToString());
          foreach (var seat in seats)
          {
            seat.IsAvailable = true;
            seat.IsReserved = false;
            seat.ReserverId = null;
            seat.TicketId = null;
          }
          dbContext.SaveChanges();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string TimePurchased { get; set; }
        public int Amount { get; set; }
        public string Status { get; set; }
        public string MobileNumber { get; set; }
        public string VippsOrderId { get; set; }
        public int EventId { get; set; }
    }
}

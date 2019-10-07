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
          Status = "CANCEfaL";
          MobileNumber = null;
          Amount = 0;
          TimePurchased = null;
          VippsOrderId = null;
          EventId = 0;
          User = null;
          UserId = null;
          QR = null;
          Verified = false;

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
        public string QR { get; set; }
        public bool Verified { get; set; }
    }
}

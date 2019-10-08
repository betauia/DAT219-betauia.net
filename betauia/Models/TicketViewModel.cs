using System.Collections.Generic;
using System.Diagnostics;

namespace betauia.Models
{
  public class TicketViewModel
  {
    public TicketViewModel(TicketModel t, string eventTitle)
    {
      TimePurchased = t.TimePurchased;
      Amount = t.Amount;
      Id = t.VippsOrderId;
      EventTitle = eventTitle;
      QR = t.QR;
      Verified = t.Verified;
      switch (t.Status)
      {
        case "STARTED":
          Status = "Ticket created";
          break;
        case "INITIATE":
          Status = "Payment initiated";
          break;
        case "CANCEL" :
          Status = "Ticket cancelled";
          break;
        case "RESERVE" :
            Status = "Ticket delivered";
            break;
        case "REFUND" :
            Status = "Ticket refunded";
            break;
        case "CAPTURE" :
          Status = "Order successful";
          break;
        default:
          Status = "An error occured getting ticket status";
          break;
      }
    }

    public string Id { get; set; }
    public string TimePurchased { get; set; }
    public int Amount { get; set; }
    public string Status { get; set; }
    public List<EventSeat> Seats { get; set; }
    public string EventTitle { get; set; }
    public string QR { get; set; }
    public bool Verified { get; set; }
  }
}

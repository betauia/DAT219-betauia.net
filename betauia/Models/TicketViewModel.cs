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
      MobileNumber = t.MobileNumber;
      Id = t.VippsOrderId;
      EventTitle = eventTitle;

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
          Status = "Payment is being captured";
          break;
        default:
          Status = "An error occured getting ticket staus";
          break;
      }
    }

    public string Id { get; set; }
    public string TimePurchased { get; set; }
    public int Amount { get; set; }
    public string Status { get; set; }
    public string MobileNumber { get; set; }
    public List<EventSeat> Seats { get; set; }
    public string EventTitle { get; set; }
  }
}

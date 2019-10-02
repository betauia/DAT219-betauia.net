using System.Collections.Generic;

namespace betauia.Models
{
  public class TicketPreviewModel
  {
    public TicketPreviewModel(TicketModel t)
    {
      TimePurchased = t.TimePurchased;
      Amount = t.Amount;
      MobileNumber = t.MobileNumber;
    }

    public string TimePurchased { get; set; }
    public int Amount { get; set; }
    public string MobileNumber { get; set; }
    public List<EventSeat> EventSeats { get; set; }
  }
}

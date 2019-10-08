using System.Collections.Generic;

namespace betauia.Models
{
  public class TicketPreviewModel
  {
    public TicketPreviewModel(TicketModel t)
    {
      TimePurchased = t.TimePurchased;
      Amount = t.Amount;
    }

    public string TimePurchased { get; set; }
    public int Amount { get; set; }
    public List<EventSeat> EventSeats { get; set; }
  }
}

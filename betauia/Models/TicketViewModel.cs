using System.Collections.Generic;

namespace betauia.Models
{
  public class TicketViewModel
  {
    public TicketViewModel(TicketModel t)
    {
      TimePurchased = t.TimePurchased;
      Amount = t.Amount;
      Status = t.Status;
    }

    public string TimePurchased { get; set; }
    public int Amount { get; set; }
    public string Status { get; set; }
    public List<EventSeat> EventSeats { get; set; }
  }
}

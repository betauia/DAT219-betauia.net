using System.Collections.Generic;

namespace betauia.Models
{
  public class TicketViewModel
  {
    public TicketViewModel(TicketModel t, string eventTitle)
    {
      TimePurchased = t.TimePurchased;
      Amount = t.Amount;
      Status = t.Status;
      MobileNumber = t.MobileNumber;
      Id = t.VippsOrderId;
      EventTitle = eventTitle;
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

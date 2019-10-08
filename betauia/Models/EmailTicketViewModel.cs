using System.Collections.Generic;

namespace betauia.Models
{
    public class EmailTicketViewModel
    {
        public string ImageLink { get; set; }
        public List<string> SeatsReserved { get; set; }
        public string UserName { get; set; }
        public string EventName { get; set; }
        public string OrderId { get; set; }

        public EmailTicketViewModel(string imageLink, List<string> seatsReserved, string userName, string eventName, string orderId)
        {
            ImageLink = imageLink;
            SeatsReserved = seatsReserved;
            UserName = userName;
            EventName = eventName;
            OrderId = orderId;
        }
    }
}
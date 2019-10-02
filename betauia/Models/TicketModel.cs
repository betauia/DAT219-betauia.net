namespace betauia.Models
{
    public class TicketModel
    {
        public TicketModel()
        {

        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string TimePurchased { get; set; }
        public int Amount { get; set; }
        public string Status { get; set; }
        public string MobileNumber { get; set; }
        public string VippsOrderId { get; set; }
    }
}

namespace betauia.Models
{
    public class TicketModel
    {
        public TicketModel(string timePurchased, float price, bool isVerified, string paymentMethod)
        {
            TimePurchased = timePurchased;
            Price = price;
            IsVerified = isVerified;
            PaymentMethod = paymentMethod;
        }
        
        public int Id { get; set; }
        
        public int OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }
        
        public string TimePurchased { get; set; }
        public float Price { get; set; }
        
        public bool IsVerified { get; set; }
        public string PaymentMethod { get; set; }
    }
}
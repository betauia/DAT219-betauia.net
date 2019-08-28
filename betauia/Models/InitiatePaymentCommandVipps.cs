namespace betauia.Models {
    public class InitiatePaymentCommand {
        CustomerInfoDto customerInfo { get; set; }
        MerchantInfoDto merchantInfo { get; set; }
        TransactionInfoInitiateDTO transaction { get; set; }

    }
    
}


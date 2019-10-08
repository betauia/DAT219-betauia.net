namespace betauia.Vipps
{
    public class RefundPaymentModels
    {
        public class RefundOrderRequest
        {
           public CapturePaymentModels.capturemerchantinfo merchantInfo { get; set; }
           public RefundOrderTransaction transaction { get; set; }
        }

        public class RefundOrderTransaction
        {
            public string transactionText { get; set; }
        }
    }
}
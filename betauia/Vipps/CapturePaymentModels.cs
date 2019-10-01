namespace betauia.Vipps
{
  public class CapturePaymentModels
  {
    public class CapturePaymentRequestModel
    {
      public MerchantInfoModel merchantInfoModel { get; set; }
      public TransactionModel transactionModel { get; set; }
    }

    public class CapturePaymentResponseModel
    {
      public string orderId { get; set; }
      public TransactionInfoModel transactioninfo { get; set; }
      public TransactionSummary transactionSummary { get; set; }
    }

    public class TransactionInfoModel
    {
      public string amount { get; set; }
      public string timeStamp { get; set; }
      public string transactionText { get; set; }
      public string status { get; set; }
      public string transactionId { get; set; }
    }

    public class TransactionSummary
    {
      public string capturedAmount { get; set; }
      public string remainingAmountToCapture { get; set; }
      public string refundedAmount { get; set; }
      public string remainingAmountToRefund { get; set; }
    }
  }
}

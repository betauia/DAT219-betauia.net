namespace betauia.Vipps
{
  public class CapturePaymentModels
  {
    public class CapturePaymentRequestModel
    {
      public MerchantInfoModel merchantInfo { get; set; }
      public TransactionModel transaction { get; set; }
    }

    public class CapturePaymentResponseModel
    {
      public string orderId { get; set; }
      public TransactionInfoModel transactionInfo { get; set; }
      public TransactionSummary transactionSummary { get; set; }
    }

    public class TransactionInfoModel
    {
      public int amount { get; set; }
      public string timeStamp { get; set; }
      public string transactionText { get; set; }
      public string status { get; set; }
      public string transactionId { get; set; }
    }

    public class TransactionSummary
    {
      public int capturedAmount { get; set; }
      public int remainingAmountToCapture { get; set; }
      public int refundedAmount { get; set; }
      public int remainingAmountToRefund { get; set; }
    }
  }
}

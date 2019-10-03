using System.Collections;
using System.Collections.Generic;

namespace betauia.Vipps
{
  public class PaymentDetailsModels
  {
    public class PaymentDetailResponseModel
    {
      public string orderId { get; set; }
      public CapturePaymentModels.TransactionSummary transactionSummary { get; set; }
      public List<TransactionLogHistoryModel> transactionLogHistory { get; set; }
    }

    public class TransactionLogHistoryModel {
      public int amount { get; set; }
      public string transactionText { get; set; }
      public string transactionId { get; set; }
      public string timeStamp { get; set; }
      public string operation { get; set; }
      public string requestId { get; set; }
      public bool operationSuccess { get; set; }
    }
  }
}

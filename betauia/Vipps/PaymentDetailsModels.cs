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

    public class PaymentStatusResponse
    {
      public string orderId { get; set; }
      public CapturePaymentModels.TransactionInfoModel transactionInfo { get; set; }
    }

    public class TransactionLogHistoryModel {
      public string amount { get; set; }
      public string transactionText { get; set; }
      public string transactionId { get; set; }
      public string timeStamp { get; set; }
      public string operation { get; set; }
      public string requestId { get; set; }
      public string operationSuccess { get; set; }
    }
  }
}

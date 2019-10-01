namespace betauia.Vipps
{
  public class InitPaymentResponseModel
  {
    public string orderid { get; set; }
    public string url { get; set; }
  }
  public class MerchantInfoModel
  {
    public string merchantSerialNumber { get; set; }
    public string callbackPrefix { get; set; }
    public string fallBack { get; set; }
    public string authToken { get; set; }
    public bool isApp { get; set; }
  }

  public class CustomerInfoModel
  {
    public string mobileNumber { get; set; }
  }

  public class TransactionModel
  {
    public string orderId { get; set; }
    public int amount { get; set; }
    public string transactionText { get; set; }
  }

  public class InitPaymentRequestModel
  {
    public MerchantInfoModel merchantInfo { get; set; }
    public CustomerInfoModel customerInfo { get; set; }
    public TransactionModel transaction { get; set; }
  }
}

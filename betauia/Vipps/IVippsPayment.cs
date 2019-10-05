using System.Threading.Tasks;

namespace betauia.Vipps
{
  public interface IVippsPayment
  {
    Task GetVippsToken();
    Task<InitPaymentResponseModel> InitiatePayment(string mobilenumber, string orderid, int amount, string text);
    Task<PaymentDetailsModels.PaymentDetailResponseModel> GetPaymentDetails(string orderid);
    Task<CapturePaymentModels.CapturePaymentResponseModel> CapturePayment(string orderid);
    Task<CapturePaymentModels.CapturePaymentResponseModel> CancelPayment(string orderid);
    Task<CapturePaymentModels.CapturePaymentResponseModel> RefundPayment(string orderid);
  }
}

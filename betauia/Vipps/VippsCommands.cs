using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using betauia.Models;
using Newtonsoft.Json;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
using System.Timers;
using betauia.Data;


namespace betauia.Vipps {
    public class VippsApiController : IVippsPayment {
        private static string client_id = "451a3d2c-d526-4cbe-b59c-940be028ba7a";
        private static string client_secret = "WFZxZGxhRDhPR2FIZkxzaEtua0k=";
        private static string merchantSeiralNumber = "571822";
        private static string OcpApimSubscriptionKey = "9e641e435a7549e29bcf6f067c390ab1";
        private static string tokenpath = "Vipps/token.json";

        private readonly PaymentDbContext _paymentDbContext;
        public VippsApiController(PaymentDbContext paymentDbContext)
        {
          GetVippsToken().Wait();
        }

        private async Task<T> TryJsonConvert<T>(string json)
        {
          T ret = default(T);
          try
          {
            var t = JsonConvert.DeserializeObject<T>(json);
            ret = t;
          }
          catch (Exception e)
          {
            Console.WriteLine(e);
            throw;
          }
          return ret;
        }

        public async Task GetVippsToken()
        {
          var client = new RestClient("https://api.vipps.no/accessToken/get");
          var request = new RestRequest(Method.POST);
          request.AddHeader("cache-control", "no-cache");
          request.AddHeader("Ocp-Apim-Subscription-Key", OcpApimSubscriptionKey);
          request.AddHeader("client_secret", client_secret);
          request.AddHeader("client_id", client_id);
          IRestResponse response = client.Execute(request);

          TokenResponse t = JsonConvert.DeserializeObject <TokenResponse>(response.Content);

          var json = JsonConvert.SerializeObject(new TokenModel {token = t.Access_token});
          File.WriteAllText(tokenpath,json);
        }

        private async Task<string> GetToken()
        {
          string ret = string.Empty;
          using (StreamReader r = new StreamReader(tokenpath))
          {
            var json = r.ReadToEnd();
            Models.TokenModel t = JsonConvert.DeserializeObject<Models.TokenModel>(json);
            ret = t.Token;
          }
          return ret;
        }

        public async Task<InitPaymentResponseModel> InitiatePayment(string mobilenumber, string orderid, int amount, string text)
        {
          var client = new RestClient("https://api.vipps.no/ecomm/v2/payments/");
          var request = new RestRequest(Method.POST);
          request.AddHeader("cache-control", "no-cache");
          request.AddHeader("Authorization", "Bearer " + await GetToken());
          request.AddHeader("Ocp-Apim-Subscription-Key", OcpApimSubscriptionKey);
          request.AddHeader("Content-Type", "application/json");

          Guid g;
          g = Guid.NewGuid();

          var minfo = new MerchantInfoModel
          {
            merchantSerialNumber = merchantSeiralNumber,
            callbackPrefix = "https://example.com/vipps/callbacks-for-payment-update",
            fallBack = "http://128.39.149.31:8080/ticketdetails/"+orderid,
            authToken = g.ToString(),
            isApp = false
          };

          var cinfo = new CustomerInfoModel
          {
            mobileNumber = mobilenumber,
          };

          var transaction = new TransactionModel
          {
            orderId = orderid,
            amount = amount,
            transactionText = text
          };

          var init = new InitPaymentRequestModel
          {
            merchantInfo = minfo,
            customerInfo = cinfo,
            transaction = transaction
          };

          var json = JsonConvert.SerializeObject(init);
          request.AddParameter("undefined",json.ToString(), ParameterType.RequestBody);
          IRestResponse response = client.Execute(request);

          if (response.Request == null)
          {
            return null;
          }


          InitPaymentResponseModel t = await TryJsonConvert<InitPaymentResponseModel>(response.Content);
          if (t != null)
          {
            return t;
          }

          var error = await TryJsonConvert<ErrorModel>(response.Content);
          if (error!=null && error.responseInfo.responseCode == 401)
          {
            await GetVippsToken();
            return await InitiatePayment(mobilenumber,orderid, amount,text);
          }
          return null;
        }

        public async Task<PaymentDetailsModels.PaymentDetailResponseModel> GetPaymentDetails(string orderid)
        {
          var client = new RestClient("https://api.vipps.no/ecomm/v2/payments/"+orderid+"/details");
          var request = new RestRequest(Method.GET);
          request.AddHeader("cache-control", "no-cache");
          request.AddHeader("Authorization", "Bearer " + await GetToken());
          request.AddHeader("Ocp-Apim-Subscription-Key", OcpApimSubscriptionKey);
          request.AddHeader("Content-Type", "application/json");

          IRestResponse response = client.Execute(request);
          if (response.Content == null) return null;

          var t = await TryJsonConvert<PaymentDetailsModels.PaymentDetailResponseModel>(response.Content);
          if (t != null)
          {
            return t;
          }

          var error = await TryJsonConvert<ErrorModel>(response.Content);
          if (error!=null && error.responseInfo.responseCode == 401)
          {
            await GetVippsToken();
            return await GetPaymentDetails(orderid);
          }
          return null;
        }

        public async Task<CapturePaymentModels.CapturePaymentResponseModel> CapturePayment(string orderid)
        {
          var client = new RestClient("https://api.vipps.no/ecomm/v2/payments/"+orderid+"/capture");
          var request = new RestRequest(Method.POST);
          request.AddHeader("cache-control", "no-cache");
          request.AddHeader("Authorization", "Bearer " + await GetToken());
          request.AddHeader("Ocp-Apim-Subscription-Key", OcpApimSubscriptionKey);
          request.AddHeader("Content-Type", "application/json");

          var minfo = new CapturePaymentModels.capturemerchantinfo
          {
            merchantSerialNumber = merchantSeiralNumber,
          };

          var transaction = new CapturePaymentModels.transactioncapture
          {
            amount = 0,
            transactionText = "Capture test"
          };

          var capturemodel = new CapturePaymentModels.CapturePaymentRequestModel
          {
            merchantInfo = minfo,
            transaction = transaction
          };



          var json = JsonConvert.SerializeObject(capturemodel);
          request.AddParameter("undefined",json.ToString(), ParameterType.RequestBody);

          IRestResponse response = client.Execute(request);
          if (response.Content == null) return null;

          var t = await TryJsonConvert<CapturePaymentModels.CapturePaymentResponseModel>(response.Content);
          if (t != null)
          {
            return t;
          }

          var error = await TryJsonConvert<ErrorModel>(response.Content);
          if (error!=null && error.responseInfo.responseCode == 401)
          {
            await GetVippsToken();
            return await CapturePayment(orderid);
          }
          return null;
        }

        public async Task<CapturePaymentModels.CapturePaymentResponseModel> CancelPayment(string orderid)
        {
          var client = new RestClient("https://api.vipps.no/ecomm/v2/payments/"+orderid+"/cancel");
          var request = new RestRequest(Method.PUT);
          request.AddHeader("cache-control", "no-cache");
          request.AddHeader("Authorization", "Bearer " + await GetToken());
          request.AddHeader("Ocp-Apim-Subscription-Key", OcpApimSubscriptionKey);
          request.AddHeader("Content-Type", "application/json");

          var minfo = new MerchantInfoModel
          {
            merchantSerialNumber = merchantSeiralNumber,
          };

          var transaction = new TransactionModel
          {
            transactionText = "Delete test"
          };

          var capturemodel = new CapturePaymentModels.CapturePaymentRequestModel
          {
            //merchantInfo = minfo,
            //transaction = transaction
          };

          var json = JsonConvert.SerializeObject(capturemodel);
          request.AddParameter("undefined",json.ToString(), ParameterType.RequestBody);

          IRestResponse response = client.Execute(request);
          if (response.Content == null) return null;

          var t = await TryJsonConvert<CapturePaymentModels.CapturePaymentResponseModel>(response.Content);
          if (t != null)
          {
            return t;
          }

          var error = await TryJsonConvert<ErrorModel>(response.Content);
          if (error!=null && error.responseInfo.responseCode == 401)
          {
            await GetVippsToken();
            return await CancelPayment(orderid);
          }
          return null;
        }

        public async Task<CapturePaymentModels.CapturePaymentResponseModel> RefundPayment(string orderid)
        {
          var client = new RestClient("https://api.vipps.no/ecomm/v2/payments/"+orderid+"/refund");
          var request = new RestRequest(Method.PUT);
          request.AddHeader("cache-control", "no-cache");
          request.AddHeader("Authorization", "Bearer " + await GetToken());
          request.AddHeader("Ocp-Apim-Subscription-Key", OcpApimSubscriptionKey);
          request.AddHeader("Content-Type", "application/json");

          var minfo = new MerchantInfoModel
          {
            merchantSerialNumber = merchantSeiralNumber,
          };

          var transaction = new TransactionModel
          {
            transactionText = "Refund test"
          };

          var capturemodel = new CapturePaymentModels.CapturePaymentRequestModel
          {
            //merchantInfo = minfo,
            //transaction = transaction
          };

          var json = JsonConvert.SerializeObject(capturemodel);
          request.AddParameter("undefined",json.ToString(), ParameterType.RequestBody);

          IRestResponse response = client.Execute(request);
          if (response.Content == null) return null;

          var t = await TryJsonConvert<CapturePaymentModels.CapturePaymentResponseModel>(response.Content);
          if (t != null)
          {
            return t;
          }

          var error = await TryJsonConvert<ErrorModel>(response.Content);
          if (error!=null && error.responseInfo.responseCode == 401)
          {
            await GetVippsToken();
            return await RefundPayment(orderid);
          }
          return null;
        }

        public class TokenModel
        {
          public string token { get; set; }
        }

        public class ErrorModel
        {
          public ResponseInfoModel responseInfo { get; set; }
          public ResultModel result { get; set; }
        }

        public class ResponseInfoModel
        {
          public int responseCode { get; set; }
          public string responseMessage { get; set; }
        }

        public class ResultModel
        {
          public string message { get; set; }
        }
    }
}

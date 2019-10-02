using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
using betauia.Data;


namespace betauia.Vipps {
    public class VippsApiController {
        private static string client_id = "451a3d2c-d526-4cbe-b59c-940be028ba7a";
        private static string client_secret = "WFZxZGxhRDhPR2FIZkxzaEtua0k=";
        private static string merchantSeiralNumber = "571822";
        private static string OcpApimSubscriptionKey = "9e641e435a7549e29bcf6f067c390ab1";

        public VippsApiController(ApplicationDbContext dbContext)
        {

        }

        public string GetVippsToken()
        {
          var client = new RestClient("https://api.vipps.no/accessToken/get");
          var request = new RestRequest(Method.POST);
          request.AddHeader("cache-control", "no-cache");
          request.AddHeader("Ocp-Apim-Subscription-Key", OcpApimSubscriptionKey);
          request.AddHeader("client_secret", client_secret);
          request.AddHeader("client_id", client_id);
          IRestResponse response = client.Execute(request);

          TokenResponse t = JsonConvert.DeserializeObject <TokenResponse>(response.Content);

          return t.Access_token;
        }


        public InitPaymentResponseModel InitiatePayment(string mobilenumber, string orderid, int amount, string ttext)
        {
          var client = new RestClient("https://api.vipps.no/ecomm/v2/payments/");
          var request = new RestRequest(Method.POST);
          request.AddHeader("cache-control", "no-cache");
          request.AddHeader("Authorization", "Bearer " + GetVippsToken());
          request.AddHeader("Ocp-Apim-Subscription-Key", OcpApimSubscriptionKey);
          request.AddHeader("Content-Type", "application/json");

          Guid g;
          g = Guid.NewGuid();

          var minfo = new MerchantInfoModel
          {
            merchantSerialNumber = merchantSeiralNumber,
            callbackPrefix = "https://example.com/vipps/callbacks-for-payment-update",
            fallBack = "localhost:8080/ticketdetails/"+orderid,
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
            transactionText = ttext
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

          InitPaymentResponseModel t = JsonConvert.DeserializeObject<InitPaymentResponseModel>(response.Content);
          return t;
        }

        public PaymentDetailsModels.PaymentDetailResponseModel GetPaymentDetails(string orderid)
        {
          var client = new RestClient("https://api.vipps.no/ecomm/v2/payments/"+orderid+"/details");
          var request = new RestRequest(Method.GET);
          request.AddHeader("cache-control", "no-cache");
          request.AddHeader("Authorization", "Bearer " + GetVippsToken());
          request.AddHeader("Ocp-Apim-Subscription-Key", OcpApimSubscriptionKey);
          request.AddHeader("Content-Type", "application/json");

          IRestResponse response = client.Execute(request);
          if (response.Content == null) return null;

          var t = JsonConvert.DeserializeObject<PaymentDetailsModels.PaymentDetailResponseModel>(response.Content);

          return t;
        }

        public CapturePaymentModels.CapturePaymentResponseModel CapturePayment(string orderid)
        {
          var client = new RestClient("https://api.vipps.no/ecomm/v2/payments/"+orderid+"/capture");
          var request = new RestRequest(Method.GET);
          request.AddHeader("cache-control", "no-cache");
          request.AddHeader("Authorization", "Bearer " + GetVippsToken());
          request.AddHeader("Ocp-Apim-Subscription-Key", OcpApimSubscriptionKey);
          request.AddHeader("Content-Type", "application/json");

          var minfo = new MerchantInfoModel
          {
            merchantSerialNumber = merchantSeiralNumber,
          };

          var transaction = new TransactionModel
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

          var t = JsonConvert.DeserializeObject<CapturePaymentModels.CapturePaymentResponseModel>(response.Content);

          return t;
        }

        public CapturePaymentModels.CapturePaymentResponseModel CancelPayment(string orderid)
        {
          var client = new RestClient("https://api.vipps.no/ecomm/v2/payments/"+orderid+"/cancel");
          var request = new RestRequest(Method.PUT);
          request.AddHeader("cache-control", "no-cache");
          request.AddHeader("Authorization", "Bearer " + GetVippsToken());
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
            merchantInfo = minfo,
            transaction = transaction
          };

          var json = JsonConvert.SerializeObject(capturemodel);
          request.AddParameter("undefined",json.ToString(), ParameterType.RequestBody);

          IRestResponse response = client.Execute(request);
          if (response.Content == null) return null;

          var t = JsonConvert.DeserializeObject<CapturePaymentModels.CapturePaymentResponseModel>(response.Content);

          return t;
        }

        public CapturePaymentModels.CapturePaymentResponseModel RefundPayment(string orderid)
        {
          var client = new RestClient("https://api.vipps.no/ecomm/v2/payments/"+orderid+"/refund");
          var request = new RestRequest(Method.PUT);
          request.AddHeader("cache-control", "no-cache");
          request.AddHeader("Authorization", "Bearer " + GetVippsToken());
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
            merchantInfo = minfo,
            transaction = transaction
          };

          var json = JsonConvert.SerializeObject(capturemodel);
          request.AddParameter("undefined",json.ToString(), ParameterType.RequestBody);

          IRestResponse response = client.Execute(request);
          if (response.Content == null) return null;

          var t = JsonConvert.DeserializeObject<CapturePaymentModels.CapturePaymentResponseModel>(response.Content);

          return t;
        }
    }
}

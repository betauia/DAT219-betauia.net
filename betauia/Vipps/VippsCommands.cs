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
          request.AddHeader("Postman-Token", "e6a69cb6-e91a-4448-8a05-03bb1a541688");
          request.AddHeader("cache-control", "no-cache");
          request.AddHeader("Authorization", "Bearer " + GetVippsToken());
          request.AddHeader("Ocp-Apim-Subscription-Key", "9e641e435a7549e29bcf6f067c390ab1");
          request.AddHeader("Content-Type", "application/json");

          Guid g;
          g = Guid.NewGuid();

          var minfo = new MerchantInfoModel
          {
            merchantSerialNumber = merchantSeiralNumber,
            callbackPrefix = "https://example.com/vipps/callbacks-for-payment-update",
            fallBack = "https://example.com/vipps/fallback-result-page/order123abc",
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
    }
}

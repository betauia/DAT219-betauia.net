using Microsoft.AspNetCore.Mvc;
using betauia.Models;
using RestSharp;

namespace betauia.Controllers {
    [ApiController]
    [Route("api/vipps")]
    public class VippsApiController : ControllerBase {
        private AuthorizationTokenResponse _atr;
        private readonly InitiatePaymentCommand _ipc;
        
        public VippsApiController(AuthorizationTokenResponse atr, InitiatePaymentCommand ipc) {
            
        }/* 
        public async IActionResult GetVippsToken() {
            var client = new RestClient("https://api.vipps.no/accessToken/get");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Content-Length", "");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Cookie", "x-ms-gateway-slice=prod; stsservicecookie=ests; ");
            request.AddHeader("Host", "api.vipps.no");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "/*");
            request.AddHeader("Ocp-Apim-Subscription-Key", vipps.env.CLIENT_ID);
            request.AddHeader("client_secret", CLIENT_SECRET);
            request.AddHeader("client_id", OCP_APIM_SUBSCRIPTION_KEY);
            IRestResponse response = client.Execute(request);

            await(response);
        }

        public IActionResult InitiatePayment() {
            var client = new RestClient("http://{{base_url}}/ecomm/v2/payments/");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Content-Length", "724");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Cookie", "x-ms-gateway-slice=prod; stsservicecookie=ests; fpc=");
            request.AddHeader("Host", "api.vipps.no");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "/*");
            request.AddHeader("Authorization", "Bearer {{access_token}}");
            request.AddHeader("Ocp-Apim-Subscription-Key", "{{Ocp-Apim-Subscription-Key}}");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", "{\n  \"merchantInfo\": {\n    \"merchantSerialNumber\": \"{{merchantSerialNumber}}\",\n    \"callbackPrefix\":\"{{callbackPrefix}}\",\n    \"fallBack\": \"{{fallBack}}\",\n    \"authToken\": \"2f9ecb80-afab-49bf-9202-51cf4b56e0ad\",\n    \"isApp\":false\n  },\n  \"customerInfo\": {\n    \"mobileNumber\": \"{{mobileNumber}}\"\n  },\n  \"transaction\": {\n    \"orderId\": \"{{orderId}}\",\n    \"amount\": {{amount}},\n    \"transactionText\": \"{{transactionTextInitiate}}\"\n  }\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return Ok();
        }

        public IActionResult CapturePayment() {
            var client = new RestClient("http://{{base_url}}/ecomm/v2/payments/{{orderId}}/capture");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "2d30d8e6-b763-4ddc-9aad-e769c6dc2dca");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("X-Request-Id", "{{orderId}}XIDC1");
            request.AddHeader("Authorization", "Bearer {{access_token}}");
            request.AddHeader("Ocp-Apim-Subscription-Key", "{{Ocp-Apim-Subscription-Key}}");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", " {\n    \"merchantInfo\": {\n        \"merchantSerialNumber\": \"{{merchantSerialNumber}}\"\n    },\n    \"transaction\": {\n        \"amount\": {{amount_capture}},\n        \"transactionText\": \"{{transactionTextCapture}}\"\n    }\n }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            return Ok();
        }

        public IActionResult RefundPayment() {
            var client = new RestClient("http://{{base_url}}/ecomm/v2/payments/{{orderId}}/refund");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "19aca508-fab7-40d9-93ed-44aad64f5681");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("X-Request-Id", "{{orderId}}XIDR1");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Ocp-Apim-Subscription-Key", "{{Ocp-Apim-Subscription-Key}}");
            request.AddHeader("Authorization", "Bearer {{access_token}}");
            request.AddParameter("undefined", "{\r\n    \"merchantInfo\": {\r\n        \"merchantSerialNumber\": \"{{merchantSerialNumber}}\"\r\n    },\r\n    \"transaction\": {\r\n        \"transactionText\":\"{{transactionTextRefund}}\"\r\n    }\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            return Ok();
        }

        public IActionResult CancelPayment() {
            var client = new RestClient("http://{{base_url}}/ecomm/v2/payments/{{orderId}}/cancel");
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Postman-Token", "e02e333c-35f9-4d38-a568-c7933a7721a5");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Authorization", "Bearer {{access_token}}");
            request.AddHeader("Ocp-Apim-Subscription-Key", "{{Ocp-Apim-Subscription-Key}}");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", "{\r\n    \"merchantInfo\": {\r\n        \"merchantSerialNumber\": \"{{merchantSerialNumber}}\"\r\n    },\r\n    \"transaction\": {\r\n        \"transactionText\":\"{{transactionTextCancel}}\"\r\n    }\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            return Ok();
        }

        public IActionResult GetPaymentDetails() {
            var client = new RestClient("http://{{base_url}}/ecomm/v2/payments/{{orderId}}/details");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Postman-Token", "9253a12f-ecd0-4aa8-862a-f58ba428a4be");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Ocp-Apim-Subscription-Key", "{{Ocp-Apim-Subscription-Key}}");
            request.AddHeader("Authorization", "Bearer {{access_token}}");
            IRestResponse response = client.Execute(request);

            return Ok(response);
        }

        public IActionResult GetOrderStatus() {
            var client = new RestClient("http://{{base_url}}/ecomm/v2/payments/{{orderId}}/status");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Postman-Token", "c99fd212-0dea-450f-9ae3-6404b9d66c93");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Ocp-Apim-Subscription-Key", "{{Ocp-Apim-Subscription-Key}}");
            request.AddHeader("Authorization", "Bearer {{access_token}}");
            IRestResponse response = client.Execute(request);

            return Ok(response);
        }*/
    }
}
using Microsoft.AspNetCore.Mvc;
using betauia.Models;
using RestSharp;

namespace betauia.Controllers {
    [ApiController]
    [Route("api/vipps")]
    public class VippsApiController : ControllerBase {
        private readonly AuthorizationTokenResponse _atr;
        private readonly InitiatePaymentCommand _ipc;
        
        public VippsApiController(AuthorizationTokenResponse atr, InitiatePaymentCommand ipc) {
            _atr = atr;
            
        }
        public IActionResult GetVippsToken() {
            var client = new RestClient("https://api.vipps.no/accessToken/get");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Content-Length", "");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Cookie", "x-ms-gateway-slice=prod; stsservicecookie=ests; fpc=AiuvQ7gu0vpErBq_sxNUZGiQPThUAQAAANhx3tQOAAAA");
            request.AddHeader("Host", "api.vipps.no");
            request.AddHeader("Postman-Token", "83621bc5-2f26-4e92-9254-1859b0929e88,394776b6-cab5-4801-bd91-d83c571c2e48");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("User-Agent", "PostmanRuntime/7.15.2");
            request.AddHeader("Ocp-Apim-Subscription-Key", "e3c20a6d93f94353a0fe3c0541c1eb0e");
            request.AddHeader("client_secret", "dkJlVDhIOVdpZkFpVGs3T013aDM=");
            request.AddHeader("client_id", "451a3d2c-d526-4cbe-b59c-940be028ba7a");
            IRestResponse response = client.Execute(request);

            
            return Ok();
        }

    }
}
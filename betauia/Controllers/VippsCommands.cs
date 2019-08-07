using Microsoft.AspNetCore.Mvc;
using betauia.Models;

namespace betauia.Controllers {
    [ApiController]
    [Route("api/vipps")]
    public class VippsApiController : ControllerBase {
        private readonly AuthorizationTokenResponse _atr;
        private readonly InitiatePaymentCommand _ipc;
        
        public VippsApiController(AuthorizationTokenResponse atr, InitiatePaymentCommand ipc) {
            
        }
    }
}
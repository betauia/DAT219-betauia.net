using System.Linq;
using System.Threading.Tasks;
using betauia.Data;
using betauia.Vipps;
using Microsoft.AspNetCore.Mvc;

namespace betauia.Controllers
{
    public class VippsCallbackApiController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IVippsPayment _vippsPayment;

        public VippsCallbackApiController(ApplicationDbContext db,IVippsPayment vippsPayment)
        {
            _db = db;
            _vippsPayment = vippsPayment;
        }
        [HttpPost]
        [Route("api/vipps/order/{id}")]
        public async Task<IActionResult> VippsCallback([FromRoute] string id, VippsModel input)
        {
            await _vippsPayment.GetPaymentDetails(id);
/*            var ticket = _db.Tickets.Where(a => a.VippsOrderId == input.orderId).First();
            switch (input.transactionInfo.status)
            {
                case "RESERVED":
                    ticket.Status = input.transactionInfo.status;
                    break;
                case "CAPTURE":
                    ticket.Status = input.transactionInfo.status;
                    break;
                case "CANCEL":
                case "REFUND":
                case "VOID":
                    ticket.CancelTicket(_db);
                    break;
            }
            _db.SaveChanges();*/
            return Ok();
        }

        public class VippsModel
        {
            public int merchantSerialNumber { get; set; }
            public string orderId { get; set; }
            public TransactionInfo transactionInfo;
        }

        public class TransactionInfo
        {
            public int amount { get; set; }
            public string status { get; set; }
            public string timeStamp { get; set; }
            public string transactionId { get; set; }
        }
    }
}
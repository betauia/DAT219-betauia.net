using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using betauia.Data;
using betauia.Models;
using betauia.Ticket;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using betauia.Tokens;
using betauia.Vipps;
using Microsoft.AspNetCore.Authorization;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

// All requests tested and working

namespace betauia.Controllers
{
  [ApiController]
  [Route("/api/ticket")]
  public class TicketApiController : ControllerBase
  {
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _um;
    private readonly IVippsPayment vipps;
    private readonly ITokenVerifier _tokenVerifier;

    public TicketApiController(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
      ITokenVerifier tokenVerifier,IVippsPayment vippsPayment)

    {
      // Set the databasecontext
      _db = db;
      _um = userManager;
      vipps = vippsPayment;
      _tokenVerifier = tokenVerifier;
    }

    [Authorize("User")]
    [Route("get")]
    [HttpGet]
    public async Task<IActionResult> GetAllTicketOnUser([FromHeader] string Authorization)
    {
      var token = Authorization.Split(' ')[1];
      var userid = await _tokenVerifier.GetTokenUser(token);

      var tickets = _db.Tickets.Where(a => a.UserId == userid && a.Status != "STARTED").ToList();
      var viewtickets = new List<TicketViewModel>();
      foreach (var ticket in tickets)
      {
        var title = _db.Events.Find(ticket.EventId).Title;
        viewtickets.Add(new TicketViewModel(ticket,title));
      }
      return Ok(viewtickets);
    }

    [Route("get/{id}")]
    [Authorize("User")]
    [HttpGet]
    public async Task<IActionResult> GetTicket([FromRoute] int id,[FromHeader] string Authorization)
    {
      var token = Authorization.Split(' ')[1];
      var userid = await _tokenVerifier.GetTokenUser(token);

      var ticket = _db.Tickets.Find(id);
      if (ticket == null) return NotFound();
      if (userid != ticket.UserId) return BadRequest("Timed out");

      var ticketview = new TicketPreviewModel(ticket);
      ticketview.EventSeats = _db.EventSeats.Where(a => a.TicketId == Convert.ToString(ticket.Id)).ToList();
      return Ok(ticketview);
    }

    [Authorize("Ticket.read")]
    [Route("user/get/{id}")]
    [HttpGet]
    public async Task<IActionResult> GetUserTickets([FromHeader] string Authorization,[FromRoute] string id)
    {
      var token = Authorization.Split(' ')[1];
      var userid = await _tokenVerifier.GetTokenUser(token);

      var tickets = _db.Tickets.Where(a => a.UserId == id).ToList();
      var viewtickets = new List<TicketViewModel>();
      foreach (var ticket in tickets)
      {
        var title = _db.Events.Find(ticket.EventId).Title;
        viewtickets.Add(new TicketViewModel(ticket,title));
      }
      return Ok(viewtickets);
    }

    [HttpPost]
    [Authorize("User")]
    [Route("newticket")]
    public async Task<IActionResult> NewTicket([FromHeader] string Authorization, [FromBody] NewTicketModel ticketModel)
    {
      var token = Authorization.Split(' ')[1];
      var userid = await _tokenVerifier.GetTokenUser(token);

      var t = new TicketModel
      {
        UserId = userid,
        Amount = 100,
        Status = "STARTED",
        EventId = ticketModel.eventId,
      };

      var deleteTickets = _db.Tickets.Where(a => a.Status == "STARTED" && a.UserId == userid).ToList();
      foreach (var ticket in deleteTickets)
      {
        ticket.CancelTicket(_db);
      }

      var seatmodels = new List<EventSeat>();
      foreach (var seat in ticketModel.seats)
      {
        var seatmodel = _db.EventSeats.Find(seat);
        if (seatmodel.ReserverId != null && seatmodel.ReserverId != userid)
        {
          return BadRequest("Seat registered to another user");
        }
        seatmodels.Add(seatmodel);
      }

      _db.Tickets.Add(t);
      _db.SaveChanges();
      foreach (var seatmodel in seatmodels)
      {
        seatmodel.TicketId = Convert.ToString(t.Id);
        seatmodel.IsAvailable = true;
        seatmodel.IsReserved = true;
        seatmodel.ReserverId = userid;
      }
      Thread thread= new Thread(()=>DeleteTicket.Delete(t.Id,(long)1000*60*10));
      thread.Start();
      _db.SaveChanges();
      return Ok(t);
    }

    [Route("initiatepayment")]
    [Authorize("User")]
    [HttpPost]
    public async Task<IActionResult> InitiatePayment([FromHeader] string Authorization, InitiatePaymentModel paymentModel)
    {
      var token = Authorization.Split(' ')[1];
      var userid = await _tokenVerifier.GetTokenUser(token);
      if (userid == null)
      {
        return BadRequest("301");
      }

      var user = _um.FindByIdAsync(userid).Result;
      if (user == null)
      {
        return BadRequest("101");
      }

      var ticket = _db.Tickets.Find(paymentModel.Id);
      if (ticket == null) return NotFound();
      if (userid != ticket.UserId) return BadRequest();
      if (ticket.Status != "STARTED") return BadRequest();

      string orderid = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
      var initpayment = await vipps.InitiatePayment(paymentModel.MobileNumber, orderid, 100, "test hello world");
      if (initpayment == null)
      {
        return BadRequest();
      }

      ticket.TimePurchased = DateTime.UtcNow.ToString("MM/dd/yyyy HH:mm:ss.fff",CultureInfo.InvariantCulture);
      ticket.Status = "INITIATE";
      ticket.VippsOrderId = initpayment.orderId;
      var seats = _db.EventSeats.Where(a => a.TicketId == ticket.Id.ToString());
      foreach (var seat in seats)
      {
        seat.IsAvailable = false;
        seat.IsReserved = false;
      }

      _db.SaveChanges();
      Thread thread= new Thread(()=>DeleteTicket.Delete(ticket.Id,(long)1000*60*6));
      thread.Start();
      return Ok(initpayment.url);
    }

    [Route("paymentstatus/{id}")]
    [Authorize("User")]
    [HttpGet]
    public async Task<IActionResult> getTicketStatus([FromRoute] string id,[FromHeader] string Authorization)
    {
      var token = Authorization.Split(' ')[1];
      var userid = await _tokenVerifier.GetTokenUser(token);

      var ticket = _db.Tickets.Where(a=>a.VippsOrderId == id).ToList()[0];
      if (ticket == null) return NotFound();
      if (userid != ticket.UserId) return BadRequest();

      var t = await vipps.GetPaymentDetails(ticket.VippsOrderId);
      var lastlog = t.transactionLogHistory[0];

      var title = _db.Events.Find(ticket.EventId).Title;

      ///////////////////
      //Captured payment/
      ///////////////////
      if (ticket.Status == "CAPTURE")
      {
        var ticketview = new TicketViewModel(ticket,title);
        ticketview.Seats = _db.EventSeats.Where(a => a.TicketId == ticket.Id.ToString()).ToList();
        return Ok(ticketview);
      }

      /////////////////////
      //Cancelled Refunded/
      /////////////////////
      if (ticket.Status == "CANCEL" || ticket.Status == "REFUND")
      {
        var ticketview = new TicketViewModel(ticket,null);
        return Ok(ticketview);
      }

      ////////////////////
      //Reserve complete//
      ////////////////////
      if (lastlog.operation == "RESERVE" && lastlog.operationSuccess == true && lastlog.amount == ticket.Amount)
      {
        var capture = await vipps.CapturePayment(ticket.VippsOrderId);
        if (capture.orderId != null)
        {
          if (capture.transactionSummary.capturedAmount == capture.transactionInfo.amount && capture.transactionInfo.status == "Captured")
          {
            ticket.Status = "CAPTURE";
            ticket.TimePurchased = capture.transactionInfo.timeStamp;
            //Generate QR code//
            ticket.QR =  QRCode.GetQr("localhost:8080/admin/ticketverify/" + ticket.VippsOrderId);
            _db.SaveChanges();

            var ticketview = new TicketViewModel(ticket,title);
            ticketview.Seats = _db.EventSeats.Where(a => a.TicketId == ticket.Id.ToString()).ToList();
            return Ok(ticketview);
          }
        }
      }

      /////////////////////////
      //TRANSACTION CANCELLED//
      /////////////////////////
      if (lastlog.operationSuccess == true && (lastlog.operation == "CANCEL" || lastlog.operation == "VOID"))
      {
        ticket.CancelTicket(_db);
        ticket.Status = "CANCEL";
        _db.SaveChanges();
        return Ok(new TicketViewModel(ticket, null));
      }

      ////////////////
      //PAYMENT REFUND
      ////////////////
      if (lastlog.operationSuccess == true && lastlog.operation == "REFUND")
      {
        ticket.CancelTicket(_db);
        ticket.Status = "REFUND";
        _db.SaveChanges();
        return Ok(new TicketViewModel(ticket, null));
      }

      //Error checking payment
      return BadRequest("An error occured when processing your ticket");
    }

    [Authorize("Ticket.read")]
    [HttpGet]
    [Route("verify/{vippsid}")]
    public async Task<IActionResult> GetVerifyTicket([FromRoute] string vippsid)
    {
      var ticket = _db.Tickets.Where(a=>a.VippsOrderId == vippsid).First();
      var title = _db.Events.FindAsync(ticket.EventId).Result.Title;
      var Seats = _db.EventSeats.Where(a => a.TicketId == ticket.Id.ToString()).ToList();
      var ticketview = new TicketViewModel(ticket, title);
      ticketview.Seats = Seats;
      return Ok(ticketview);
    }

    [Authorize("Ticket.write")]
    [HttpPost]
    [Route("verify")]
    public async Task<IActionResult> VerifyTicket(Ticketid ticketid)
    {
      var ticket = _db.Tickets.Where(a => a.VippsOrderId == ticketid.Id).First();
      var title = _db.Events.FindAsync(ticket.EventId).Result.Title;
      ticket.Verified = true;
      await _db.SaveChangesAsync();
      var Seats = _db.EventSeats.Where(a => a.TicketId == ticket.Id.ToString()).ToList();
      var ticketview = new TicketViewModel(ticket, title);
      ticketview.Seats = Seats;
      return Ok(ticketview);    
    }

    [Authorize("Ticket.write")]
    [HttpDelete]
    [Route("delete/{id}")]
    public async Task<IActionResult> Cancel(string id)
    {
      var ticket = _db.Tickets.Where(a => a.VippsOrderId == id).First();
      
      var t = await vipps.GetPaymentDetails(ticket.VippsOrderId);
      var lastlog = t.transactionLogHistory[0];
      if (lastlog.operationSuccess == true && lastlog.operation == "RESERVE")
      {
        var result = await vipps.RefundPayment(ticket.VippsOrderId);
        if (result.transactionSummary.refundedAmount == ticket.Amount)
        {
          ticket.Status = "REFUND";
          await _db.SaveChangesAsync();
          return Ok();
        }
      }

      if (lastlog.operationSuccess == true && lastlog.operation == "RESERVE")
      {
        var result = await vipps.CancelPayment(ticket.VippsOrderId);
        ticket.Status = "CANCEL";
        await _db.SaveChangesAsync();
        return Ok();
      }
      return BadRequest();
    }

    public class Ticketid
    {
      public string Id { get; set; }
    }

    public class NewTicketModel
    {
      public int eventId { get; set; }
      public List<string> seats { get; set; }
    }
    public class InitiatePaymentModel
    {
      public int Id { get; set; }
      public string MobileNumber { get; set; }
    }
  }
}

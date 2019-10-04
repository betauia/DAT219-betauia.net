using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using betauia.Data;
using betauia.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using betauia.Tokens;
using betauia.Vipps;
using Microsoft.AspNetCore.Authorization;

// All requests tested and working

namespace betauia.Controllers
{
  [ApiController]
  [Route("/api/ticket")]
  public class TicketApiController : ControllerBase
  {
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _um;
    private readonly RoleManager<IdentityRole> _rm;
    private readonly TokenFactory _tf;
    private readonly VippsApiController vipps;

    public TicketApiController(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
      RoleManager<IdentityRole> roleManager)
    {
      // Set the databasecontext
      _db = db;
      _um = userManager;
      _rm = roleManager;
      _tf = new TokenFactory(_um, _rm);
      vipps = new VippsApiController();
    }

    [Route("get")]
    [HttpGet]
    public IActionResult GetAllTicketOnUser([FromHeader] string Authorization)
    {
      var token = Authorization.Split(' ')[1];
      var userid = _tf.AuthenticateUser(token);
      if (userid == null)
      {
        return BadRequest("301");
      }

      var user = _um.FindByIdAsync(userid).Result;
      if (user == null)
      {
        return BadRequest("101");
      }
      if (user.Active == false)
      {
        return BadRequest("102");
      }

      if (user.ForceLogOut)
      {
        return BadRequest("103");
      }

      var tickets = _db.Tickets.Where(a => a.UserId == userid).ToList();
      var viewtickets = new List<TicketViewModel>();
      foreach (var ticket in tickets)
      {
        var title = _db.Events.Find(ticket.EventId).Title;
        viewtickets.Add(new TicketViewModel(ticket,title));
      }
      return Ok(viewtickets);
    }

    [Route("get/{id}")]
    [HttpGet]
    public IActionResult GetTicket([FromRoute] int id,[FromHeader] string Authorization)
    {
      var token = Authorization.Split(' ')[1];
      var userid = _tf.AuthenticateUser(token);
      if (userid == null)
      {
        return BadRequest("301");
      }

      var user = _um.FindByIdAsync(userid).Result;
      if (user == null)
      {
        return BadRequest("101");
      }

      var ticket = _db.Tickets.Find(id);
      if (ticket == null) return NotFound();
      if (userid != ticket.UserId) return BadRequest();
      if (user.Active == false)
      {
        return BadRequest("102");
      }

      if (user.ForceLogOut)
      {
        return BadRequest("103");
      }

      var ticketview = new TicketPreviewModel(ticket);
      ticketview.EventSeats = _db.EventSeats.Where(a => a.TicketId == Convert.ToString(ticket.Id)).ToList();
      return Ok(ticketview);
    }

    [Authorize("Ticket.read")]
    [Route("user/get/{id}")]
    [HttpGet]
    public IActionResult GetUserTickets([FromHeader] string Authorization,[FromRoute] string id)
    {
      var token = Authorization.Split(' ')[1];
      var userid = _tf.AuthenticateUser(token);
      if (userid == null)
      {
        return BadRequest("301");
      }

      var user = _um.FindByIdAsync(userid).Result;
      if (user == null)
      {
        return BadRequest("101");
      }
      if (user.Active == false)
      {
        return BadRequest("102");
      }

      if (user.ForceLogOut)
      {
        return BadRequest("103");
      }

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
    [Route("newticket")]
    public IActionResult NewTicket([FromHeader] string Authorization, [FromBody] NewTicketModel ticketModel)
    {
      var token = Authorization.Split(' ')[1];
      var userid = _tf.AuthenticateUser(token);
      if (userid == null)
      {
        return BadRequest("301");
      }

      var user = _um.FindByIdAsync(userid).Result;
      if (user == null)
      {
        return BadRequest("101");
      }

      if (user.Active == false)
      {
        return BadRequest("102");
      }

      if (user.ForceLogOut)
      {
        return BadRequest("103");
      }

      var t = new TicketModel
      {
        UserId = userid,
        Amount = 100,
        Status = "STARTED",
        MobileNumber = user.PhoneNumber,
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

      _db.SaveChanges();
      return Ok(t);
    }

    [Route("initiatepayment")]
    [HttpPost]
    public IActionResult InitiatePayment([FromHeader] string Authorization, InitiatePaymentModel paymentModel)
    {
      var token = Authorization.Split(' ')[1];
      var userid = _tf.AuthenticateUser(token);
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

      if (user.Active == false)
      {
        return BadRequest("102");
      }

      if (user.ForceLogOut)
      {
        return BadRequest("103");
      }

      string orderid = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
      var initpayment = vipps.InitiatePayment(paymentModel.MobileNumber, orderid, 100, "test hello world");
      if (initpayment == null)
      {
        return BadRequest();
      }

      ticket.TimePurchased = DateTime.UtcNow.ToString("MM/dd/yyyy HH:mm:ss.fffZ",CultureInfo.InvariantCulture);
      ticket.Status = "INITIATE";
      ticket.VippsOrderId = initpayment.orderId;
      ticket.MobileNumber = paymentModel.MobileNumber;
      var seats = _db.EventSeats.Where(a => a.TicketId == ticket.Id.ToString());
      foreach (var seat in seats)
      {
        seat.IsAvailable = false;
      }

      _db.SaveChanges();
      return Ok(initpayment.url);
    }

    [Route("paymentstatus/{id}")]
    [HttpGet]
    public IActionResult getTicketStatus([FromRoute] string id,[FromHeader] string Authorization)
    {
      var token = Authorization.Split(' ')[1];
      var userid = _tf.AuthenticateUser(token);
      if (userid == null)
      {
        return BadRequest("301");
      }

      var user = _um.FindByIdAsync(userid).Result;
      if (user == null)
      {
        return BadRequest("101");
      }

      var ticket = _db.Tickets.Where(a=>a.VippsOrderId == id).ToList()[0];
      if (ticket == null) return NotFound();
      if (userid != ticket.UserId) return BadRequest();

      if (user.Active == false)
      {
        return BadRequest("102");
      }

      if (user.ForceLogOut)
      {
        return BadRequest("103");
      }

      var t = vipps.GetPaymentDetails(ticket.VippsOrderId);
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

      ////////////////////
      //Reserve complete//
      ////////////////////
      if (lastlog.operation == "RESERVE" && lastlog.operationSuccess == true && lastlog.amount == ticket.Amount)
      {
        var capture = vipps.CapturePayment(ticket.VippsOrderId);
        if (capture.orderId != null)
        {
          if (capture.transactionSummary.capturedAmount == capture.transactionInfo.amount)
          {
            ticket.Status = "CAPTURE";
            ticket.TimePurchased = capture.transactionInfo.timeStamp;
            //Generate QR code//
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
      if (lastlog.operationSuccess == true && lastlog.operation == "CANCEL")
      {
        ticket.CancelTicket(_db);
        _db.SaveChanges();
        return Ok(new TicketViewModel(ticket, null));
      }

      return Ok(new TicketViewModel(ticket,title));
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

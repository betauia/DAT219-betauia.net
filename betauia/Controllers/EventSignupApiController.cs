using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using betauia.Controllers;
using betauia.Data;
using betauia.Models;
using betauia.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace betauia.Controllers
{
  [Route("api/eventsignup")]
  [ApiController]
  public class EventSignupApiController : ControllerBase
  {
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _um;
    private readonly ITokenVerifier _tokenVerifier;
    public EventSignupApiController(ApplicationDbContext db,UserManager<ApplicationUser> userManager,
      RoleManager<IdentityRole> roleManager,ITokenVerifier tokenVerifier)
    {
      _db = db;
      _um = userManager;
      _tokenVerifier = tokenVerifier;
    }

    [Authorize("Event.write")]
    [HttpGet("attendee/get/{id}")]
    public IActionResult GetAllAttendees([FromRoute] string id)
    {
      var attendees = _db.EventAtendees.Where(a => a.EventId.ToString() == id).ToList();
      var attendeesView =  new List<EventAttendView>();
      foreach (var atendee in attendees)
      {
        if (atendee.Email == null)
        {
          var user = _um.FindByIdAsync(atendee.Userid).Result;
          atendee.Email = user.Email;
          atendee.Firstname = user.FirstName;
          atendee.Lastname = user.LastName;
        }

        var attview = new EventAttendView
        {
          Email = atendee.Email,
          Firstname = atendee.Firstname,
          Lastname = atendee.Lastname
        };
        attendeesView.Add(attview);
      }


      var tickets = _db.Tickets.Where(a => a.EventId.ToString() == id).ToList();
      var ticketviews = new List<SimpleTicketView>();
      foreach (var ticket in tickets)
      {
        var user = _um.FindByIdAsync(ticket.UserId).Result;
        var ticketView = new SimpleTicketView
        {
          Id = ticket.Id.ToString(),
          Email = user.Email,
          Firstname = user.FirstName,
          Lastname = user.LastName,
          Status = ticket.Status,
          Vippsid = ticket.VippsOrderId,
          Seats = _db.EventSeats.Where(a => a.TicketId == ticket.Id.ToString()).ToList()
        };
        ticketviews.Add(ticketView);
      }

      var eventattview = new EventAttTickView
      {
        AttendViews = attendeesView,
        TicketViews = ticketviews
      };
      return Ok(eventattview);
    }

    [Authorize("AccountVerified")]
    [HttpPost("user/{id}")]
    public async Task<IActionResult> SignUpEventUser(int id, [FromHeader] string Authorization)
    {
      var token = Authorization.Split(' ')[1];

      var userid = await _tokenVerifier.GetTokenUser(token);
      var user = _um.FindByIdAsync(userid).Result;

      var eventAtendeeTest = _db.EventAtendees.Where(a => a.EventId == id && a.Userid == userid).ToList();
      if (eventAtendeeTest.Count!=0)
      {
        return BadRequest("user already registered");
      }

      if (_db.EventAtendees.Where(a => a.EventId == id && a.Email == user.Email).ToList().Count != 0)
      {
        return BadRequest("Email already signed up");
      }

      var eventAtendee = new EventAtendee
      {
        EventId = id,
        Userid = userid,
        Email = user.Email,
        Firstname =  user.FirstName,
        Lastname = user.LastName,
      };
      _db.EventAtendees.Add(eventAtendee);
      _db.Events.Find(id).Atendees++;
      _db.SaveChanges();
      return Ok();
    }

    [HttpPost("email/{id}")]
    public async Task<IActionResult> SignUpEventEmail(int id,[FromBody]EventAtendee atendee)
    {
      var eventAtendeeTest = _db.EventAtendees.Where(a => a.EventId == id && a.Email == atendee.Email).ToList();
      if (eventAtendeeTest.Count != 0)
      {
        return BadRequest("Email already registered");
      }

      atendee.EventId = id;
      _db.EventAtendees.Add(atendee);
      _db.Events.Find(id).Atendees++;
      _db.SaveChanges();
      return Ok();
    }

    public class EventAttTickView
    {
      public List<EventAttendView> AttendViews { get; set; }
      public List<SimpleTicketView> TicketViews { get; set; }
    }
    public class EventAttendView
    {
      public string Email{ get; set; }
      public string Firstname{ get; set; }
      public string Lastname{ get; set; }
    }

    public class SimpleTicketView
    {
      public string Id { get; set; }
      public string Email{ get; set; }
      public string Firstname{ get; set; }
      public string Lastname{ get; set; }
      public string Status{ get; set; }
      public string Vippsid{ get; set; }
      public List<EventSeat> Seats{ get; set; }
    }
  }
}

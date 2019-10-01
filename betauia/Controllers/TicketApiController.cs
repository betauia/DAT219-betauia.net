using System;
using System.Linq;
using System.Threading.Tasks;
using betauia.Data;
using betauia.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using betauia.Tokens;
using betauia.Vipps;
// All requests tested and working

namespace betauia.Controllers
{
  [Route("api/ticket")]
  [ApiController]
  public class TicketApiController : ControllerBase
  {
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _um;
    private readonly RoleManager<IdentityRole> _rm;
    private readonly TokenFactory _tf;
    private readonly VippsApiController vipps;
    public TicketApiController(ApplicationDbContext db,UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, VippsApiController vipps)
    {
      // Set the databasecontext
      _db = db;
      _um = userManager;
      _rm = roleManager;
      _tf = new TokenFactory(_um,_rm);
      vipps = this.vipps;
    }

    [HttpGet("{id}")]
    public IActionResult GetTicket([FromHeader] string Authorization, string id)
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

      var ticketview = new TicketViewModel(ticket);
      ticketview.EventSeats = _db.EventSeats.Where(a => a.TicketId == ticket.Id).ToList();
      return Ok(ticket);
    }

    [HttpGet]
    [Route("/api/newticket")]
    public IActionResult StartTransaction([FromHeader] string Authorization)
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
        TimePurchased = DateTime.UtcNow.ToString(),
        Amount = 100,
        Status = "Started"
      };

      _db.Tickets.Add(t);
      var initpayment = vipps.InitiatePayment("90666350",t.Id.ToString(), 100, "test hello world");
      if (initpayment == null)
      {
        return BadRequest();
      }

      t.Status = "INITIATE";
      _db.SaveChanges();

      return Ok(initpayment.url);
    }
  }
}

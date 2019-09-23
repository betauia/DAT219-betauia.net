using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private readonly RoleManager<IdentityRole> _rm;
    private readonly TokenFactory _tf;

    public EventSignupApiController(ApplicationDbContext db,UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
      _db = db;
      _um = userManager;
      _rm = roleManager;
      _tf = new TokenFactory(_um,_rm);
    }

    [Authorize("AccountVerified")]
    [HttpPost("user/{id}")]
    public IActionResult SignUpEventUser(int id, [FromHeader] string Authorization)
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
    public IActionResult SignUpEventEmail(int id,[FromBody]EventAtendee atendee)
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
  }
}

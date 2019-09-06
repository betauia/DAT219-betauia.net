using System.Collections;
using System.Collections.Generic;
using betauia.Data;
using betauia.Models;
using betauia.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace betauia.Controllers
{
  [Route("api/reserveseat")]
  [ApiController]
  public class SeatReserveApiController : ControllerBase
  {
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _um;
    private readonly RoleManager<IdentityRole> _rm;
    private readonly TokenFactory _tf;

    public SeatReserveApiController(ApplicationDbContext db,UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
      _db = db;
      _um = userManager;
      _rm = roleManager;
      _tf = new TokenFactory(_um,_rm);
    }

    [HttpPost("/{token}")]
    public IActionResult ReserveSeat(string token, IEnumerable<EventSeat> eventSeats)
    {
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

      foreach (var eseat in eventSeats)
      {
        var seat = _db.EventSeats.FindAsync(eseat.Id).Result;
        seat.IsAvailable = false;
        seat.IsReserved = true;
        seat.ReserverId = userid;
        _db.EventSeats.Update(seat);
      }
      _db.SaveChanges();
      return Ok();
    }
  }
}

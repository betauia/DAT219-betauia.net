using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using betauia.Data;
using betauia.Models;
using betauia.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace betauia.Controllers
{
  [Route("api/dontusethisroutefuckoff")]
  [ApiController]
  public class SeatReserveApiController : ControllerBase
  {
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _um;
    private readonly ITokenVerifier _tokenVerifier;

    public SeatReserveApiController(ApplicationDbContext db,UserManager<ApplicationUser> userManager,
      ITokenVerifier tokenVerifier)
    {
      _db = db;
      _um = userManager;
      _tokenVerifier = tokenVerifier;
    }

    [HttpPost]
    public async Task<IActionResult> ReserveSeat([FromHeader] string Authorization, IEnumerable<EventSeat> eventSeats)
    {
      var token = Authorization.Split(' ')[1];

      var userid = await _tokenVerifier.GetTokenUser(token);

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

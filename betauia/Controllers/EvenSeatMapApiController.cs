using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using betauia.Data;
using betauia.Models;
using betauia.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace betauia.Controllers
{
    [Route("api/eventseatmap")]
    [ApiController]
    public class EvenSeatMapApiController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _um;
        private readonly RoleManager<IdentityRole> _rm;
        private readonly ITokenVerifier _tokenVerifier;
        public EvenSeatMapApiController(ApplicationDbContext db,UserManager<ApplicationUser> userManager,
          RoleManager<IdentityRole> roleManager,ITokenVerifier tokenVerifier)
        {
          _db = db;
          _um = userManager;
          _rm = roleManager;
          _tokenVerifier = tokenVerifier;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSeatmap([FromHeader] string Authorization, string id)
        {
          var token = Authorization.Split(' ')[1];
          var userid = await _tokenVerifier.GetTokenUser(token);

          var seatmap = _db.EventSeatMaps.Find(id);
          EventSeatm ret = new EventSeatm();
          ret.seatMapModel = seatmap;

          var tseats = _db.EventSeats.Where(r => r.OwnerId == seatmap.Id).ToList();
          foreach (var seat in tseats)
          {
            if (userid == seat.ReserverId && seat.IsReserved)
            {
              seat.IsAvailable = true;
              seat.IsReserved = true;
            }else if (seat.ReserverId != null)
            {
              seat.IsAvailable = false;
              seat.IsReserved = false;
            }
            else
            {
              seat.IsAvailable = true;
              seat.IsReserved = false;
            }
          }

          ret.Seats = tseats;


          if (seatmap == null)
              return NotFound("Failed to find seatMap.");

          return Ok(ret);
        }

        public class EventSeatm
        {
            public EventSeatMap seatMapModel { get; set; }
            public List<EventSeat> Seats { get; set; }
        }
    }
}

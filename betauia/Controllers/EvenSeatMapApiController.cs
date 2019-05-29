using System.Collections.Generic;
using System.Linq;
using betauia.Data;
using betauia.Models;
using Microsoft.AspNetCore.Mvc;

namespace betauia.Controllers
{
    [Route("api/eventseatmap")]
    [ApiController]
    public class EvenSeatMapApiController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public EvenSeatMapApiController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("{id}")]
        public IActionResult GetSeatmap(string id)
        {
            var seatmap = _db.EventSeatMaps.Find(id);
            EventSeatm ret = new EventSeatm();
            ret.seatMapModel = seatmap;

            var tseats = _db.EventSeats.Where(r => r.OwnerId == seatmap.Id).ToList();
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
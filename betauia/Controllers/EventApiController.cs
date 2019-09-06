using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using betauia.Data;
using betauia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace betauia.Controllers
{
    [Route("api/event")]
    [ApiController]
    public class EventApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventApiController(ApplicationDbContext context)
                 {
            // Set the dbcontext
            _context = context;
        }

        // Tested and working
        // GET: Get all events
        [HttpGet]
        public IActionResult GetAll()
        {
            // Return with 200 OK status code
            var events = _context.Events.ToList();
            foreach (var eventModel in events)
            {
              var seatmap = _context.EventSeatMaps.Find(eventModel.SeatMapId);
              if (seatmap != null) eventModel.SeatMap = seatmap;
            }
            return Ok(events);
        }

        // Tested and working
        // GET: Get event by id
        [HttpGet("{id}")]
        public IActionResult GetEventModel(int id)
        {
            // Get user by id
            var eventModel = _context.Events.Find(id);

            // Check if user is valid
            if (eventModel == null)
                return NotFound();

            // Return user
            return Ok(eventModel);
        }

        // Tested and working
        // PUT: Update event by id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventModel(int id, EventModel eventModel)
        {
            // Check if id matches user id
            if (id != eventModel.Id) return BadRequest();


            // Set the current state to say that some or all of its properties has been modified
            _context.Entry(eventModel).State = EntityState.Modified;

            try
            {
                // Save changes
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Check if the event was created
                if (!EventModelExists(id)) return NotFound();
                else throw;
            }
            return Ok(eventModel);
        }

        // Tested and working
        // POST: Add new event
        [HttpPost]
        public IActionResult Post([FromBody] EventModel eventModel)
        {
            // Return if id is set to avoid overwriting an existing event
            if (eventModel.Id != 0) return BadRequest();

            var tseatmapid = eventModel.SeatMapId;
            eventModel.SeatMapId = null;

            _context.Add(eventModel);
            _context.SaveChanges();

            if (tseatmapid != null)
            {
                var tSeatmap = _context.SeatMaps.Find(tseatmapid);
                var seatmap = CreateSeatMap(eventModel, tSeatmap);

                var tseats = _context.Seats.Where(r => r.OwnerId == tSeatmap.Id).ToList();
                var seats = CreateSeats(seatmap, tseats);
                _context.EventSeatMaps.Add(seatmap);
                _context.EventSeats.AddRange(seats);
                eventModel.SeatMapId = seatmap.Id;
                eventModel.SeatMap = seatmap;
            }

            // Add and save
            _context.Update(eventModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetEventModel), new {id = eventModel.Id}, eventModel);
        }

        // Tested and working
        // DELETE: Delete event by id
        [HttpDelete("{id}")]
        public IActionResult DeleteEventModel(int id)
        {
            // Receive and check if event is valid
            var eventModel = _context.Events.Find(id);
            if (eventModel == null) return NotFound();

            // Remove and update
            _context.Events.Remove(eventModel);
            _context.SaveChanges();

            return Ok(eventModel);
        }

        // Function to check if an event by id exists
        private bool EventModelExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }

        private EventSeatMap CreateSeatMap(EventModel eventModel, SeatMapModel seatMapModel)
        {
            var seatmap = new EventSeatMap
            {
                NumSeats = seatMapModel.NumSeats,
                NumSeatsAvailable = seatMapModel.NumSeats,
                EventId = eventModel.Id,
                Id = eventModel.Id + seatMapModel.Id,
            };
            return seatmap;
        }

        private List<EventSeat> CreateSeats(EventSeatMap seatMap, List<SeatModel> tseats)
        {
            List<EventSeat> seats = new List<EventSeat>();
            foreach (var tseat in tseats)
            {
                var seat = new EventSeat
                {
                    Number = tseat.Number,
                    x = tseat.x,
                    y = tseat.y,
                    OwnerId = seatMap.Id,
                    Id = seatMap.Id + tseat.Number,
                };
                seats.Add(seat);
            }
            return seats;
        }
    }
}

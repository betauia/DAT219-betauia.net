using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using betauia.Data;
using betauia.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// All requests tested and working //

namespace betauia.Controllers
{
    [Route("api/seatmap")]
    [ApiController]
    public class SeatMapApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SeatMapApiController(ApplicationDbContext context)
        {
            // Set the databasecontext
            _context = context;
        }

        // GET: Get all SeatMaps
        [Authorize("Seatmap.write")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Return with 200 OK status code
            return Ok(_context.SeatMaps.ToList());
        }

        // GET: Get SeatMap by id
        [Authorize("Seatmap.read")]
        [HttpGet("{id}")]
        public async Task<ActionResult<SeatMap>> GetSeatMap(string id)
        {
            // Get SeatMap by id
            var seatMap = await _context.SeatMaps.FindAsync(id);
            SeatMap ret = new SeatMap();
            ret.seatMapModel = seatMap;

            List<SeatModel> seats = _context.Seats.Where(a=>a.OwnerId==seatMap.Id).ToList();
            ret.Seats = seats;

            // Check if SeatMap is valid
            if (seatMap == null)
                return NotFound("Failed to find seatMap.");

            //seatMap.NumSeatsAvailable = _context.Seats.Where(e => e.OwnerId == seatMap.Id).Where(e => e.IsAvailable).ToList().Count;

            // Stop the entity from being tracked by context
            _context.Entry(_context.SeatMaps.Find(id)).State = EntityState.Detached;

            // Set the current state to say that some or all of its properties has been modified
            _context.Entry(seatMap).State = EntityState.Modified;

            try
            {
                // Save changes
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeatMapExists(id)) return NotFound();
                else throw;
            }

            // Return seatmap
            return ret;
        }

        // PUT: Update SeatMap by id
        [Authorize("Seatmap.read")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeatMap(string id, SeatMapModel seatMap)
        {
            // Check if id matches SeatMap id
            if (id != seatMap.Id) return BadRequest();

            //seatMap.NumSeatsAvailable = (seatMap.NumSeats - _context.Seats.Count(e => e.IsAvailable == false));

            // Stop the entity from being tracked by context
            _context.Entry(_context.SeatMaps.Find(id)).State = EntityState.Detached;

            // Set the current state to say that some or all of its properties has been modified
            _context.Entry(seatMap).State = EntityState.Modified;

            try
            {
                // Save changes
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeatMapExists(id)) return NotFound();
                else throw;
            }

            return Ok(seatMap);
        }

        // POST: Add new SeatMap
        [Authorize("Seatmap.write")]
        [HttpPost]
        public async Task<IActionResult> Post(SeatMap seatMap)
        {
            var seatMapModel = seatMap.seatMapModel;
            // Return if id is set to avoid overwriting an existing SeatMap
            if (seatMapModel.Id == null) return BadRequest("No id in seatMap.");
            seatMapModel.NumSeats = seatMap.Seats.Count;

            foreach (var seat in seatMap.Seats)
            {
                seat.Owner = seatMapModel;
                seat.OwnerId = seatMap.seatMapModel.Id;
                seat.Id = seat.OwnerId + seat.Number;
                _context.Add(seat);
            }

            // Add and save
            _context.Add(seatMapModel);
            _context.SaveChanges();

            return Created("Created!", seatMap);
        }

        // DELETE: Delete SeatMap by id
        [Authorize("Seatmap.write")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<SeatMapModel>> DeleteSeatMap(string id)
        {
            // Receive and check if SeatMap is valid
            var seatMap = await _context.SeatMaps.FindAsync(id);
            if (seatMap == null) return NotFound("Failed to find seatMap.");

            // Remove and update
            _context.Seats.RemoveRange(_context.Seats.Where(e => e.OwnerId == id));
            _context.SeatMaps.Remove(seatMap);

            await _context.SaveChangesAsync();

            return seatMap;
        }

        // Function to check if a SeatMap by id exists
        private bool SeatMapExists(string id)
        {
            return _context.SeatMaps.Any(e => e.Id == id);
        }

        public class SeatMap
        {
            public SeatMapModel seatMapModel { get; set; }
            public List<SeatModel> Seats { get; set; }
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using betauia.Data;
using betauia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// All requests tested and working // 

/*
 * To use:
 * Use put to update amount of seats etc
 * Use put on SeatApi to change IsAvailable (bool) variable
 * Seats will be created/deleted automatically upon changing NumSeats
 */
namespace betauia.Controllers
{
    [Route("api/SeatMapApi")]
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
        [HttpGet]
        public IActionResult GetAll()
        {
            // Return with 200 OK status code
            return Ok(_context.SeatMaps.ToList());
        }

        // GET: Get SeatMap by id
        [HttpGet("{id}")]
        public async Task<ActionResult<SeatMapModel>> GetSeatMap(int id)
        {
            // Get SeatMap by id
            var seatMap = await _context.SeatMaps.FindAsync(id);
            
            // Check if SeatMap is valid
            if (seatMap == null)
                return NotFound();

            seatMap.NumSeatsAvailable =
                _context.Seats.Where(e => e.OwnerId == seatMap.Id).Where(e => e.IsAvailable == true).ToList().Count;
            
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
                // Check if the SeatMap was created
                if (!ApplicationUserExists(id)) return NotFound();
                else throw;
            }
            
            // Return user
            return seatMap;
        }
        
        // PUT: Update SeatMap by id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeatMap(int id, SeatMapModel seatMap)
        {
            // Check if id matches SeatMap id
            if (id != seatMap.Id) return BadRequest();
            if (seatMap.NumSeats <= 0) return BadRequest("Too few seats chosen.");

            var oldNumSeats = _context.SeatMaps.Find(id).NumSeats;

            if (oldNumSeats < seatMap.NumSeats)
            {
                for (var i = oldNumSeats + 1; i <= seatMap.NumSeats; i++)
                    _context.Add(new SeatModel(){Owner = seatMap});
            }
            else if (oldNumSeats > seatMap.NumSeats)
            {
                if (!RemoveSeats(oldNumSeats - seatMap.NumSeats, seatMap))
                    return BadRequest("Not enough available seats to remove.");
            }
            
            seatMap.NumSeatsAvailable = (seatMap.NumSeats - _context.Seats.Count(e => e.IsAvailable == false));

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
                // Check if the SeatMap was created
                if (!ApplicationUserExists(id)) return NotFound();
                else throw;
            }
            
            return Ok(seatMap);
        }
        
        // POST: Add new SeatMap
        [HttpPost]
        public IActionResult Post(SeatMapModel seatMap)
        {
            // Return if id is set to avoid overwriting an existing SeatMap
            if (seatMap.Id != 0) return BadRequest();

            for (var i = 1; i <= seatMap.NumSeats; i++)
                _context.Add(new SeatModel(){Owner = seatMap});

            // Add and save
            _context.Add(seatMap);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetSeatMap), new {id = seatMap.Id}, seatMap);
        }
        
        // DELETE: Delete SeatMap by id
        [HttpDelete("{id}")]
        public async Task<ActionResult<SeatMapModel>> DeleteSeatMap(int id)
        {
            // Receive and check if SeatMap is valid
            var seatMap = await _context.SeatMaps.FindAsync(id);
            if (seatMap == null) return NotFound();
            
            _context.Seats.RemoveRange(_context.Seats.Where(e => e.OwnerId == id));

            // Remove and update
            _context.SeatMaps.Remove(seatMap);
            await _context.SaveChangesAsync();

            return seatMap;
        }

        // Function to check if a SeatMap by id exists
        private bool ApplicationUserExists(int id)
        {
            return _context.SeatMaps.Any(e => e.Id == id);
        }

        private bool RemoveSeats(int amount, SeatMapModel owner)
        {
            var seats = _context.Seats.Where(e => e.OwnerId == owner.Id).Where(e => e.IsAvailable == true).ToList();
            if (seats.Count < amount) return false;
            
            for (var i = 0; i < amount; i++)
            {
                _context.Seats.Remove(seats[i]);
            }

            return true;
        }
    }
}
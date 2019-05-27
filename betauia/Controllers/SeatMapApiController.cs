using System.Linq;
using System.Threading.Tasks;
using betauia.Data;
using betauia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// All requests tested and working // 

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
        public async Task<ActionResult<SeatMapModel>> GetSeatMap(string id)
        {
            // Get SeatMap by id
            var seatMap = await _context.SeatMaps.FindAsync(id);
            
            // Check if SeatMap is valid
            if (seatMap == null)
                return NotFound("Failed to find seatMap.");

            seatMap.NumSeatsAvailable =
                _context.Seats.Where(e => e.OwnerId == seatMap.Id).Where(e => e.IsAvailable).ToList().Count;
            
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
            return seatMap;
        }
        
        // PUT: Update SeatMap by id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeatMap(string id, SeatMapModel seatMap)
        {
            // Check if id matches SeatMap id
            if (id != seatMap.Id) return BadRequest();

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
                if (!SeatMapExists(id)) return NotFound();
                else throw;
            }
            
            return Ok(seatMap);
        }
        
        // POST: Add new SeatMap
        [HttpPost]
        public IActionResult Post(SeatMapModel seatMap)
        {
            // Return if id is set to avoid overwriting an existing SeatMap
            if (seatMap.Id == null) return BadRequest("No id in seatMap.");

            // Add and save
            _context.Add(seatMap);
            _context.SaveChanges();

            return Created("Created!", seatMap);
        }
        
        // DELETE: Delete SeatMap by id
        [HttpDelete("{id}")]
        public async Task<ActionResult<SeatMapModel>> DeleteSeatMap(string id)
        {
            // Receive and check if SeatMap is valid
            var seatMap = await _context.SeatMaps.FindAsync(id);
            if (seatMap == null) return NotFound("Failed to find seatMap.");
            
            _context.Seats.RemoveRange(_context.Seats.Where(e => e.OwnerId == id));

            // Remove and update
            _context.SeatMaps.Remove(seatMap);
            await _context.SaveChangesAsync();

            return seatMap;
        }

        // Function to check if a SeatMap by id exists
        private bool SeatMapExists(string id)
        {
            return _context.SeatMaps.Any(e => e.Id == id);
        }
    }
}
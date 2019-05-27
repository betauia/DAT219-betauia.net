using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using betauia.Data;
using betauia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// All current requests are tested and working //

namespace betauia.Controllers
{
    [Route("api/SeatApi")]
    [ApiController]
    public class SeatApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SeatApiController(ApplicationDbContext context)
        {
            // Set the databasecontext
            _context = context;
        }

        
        // GET: Get all Seats
        [HttpGet]
        public IActionResult GetAll()
        {
            // Return with 200 OK status code
            return Ok(_context.Seats.ToList());
        }

        // GET: Returns a list with the given owner id
        [HttpGet("{id}")]
        public IActionResult GetAllById(string id)
        {
            return Ok(_context.Seats.Where(e => e.OwnerId == id));
        }
        
        // PUT: Update SeatModel by id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeatMap(int id, SeatModel seatModel)
        {
            // Check if id matches SeatModel id
            if (id != seatModel.Id) return BadRequest();

            // Stop the entity from being tracked by context
            _context.Entry(_context.Seats.Find(id)).State = EntityState.Detached;
            
            // Set the current state to say that some or all of its properties has been modified
            _context.Entry(seatModel).State = EntityState.Modified;
            
            try
            {
                // Save changes
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Check if the SeatMap was created
                if (!SeatModelExists(id)) return NotFound();
                else throw;
            }

            return Ok(seatModel);
        }
        
        // Receive a list of seats (JSON) and adding them all
        [HttpPost]
        public IActionResult Post(List<SeatModel> seats) {

            foreach (var seat in seats)
            {
                _context.Seats.Add(seat);
            }

            try
            {
                // Add and save
                _context.SaveChanges();
            }
            catch (Exception)
            {
                // Possible errors: Incorrect ownerId or ID already taken
                return BadRequest("Failed to save changes. Check if ownerId is correct or if any ID is taken.");
            }
            
            return Created("Created", seats);
        }

        // Delete by one single id
        [HttpDelete("{id}")]
        public async Task<ActionResult<SeatModel>> Delete(int id)
        {
            var seat = await _context.Seats.FindAsync(id);
            if (seat == null) return NotFound();

            _context.Seats.Remove(seat);
            await _context.SaveChangesAsync();

            return seat;
        }

        // Receive a list of ids to delete
        [HttpDelete]
        public async Task<IActionResult> Delete(List<int> seats)
        {
            if (seats == null) return BadRequest("No id received.");
            foreach (var id in seats)
            {
                var seat = await _context.Seats.FindAsync(id);
                _context.Seats.Remove(seat);
            }

            await _context.SaveChangesAsync();

            return Ok("Seats removed.");
        }
        
        // Function to check if a SeatMap by id exists
        private bool SeatModelExists(int id)
        {
            return _context.Seats.Any(e => e.Id == id);
        }
    }
}
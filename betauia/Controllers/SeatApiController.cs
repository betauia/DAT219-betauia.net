using System.Collections;
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
        
        
        [HttpPost]
        public IActionResult Post(SeatList seatListModel) {

            foreach (var seat in seatListModel.seats)
            {
                _context.Add(seat);
            }
            // Add and save
            _context.SaveChanges();
            
            return Created("Created", seatListModel);

        }
        
        // Function to check if a SeatMap by id exists
        private bool SeatModelExists(int id)
        {
            return _context.Seats.Any(e => e.Id == id);
        }
        
    }

    public class SeatList
    {
        public List<SeatModel> seats { get; set; }
    }
    
}
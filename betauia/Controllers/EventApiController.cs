using System.Linq;
using System.Threading.Tasks;
using betauia.Data;
using betauia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace betauia.Controllers
{
    [Route("api/EventApi")]
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
            return Ok(_context.Events.ToList());
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
        public IActionResult Post(EventModel eventModel)
        {
            // Return if id is set to avoid overwriting an existing event
            if (eventModel.Id != 0) return BadRequest();
            
            // Add and save
            _context.Add(eventModel);
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
    }
}
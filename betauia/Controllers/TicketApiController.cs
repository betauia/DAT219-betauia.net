using System.Linq;
using System.Threading.Tasks;
using betauia.Data;
using betauia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace betauia.Controllers
{
    [Route("api/TicketApi")]
    [ApiController]
    public class TicketApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TicketApiController(ApplicationDbContext context)
        {
            // Set the databasecontext
            _context = context;
        }

        // GET: Get all tickets
        [HttpGet]
        public IActionResult GetAll()
        {
            // Return with 200 OK status code
            return Ok(_context.Tickets.ToList());
        }

        // GET: Get TicketModel by id
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketModel>> GetSeatMap(int id)
        {
            // Get ticket by id
            var ticket = await _context.Tickets.FindAsync(id);
            
            // Check if ticket is valid
            if (ticket == null)
                return NotFound("Failed to find ticket.");
            
            // Return ticket
            return ticket;
        }
        
        // PUT: Update ticket by id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(int id, TicketModel ticketModel)
        {
            // Check if id matches ticket id
            if (id != ticketModel.Id) return BadRequest();

            // Stop the entity from being tracked by context
            _context.Entry(_context.Tickets.Find(id)).State = EntityState.Detached;
            
            // Set the current state to say that some or all of its properties has been modified
            _context.Entry(ticketModel).State = EntityState.Modified;
            
            try
            {
                // Save changes
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id)) return NotFound();
                else throw;
            }
            
            return Ok(ticketModel);
        }
        
        // POST: Add new ticket
        [HttpPost]
        public IActionResult Post(TicketModel ticketModel)
        {
            // Return if id is set to avoid overwriting an existing ticket
            if (ticketModel.Id != 0) return BadRequest("No id in ticket.");

            // Add and save
            _context.Add(ticketModel);
            _context.SaveChanges();

            return Created("Created!", ticketModel);
        }
        
        // DELETE: Delete ticket by id
        [HttpDelete("{id}")]
        public async Task<ActionResult<TicketModel>> DeleteTicket(int id)
        {
            // Receive and check if SeatMap is valid
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null) return NotFound("Failed to find ticket.");
            
            // Remove and update
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return ticket;
        }

        // Function to check if a ticket by id exists
        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
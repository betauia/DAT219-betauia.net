    using System.Linq;
using System.Threading.Tasks;
using betauia.Data;
using betauia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

/* Author: Chris */
// All requests are tested and working //
namespace betauia.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserApiController(ApplicationDbContext context)
        {
            // Set the databasecontext
            _context = context;
        }

        // GET: Get all users
        [HttpGet]
        public IActionResult GetAll()
        {
            // Return with 200 OK status code
            return Ok(_context.Users.ToList());
        }

        // GET: Get user by id
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUser>> GetApplicationUser(string id)
        {
            // Get user by id
            var applicationUser = await _context.Users.FindAsync(id);
            
            // Check if user is valid
            if (applicationUser == null)
                return NotFound();

            // Return user
            return applicationUser;
        }
        
        // PUT: Update user by id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicationUser(string id, ApplicationUser applicationUser)
        {
            // Check if id matches user id
            if (id != applicationUser.Id) return BadRequest();

           
            // Set the current state to say that some or all of its properties has been modified
            _context.Entry(applicationUser).State = EntityState.Modified;
            
            try
            {
                // Save changes
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Check if the user was created
                if (!ApplicationUserExists(id)) return NotFound();
                else throw;
            }
            
            return Ok(applicationUser);
        }
        
        // POST: Add new user
        [HttpPost]
        public IActionResult Post(ApplicationUser applicationUser)
        {
            // Return if id is set to avoid overwriting an existing user
            if (applicationUser.Id != null) return BadRequest();

            // Add and save
            _context.Add(applicationUser);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetApplicationUser), new {id = applicationUser.Id}, applicationUser);
        }
        
        // DELETE: Delete user by id
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApplicationUser>> DeleteApplicationUser(string id)
        {
            // Receive and check if user is valid
            var applicationUser = await _context.Users.FindAsync(id);
            if (applicationUser == null) return NotFound();

            // Remove and update
            _context.Users.Remove(applicationUser);
            await _context.SaveChangesAsync();

            return applicationUser;
        }

        // Function to check if a user by id exists
        private bool ApplicationUserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
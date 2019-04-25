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
    [Route("api/PageApi")]
    [ApiController]
    public class PageApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PageApiController(ApplicationDbContext context)
        {
            // Set the dbcontext
            _context = context;
        }
        
        // GET: Get all pages
        [HttpGet]
        public IActionResult GetAll()
        {
            // Return with 200 OK status code
            return Ok(_context.Pages.ToList());
        }

        // GET: Get user by id
        [HttpGet("{id}")]
        public IActionResult GetApplicationUser(int id)
        {
            // Get user by id
            var pageModel = _context.Pages.Find(id);
            
            // Check if user is valid
            if (pageModel == null)
                return NotFound();

            // Return user
            return Ok(pageModel);
        }
        
        // PUT: Update user by id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicationUser(int id, PageModel pageModel)
        {
            // Check if id matches user id
            if (id != pageModel.Id) return BadRequest();
            pageModel.UpdateEditTime();
            
            var page = _context.Pages.Find(id);
            pageModel.CreationTime = page.CreationTime;
            _context.Entry(page).State = EntityState.Detached;
            
            // Set the current state to say that some or all of its properties has been modified
            _context.Entry(pageModel).State = EntityState.Modified;
            
            try
            {
                // Save changes
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Check if the user was created
                if (!PageModelExists(id)) return NotFound();
                else throw;
            }
            return Ok(pageModel);
        }
 
        // POST: Add new user
        [HttpPost]
        public IActionResult Post(PageModel pageModel)
        {
            // Return if id is set to avoid overwriting an existing user
            if (pageModel.Id != 0) return BadRequest();
            
            // Add and save
            _context.Add(pageModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetApplicationUser), new {id = pageModel.Id}, pageModel);
        }
        
        // DELETE: Delete user by id
        [HttpDelete("{id}")]
        public IActionResult DeleteApplicationUser(int id)
        {
            // Receive and check if user is valid
            var pageModel = _context.Pages.Find(id);
            if (pageModel == null) return NotFound();

            // Remove and update
            _context.Pages.Remove(pageModel);
            _context.SaveChanges();

            return Ok(pageModel);
        }

        // Function to check if a user by id exists
        private bool PageModelExists(int id)
        {
            return _context.Pages.Any(e => e.Id == id);
        }
    }
}
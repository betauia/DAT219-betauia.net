using System.Diagnostics;
using System.Linq;
using betauia.Data;
using betauia.Models;
using Microsoft.AspNetCore.Mvc;

namespace betauia.Controllers
{
    [Route("api/Blog")]
    [ApiController]
    public class BlogApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BlogApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("id")]
        public IActionResult Get(int id)
        {
            Debug.Write("id found");
            var peer = _context.Posts.Find(id);
            if (peer == null)
                return NotFound();
            return Ok(peer);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            // Return with 200 OK status code
            return Ok(_context.Posts.ToList());
        }
        
        [HttpPost]
        public IActionResult Post(BlogPost blogPost)
        {
            if (blogPost.Id != 0) 
                return BadRequest();
            
            _context.Add(blogPost);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new {id = blogPost.Id}, blogPost);
        }

        [HttpPut("{id}")]
        public IActionResult Put(BlogPost blogPost)
        {
            if (!_context.Posts.Any(p => p.Id == blogPost.Id))
                return NotFound();

            _context.Update(blogPost);
            _context.SaveChanges();
            return Ok(blogPost);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var post = _context.Posts.Find(id);
            if (post == null)
                return NotFound();

            _context.Remove(post);
            _context.SaveChanges();
            return Ok(post);
        }
    }
}
using System.Linq;
using betauia.Data;
using betauia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace betauia.Controllers
{
    [Route("api/blog")]
    [ApiController]
    public class BlogApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BlogApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            // Return with 200 OK status code
            return Ok(_context.Posts.ToList());
        }
        
        [HttpGet("id")]
        public IActionResult GetBlogPost(int id)
        {
            var blogPostModel = _context.Posts.Find(id);
            
            if (blogPostModel == null)
                return NotFound();
            
            return Ok(blogPostModel);
        }
        
        [HttpPost]
        public IActionResult Post(BlogPost blogPost)
        {
            if (blogPost.Id != 0) 
                return BadRequest();
            
            _context.Add(blogPost);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetBlogPost), new {id = blogPost.Id}, blogPost);
        }

        [HttpPut("{id}")]
        public IActionResult Put(BlogPost blogPost)
        {
            if (!_context.Posts.Any(p => p.Id == blogPost.Id))
                return NotFound();
            
            blogPost.UpdateEditTime();

            var post = _context.Posts.Find(blogPost.Id);
            blogPost.CreationDate = post.CreationDate;
            _context.Entry(post).State = EntityState.Detached;

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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using betauia.Data;
using betauia.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace betauia.Controllers
{
    [Route("api/blog")]
    public class BlogApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BlogApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Return with 200 OK status code
            var blogviews = new List<BlogViewModel>();
            var blogs = _context.Posts.ToList();
            foreach (var blog in blogs)
            {
              blogviews.Add(new BlogViewModel(blog));
            }

            return Ok(blogviews);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogPost(int id)
        {
            var blogPostModel = _context.Posts.Find(id);

            if (blogPostModel == null)
                return NotFound();

            return Ok(new BlogViewModel(blogPostModel));
        }

        //[Authorize(Roles = "Admin")]
        [Authorize(Policy = "Blog.write")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]BlogPost blogPost)
        {
            if (blogPost.Id != 0)    
                return BadRequest();

            _context.Add(blogPost);
            _context.SaveChanges();
            return Ok(blogPost.Id);
        }

        [Authorize(Policy = "Blog.write")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody]BlogPost blogPost)
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

        [Authorize(Policy = "Blog.write")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var post = _context.Posts.Find(id);
            if (post == null)
                return NotFound();

            post.Title = null;
            post.Content = null;
            post.Summary = null;
            post.CreationDate = DateTime.MinValue;
            post.LastEditDate = DateTime.MinValue;
            _context.Update(post);
            _context.SaveChanges();
            return Ok(post);
        }
    }
}

using FinalProject1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public BlogController(AppDbContext appDbContext) 
        {
            _appDbContext= appDbContext;
        }
        [HttpGet("/blogs")]
        public IActionResult GetAllBlogs()
        {
            return Ok(_appDbContext.BlogPost.ToList());
        }
        [HttpGet ("/blogs/{id}")]
        public IActionResult GetBlogsByUserId(int userId)
        {
            var BlogPosts = _appDbContext.BlogPost.Where(x => x.UserId == userId);
            if (BlogPosts == null)
            {
                return NotFound();
            }
            return Ok(BlogPosts);
        }
        [HttpPost("/blogs")]
        public IActionResult CreateBlogPost([FromBody]BlogPost blogPost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _appDbContext.BlogPost.Add(blogPost);
            var blogPostId = _appDbContext.SaveChanges();
            blogPost.BlogId= blogPostId;
            blogPost.CreatedDate = DateTime.Now;
            blogPost.isActive = true;
            return Ok(blogPost);
        }
        [HttpDelete("/blogs/{id}")]
        public IActionResult DeletBlogPost(int blogPostId)
        {
            var blogPost = _appDbContext.BlogPost.Where(x=> x.BlogId == blogPostId).FirstOrDefault();
            if (blogPost == null)
            {
                return NotFound();
            }
            blogPost.IsActive = false;
            blogPost.UpdatedDate = DateTime.Now;
            return NoContent();
        }
        [HttpPut("/blogs/{id}")]
        public IActionResult UpdateBlogPost([FromBody] BlogPost newBlogPost, int blogPostId)
        {
            var blogPost = _appDbContext.BlogPost.Where(x => x.BlogId == blogPostId).FIrstOrDefault();
            if (blogPost == null)
            {
                return NotFound();
            }
            blogPost.Title = newBlogPost.Title;
            blogPost.Content = newBlogPost.Content;
            blogPost.UpdatedDat= DateTime.Now;
            return Ok(blogPost);
        }
    }
}

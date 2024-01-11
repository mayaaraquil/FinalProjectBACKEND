
using FinalProject1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GetAllBlogs()
        {
            return Ok(await _appDbContext.BlogPost.ToListAsync());
        }
        [HttpGet ("/blogs/user/{id}")]
        public async Task<IActionResult> GetBlogsByUserId(int userId)
        {
            var BlogPosts = await _appDbContext.BlogPost.FirstOrDefaultAsync(x => x.UserId == userId);
            if (BlogPosts == null)
            {
                return NotFound();
            }
            return Ok(BlogPosts);
        }
        [HttpGet("/blogs/{id}")]
        [HttpPost("/blogs")]
        public async Task<IActionResult> CreateBlogPost([FromBody]BlogPost blogPost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _appDbContext.BlogPost.Add(blogPost);
            await _appDbContext.SaveChangesAsync();
            return Ok(); 

        }
        [HttpDelete("/blogs/{id}")]
        public async Task<IActionResult> DeletBlogPost(int blogPostId)
        {
            var blogPost =await _appDbContext.BlogPost.FirstOrDefaultAsync(x => x.BlogId == blogPostId);
            if (blogPost == null)
            {
                return NotFound();
            }
            _appDbContext.BlogPost.Remove(blogPost);
            await _appDbContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("/blogs/{id}")]
        public async Task<IActionResult> UpdateBlogPost([FromBody] BlogPost newBlogPost, int blogPostId)
        {
            var blogPost = await _appDbContext.BlogPost.FirstOrDefaultAsync(x => x.BlogId == blogPostId);
            if (blogPost == null)
            {
                return NotFound();
            }
            blogPost.Title = newBlogPost.Title;
            blogPost.Content = newBlogPost.Content;
            blogPost.UpdatedDate= DateTime.Now;
            await _appDbContext.SaveChangesAsync();
            return Ok(blogPost);
        }
    }
}


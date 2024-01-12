
using FinalProject1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : BaseController
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserController _userController;
        public BlogController(AppDbContext appDbContext, UserController userController) 
        {
            _appDbContext= appDbContext;
            _userController= userController;
        }
        [HttpGet("/blogs")]
        public async Task<IActionResult> GetAllBlogs()
        {
            return Ok(await _appDbContext.BlogPost.ToListAsync());
        }
        [HttpGet ("/blogs/user/{id}")]
        public async Task<IActionResult> GetBlogsByUserId(string authZeroUserId)
        {
            var BlogPosts = await _appDbContext.BlogPost.FirstOrDefaultAsync(x => x.AuthZeroUserId == authZeroUserId);
            if (BlogPosts == null)
            {
                return NotFound();
            }
            return Ok(BlogPosts);
        }
        
        [HttpPost("/blogs")]
        public async Task<IActionResult> CreateBlogPost([FromBody]BlogPostCreationDto blogPostDto)
        {
            try
            {
                
                var newBlogPost = new BlogPost
                {
                    Title = blogPostDto.Title,
                    Content = blogPostDto.Content,
                    AuthZeroUserId = GetUserId()
                };
                return Ok("Blog post created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to create blog post: {ex.Message}");
            }
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


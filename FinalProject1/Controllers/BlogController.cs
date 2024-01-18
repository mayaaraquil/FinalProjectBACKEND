
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
        public BlogController(AppDbContext appDbContext) 
        {
            _appDbContext= appDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBlogs()
        {
            return Ok(await _appDbContext.BlogPost.ToListAsync());
        }
        [HttpGet ("{id}")]
        public async Task<IActionResult> GetBlogsByUserId(string authZeroUserId)
        {
            var BlogPosts = await _appDbContext.BlogPost.FirstOrDefaultAsync(x => x.UserId == authZeroUserId);
            if (BlogPosts == null)
            {
                return NotFound();
            }
            return Ok(BlogPosts);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody]BlogPost blogPostDto)
        {
           if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var blog = new BlogPost();
            blog.BlogTitle = blogPostDto.BlogTitle;
            blog.BlogContent = blogPostDto.BlogContent;
            blog.CreatedDate = DateTime.Now;
            blog.UpdatedDate = DateTime.Now;
            blog.isActive = true;
            blog.UserId = GetUserId();

            _appDbContext.BlogPost.Add(blog);
            await _appDbContext.SaveChangesAsync();
            return Ok(blog);

           
        }
        [HttpDelete("{id}")]
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
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlogPost([FromBody] BlogPost newBlogPost, int blogPostId)
        {
            var blogPost = await _appDbContext.BlogPost.FirstOrDefaultAsync(x => x.BlogId == blogPostId);
            if (blogPost == null)
            {
                return NotFound();
            }
            blogPost.BlogContent = newBlogPost.BlogContent;
            blogPost.BlogContent = newBlogPost.BlogContent;
            blogPost.UpdatedDate= DateTime.Now;
            await _appDbContext.SaveChangesAsync();
            return Ok(blogPost);
        }
    }
}


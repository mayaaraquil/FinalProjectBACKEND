using FinalProject1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

namespace FinalProject1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {

        private readonly AppDbContext _appDbContext;

        public CommentController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetCommentsAsync()
        {
            return Ok(await _appDbContext.Comments.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var comment = await _appDbContext.Comments.FirstOrDefaultAsync(x => x.CommentId == id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetCommentBySearch( SearchTerms searchTerms)
        {
            //initialize to build a query dynamically
            IQueryable<Comments> query = _appDbContext.Comments.AsQueryable();

            //filter author
            if (searchTerms.author.HasValue)
            {
                query = query.Where(x => x.UserId == searchTerms.author);
            }

            //filter post type
            if (searchTerms.postType.HasValue)
            {
                query = query.Where(x => x.PostType == searchTerms.postType);
            }

            //filter post id 
            if (searchTerms.EqualspostId.HasValue)
            {
                if (postType.HasValue)
                {
                    if (postType == PostTypes.Blog)
                    {
                        query = query.Where(x => x.BlogId == postId);
                    }
                    else if (postType == PostTypes.Playlist)
                    {
                        query = query.Where(x => x.PlaylistId == postId);
                    }
                    else if (postType == PostTypes.Song)
                    {
                        query = query.Where(x => x.SongId == postId);
                    }
                    else if(postType == PostTypes.Video)
                    {
                        query = query.Where(x => x.VideoId == postId);
                    }
                    
                }
                else
                {
                    return BadRequest("Need both post id and type");
                }
            }

            var comments = await query.ToListAsync();

            if(comments.Count() == 0)
            {
                return BadRequest("No comments found for the specified criteria");
            }

            return Ok(comments);

        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] Comments comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

       

            _appDbContext.Comments.Add(comment);
            await _appDbContext.SaveChangesAsync();
            return Ok(comment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment([FromBody] Comments comment, int id)
        {
            var dbcomment = await _appDbContext.Comments.FirstOrDefaultAsync(x => x.CommentId == id);

            if (dbcomment == null)
            {
                return NotFound();
            }

            dbcomment.CommentText = comment.CommentText;
            await _appDbContext.SaveChangesAsync();
            return Ok(dbcomment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _appDbContext.Comments.FirstOrDefaultAsync(x => x.CommentId == id);

            if (comment == null)
            {
                return NotFound();
            }

            _appDbContext.Comments.Remove(comment);
            await _appDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}

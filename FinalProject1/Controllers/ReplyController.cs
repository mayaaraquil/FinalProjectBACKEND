using FinalProject1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;

namespace FinalProject1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReplyController : ControllerBase
    {

        private readonly AppDbContext _appDbContext;
        public ReplyController(AppDbContext appDbContext){
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetRepliesAsync()
        {
            return Ok(await _appDbContext.Replies.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReplyByComment(int id)
        {
            var reply = await _appDbContext.Replies.FirstOrDefaultAsync(x => x.ReplyId == id);

            if(reply == null)
            {
                return NotFound();
            }

            return Ok(reply);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetReplyBySearch(int? author, int? parentId)
        {
            IQueryable<Reply> query = _appDbContext.Replies.AsQueryable();

            if (author.HasValue)
            {
                query = query.Where(x => x.AuthorId == author.Value);
            }

            if (parentId.HasValue)
            {
                query = query.Where(x => x.ParentCommentId == parentId.Value);
            }

            var replies = await query.ToListAsync();

            if(replies.Count == 0) 
            {
                return BadRequest("No replies found");
            }

            return Ok(replies);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReply([FromBody] Reply reply )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _appDbContext.Replies.Add(reply);
            await _appDbContext.SaveChangesAsync();
            return Ok(reply);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReply([FromBody] Reply replies, int id)
        {
            var dbReply = await _appDbContext.Replies.FirstOrDefaultAsync(x => x.ReplyId == id);

            if(dbReply == null)
            {
                return NotFound();
            }

            dbReply.ReplyText = replies.ReplyText;
            await _appDbContext.SaveChangesAsync();
            return Ok(dbReply);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReply(int id)
        {
            var reply = await _appDbContext.Replies.FirstOrDefaultAsync(x => x.ReplyId == id);

            if(reply == null)
            {
                return NotFound();
            }

            _appDbContext.Replies.Remove(reply);
            await _appDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}

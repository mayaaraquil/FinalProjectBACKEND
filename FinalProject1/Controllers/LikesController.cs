using FinalProject1.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : Controller
    {
        private readonly AppDbContext _context;

        public LikesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Likes>>> GetLikes()
        {
            return await _context.Likes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Likes>> GetLikesById(int id)
        {
            var likes = await _context.Likes.FindAsync(id);

            if (likes == null)
            {
                return NotFound();
            }
            return likes;
        }

        [HttpPost]
        public async Task<ActionResult<Likes>> CreateLikes(Likes likes)
        {
            _context.Likes.Add(likes);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLikesById), new {id = likes.LikeId }, likes);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLike(int id, Likes likes)
        {
            if (id != likes.LikeId)
            {
                return BadRequest();
            }

            _context.Entry(likes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LikesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLike(int id)
        {
            var like = await _context.Likes.FindAsync(id);
            if (like == null)
            {
                return NotFound();
            }

            _context.Likes.Remove(like);
            return NoContent();
        }

        private bool LikesExists(int id)
        {
            return _context.Likes.Any(e => e.LikeId == id);
        }
    }
}

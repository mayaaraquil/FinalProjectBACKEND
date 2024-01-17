using FinalProject1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : BaseController
    {
        private readonly AppDbContext _context;

        public SongController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetSongs()
        {
            var userId = GetUserId();
            Console.WriteLine(userId);
            var songs = await _context.Songs.ToListAsync();
            var filteredSongs = songs.Where(x => x.authId == userId);
            return Ok(filteredSongs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Song>> GetSongById(string id)
        {
            var userId = GetUserId();
            var song = await _context.Songs.Where(x => x.authId == userId).FirstOrDefaultAsync(x => x.SpotifySongId == id);
            return Ok(song);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSong([FromBody]Song song)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            song.CreatedDate = DateTime.Now;
            song.UpdatedDate = DateTime.Now;
            song.isActive = true;
            song.authId = GetUserId();
            _context.Songs.Add(song);
            await _context.SaveChangesAsync();
            return Ok(song);
        }

         

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(string id)
        {
            var userId = GetUserId();
            var song = await _context.Songs.Where(x => x.authId == userId).FirstOrDefaultAsync(x => x.SpotifySongId == id);
            if (song == null)
            {
                return NotFound();
            }
            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}

using FinalProject1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayListController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        
        public PlayListController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetPlayListAsync()
        {
            return Ok(await _appDbContext.Playlists.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayListById(int id)
        {
            var playlist = await _appDbContext.Playlists.FirstOrDefaultAsync(x => x.PlaylistId == id);
            if (playlist == null)
            {
                return NotFound();
            }

            return Ok(playlist);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetPlayListsByName(string name)
        {
            if(name == null)
            {
                return BadRequest("Name cannot be null");
            }

            var plalists = await _appDbContext.Playlists.Where(x => x.PlaylistName.ToLower().Trim().Contains(name.ToLower().Trim())).ToListAsync();
            return Ok(plalists);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlaylist([FromBody] Playlist playlist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _appDbContext.Playlists.Add(playlist);
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlaylist([FromBody] Playlist playlist, int id)
        {
            var dbplaylist = await _appDbContext.Playlists.FirstOrDefaultAsync(x => x.PlaylistId == id);

            if(dbplaylist == null)
            {
                return NotFound();
            }

            dbplaylist.PlaylistName = playlist.PlaylistName;

            await _appDbContext.SaveChangesAsync();

            return Ok(playlist);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayList(int id)
        {
            var playlist = await _appDbContext.Playlists.FirstOrDefaultAsync(x => x.PlaylistId==id);

            if(playlist == null)
            {
                return NotFound();
            }

            _appDbContext.Playlists.Remove(playlist);
            await _appDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}

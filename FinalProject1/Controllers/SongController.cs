using FinalProject1.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : Controller
    {
        private readonly List<Song> _songs = new List<Song>();

        [HttpGet]
        public ActionResult<IEnumerable<Song>> GetSongs()
        {
            return _songs;
        }

        [HttpGet("{id}")]
        public ActionResult<Song> GetSongById(int id)
        {
            var song = _songs.FirstOrDefault(s => s.SongId == id);
            if (song == null)
            {
                return NotFound();
            }
            return song;
        }

        [HttpPost]
        public ActionResult<Song> CreateSong(Song song)
        {
            _songs.Add(song);
            return CreatedAtAction(nameof(GetSongById), new { id = song.SongId }, song);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSong(int id, Song song)
        {
            var existingSong = _songs.FirstOrDefault(s => s.SongId == id);
            if (existingSong == null)
            {
                return NotFound();
            }
            existingSong.SpotifySongId = song.SpotifySongId;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSong(int id)
        {
            var song = _songs.FirstOrDefault(s => s.SongId == id);
            if (song == null)
            {
                return NotFound();
            }
            _songs.Remove(song);
            return NoContent();
        }

    }
}

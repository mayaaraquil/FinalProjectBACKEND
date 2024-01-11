using FinalProject1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public VideoController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpGet("/videos")]
        public async Task<IActionResult> GetVideosAsync()
        {
            return Ok(await _appDbContext.Videos.ToListAsync());
        }
        [HttpGet("/videos/{id}")]
        public async Task<IActionResult> GetVideosByUserIdAsync(int userId)
        {
            var videos = await _appDbContext.Videos.Where(x => x.UserId == userId).FirstOrDefaultAsync(x => x.UserId == userId);
            if (videos == null)
            {
                return NotFound();
            }
            return Ok(videos);
        }
        [HttpPost("/videos")]
        public async Task<IActionResult> CreateVideo([FromBody] Video video)
        {
            video.CreatedDate = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _appDbContext.Videos.Add(video);
            await _appDbContext.SaveChangesAsync();
            return Ok(video);
        }
        [HttpPut("/videos/{id}")]
        public async Task<IActionResult> UpdateVideoAsync([FromBody]Video video, int id)
        {
            var originalVideo= await _appDbContext.Videos.FirstOrDefaultAsync(x => x.VideoId == id);
            if(originalVideo == null)
            {
                return NotFound();
            }
            video.VideosUrl = originalVideo.VideosUrl;
            video.UpdatedDate = DateTime.Now;
            await _appDbContext.SaveChangesAsync();
            return Ok(video);
        }
        [HttpDelete("/videos/{id}")]
        public async Task<IActionResult> DeleteVideo(int id)
        {
            var video = await _appDbContext.Videos.FirstOrDefaultAsync(x => x.VideoId == id);
            if (video == null)
            {
                return NotFound();
            }
            _appDbContext.Videos.Remove(video);
            await _appDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}

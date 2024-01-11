using FinalProject1.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : Controller
    {
        private readonly List<Likes> _likes = new List<Likes>();

        [HttpGet]
        public ActionResult<IEnumerable<Likes>> GetLikes()
        {
            return _likes;
        }

        [HttpGet("{id}")]
        public ActionResult<Likes> GetLikesById(int id)
        {
            var likes = _likes.FirstOrDefault(l => l.LikeId == id);
            if (likes == null)
            {
                return NotFound();
            }
            return likes;
        }

        [HttpPost]
        public ActionResult<Likes> CreateLikes(Likes likes)
        {
            _likes.Add(likes);
            return CreatedAtAction(nameof(GetLikesById), new {id = likes.LikeId }, likes);
        }

        [HttpPut]
        public IActionResult UpdateLike(int id, Likes likes)
        {
            var existingLike = _likes.FirstOrDefault(l => l.LikeId == id);
            if (existingLike == null)
            {
                return NotFound();
            }

            existingLike.UserId = likes.UserId;
            existingLike.LikedItemId = likes.LikedItemId;
            existingLike.Posts = likes.Posts;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLike(int id)
        {
            var like = _likes.FirstOrDefault(l => l.LikeId == id);
            if (like == null)
            {
                return NotFound();
            }

            _likes.Remove(like);
            return NoContent();
        }
    }
}

using FinalProject1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly AppDbContext _appdbContext;

        public UserController(AppDbContext appdbContext)
        {
            _appdbContext = appdbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {
            var blah = GetUserId();
            return Ok(await _appdbContext.Users.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            var user = await _appdbContext.Users.FirstOrDefaultAsync(x => x.userId == id);

            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] EndUser user)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

          
            user.CreatedDate = DateTime.Now;
            user.UpdatedDate = DateTime.Now;
            user.isActive = true;

            _appdbContext.Users.Add(user);
            await _appdbContext.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] EndUser user, int id)
        {
            var originalUser = await _appdbContext.Users.FirstOrDefaultAsync(x => x.userId == id);

            if(originalUser == null)
            {
                return NotFound();
            }

            originalUser.username = user.username;
            originalUser.profilePicture = user.profilePicture;
            originalUser.bio = user.bio;
            originalUser.UpdatedDate = DateTime.Now;

            await _appdbContext.SaveChangesAsync();
            return Ok(originalUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _appdbContext.Users.FirstOrDefaultAsync(x =>x.userId == id);
            if(user == null)
            {
                return NotFound();
            }

            _appdbContext.Users.Remove(user);
            await _appdbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}

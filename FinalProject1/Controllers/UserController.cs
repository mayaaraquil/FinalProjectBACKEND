using FinalProject1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _appdbContext;

        public UserController(AppDbContext appdbContext)
        {
            _appdbContext = appdbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {
            return Ok(await _appdbContext.Users.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            var user = await _appdbContext.Users.FirstOrDefaultAsync(x => x.UserId == id);

            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

          

            _appdbContext.Users.Add(user);
            await _appdbContext.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] User user, int id)
        {
            var originalUser = await _appdbContext.Users.FirstOrDefaultAsync(x => x.UserId == id);

            if(originalUser == null)
            {
                return NotFound();
            }

            originalUser.Username = user.Username;
            originalUser.ProfilePicture = user.ProfilePicture;
            originalUser.Bio = user.Bio;

            await _appdbContext.SaveChangesAsync();
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _appdbContext.Users.FirstOrDefaultAsync(x =>x.UserId == id);
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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FinalProject1.Controllers
{
    public class BaseController : ControllerBase
    {
        protected string GetUserId()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var JsonToken = handler.ReadToken(token) as JwtSecurityToken;
            var userId = JsonToken?.Claims.FirstOrDefault(claim => claim.Type == "sub")?.Value;

            return userId;
        }
    }
}

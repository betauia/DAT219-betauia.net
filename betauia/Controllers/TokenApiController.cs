using System;
using System.Linq;
using System.Threading.Tasks;
using betauia.Data;
using betauia.Models;
using betauia.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace betauia.Controllers
{
    [ApiController]
    public class TokenApiController : Controller
    {
      private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _um;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenManager _tokenManager;
        private readonly ITokenVerifier _tokenVerifier;
        private readonly TokenFactory _tokenFactory;
        public TokenApiController(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
          RoleManager<IdentityRole> rm, ITokenManager tokenManager, ITokenVerifier tokenVerifier)
        {
            _db = db;
            _um = userManager;
            _roleManager = rm;
            _tokenManager = tokenManager;
            _tokenVerifier = tokenVerifier;
            _tokenFactory = new TokenFactory(_um,rm,tokenManager);
        }

        [HttpDelete]
        [Route("api/token/{token}")]
        public async Task<IActionResult> DeleteToken(string token)
        {
          await _tokenManager.RemoveUserTokensAsync(token);
          return Ok();
        }

        [HttpGet]
        [Route("api/token/valid/{token}")]
        public async Task<IActionResult> ValidateTokenAsync(string token)
        {
          var id = await _tokenVerifier.GetTokenUser(token);
            if (id == null)
            {
                return BadRequest("301");
            }

            var user = await _um.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest("101");
            }

            return Ok();
        }

        [HttpGet]
        [Route("api/token/role/{token}")]
        public async Task<IActionResult> GetRole(string token)
        {
            var id = await _tokenVerifier.GetTokenUser(token);
            if (id == null)
            {
                return BadRequest("301");
            }

            var user = await _um.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest("101");
            }

            var roles = _um.GetRolesAsync(user).Result;
            return Ok(roles);
        }

        [HttpGet]
        [Authorize("Adminpanel")]
        [Route("api/token/adminpanel")]
        public async Task<IActionResult> ValidateAdmin()
        {
          return Ok();
        }

        [HttpGet]
        [Authorize("Ticket.free")]
        [Route("api/token/freeticket")]
        public async Task<IActionResult> FreeTicket()
        {
            return Ok();
        }

        [HttpGet]
        [Route("api/token/accountverified")]
        public async Task<IActionResult> VerifiedAccount([FromHeader] string Authorization)
        {
          var id = await _tokenVerifier.GetTokenUser(Authorization.Split(' ')[1]);
          var userid =await _um.FindByIdAsync(id);
          var claims = await _um.GetClaimsAsync(userid);
          if (claims.Any(a => a.Type == "AccountVerified" && a.Value == "true")) return Ok();
          return BadRequest(603);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/token/refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshToken refreshToken)
        {
            var refreshTokenFromDatabase = _db.RefreshTokens.SingleOrDefault(i => i.Token == refreshToken.Token);

            if (refreshTokenFromDatabase == null)
                return BadRequest();
            if (refreshTokenFromDatabase.ExpiresUtc < DateTime.Now.ToUniversalTime())
                return Unauthorized();
            var  user = await _um.FindByIdAsync(refreshTokenFromDatabase.UserId);
            if (user.Banned)
                return BadRequest(602);

            var token = await _tokenFactory.GetTokenAsync(user);

            var response = new TokenResponse
            {
                AccessToken = token,
                RefreshToken = refreshTokenFromDatabase.Token,
            };
            return Ok(response);
        }
    }
}

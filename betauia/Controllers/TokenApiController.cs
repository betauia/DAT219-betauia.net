using System.Linq;
using System.Threading.Tasks;
using betauia.Data;
using betauia.Models;
using betauia.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        public TokenApiController(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
          RoleManager<IdentityRole> rm, ITokenManager tokenManager, ITokenVerifier tokenVerifier)
        {
            _db = db;
            _um = userManager;
            _roleManager = rm;
            _tokenManager = tokenManager;
            _tokenVerifier = tokenVerifier;
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
        [Route("api/token/accountverified")]
        public async Task<IActionResult> VerifiedAccount([FromHeader] string Authorization)
        {
          var id = await _tokenVerifier.GetTokenUser(Authorization.Split(' ')[1]);
          var userid =await _um.FindByIdAsync(id);
          var claims = await _um.GetClaimsAsync(userid);
          int i = 1;
          return Ok();
        }
    }
}

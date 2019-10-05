using System.Threading.Tasks;
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
        private readonly UserManager<ApplicationUser> _um;
        private readonly ITokenManager _tokenManager;
        private readonly ITokenVerifier _tokenVerifier;
        public TokenApiController(UserManager<ApplicationUser> userManager,
          ITokenManager tokenManager, ITokenVerifier tokenVerifier)
        {
            _um = userManager;
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
    }
}

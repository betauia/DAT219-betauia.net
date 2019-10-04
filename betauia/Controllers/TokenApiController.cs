using System.Threading.Tasks;
using betauia.Models;
using betauia.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace betauia.Controllers
{
    [ApiController]
    public class TokenApiController : Controller
    {
        private readonly UserManager<ApplicationUser> _um;
        private readonly RoleManager<IdentityRole> _rm;
        private readonly TokenFactory _tf;
        private readonly ITokenManager _tokenManager;
        public TokenApiController(UserManager<ApplicationUser> userManager,
          RoleManager<IdentityRole> roleManager, ITokenManager tokenManager)
        {
            _um = userManager;
            _rm = roleManager;
            _tf = new TokenFactory(_um,_rm,tokenManager);
            _tokenManager = tokenManager;
        }

        [HttpPost]
        [Route("api/token/valid/{token}")]
        public async Task<IActionResult> ValidateTokenAsync(string token)
        {
            var id = await _tf.AuthenticateUserAsync(token);
            if (id == null)
            {
                return BadRequest("301");
            }

            var user = await _um.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest("101");
            }

            if (user.Active == false)
            {
                return BadRequest("102");
            }

            if (user.ForceLogOut)
            {
                return BadRequest("103");
            }

            return Ok();
        }

        [HttpPost]
        [Route("api/token/role/{token}")]
        public async Task<IActionResult> ValidateAdmin(string token)
        {
            var id = await _tf.AuthenticateUserAsync(token);
            if (id == null)
            {
                return BadRequest("301");
            }

            var user = await _um.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest("101");
            }

            if (user.Active == false)
            {
                return BadRequest("102");
            }

            if (user.ForceLogOut)
            {
                return BadRequest("103");
            }

            var roles = _um.GetRolesAsync(user).Result;
            return Ok(roles);
        }
    }
}

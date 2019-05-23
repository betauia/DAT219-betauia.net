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
        
        public TokenApiController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _um = userManager;
            _rm = roleManager;
            _tf = new TokenFactory(_um,_rm);
        }

        [HttpPost]
        [Route("api/token/valid/{token}")]
        public IActionResult ValidateToken(string token)
        {
            var id = _tf.AuthenticateUser(token);
            if (id == null)
            {
                return BadRequest("301");
            }

            var user = _um.FindByIdAsync(id).Result;
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
        public IActionResult ValidateAdmin(string token)
        {
            var id = _tf.AuthenticateUser(token);
            if (id == null)
            {
                return BadRequest("301");
            }

            var user = _um.FindByIdAsync(id).Result;
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
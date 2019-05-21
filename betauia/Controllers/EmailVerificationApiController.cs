using System.Security.Claims;
using betauia.Data;
using betauia.Models;
using betauia.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace betauia.Controllers
{
    [ApiController]
    public class EmailVerificationApiController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _um;
        private readonly RoleManager<IdentityRole> _rm;
        private readonly TokenFactory _tf;
        
        public EmailVerificationApiController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _um = userManager;
            _rm = roleManager;
            _tf = new TokenFactory(_um,_rm);
        }

        [HttpGet]
        [Route("api/verifyemail/{id}")]
        public IActionResult SendEmailVerification(string id)
        {
            var user = _um.FindByIdAsync(id).Result;
            if (user == null)
            {
                return BadRequest(101);
            }
            
            if(user.)
        }

        [HttpPost]
        [Route("api/verifyemail/{token}")]
        public IActionResult VerifyEmail(string token)
        {
            var id = _tf.AuthenticateUser(token);
            if (id == null)
            {
                return BadRequest("301");
            }

            var user = _um.FindByIdAsync(id).Result;
            if (user == null)
            {
                return BadRequest("301");
            }

            user.VerifiedEmail = true;
            var claim = new Claim("EmailVerified", "true", ClaimValueTypes.String);
            var result = _um.AddClaimAsync(user, claim).Result;
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors);
        }
    }
}
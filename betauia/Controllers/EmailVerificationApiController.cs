using System.Security.Claims;
using betauia.Data;
using betauia.Models;
using betauia.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace betauia.Controllers
{
    [ApiController]
    public class EmailVerificationApiController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _um;
        private readonly RoleManager<IdentityRole> _rm;
        private readonly TokenFactory _tf;

        public EmailVerificationApiController(UserManager<ApplicationUser> userManager,
          RoleManager<IdentityRole> roleManager,ITokenManager tokenManager)
        {
            _um = userManager;
            _rm = roleManager;
            _tf = new TokenFactory(_um,_rm,tokenManager);
        }

        [HttpGet]
        [Authorize("User")]
        [Route("api/getemailverification")]
        public async Task<IActionResult> SendEmailVerification([FromHeader] string Authorization)
        {
          var usertoken = Authorization.Split(' ')[1];
          var userid = await _tf.AuthenticateUserAsync(usertoken);

            var user = _um.FindByIdAsync(userid).Result;
            if (user == null)
            {
                return BadRequest("101");
            }

            if (user.Active == false)
            {
                return BadRequest("102");
            }

            if (user.VerifiedEmail)
            {
                return BadRequest("205");
            }

            var token = await _tf.GetEmailVerificationTokenAsync(user);
            var url = "http://localhost:8081/verifyemail/" + token;

            SmtpClient smtp = new SmtpClient("smtp.gtm.no");
            smtp.EnableSsl = false;
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("betalan@betauia.net","8iFK0N2tdz");
            smtp.Send("noreply@betauia.net","erikaspen1@gmail.com","Verify you email at betauia.net",url);

            return Ok(token);
        }

        [HttpGet]
        [Route("api/verifyemail/{token}")]
        public async Task<IActionResult> VerifyEmail(string token)
        {
            var id = await _tf.VerifyEmailAsync(token);
            if (id == null)
            {
                return BadRequest("301");
            }

            var user = _um.FindByIdAsync(id).Result;
            if (user == null)
            {
                return BadRequest("301");
            }

            if (user.VerifiedEmail == true)
            {
                return BadRequest("205");
            }

            user.VerifiedEmail = true;
            _um.UpdateAsync(user).Wait();
            var claim = new Claim("EmailVerified", "true", ClaimValueTypes.String);
            var result = _um.AddClaimAsync(user, claim).Result;

            claim = new Claim("AccountVerified","true",ClaimValueTypes.String);
            result = _um.AddClaimAsync(user, claim).Result;
            if (result.Succeeded)
            {
                user.ForceLogOut = true;
                return Ok();
            }
            return BadRequest(result.Errors);
        }
    }
}

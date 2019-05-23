using System.Security.Claims;
using betauia.Data;
using betauia.Models;
using betauia.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

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
        [Authorize("User")]
        [Route("api/getemailverification/{id}")]
        public IActionResult SendEmailVerification(string id)
        {
            var user = _um.FindByIdAsync(id).Result;
            if (user == null)
            {
                return BadRequest("101");
            }

            if (user.Active == false)
            {
                return BadRequest("102");
            }
            
            if (user.VerifiedEmail == true)
            {
                return BadRequest("205");
            }

            var token = _tf.GetEmailVerificationToken(user);
            var url = "http://localhost:8081/verifyemail/" + token;
            
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("erikaspen1@gmail.com","applicatoinkey");
            smtp.Send("erikaspen1@gmail.com","erikaa17@uia.no","Verify you email at betauia.net",url);
            
            return Ok(token);
        }

        [HttpGet]
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

            if (user.VerifiedEmail == true)
            {
                return BadRequest("205");
            }

            user.VerifiedEmail = true;
            _um.UpdateAsync(user).Wait();
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
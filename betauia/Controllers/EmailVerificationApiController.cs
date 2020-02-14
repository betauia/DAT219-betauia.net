using System.Security.Claims;
using betauia.Data;
using betauia.Models;
using betauia.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using betauia.Email;

namespace betauia.Controllers
{
    [ApiController]
    public class EmailVerificationApiController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _um;
        private readonly RoleManager<IdentityRole> _rm;
        private readonly TokenFactory _tf;
        private readonly ITokenVerifier _tokenVerifier;
        private readonly ITokenManager _tokenManager;
        private readonly IEmailRender _emailRender;

        public EmailVerificationApiController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, ITokenManager tokenManager, ITokenVerifier tokenVerifier,
            IEmailRender emailrender)
        {
            _um = userManager;
            _rm = roleManager;
            _tf = new TokenFactory(_um,_rm,tokenManager);
            _tokenVerifier = tokenVerifier;
            _tokenManager = tokenManager;
            _emailRender = emailrender;
        }

        [HttpGet]
        [Authorize("User")]
        [Route("api/getemailverification")]
        public async Task<IActionResult> SendEmailVerification([FromHeader] string Authorization)
        {
          var usertoken = Authorization.Split(' ')[1];
          var userid = await _tokenVerifier.GetTokenUser(usertoken);
          var user = await _um.FindByIdAsync(userid);

          var token = await _tf.GetEmailVerificationTokenAsync(user);
            
          var url = "https://betauia.net/verifyemail/" + token;
          var emailviewmodel = new EmailViewModel(url);
          var htmlbody = await _emailRender.RenderViewToStringAsync($"Views/Emails/VerifyAccount/EmailVerifyAccount.cshtml",emailviewmodel);

          var message = new MailMessage("noreply@betauia.net", user.Email)
          {
              Subject = "betauia email verification",
          };
          message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(htmlbody,Encoding.UTF8,MediaTypeNames.Text.Html));
          using (var smtp = new SmtpClient("smtp.gtm.no", 587))
          {
              smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
              smtp.UseDefaultCredentials = false;
              smtp.Credentials = new NetworkCredential("betalan@betauia.net","8iFK0N2tdz");
              smtp.EnableSsl = false;
              await smtp.SendMailAsync(message);
          }

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
                await _tokenManager.RemoveUserTokensAsync(id);
                return Ok();
            }
            return BadRequest(result.Errors);
        }
    }
}

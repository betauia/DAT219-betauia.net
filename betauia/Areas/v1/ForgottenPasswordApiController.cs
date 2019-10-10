using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using betauia.Data;
using betauia.Models;
using betauia.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace betauia.Areas.v1
{
  [ApiController]
  public class ForgottenPasswordApiController : ControllerBase
  {
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _um;
    private readonly RoleManager<IdentityRole> _rm;
    private readonly TokenFactory _tf;
    private readonly ITokenVerifier _tokenVerifier;
    private readonly ITokenManager _tokenManager;

    public ForgottenPasswordApiController(ApplicationDbContext db,UserManager<ApplicationUser> userManager,
      RoleManager<IdentityRole> roleManager,ITokenManager tokenManager,ITokenVerifier tokenVerifier)
    {
      _um = userManager;
      _rm = roleManager;
      _db = db;
      _tf = new TokenFactory(_um,_rm,tokenManager);
      _tokenManager = tokenManager;
      _tokenVerifier = tokenVerifier;
    }

    [HttpGet]
    [Authorize("User")]
    [Route("api/resetpassword/get")]
    public async Task<IActionResult> GetPasswordEmail([FromHeader] string Authorization)
    {
      var token = Authorization.Split(' ')[1];
      var id = await _tokenVerifier.GetTokenUser(token);
      var user = _um.FindByIdAsync(id).Result;

      token = await _tf.GetPasswordRestTokenAsync(user);
      
      var url = "http://128.39.149.31/resetpassword/" + token;
      SmtpClient smtp = new SmtpClient("smtp.gtm.no");
      smtp.EnableSsl = false;
      smtp.Port = 587;
      smtp.Credentials = new NetworkCredential("betalan@betauia.net","8iFK0N2tdz");
      smtp.Send("noreply@betauia.net",user.Email,"Go to reset your password",url);
      return Ok();
    }

    [HttpPost]
    [Route("api/resetpassword/get")]
    public async Task<IActionResult> GetPasswordEmailFromEmail(ForgottenPasswordModel forgottenPasswordModel)
    {
      var user = _um.FindByEmailAsync(forgottenPasswordModel.Email).Result;
      if (user == null)
      {
        return NotFound();
      }

      var token = await _tf.GetPasswordRestTokenAsync(user);
      
      var url = "http://128.39.149.31/resetpassword/" + token;

      SmtpClient smtp = new SmtpClient("smtp.gtm.no");
      smtp.EnableSsl = false;
      smtp.Port = 587;
      smtp.Credentials = new NetworkCredential("betalan@betauia.net","8iFK0N2tdz");
      smtp.Send("noreply@betauia.net",forgottenPasswordModel.Email,"Go to reset your password",url);
      return Ok();
    }
    
    [HttpPost]
    [Route("api/resetpassword/post")]
    public async Task<IActionResult> ResetPassword(PasswordResetModel passwordResetModel)
    {
      var id = await _tf.VerifyPasswordResetTokenAsync(passwordResetModel.Token);
      if (id == null)
      {
        return BadRequest("301");
      }

      var user = _um.FindByIdAsync(id).Result;
      if (user == null)
      {
        return BadRequest("301");
      }

      var t = _um.GeneratePasswordResetTokenAsync(user).Result;
      var result =  await  _um.ResetPasswordAsync(user, t, passwordResetModel.Password);
      if (result.Succeeded)
      {
        await _tokenManager.RemoveUserTokensAsync(id);
        return Ok();
      }
      return BadRequest(result);
    }

    public class PasswordResetModel
    {
      public string Token { get; set; }

      [Required]
      [DataType(DataType.Password)]
      public string Password { get; set; }

      [Required]
      [DataType(DataType.Password)]
      [Compare("Password",ErrorMessage = "203")]
      public string Againpassword { get; set; }
    }

    public class ForgottenPasswordModel
    {
      [Required]
      [DataType(DataType.EmailAddress,ErrorMessage = "not valid email")]
      public string Email { get; set; }
    }
  }
}

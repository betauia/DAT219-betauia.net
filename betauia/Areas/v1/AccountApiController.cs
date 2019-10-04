using System.Threading.Tasks;
using betauia.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using betauia.Data;
using betauia.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace betauia.Areas.v1
{
    [Area("v1")]
    //[Route("api/v1/user")]
    [ApiController]
    public class AccountApiController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _um;
        private readonly RoleManager<IdentityRole> _rm;
        private readonly TokenFactory _tf;

        public AccountApiController(ApplicationDbContext db,UserManager<ApplicationUser> userManager,
          RoleManager<IdentityRole> roleManager,ITokenManager tokenManager)
        {
            _um = userManager;
            _rm = roleManager;
            _db = db;
            _tf = new TokenFactory(_um,_rm,tokenManager);
        }

        [HttpPost]
        [Route("api/account/register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //Check if username is taken
            var tUser = _um.FindByNameAsync(registerModel.UserName).Result;
            if (tUser != null)
            {
                return BadRequest("202");
            }

            //Check if email is taken
            tUser = null;
            tUser = _um.FindByEmailAsync(registerModel.Email).Result;
            if (tUser != null)
            {
                return BadRequest("201");
            }

            var user = new ApplicationUser
            {
                UserName = registerModel.UserName,
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                Email = registerModel.Email
            };

            var result = _um.CreateAsync(user, registerModel.Password).Result;

            const string role = "User";
            _um.AddClaimAsync(user, new Claim("Role", "User"));

            if (result.Succeeded)
            {
                if (_rm.FindByNameAsync(role) == null)
                {
                    _rm.CreateAsync(new IdentityRole(role)).Wait();
                }
                _um.AddToRoleAsync(user, role).Wait();
                /*
                _userManager.AddClaimAsync(user, new Claim("username", user.UserName));
                _userManager.AddClaimAsync(user, new Claim("firstName", user.FirstName));
                _userManager.AddClaimAsync(user, new Claim("lastName", user.LastName));
                _userManager.AddClaimAsync(user, new Claim("email", user.Email));
                _userManager.AddClaimAsync(user, new Claim("role", role));
                */
                return Ok(await _tf.GetTokenAsync(user));
            }
            return BadRequest(result.Errors);
        }

        [Route("/api/account/login")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Loginmodel loginmodel)
        {
            var user = _um.FindByNameAsync(loginmodel.Username).Result;
            if (user == null)
            {
                user = _um.FindByEmailAsync(loginmodel.Username).Result;
                if (user == null)
                {
                    return BadRequest("101");
                }
            }

            if (user.Active == false)
            {
                return BadRequest("102");
            }

            if (_um.CheckPasswordAsync(user, loginmodel.Password).Result)
            {
                var token = await _tf.GetTokenAsync(user);
                return Ok(token);
            }
            return BadRequest();
        }

        [Route("/api/account/get")]
        [Authorize(Policy = "User")]
        [HttpPost]
        public async Task<IActionResult> GetAccountInfo([FromHeader] string Authorization)
        {
           var token = Authorization.Split(' ')[1];
            var id = await _tf.AuthenticateUserAsync(token);
            var user = _um.FindByIdAsync(id).Result;
            if (user == null)
            {
                return BadRequest("301");
            }
            if (user.Active == false)
            {
                return BadRequest("102");
            }

            var profile = new ProfileViewModel(user);
            return Ok(profile);
        }

        [Route("/api/account/edit")]
        [HttpPut]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> EditAccount([FromHeader] string Authorization, ProfileViewModel Profile)
        {
          var token = Authorization.Split(' ')[1];
          var id = await _tf.AuthenticateUserAsync(token);

          if (id == string.Empty) return BadRequest();
          if (id != Profile.Id) return BadRequest();

          var user = _um.FindByIdAsync(Profile.Id).Result;
          if (user == null) return NotFound("101");

          if (Profile.FirstName == "") return BadRequest("206");
          user.FirstName = Profile.FirstName;

          if (Profile.LastName == "") return BadRequest("207");
          user.LastName = Profile.LastName;

          if (Profile.Email != "" && Profile.Email != user.Email)
          {
            if (_um.FindByEmailAsync(Profile.Email).Result != null) return BadRequest("201");
            if (new EmailAddressAttribute().IsValid(Profile.Email) == false) return BadRequest("204");
            user.Email = Profile.Email;
          }

          if (Profile.UserName != "" && Profile.UserName != user.UserName)
          {
            if (_um.FindByNameAsync(Profile.UserName).Result != null) return BadRequest("202");
            user.UserName = Profile.UserName;
          }

          await _um.UpdateAsync(user);
          return Ok(new AdminUserView(user));
        }
    }
}

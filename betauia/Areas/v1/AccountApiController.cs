using System.Threading.Tasks;
using betauia.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
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
        
        public AccountApiController(ApplicationDbContext db,UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _um = userManager;
            _rm = roleManager;
            _db = db;
            _tf = new TokenFactory(_um,_rm);
        }

        [HttpPost]
        [Route("api/account/register")]
        public IActionResult Register([FromBody]RegisterModel registerModel)
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
                return Ok(_tf.GetToken(user));
            }
            return BadRequest(result.Errors);
        }
        
        [Route("/api/account/login")]
        [HttpPost]
        public IActionResult Create([FromBody]Loginmodel loginmodel)
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
                var token = _tf.GetToken(user);
                return Ok(token);
            }
            return BadRequest();
        }

        [Route("/api/account/get")]
        [Authorize(Policy = "User")]
        [HttpPost]
        public IActionResult GetAccountInfo([FromBody] TokenModel tokenModel)
        {
            var id = _tf.AuthenticateUser(tokenModel.Token);
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
    }
}
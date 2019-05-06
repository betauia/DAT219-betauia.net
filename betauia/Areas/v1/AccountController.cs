using System.Threading.Tasks;
using betauia.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;

namespace betauia.Areas.v1
{
    [Area("v1")]
    //[Route("api/v1/user")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        [Route("api/v1/account/register")]
        public IActionResult Post(RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = new ApplicationUser
            {
                UserName = registerModel.UserName, FirstName = registerModel.FirstName,
                LastName = registerModel.LastName, Email = registerModel.Email
            };

            var result = _userManager.CreateAsync(user, registerModel.Password).Result;
            
            string role = "User";
            
            if (result.Succeeded)
            {
                if (_roleManager.FindByNameAsync(role) == null)
                {
                    _roleManager.CreateAsync(new IdentityRole(role));
                } 
                _userManager.AddToRoleAsync(user, role).Wait();
                _userManager.AddClaimAsync(user, new Claim("username", user.UserName));
                _userManager.AddClaimAsync(user, new Claim("firstName", user.FirstName));
                _userManager.AddClaimAsync(user, new Claim("lastName", user.LastName));
                _userManager.AddClaimAsync(user, new Claim("email", user.Email));
                _userManager.AddClaimAsync(user, new Claim("role", role));
                return Ok(new ProfileViewModel(user));
            }
            return BadRequest(result.Errors);
        }
    }
}
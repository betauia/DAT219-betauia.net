using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using betauia.Data;
using betauia.Models;
using betauia.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace betauia.Controllers
{
    [Authorize]
    [ApiController]
    public class UserRoleApiController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _um;
        private readonly RoleManager<IdentityRole> _rm;
        private readonly TokenFactory _tf;
        private readonly ITokenManager _tokenManager;

        public UserRoleApiController(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,ITokenManager tokenManager)
        {
            _um = userManager;
            _rm = roleManager;
            _db = db;
            _tf = new TokenFactory(_um, _rm,tokenManager);
            _tokenManager = tokenManager;
        }


        //get users from role
        [Authorize("Account.read")]
        [Authorize("Roles.read")]
        [HttpGet]
        [Route("api/user/role/{id}")]
        public async Task<IActionResult> GetUsersInRole(string id)
        {
            var role = _rm.FindByIdAsync(id).Result;
            if (role == null)
            {
                return NotFound();
            }

            var users = _um.GetUsersInRoleAsync(role.Name).Result;

            var usersafe = new List<AdminUserView>();
            foreach (var user in users)
            {
                usersafe.Add(new AdminUserView
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    VerifiedEmail = user.VerifiedEmail
                });
            }
            return Ok(usersafe);
        }

        //get users not in role
        [Authorize("Account.read")]
        [Authorize("Roles.read")]
        [HttpGet]
        [Route("api/user/nrole/{id}")]
        public async Task<IActionResult> GetUsersNotInRole(string id)
        {
            var role = _rm.FindByIdAsync(id).Result;
            if (role == null) return NotFound();

            var allusers = _um.Users.ToList();
            var roleusers = _um.GetUsersInRoleAsync(role.Name).Result;

            var nousers = allusers.Except(roleusers).ToList();

            var usersafe = new List<AdminUserView>();
            foreach (var user in nousers)
            {
                usersafe.Add(new AdminUserView
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    VerifiedEmail = user.VerifiedEmail
                });
            }
            return Ok(usersafe);
        }

        [Authorize("Account.write")]
        [Authorize("Roles.read")]
        [HttpPost]
        [Route("api/user/role/{userid}/{roleid}")]
        public async Task<IActionResult> SetUserRoles(string userid, string roleid)
        {
            var user = _um.FindByIdAsync(userid).Result;
            if (user == null)
            {
                return NotFound();
            }

            var role = _rm.FindByIdAsync(roleid).Result;
            if (role == null) return NotFound();

            _um.AddToRoleAsync(user, role.Name).Wait();
            await _tokenManager.RemoveUserTokensAsync(userid);
            return Ok();
        }

        [Authorize("Account.write")]
        [Authorize("Roles.read")]
        [HttpDelete]
        [Route("api/user/role/{id}")]
        public async Task<IActionResult> DeleteUserFromRole(string id, RoleList roleList)
        {
            var user = _um.FindByIdAsync(id).Result;
            if (user == null)
            {
                return NotFound();
            }

            _um.RemoveFromRolesAsync(user, roleList.roles).Wait();
            await _tokenManager.RemoveUserTokensAsync(id);
            return Ok();
        }

        public class RoleList
        {
            public List<string> roles { get; set; }
        }
    }
}

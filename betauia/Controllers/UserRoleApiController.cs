using System.Collections.Generic;
using System.Linq;
using betauia.Data;
using betauia.Models;
using betauia.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace betauia.Controllers
{
    [ApiController]
    public class UserRoleApiController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _um;
        private readonly RoleManager<IdentityRole> _rm;
        private readonly TokenFactory _tf;

        public UserRoleApiController(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _um = userManager;
            _rm = roleManager;
            _db = db;
            _tf = new TokenFactory(_um, _rm);
        }
        
        
        //get users from role
        [HttpGet]
        [Route("api/user/role/{id}")]
        public IActionResult GetUsersInRole(string id)
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
                    Active = user.Active,
                    ForceLogout = user.ForceLogOut,
                    VerifiedEmail = user.VerifiedEmail
                });
            }
            return Ok(usersafe);
        }
        
        //get users not in role
        [HttpGet]
        [Route("api/user/nrole/{id}")]
        public IActionResult GetUsersNotInRole(string id)
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
                    Active = user.Active,
                    ForceLogout = user.ForceLogOut,
                    VerifiedEmail = user.VerifiedEmail
                });
            }
            return Ok(usersafe);
        }
        
        [HttpPost]
        [Route("api/user/role/{userid}/{roleid}")]
        public IActionResult SetUserRoles(string userid, string roleid)
        {
            var user = _um.FindByIdAsync(userid).Result;
            if (user == null)
            {
                return NotFound();
            }

            var role = _rm.FindByIdAsync(roleid).Result;
            if (role == null) return NotFound();

            _um.AddToRoleAsync(user, role.Name).Wait();
            
            return Ok();
        }

        [HttpDelete]
        [Route("api/user/role/{id}")]
        public IActionResult DeleteUserFromRole(string id, RoleList roleList)
        {
            var user = _um.FindByIdAsync(id).Result;
            if (user == null)
            {
                return NotFound();
            }

            _um.RemoveFromRolesAsync(user, roleList.roles).Wait();
            return Ok();
        }

        public class RoleList                             
        {                                                 
            public List<string> roles { get; set; }       
        }                                                 
    }
}
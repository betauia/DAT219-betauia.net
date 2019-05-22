using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using betauia.Data;
using betauia.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using betauia.Tokens;

/* Author: Chris */
// All requests are tested and working //
namespace betauia.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/user")]
    public class UserApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _um;
        private readonly RoleManager<IdentityRole> _rm;
        private readonly TokenFactory _tf;
        
        public UserApiController(ApplicationDbContext context,UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm)
        {
            // Set the databasecontext
            _context = context;
            _um = um;
            _rm = rm;
            _tf = new TokenFactory(um,rm);
        }

        // GET: Get all users
        [Authorize("Account.read")]
        [HttpGet]
        public IActionResult GetAll()
        {
            // Return with 200 OK status code
            var userlist = new List<AdminUserView>();
            var users = _um.Users;
            foreach (var user in users)
            {
                userlist.Add(new AdminUserView
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
            return Ok(userlist);
        }

        // GET: Get user by id
        [Authorize("Account.read")]
        [HttpGet("{id}")]
        public IActionResult GetApplicationUser(string id)
        {
            var user = _um.FindByIdAsync(id).Result;
            if (user == null)
            {
                return BadRequest("101");
            }

            return Ok(new AdminUserView
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
        
        // PUT: Update user by id
        [Authorize("Account.write")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicationUser(string id, ApplicationUser applicationUser)
        {
            // Check if id matches user id
            if (id != applicationUser.Id) return BadRequest();

           
            // Set the current state to say that some or all of its properties has been modified
            _context.Entry(applicationUser).State = EntityState.Modified;
            
            try
            {
                // Save changes
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Check if the user was created
                if (!ApplicationUserExists(id)) return NotFound();
                else throw;
            }
            
            return Ok(applicationUser);
        }
        
        // POST: Add new user
        [Authorize("Account.write")]
        [HttpPost]
        public IActionResult Post([FromBody]RegisterModel registerModel)
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

            //create user
            var user = new ApplicationUser
            {
                Email = registerModel.Email, 
                UserName = registerModel.UserName,
                FirstName = registerModel.FirstName, 
                LastName = registerModel.LastName
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
                return Ok(new ProfileViewModel(user));
            }
            return BadRequest(result.Errors);
        }
        
        // DELETE: Delete user by id
        [Authorize("User")]
        [HttpDelete("{id}")]
        public IActionResult DeleteApplicationUser([FromBody] TokenModel tokenModel)
        {
            var id = _tf.AuthenticateUser(tokenModel.Token);

            // Receive and check if user is valid
            var user = _context.Users.FindAsync(id).Result;
            if (user == null) return NotFound();

            //deactivate account
            user.Active = false;
            user.ForceLogOut = true;
            
            //Delete information
            user.FirstName = null;
            user.LastName = null;
            user.UserName = null;
            
            //deletes password
            _um.RemovePasswordAsync(user).Wait();
            _um.AddPasswordAsync(user, "Password1.").Wait();

            return Ok();
        }

        // Function to check if a user by id exists
        private bool ApplicationUserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
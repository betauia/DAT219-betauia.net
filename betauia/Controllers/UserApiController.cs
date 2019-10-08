using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        private readonly ITokenVerifier _tokenVerifier;
        private readonly ITokenManager _tokenManager;
        public UserApiController(ApplicationDbContext context,UserManager<ApplicationUser> um,
          RoleManager<IdentityRole> rm,ITokenVerifier tokenVerifier,ITokenManager tokenManager)
        {
            // Set the databasecontext
            _context = context;
            _um = um;
            _rm = rm;
            _tokenVerifier = tokenVerifier;
            _tokenManager = tokenManager;
        }

        // GET: Get all users
        [Authorize("Account.read")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
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
                    VerifiedEmail = user.VerifiedEmail
                });
            }
            return Ok(userlist);
        }

        // GET: Get user by id
        [Authorize("Account.read")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicationUser(string id)
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

        [Authorize("Account.write")]
        [HttpPut]
        public async Task<IActionResult> UpdateApplicationUser([FromBody]AdminUserView adminUserView)
        {
            var user = _um.FindByIdAsync(adminUserView.Id).Result;
            if (user == null) return NotFound("101");

            user.FirstName = adminUserView.FirstName;
            user.LastName = adminUserView.LastName;
            if (adminUserView.Email != "" && adminUserView.Email != user.Email)
            {
                if (_um.FindByEmailAsync(adminUserView.Email).Result != null) return BadRequest(201);
                if (new EmailAddressAttribute().IsValid(adminUserView.Email) == false) return BadRequest("204");
                user.Email = adminUserView.Email;
            }

            if (adminUserView.UserName != "" && adminUserView.UserName != user.UserName)
            {
                if (_um.FindByNameAsync(adminUserView.UserName).Result != null) return BadRequest("202");
                user.UserName = adminUserView.UserName;
            }

            user.VerifiedEmail = adminUserView.VerifiedEmail;

            _um.UpdateAsync(user).Wait();
            return Ok(new AdminUserView(user));
        }

        // POST: Add new user
        [Authorize("Account.write")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegisterModel registerModel)
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
        [Authorize("Account.write")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationUser(string id,[FromBody] TokenModel tokenModel)
        {
            var userid = await _tokenVerifier.GetTokenUser(tokenModel.Token);
            if (userid != id) return BadRequest();
            // Receive and check if user is valid
            var user = _context.Users.FindAsync(id).Result;
            if (user == null) return NotFound();

            //Delete information
            user.FirstName = null;
            user.LastName = null;
            user.UserName = null;

            //deletes password
            _um.RemovePasswordAsync(user).Wait();
            _um.AddPasswordAsync(user, "Password1.").Wait();

            return Ok();
        }


        [Route("ban/{id}")]
        [HttpGet]
        [Authorize("Account.write")]
        public async Task<IActionResult> BanApplicationUser([FromRoute] string id)
        {
          var user = await _context.Users.FindAsync(id);
          if (user == null) return NotFound();
          user.Banned = true;
          await _tokenManager.RemoveUserTokensAsync(id);
          await _context.SaveChangesAsync();
          return Ok(new ProfileViewModel(user));
        }

        // Function to check if a user by id exists
        private bool ApplicationUserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}

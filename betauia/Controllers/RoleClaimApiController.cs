using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using betauia.Data;
using betauia.Models;
using betauia.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace betauia.Controllers
{

    [ApiController]
    public class RoleClaimApiController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _um;
        private readonly RoleManager<IdentityRole> _rm;
        private readonly TokenFactory _tf;
        
        public RoleClaimApiController(ApplicationDbContext db,UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _um = userManager;
            _rm = roleManager;
            _db = db;
            _tf = new TokenFactory(_um,_rm);
        }

        //Configures all user claims, included from roles
        [HttpGet]
        [Route("api/claim/user/{id}")]
        public IActionResult GetAllRoles(string id)
        {
            var user = _um.FindByIdAsync(id).Result;
            if (user != null)
            {
                //var claims = new Claim[] { };
                var claims = _um.GetClaimsAsync(user).Result;

                var roles = _um.GetRolesAsync(user).Result;
                foreach (var role in roles)
                {
                    var roleClaims = _rm.GetClaimsAsync(_rm.FindByNameAsync(role).Result).Result;
                    foreach (var roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }

                var JsonClaims = new List<ClaimModel>();
                foreach (var claim in claims)
                {
                    JsonClaims.Add(new ClaimModel {ClaimName = claim.Type, ClaimValue = claim.Value});
                }

                return Ok(JsonClaims);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("api/claim/user/{id}")]
        public IActionResult SetRolesToUser(string id, ClaimList claimListModel)
        {
            var user = _um.FindByIdAsync(id).Result;
            if (user == null)
            {
                return NotFound();
            }
            
            foreach (var claim in claimListModel.claims)
            {
                var tClaim = new Claim(claim.ClaimName,claim.ClaimValue,ClaimValueTypes.String);
                _um.AddClaimAsync(user, tClaim).Wait();
            }
            return Ok();
        }
        
        //Configures user claims, excludes role
        [HttpGet]
        [Route("api/role/user/{id}")]
        public IActionResult GetUserRoles(string id)
        {
            var user = _um.FindByIdAsync(id).Result;
            if (user == null)
            {
                return NotFound();
            }

            var roles = _um.GetRolesAsync(user).Result;
            return Ok(roles);
        }

        [HttpPost]
        [Route("api/role/user/{id}")]
        public IActionResult SetUserRoles(string id, RoleList roleList)
        {
            var user = _um.FindByIdAsync(id).Result;
            if (user == null)
            {
                return NotFound();
            }

            foreach (var role in roleList.roles)
            {
                _um.AddToRoleAsync(user, role).Wait();
            }

            return Ok();
        }

        [HttpPut]
        [Route("api/role/user/{id}")]
        public IActionResult AddUserRole(string id, ClaimModel claimModel)
        {
            var user = _um.FindByIdAsync(id).Result;
            if (user == null)
            {
                return BadRequest("101");
            }

            if (claimModel.ClaimName == "")
            {
                return BadRequest("403");
            }

            if (claimModel.ClaimValue == "")
            {
                return BadRequest("404");
            }
            
            var claim = new Claim(claimModel.ClaimName, claimModel.ClaimValue, ClaimValueTypes.String);

            var result = _um.AddClaimAsync(user, claim).Result;
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpDelete]
        [Route("api/role/user/{id}")]
        public IActionResult DeleteClaimFromUser(string id, ClaimModel claimModel)
        {
            var user = _um.FindByIdAsync(id).Result;
            if (user == null)
            {
                return BadRequest("101");
            }
            
            if (claimModel.ClaimName == "")
            {
                return BadRequest("403");
            }

            if (claimModel.ClaimValue == "")
            {
                return BadRequest("404");
            }

            var claim = new Claim(claimModel.ClaimName, claimModel.ClaimValue);
            var result = _um.RemoveClaimAsync(user,claim).Result;
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        
        //Configures role claims
        [HttpGet]
        [Route("api/claim/role/{id}")]
        public IActionResult GetRoleClaims(string id)
        {
            var role = _rm.FindByNameAsync(id).Result;
            if (role == null)
            {
                return NotFound();
            }

            var claims = _rm.GetClaimsAsync(role).Result;
            var claimList = new List<ClaimModel>();
            foreach (var claim in claims)
            {
                claimList.Add(new ClaimModel{ClaimName =  claim.Type, ClaimValue = claim.Value});
            }
            return Ok(claimList);
        }

        [HttpPost]
        [Route("api/claim/role/{id}")]
        public IActionResult SetRoleClaims(string id, ClaimList claimList)
        {
            var role = _rm.FindByNameAsync(id).Result;
            if (role == null)
            {
                return NotFound();
            }

            foreach (var claim in claimList.claims)
            {
                var tClaim = new Claim(claim.ClaimName,claim.ClaimValue,ClaimValueTypes.String);
                _rm.AddClaimAsync(role, tClaim).Wait();
            }
            return Ok();
        }

        [HttpPut]
        [Route("api/claim/role/{id}")]
        public IActionResult PutRoleToClaim(string id, ClaimModel claimModel)
        {
            var role = _rm.FindByIdAsync(id).Result;
            if (role == null)
            {
                return BadRequest("401");
            }

            if (claimModel.ClaimName == "")
            {
                return BadRequest("403");
            }

            if (claimModel.ClaimValue == "")
            {
                return BadRequest("404");
            }
            
            var claim = new Claim(claimModel.ClaimName, claimModel.ClaimValue, ClaimValueTypes.String);

            var result = _rm.AddClaimAsync(role, claim).Result;
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpDelete]
        [Route("api/claim/role{id}")]
        public IActionResult DeleteClaimFromRole(string id, ClaimModel claimModel)
        {
            var role = _rm.FindByIdAsync(id).Result;
            if (role == null)
            {
                return BadRequest("401");
            }
            
            if (claimModel.ClaimName == "")
            {
                return BadRequest("403");
            }

            if (claimModel.ClaimValue == "")
            {
                return BadRequest("404");
            }

            var claim = new Claim(claimModel.ClaimName, claimModel.ClaimValue);
            var result = _rm.RemoveClaimAsync(role,claim).Result;
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
    }
    
    public class RoleList
    {
        public List<string> roles { get; set; }
    }
    public class ClaimList
    {
        public List<ClaimModel> claims { get; set; }
    }
}
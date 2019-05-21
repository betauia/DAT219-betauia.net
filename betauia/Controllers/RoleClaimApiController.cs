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

        [HttpGet]
        [Route("api/claim/user/get/{id}")]
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
        [Route("api/claim/user/set/{id}")]
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

        [HttpGet]
        [Route("api/role/user/get/{id}")]
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
        [Route("api/role/user/set/{id}")]
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

        [HttpGet]
        [Route("api/claim/role/get/{id}")]
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
        [Route("api/claim/role/set/{id}")]
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
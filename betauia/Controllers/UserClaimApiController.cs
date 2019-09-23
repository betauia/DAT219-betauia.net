using System.Collections.Generic;
using System.Security.Claims;
using betauia.Data;
using betauia.Models;
using betauia.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace betauia.Controllers
{
    [ApiController]
    [Authorize]
    public class UserClaimApiController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _um;
        private readonly RoleManager<IdentityRole> _rm;
        private readonly TokenFactory _tf;

        public UserClaimApiController(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _um = userManager;
            _rm = roleManager;
            _db = db;
            _tf = new TokenFactory(_um, _rm);
        }

        [Authorize("Claims.read")]
        [Authorize("Account.read")]
        [HttpGet]
        [Route("api/allclaims/user/{id}")]
        public IActionResult GetAllClaims(string id)
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

        [Authorize("Claims.read")]
        [Authorize("Account.read")]
        [HttpGet]
        [Route("api/claim/user/{id}")]
        public IActionResult GetUserClaims(string id)
        {
            var user = _um.FindByIdAsync(id).Result;
            if (user == null) return NotFound();

            var claims = _um.GetClaimsAsync(user).Result;
            return Ok(claims);
        }


        [Authorize("Claims.write")]
        [Authorize("Account.write")]
        [HttpPut]
        [Route("api/claim/user/{id}")]
        public IActionResult AddClaimToUser(string id, ClaimModel claimModel)
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

        [Authorize("Claims.write")]
        [Authorize("Account.write")]
        [HttpDelete]
        [Route("api/claim/user/{id}")]
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
            var result = _um.RemoveClaimAsync(user, claim).Result;
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
}

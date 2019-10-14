using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
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
        private readonly ITokenFactory _tf;
        private readonly ITokenManager _tokenManager;

        public UserClaimApiController(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,ITokenManager tokenManager,ITokenFactory tf)
        {
            _um = userManager;
            _rm = roleManager;
            _db = db;
            _tf = tf;
            _tokenManager = tokenManager;
        }

        [Authorize("Claims.read")]
        [Authorize("Account.read")]
        [HttpGet]
        [Route("api/allclaims/user/{id}")]
        public async Task<IActionResult> GetAllClaims(string id)
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
        public async Task<IActionResult> GetUserClaims(string id)
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
        public async Task<IActionResult> AddClaimToUser(string id, ClaimModel claimModel)
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
                await _tokenManager.RemoveUserTokensAsync(id);
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
        public async Task<IActionResult> DeleteClaimFromUser(string id, ClaimModel claimModel)
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
                await _tokenManager.RemoveUserTokensAsync(id);
                return Ok();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

    }
}

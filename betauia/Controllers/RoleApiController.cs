using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using betauia.Data;
using betauia.Models;
using betauia.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace betauia.Controllers
{
    [ApiController]
    [Route("api/role")]
    public class RoleApiController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _um;
        private readonly RoleManager<IdentityRole> _rm;
        private readonly TokenFactory _tf;

        public RoleApiController(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _um = userManager;
            _rm = roleManager;
            _db = db;
            _tf = new TokenFactory(_um, _rm);
        }
        
        //get all roles
        [HttpGet]
        public IActionResult GetAllRoles()
        {
            var roles = _rm.Roles.ToList();
            return Ok(roles);
        }

        //get role
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetRole(string id)
        {
            var role = _rm.FindByIdAsync(id).Result;
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpPost]
        [Route("{name}")]
        public IActionResult AddRole(string name)
        {
            var role = new IdentityRole(name);
            _rm.CreateAsync(role).Wait();
            return Ok();
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteRole(string id)
        {
            var role = _rm.FindByIdAsync(id).Result;
            if (role == null) return NotFound();

            var result = _rm.DeleteAsync(role).Result;
            if (result.Succeeded) return Ok();
            return BadRequest(result.Errors);
        }
        
        
    }
}
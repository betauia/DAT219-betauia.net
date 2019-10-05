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
    public class ClaimUserApiController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _um;
        private readonly RoleManager<IdentityRole> _rm;

        public ClaimUserApiController(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, ITokenManager tokenManager)
        {
            _um = userManager;
            _rm = roleManager;
            _db = db;
        }
    }
}

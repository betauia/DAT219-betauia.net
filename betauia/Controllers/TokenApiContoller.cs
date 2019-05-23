using betauia.Data;
using betauia.Models;
using betauia.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace betauia.Controllers
{
    public class TokenApiContoller : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _um;
        private readonly RoleManager<IdentityRole> _rm;
        private readonly TokenFactory _tf;
        
        public TokenApiContoller(ApplicationDbContext db, UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm)
        {
            _db = db;
            _um = um;
            _rm = rm;
            _tf = new TokenFactory(um,rm);
        }
        
        [Route("/token")]
        [HttpPost]
        public IActionResult Create([FromBody]Loginmodel loginmodel)
        {
            var user = _um.FindByNameAsync(loginmodel.Username).Result;
            if (_um.CheckPasswordAsync(user, loginmodel.Password).Result)
            {
                   var token = _tf.GetToken(user);
                   return Ok(token);
            }
            return BadRequest();
        }
    }
}
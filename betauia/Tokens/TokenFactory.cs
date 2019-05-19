using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using betauia.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace betauia.Tokens
{
    public class TokenFactory
    {
        private readonly UserManager<ApplicationUser> _um;
        private readonly RoleManager<IdentityRole> _rm;

        public TokenFactory(UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm)
        {
            _um = um;
            _rm = rm;
        }
        public string GetToken(ApplicationUser user)
        {
            var claims = _um.GetClaimsAsync(user).Result;
            claims.Add(new Claim(JwtRegisteredClaimNames.NameId, user.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            var roles = _um.GetRolesAsync(user).Result;
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role,role));
                var roletype = _rm.FindByNameAsync(role).Result;
                var claimsFromRole = _rm.GetClaimsAsync(roletype).Result;
                foreach (var claim in claimsFromRole)
                {
                    claims.Add(claim);
                }
            }
            
            var token2 = new JwtSecurityToken(
                issuer: "betauia",
                audience: "https://localhost:5001",
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(28),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("abcdefghijklmonopg")), SecurityAlgorithms.HmacSha256)
                );
            
            return new JwtSecurityTokenHandler().WriteToken(token2);
        }
    }
}
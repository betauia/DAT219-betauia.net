using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using betauia.Data;
using betauia.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace betauia.Tokens
{
    public class TokenFactory : ITokenFactory
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _um;
        private readonly RoleManager<IdentityRole> _rm;
        private readonly ITokenManager _tokenManager;

        public TokenFactory(UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm, ITokenManager tokenManager,
            ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _um = um;
            _rm = rm;
            _tokenManager = tokenManager;
        }
        public async Task<string> GetTokenAsync(ApplicationUser user)
        {
            var claims = _um.GetClaimsAsync(user).Result;
            //claims.Add(new Claim(JwtRegisteredClaimNames.NameId, user.UserName));
            claims.Add(new Claim("id",user.Id));
            //claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
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

            var token = new JwtSecurityToken(
                issuer: "betauia",
                audience: "https://localhost:5001",
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("abcdefghijklmonopg")), SecurityAlgorithms.HmacSha256)
                );

            var stoken = new JwtSecurityTokenHandler().WriteToken(token);
            await _tokenManager.AddUserTokenAsync(user.Id, stoken);
            return stoken;
        }
        public async Task<string> GetEmailVerificationTokenAsync(ApplicationUser user)
        {
            var claims = new Claim[]
            {
                new Claim("id", user.Id),
                new Claim("EmailVerification", "true")
            };

            var token = new JwtSecurityToken(
                issuer:"betauia",
                audience:"https://localhost:5001",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("abcdefghijklmonopg")), SecurityAlgorithms.HmacSha256)
            );
            var stoken = new JwtSecurityTokenHandler().WriteToken(token);
            await _tokenManager.AddTokenAsync(stoken);
            //Thread t = new Thread(()=>DeleteToken.Delete(_tokenManager,stoken,(long)1000*60*60*24));
            //t.Start();
            return stoken;
        }


        public async Task<string> GetPasswordResetTokenAsync(ApplicationUser user)
        {
            var claims = new Claim[]
            {
                new Claim("id", user.Id),
                new Claim("password.reset", "true")
            };

            var token = new JwtSecurityToken(
                issuer:"betauia",
                audience:"https://localhost:5001",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("abcdefghijklmonopg")), SecurityAlgorithms.HmacSha256)
            );
            var stoken = new JwtSecurityTokenHandler().WriteToken(token);
            await _tokenManager.AddTokenAsync(stoken);
            //Thread t = new Thread(()=>DeleteToken.Delete(_tokenManager,stoken,(long)1000*30));
            //t.Start();
            return stoken;
        }
        public async Task<string> GetEventSignupTokenAsync(string id)
        {
          var claims = new Claim[]
          {
            new Claim("id", id),
            new Claim("eventemail","true",ClaimValueTypes.String),
          };

          var token = new JwtSecurityToken(
            issuer:"betauia",
            audience:"https://localhost:5001",
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("abcdefghijklmonopg")), SecurityAlgorithms.HmacSha256)
          );
          var stoken = new JwtSecurityTokenHandler().WriteToken(token);
          await _tokenManager.AddTokenAsync(stoken);
          //Thread t = new Thread(()=>DeleteToken.Delete(_tokenManager,stoken,(long)1000*60));
          //t.Start();
          return stoken;
        }

        public async Task<string> GetRefreshTokenAsync(string userid)
        {
            var newRefreshToken = new RefreshToken
            {
                UserId = userid,
                Token = Guid.NewGuid().ToString(),
                IssuedUtc = DateTime.Now.ToUniversalTime(),
                ExpiresUtc = DateTime.Now.AddMonths(3).ToUniversalTime(),
            };
            _dbContext.RefreshTokens.Add(newRefreshToken);
            await _dbContext.SaveChangesAsync();
            return newRefreshToken.Token;
        }
    }
}

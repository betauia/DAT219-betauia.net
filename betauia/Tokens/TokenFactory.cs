using System;
using System.Collections.Generic;
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
                expires: DateTime.Now.AddDays(28),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("abcdefghijklmonopg")), SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string AuthenticateUser(string token)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("abcdefghijklmonopg"));
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            List<Exception> validationFailures = null;
            SecurityToken validateToken;
            var validator = new JwtSecurityTokenHandler();

            TokenValidationParameters validationParameters = new TokenValidationParameters();
            validationParameters.ValidateIssuer = true;
            validationParameters.ValidIssuer = "betauia";

            validationParameters.ValidateAudience = true;
            validationParameters.ValidAudience = "https://localhost:5001";

            validationParameters.IssuerSigningKey = key;
            validationParameters.ValidateIssuerSigningKey = true;

            if (validator.CanReadToken(token))
            {
                ClaimsPrincipal principal;
                try
                {
                    principal = validator.ValidateToken(token, validationParameters, out validateToken);
                    if (principal.HasClaim(c => c.Type == "id"))
                    {
                        return principal.Claims.Where(c => c.Type == "id").First().Value;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return string.Empty;
        }

        public string GetEmailVerificationToken(ApplicationUser user)
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
                expires: DateTime.Now.AddHours(2),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("abcdefghijklmonopg")), SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string VerifyEmail(string token)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("abcdefghijklmonopg"));
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            List<Exception> validationFailures = null;
            SecurityToken validateToken;
            var validator = new JwtSecurityTokenHandler();

            TokenValidationParameters validationParameters = new TokenValidationParameters();
            validationParameters.ValidateIssuer = true;
            validationParameters.ValidIssuer = "betauia";

            validationParameters.ValidateAudience = true;
            validationParameters.ValidAudience = "https://localhost:5001";

            validationParameters.IssuerSigningKey = key;
            validationParameters.ValidateIssuerSigningKey = true;

            if (validator.CanReadToken(token))
            {
                ClaimsPrincipal principal;
                try
                {
                    principal = validator.ValidateToken(token, validationParameters, out validateToken);
                    if (principal.HasClaim(c => c.Type == "EmailVerification"))
                    {
                        if (principal.Claims.Where(c => c.Type == "EmailVerification").First().Value == "true")
                        {
                            if (principal.HasClaim(c => c.Type == "id"))
                            {
                                return principal.Claims.Where(e => e.Type == "id").First().Value;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return string.Empty;
        }
    }
}

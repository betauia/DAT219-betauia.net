using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using betauia.Models;
using Microsoft.IdentityModel.Tokens;

namespace betauia.Tokens
{
    public class TokenFactory
    {
        public string GetToken(string user)
        {
            var claims = new Claim[]
            {
                //new Claim(ClaimTypes.Name, user),
                new Claim(JwtRegisteredClaimNames.NameId, user), 
                new Claim(JwtRegisteredClaimNames.Email, "test@email.com"),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };
            
            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("abcdefghijklmonopg")),
                    SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));


            var claims2 = new Claim[]
            {
                new Claim(ClaimTypes.Role,"Admin"), 
                new Claim(JwtRegisteredClaimNames.NameId, user),
                new Claim(JwtRegisteredClaimNames.Email, "test@email.com")
            };
            
            var token2 = new JwtSecurityToken(
                issuer: "betauia",
                audience: "https://localhost:5001",
                claims: claims2,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(28),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("abcdefghijklmonopg")), SecurityAlgorithms.HmacSha256)
                );
            
            return new JwtSecurityTokenHandler().WriteToken(token2);
        }
    }
}
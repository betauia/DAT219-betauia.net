using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace betauia.Tokens
{
  public class TokenVerifier : ITokenVerifier
  {
    private readonly ITokenManager _tokenManager;

    public TokenVerifier(ITokenManager tokenManager)
    {
      _tokenManager = tokenManager;
    }

    public async Task<string> GetTokenUser(string token)
    {
      if (await _tokenManager.IsActiveAsync(token) == false) return null;
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

    public async Task<string> VerifyEventSignupToken(string token)
    {
      if (await _tokenManager.IsActiveAsync(token) == false) return null;
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
      await _tokenManager.DeactivateAsync(token);
      if (validator.CanReadToken(token))
      {
        ClaimsPrincipal principal;
        try
        {
          principal = validator.ValidateToken(token, validationParameters, out validateToken);
          if (principal.HasClaim(c => c.Type == "eventemail"))
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
    
    public async Task<string> VerifyEmailAsync(string token)
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
        await _tokenManager.DeactivateAsync(token);
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
    
    public async Task<string> VerifyPasswordResetTokenAsync(string token)
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
        await _tokenManager.DeactivateAsync(token);
        ClaimsPrincipal principal;
        try
        {
          principal = validator.ValidateToken(token, validationParameters, out validateToken);
          if (principal.HasClaim(c => c.Type == "password.reset"))
          {
            if (principal.Claims.Where(c => c.Type == "password.reset").First().Value == "true")
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

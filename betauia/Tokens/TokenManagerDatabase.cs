using System;
using System.Linq;
using System.Threading.Tasks;
using betauia.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace betauia.Tokens
{
  public class TokenManagerDatabase : ITokenManager
  {
    private readonly ApplicationDbContext db;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public TokenManagerDatabase(ApplicationDbContext dbContext,IHttpContextAccessor httpContextAccessor)
    {
      db = dbContext;
      _httpContextAccessor = httpContextAccessor;
    }

    public async Task AddUserTokenAsync(string id,string token)
    {
      var to = await db.TokenUserModels.FindAsync(id);
      if (to != null)
      {
        to.Token = token;
        db.TokenUserModels.Update(to);
        await db.SaveChangesAsync();
        return;
      }
      var t = new TokenUserModel
      {
        User = id,
        Token = token
      };
      await db.TokenUserModels.AddAsync(t);
      await db.SaveChangesAsync();
    }

    public async Task RemoveUserTokensAsync(string id)
    {
      db.TokenUserModels.RemoveRange(db.TokenUserModels.Where(a=>a.User==id));
      await db.SaveChangesAsync();
    }

    public async Task<bool> IsActiveAsync(string token)
    {
      if (token == string.Empty) return true;
      return db.TokenUserModels.Where(a => a.Token == token).Any() ;
    }

    public async Task AddTokenAsync(string token)
    {
      var t = new TokenUserModel
      {
        User = token,
        Token = "true",
      };
      await db.TokenUserModels.AddAsync(t);
      await db.SaveChangesAsync();
    }

    public async Task DeactivateAsync(string token)
    {
      db.TokenUserModels.Remove(db.TokenUserModels.Where(a => a.Token == token).ToList()[0]);
      await db.SaveChangesAsync();
    }

    public async Task DeactivateCurrentAsync()
    {
      await DeactivateAsync(GetCurrentAsync());
    }

    public async Task<bool> IsCurrentActiveAsync()
    {
      return await IsActiveAsync(GetCurrentAsync());
    }

    private string GetCurrentAsync()
    {
      var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["authorization"];
      return authorizationHeader == StringValues.Empty
        ? string.Empty
        : authorizationHeader.Single().Split(" ").Last();
    }
  }
}

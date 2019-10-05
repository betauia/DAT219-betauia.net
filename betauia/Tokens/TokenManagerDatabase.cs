using System;
using System.Linq;
using System.Threading.Tasks;
using betauia.Data;

namespace betauia.Tokens
{
  public class TokenManagerDatabase : ITokenManager
  {
    private readonly ApplicationDbContext db;

    public TokenManagerDatabase(ApplicationDbContext dbContext)
    {
      db = dbContext;
    }

    public async Task AddUserTokenAsync(string id,string token)
    {
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
      return db.TokenUserModels.Where(a => a.Token == token).Any() ;
    }

    public async Task AddTokenAsync(string token)
    {
      var t = new TokenUserModel
      {
        User = "true",
        Token = token
      };
      await db.TokenUserModels.AddAsync(t);
      await db.SaveChangesAsync();
    }

    public async Task DeactivateAsync(string token)
    {
      db.TokenUserModels.Remove(db.TokenUserModels.Where(a => a.Token == token).ToList()[0]);
      await db.SaveChangesAsync();
    }
  }
}

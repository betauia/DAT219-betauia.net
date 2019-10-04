using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace betauia.Tokens
{
  public class TokenManager : ITokenManager
  {
    private readonly IDistributedCache _cache;

    public TokenManager(IDistributedCache cache)
    {
      _cache = cache;
    }

    public async Task AddUserTokenAsync(string id,string token)
    {
      var el = await _cache.GetStringAsync(id);
      await _cache.SetStringAsync(id, el + token + ",");
      await _cache.SetStringAsync(token, "1");
      var t = await _cache.GetStringAsync(id);
      int i = 2;
    }

    public async Task RemoveUserTokensAsync(string id)
    {
      var tokenList = _cache.GetStringAsync(id).Result.Split(",");
      foreach (var token in tokenList)
      {
        await _cache.RemoveAsync(token);
      }
    }

    public async Task<bool> IsActiveAsync(string token)
    {
      return await _cache.GetAsync(token)!=null;
    }

    public async Task AddTokenAsync(string token)
    {
      await _cache.SetStringAsync(token, "1");
    }

    public async Task DeactivateAsync(string token)
    {
      await _cache.RemoveAsync(token);
    }
  }
}

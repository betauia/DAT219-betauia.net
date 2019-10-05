using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Primitives;

namespace betauia.Tokens
{
  public class TokenManager : ITokenManager
  {
    private readonly IDistributedCache _cache;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenManager(IDistributedCache cache,IHttpContextAccessor httpContextAccessor)
    {
      _cache = cache;
      _httpContextAccessor = httpContextAccessor;
    }

    public async Task AddUserTokenAsync(string id,string token)
    {
      var el = await _cache.GetStringAsync(id);
      await _cache.SetStringAsync(id, el + token + ",");
      await _cache.SetStringAsync(token, "1");
    }

    public async Task RemoveUserTokensAsync(string id)
    {
      var allTokens = await _cache.GetStringAsync(id);
      var tokenList = allTokens.Split(",");
      foreach (var token in tokenList)
      {
        if (token != string.Empty)
        {
          await _cache.SetStringAsync(token, "0");
        }
      }
    }

    public async Task<bool> IsActiveAsync(string token)
    {
      if (token == string.Empty) return true;
      return await _cache.GetStringAsync(token)=="1";
    }

    public async Task AddTokenAsync(string token)
    {
      await _cache.SetStringAsync(token, "1");
    }

    public async Task DeactivateAsync(string token)
    {
      await _cache.SetStringAsync(token,"0");
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
      var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
      return authorizationHeader == StringValues.Empty
        ? string.Empty
        : authorizationHeader.Single().Split(" ").Last();
    }
  }
}

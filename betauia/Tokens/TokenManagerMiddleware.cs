using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace betauia.Tokens
{
  public class TokenManagerMiddleware : IMiddleware
  {
    private readonly ITokenManager _tokenManager;

    public TokenManagerMiddleware(ITokenManager tokenManager)
    {
      _tokenManager = tokenManager;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
      if (await _tokenManager.IsCurrentActiveAsync())
      {
        await next(context);
        return;
      }
      context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
    }
  }
}

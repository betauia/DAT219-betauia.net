using System.Threading.Tasks;

namespace betauia.Tokens
{
  public interface ITokenManager
  {
    Task AddUserTokenAsync(string token, string id);
    Task RemoveUserTokensAsync(string id);
    Task<bool> IsActiveAsync(string token);
    Task AddTokenAsync(string token);
    Task DeactivateAsync(string token);
  }
}

using System.Threading.Tasks;

namespace betauia.Tokens
{
  public interface ITokenVerifier
  {
    Task<string> GetTokenUser(string token);
    Task<string> VerifyEventSignupToken(string token);
  }
}

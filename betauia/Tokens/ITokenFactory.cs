using System.Threading.Tasks;
using betauia.Models;

namespace betauia.Tokens
{
    public interface ITokenFactory
    {
        Task<string> GetTokenAsync(ApplicationUser user);
        Task<string> GetEmailVerificationTokenAsync(ApplicationUser user);
        Task<string> GetPasswordResetTokenAsync(ApplicationUser user);
        Task<string> GetEventSignupTokenAsync(string id);
        Task<string> GetRefreshTokenAsync(string userid);
    }
}
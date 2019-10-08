using System.Threading;
using System.Threading.Tasks;

namespace betauia.Tokens
{
    public class DeleteToken
    {
        public async static void Delete(ITokenManager tokenManager, string token,long delay)
        {
            while (delay > 0)
            {
                var currentDelay = delay > int.MaxValue ? int.MaxValue : (int) delay;
                await Task.Delay(currentDelay);
                delay -= currentDelay;
            }
            tokenManager.DeactivateAsync(token).Wait();
        }
    }
}
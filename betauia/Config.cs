using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace betauia
{
    public class Config : IClientStore
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("myAPI", "my api")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client()
                {
                    ClientId = "myClient",
                    ClientName = "My Custom Client",
                    AccessTokenLifetime = 60 * 60 * 24,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowAccessTokensViaBrowser = true,
                    RequireClientSecret = false,
                    AllowedScopes =
                    {
                        "myAPI"
                    }
                }
            };
        }
        
        public Task<Client> FindClientByIdAsync(string clientId)
        {
            return Task.FromResult(GetClients().FirstOrDefault(c => c.ClientId == clientId));
        }
        
        public static List<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }
    }
}
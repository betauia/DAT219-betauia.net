using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using IdentityServer4.Services;

namespace betauia
{
    public class Config : IClientStore
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            var api = new ApiResource("api1", "my api");
            api.ApiSecrets.Add(new Secret("secret".Sha256()));
            return new List<ApiResource>
            {
                api,
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
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AccessTokenLifetime = 60 * 60 * 24,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowAccessTokensViaBrowser = true,
                    RequireClientSecret = false,
                    AllowedScopes =
                    {
                        IdentityServer4.IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServer4.IdentityServerConstants.StandardScopes.Email,
                        IdentityServer4.IdentityServerConstants.StandardScopes.Profile,
                        IdentityServer4.IdentityServerConstants.StandardScopes.Address,
                        "api1"
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
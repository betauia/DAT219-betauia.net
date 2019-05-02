using System.Collections;
using System.Collections.Generic;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Models;

namespace betauia
{
    public static class MyApiResources
    {
        public static IEnumerable<ApiResource> GetAllResources()
        {
            return new[]
            {
                new ApiResource("myApi", "myApiSet", new[] {JwtClaimTypes.Name, JwtClaimTypes.Role})
            };
        }
    }
}
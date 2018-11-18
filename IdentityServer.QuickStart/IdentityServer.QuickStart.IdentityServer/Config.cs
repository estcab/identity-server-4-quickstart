using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer.QuickStart.IdentityServer
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            yield return new ApiResource("api_1", "My API");
        }

        public static IEnumerable<Client> GetClients()
        {
            yield return new Client
            {
                ClientId = "client_1",

                AllowedGrantTypes = GrantTypes.ClientCredentials,

                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                AllowedScopes = { "api_1" }

            };
        }
    }
}

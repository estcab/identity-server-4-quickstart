﻿using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace IdentityServer.QuickStart.ResourceOwnerClient
{
    class Program
    {
        public static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

        public static async Task MainAsync(string[] args)
        {
            // discover endpoints from metadata
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "client_2", "secret");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("bob", "password", "api_1");

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);

            // call api
            using (var client = new HttpClient())
            {
                client.SetBearerToken(tokenResponse.AccessToken);

                var response = await client.GetAsync("http://localhost:5001/api/identity");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.StatusCode);
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(JArray.Parse(content));
                }

            }


            Console.ReadLine();
        }
    }

}

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CrystalProcess.API.Tests.Utils
{
    public static class Utilities<T> where T : class
    {
        public static HttpClient CreateClient()
        {
            var factory = new CustomWebApplicationFactory<T>();
            var httpClient = factory.CreateClient();
            return httpClient;
        }

        public static async Task<string> RegisterandLoginUser(string password, string userName, HttpClient client)
        {
            var request = new
            {
                Url = "api/auth/register",
                Body = new
                {
                    username = userName,
                    password = password
                }
            };

            var loginRequest = new
            {
                Url = "api/auth/login",

                Body = new
                {
                    username = userName,
                    password = password
                }
            };
            var response = await client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var stringContent = ContentHelper.GetStringContent(loginRequest.Body);
            var tokenResponse = await client.PostAsync(loginRequest.Url, stringContent);
            var jsonCompactSerializedString = await tokenResponse.Content.ReadAsStringAsync();
            return jsonCompactSerializedString;
        }

        public static string StripTokenValue(string token)
        {
            var tokenValue = "";
            JObject jObject = JObject.Parse(token);
            tokenValue = (string)jObject.SelectToken("token");
            return tokenValue;
        }
    }
}

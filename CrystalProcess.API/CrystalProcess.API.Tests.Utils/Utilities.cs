using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

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
    }
}

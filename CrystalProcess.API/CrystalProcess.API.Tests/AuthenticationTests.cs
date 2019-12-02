using System.Net;
using System.Threading.Tasks;
using CrystalProcess.API.Tests.Utils;
using Xunit;

namespace CrystalProcess.API.Tests
{
    public class AuthenticationTests:IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private CustomWebApplicationFactory<Startup> _factory = new CustomWebApplicationFactory<Startup>();

        [Fact]
        public async Task Can_not_access_stages_controller_with_out_being_loggedIn()
        {
            //arrange 
            var client = Utilities<Startup>.CreateClient();
            var request = new
            {
                Url="api/stages"
            };
            
            //act
            var result = await client.GetAsync(request.Url);
            
            //assert
            Assert.Equal(HttpStatusCode.Unauthorized,result.StatusCode);
        }

    }
}
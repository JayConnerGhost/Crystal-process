using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrystalProcess.API.Tests.Utils;
using CrystalProcess.Models;
using Newtonsoft.Json;
using Xunit;

namespace CrystalProcess.API.Tests
{
    public class StagesTests:IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private CustomWebApplicationFactory<Startup> _factory = new CustomWebApplicationFactory<Startup>();

        [Fact]
        public async Task Can_get_swim_lanes()
        {
            //arrange
            var swimLaneExpectedCount=2;
            var client = Utilities<Startup>.CreateClient();
            //set up swim lanes -calling controller 
            //
            //2x posts
            var postRequest1 = new
            {
                Url = "api/stages",
                Body = new
                {
                    Title = "Test stage 1",
                    Order = 0,
                }
            };

            var postRequest2 = new
            {
                Url = "api/stages",
                Body = new
                {
                    Title = "Test stage 2",
                    Order = 1,
                }
            };

            await client.PostAsync(postRequest1.Url, ContentHelper.GetStringContent(postRequest1.Body));
            await client.PostAsync(postRequest2.Url, ContentHelper.GetStringContent(postRequest2.Body));

            //act
            var request=new
            {
                Url="api/stages"
            };
            var response= await client.GetAsync(request.Url);


            //assert
            var lanes= JsonConvert.DeserializeObject<List<Stage>>(await response.Content.ReadAsStringAsync()); 
            Assert.Equal(swimLaneExpectedCount,lanes.Count);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrystalProcess.API.Controllers;
using CrystalProcess.API.Responses;
using CrystalProcess.Models;
using CrystalProcess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NSubstitute;
using Xunit;

namespace CrystalProcess.Controllers.Tests
{
    public class StageTests
    {

        [Fact]
        public async Task Stages_are_returned_in_order()
        {
            //arrange 
            var repositoryFake=NSubstitute.Substitute.For<IStageRepository>();
            var logger= NSubstitute.Substitute.For<ILogger<StagesController>>();
            var sut=new StagesController(repositoryFake,logger);
            repositoryFake.Get().Returns(new List<Stage>
            {
                new Stage{Id=2,Order=2,Title="test 2"},
                new Stage{Id=1,Order=0,Title="test 1"},
                new Stage{Id=3,Order=1,Title="test 3"},
            });


            //act
            var result = await sut.Get();

            //Assert
            var receivedStages = result as OkObjectResult;
            var data = (List<StageResponse>) receivedStages.Value;
            Assert.Equal(0,data[0].Order);
            Assert.Equal(1,data[1].Order);
            Assert.Equal(2, data[2].Order);

        }
    }
}

using System.Collections.Generic;
using System.Linq;
using CrystalProcess.API.Responses;
using CrystalProcess.Models;
using Microsoft.AspNetCore.Mvc;

namespace CrystalProcess.API.Controllers
{
    public class CustomStagesControllerBase:ControllerBase
    {
        protected List<StageResponse> ConvertStageResponses(List<Stage> stages)
        {
            return stages.Select(ConvertResponse).ToList();
        }
        protected StageResponse ConvertResponse(Stage entity)
        {
            return new StageResponse()
            {
                Order = entity.Order,
                Title = entity.Title,
                Id = entity.Id
            };
        }
    }
}
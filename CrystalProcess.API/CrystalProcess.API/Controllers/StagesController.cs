using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrystalProcess.API.Requests;
using CrystalProcess.API.Responses;
using CrystalProcess.Models;
using CrystalProcess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CrystalProcess.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StagesController : CustomControllerBase
    {
        private readonly IStageRepository _repository;
        private readonly ILogger<StagesController> _logger;

        public StagesController(IStageRepository repository, ILogger<StagesController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Stage> stages=null;
            try
            {
                var result = await _repository.Get();
                stages =  new List<Stage>(result.OrderBy(x=>x.Order));
            }
            catch (Exception ex)
            {
                _logger.LogError(Guid.NewGuid().ToString(), ex);
            }

            return Ok(ConvertReponses(stages));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewStageRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = new Stage() {Title = request.Title, Order = request.Order};
            try
            {
                await _repository.Add(entity);
                
            }
            catch (Exception ex)
            {
               _logger.LogError(Guid.NewGuid().ToString(),ex);
            }

            return Created(Url.RouteUrl(entity.Id),ConvertResponse(entity));
        }

        
    }

    public class CustomControllerBase:ControllerBase
    {
        protected List<StageResponse> ConvertReponses(List<Stage> stages)
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
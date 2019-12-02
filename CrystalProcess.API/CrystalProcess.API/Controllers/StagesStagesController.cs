using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrystalProcess.API.Requests;
using CrystalProcess.Models;
using CrystalProcess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CrystalProcess.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StagesStagesController : CustomStagesControllerBase
    {
        private readonly IStageRepository _repository;
        private readonly ILogger<StagesStagesController> _logger;

        public StagesStagesController(IStageRepository repository, ILogger<StagesStagesController> logger)
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

            return Ok(ConvertStageResponses(stages));
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
}
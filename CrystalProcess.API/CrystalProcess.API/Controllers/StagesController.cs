using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrystalProcess.API.Requests;
using CrystalProcess.API.Responses;
using CrystalProcess.Data;
using CrystalProcess.Models;
using CrystalProcess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CrystalProcess.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StagesController : ControllerBase
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
                stages = (List<Stage>) await _repository.Get();
            }
            catch (Exception ex)
            {
                _logger.LogError(Guid.NewGuid().ToString(), ex);
            }

            return Ok(stages);
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
                _repository.Add(entity);
                
            }
            catch (Exception ex)
            {
               _logger.LogError(Guid.NewGuid().ToString(),ex);
            }

            return Created(Url.RouteUrl(entity.Id),ConvertResponse(entity));
        }

        private NewStageResponse ConvertResponse(Stage entity)
        {
            return new NewStageResponse()
            {
                Order = entity.Order,
                Title=entity.Title,
                Id = entity.Id
            };
        }
    }
}
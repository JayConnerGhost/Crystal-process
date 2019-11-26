using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrystalProcess.API.Requests;
using CrystalProcess.API.Responses;
using CrystalProcess.Data;
using CrystalProcess.Models;
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
        private readonly ApplicationDbContext _context;
        private readonly ILogger<StagesController> _logger;

        public StagesController(ApplicationDbContext context, ILogger<StagesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Stage> stages=null;
            try
            {
                stages = await _context.Stages.ToListAsync();
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
                _context.Stages.Add(entity);
                await _context.SaveChangesAsync();
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
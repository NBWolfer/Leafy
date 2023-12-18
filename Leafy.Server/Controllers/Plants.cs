using Leafy.Application.Features.Commands.PlantCommands;
using Leafy.Application.Features.Queries.PlantQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Leafy.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Plants : ControllerBase
    {
        private IMediator _mediator;

        public Plants(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> PlantList()
        {
            var results = await _mediator.Send(new GetPlantQuery());
            
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> PlantById(int id)
        {
            var result = await _mediator.Send(new GetPlantByIdQuery(id));
            string resultjson = "{data:{Id:"+result.Id+",Name:"+result.Name+",LatinName:"+result.LatinName+",DiseaseId:"+result.DiseaseId+",Description:"+result.Description+"}}";
            return Ok(resultjson);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlant(CreatePlantCommand command)
        {
            await _mediator.Send(command);
            return Ok("Plant Created!");
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePlant(int id)
        {
            await _mediator.Send(new RemovePlantCommand(id));
            return Ok("Plant Deleted!");
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePlant(UpdatePlantCommand command)
        {
            await _mediator.Send(command);
            return Ok("Plant Updated!");
        }
    }
}

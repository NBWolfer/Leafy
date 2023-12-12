using Leafy.Application.Features.Commands.DiseaseCommands;
using Leafy.Application.Features.Queries.DiseaseQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Leafy.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Diseases : ControllerBase
    {
        private IMediator _mediator;

        public Diseases(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> DiseaseList()
        {
            var result = await _mediator.Send(new GetDiseaseQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> DiseaseById(int id)
        {
            var result = await _mediator.Send(new GetDiseaseByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDisease(CreateDiseaseCommand command)
        {
            await _mediator.Send(command);
            return Ok("Disease Created!");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDisease(int id)
        {
            await _mediator.Send(new RemoveDiseaseCommand(id));
            return Ok("Disease Deleted!");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDisease(UpdateDiseaseCommand command)
        {
            await _mediator.Send(command);
            return Ok("Disease Updated!");
        }
    }
}

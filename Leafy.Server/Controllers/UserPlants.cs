using Leafy.Application.Features.Commands;
using Leafy.Application.Features.Commands.UserPlantCommands;
using Leafy.Application.Features.Queries.UserPlantQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Leafy.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPlants : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserPlants(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("UserPlants")]
        public async Task<IActionResult> UserPlantList()
        {
            var result = await _mediator.Send(new GetUserPlantQuery());

            return Ok(result);
        }

        [HttpGet("UserPlants/{id}")]
        public async Task<IActionResult> UserPlantById(int id)
        {
            var result = await _mediator.Send(new GetUserPlantByIdQuery(id));
            return Ok(result);
        }

        [HttpGet("UserPlantsExpanded/")]
        public async Task<IActionResult> UserPlantExpanded()
        {
            var result = await _mediator.Send(new GetUserPlantExpandedQuery());
            return Ok(result);
        }

        [HttpGet("UserPlantsExpanded/{id}")]
        public async Task<IActionResult> UserPlantByIdExpanded(int id)
        {
            var result = await _mediator.Send(new GetUserPlantByIdExpandedQuery(id));
            return Ok(result);
        }

        [HttpPost("UserPlants")]
        public async Task<IActionResult> CreateUserPlant(CreateUserPlantCommand command)
        {
            await _mediator.Send(command);
            return Ok("UserPlant Created!");
        }

        [HttpDelete("UserPlants")]
        public async Task<IActionResult> DeleteUserPlant(int id)
        {
            await _mediator.Send(new RemoveUserPlantCommand(id));
            return Ok("UserPlant Deleted!");
        }
    }
}

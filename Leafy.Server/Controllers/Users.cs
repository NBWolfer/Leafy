using Leafy.Application.Features.Commands.UserCommands;
using Leafy.Application.Features.Queries.UserQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Leafy.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Users : ControllerBase
    {
        private readonly IMediator _mediator;

        public Users(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Policy = "adminOnly")]
        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            var users = await _mediator.Send(new GetUserQuery());
            if (users == null)
                return NotFound("Kullanıcılar getirilemedi!");
            return Ok(users);
        }

        [Authorize(Policy = "admin-user")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(id));
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
        {
            await _mediator.Send(command);
            return Ok("User created!");
        }

        [Authorize(Policy = "admin-user")]
        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserCommand command)
        {
            await _mediator.Send(command);
            return Ok("User updated!");
        }

        [Authorize(Policy = "admin-user")]
        [HttpDelete]
        public async Task<IActionResult> RemoveUser(int id)
        {
            await _mediator.Send(new RemoveUserCommand(id));
            return Ok("User removed!");
        }
    }
}

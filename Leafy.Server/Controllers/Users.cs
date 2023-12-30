using Leafy.Application.Features.Commands.UserCommands;
using Leafy.Application.Features.Queries.UserQueries;
using Leafy.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace Leafy.Server.Controllers
{
    [Authorize]
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
            try {
                Claim claim = new Claim(ClaimTypes.Role, "user");
                if(User.Claims.Contains(claim))
                {
                    return Unauthorized("User is not authorized!");
                }
                var users = await _mediator.Send(new GetUserQuery());
                if (users is null)
                    return NotFound(JsonSerializer.Serialize(new
                    {
                        Title = "Hata!",
                        Message = "Kullanıcılar getirilemedi!"
                    }));
                return Ok(JsonSerializer.Serialize(new
                {
                    Title = "GetAllUsers",
                    Data = users
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(JsonSerializer.Serialize(new
                {
                    Title = "Hata!",
                    Message = ex.ToString()
                }));
            }
    }

        [Authorize(Policy = "admin-user")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            try { 
                var user = await _mediator.Send(new GetUserByIdQuery(id));
                if (user is null)
                    return NotFound("Kullanıcı bulunamadı!");
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
        {
            try {
                await _mediator.Send(command);
                return Ok("User created!");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Policy = "admin-user")]
        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserCommand command)
        {
            try {
                await _mediator.Send(command);
                return Ok("User updated!");
            }
            catch (Exception ex)
            {
                return Unauthorized("User is not authorized!");
            }
        }

        [Authorize(Policy = "admin-user")]
        [HttpDelete]
        public async Task<IActionResult> RemoveUser(int id)
        {
            try { 
                await _mediator.Send(new RemoveUserCommand(id));
                return Ok("User removed!");
            } 
            catch (Exception ex)
            {
                return Unauthorized("User is not authorized!");
            }
        }
    }
}

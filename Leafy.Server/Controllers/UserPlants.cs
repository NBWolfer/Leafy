using Leafy.Application.Features.Commands;
using Leafy.Application.Features.Commands.UserPlantCommands;
using Leafy.Application.Features.Queries.UserPlantQueries;
using Leafy.Application.Interfaces;
using Leafy.Domain.Entities;
using Leafy.Persistance.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Leafy.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPlants : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private readonly IToken _token;
        private readonly IUserRepository _userRepository;

        public UserPlants(IMediator mediator, IConfiguration configuration, IToken token, IUserRepository userRepository)
        {
            _token = token;
            _configuration = configuration;
            _mediator = mediator;
            _userRepository = userRepository;
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

        [HttpPost("UserPlantsExpanded/")]
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

        [HttpPost("UserPlantByUserExpanded")]
        public async Task<IActionResult> UserPlantByUserExpanded()
        {
            var handler = new JwtSecurityTokenHandler();

            var validateParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetValue<string>("secretKey") ?? "")),
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuer = false,
            };
            var accessToken = Request.Cookies["accessToken"];
            if (accessToken == null)
            {
                var refreshToken = Request.Cookies["refreshToken"];
                if (refreshToken == null)
                {
                    return Ok(new { message = "Tekrar giriş yapın!", status = 401 });
                }
                var principalRefreshToken = handler.ValidateToken(refreshToken, validateParams, out SecurityToken validatedRefreshToken);
                if (validatedRefreshToken == null)
                {
                    return Ok(new { message = "Geçersiz token!", status = 403 });
                }
                else
                {
                    accessToken = _token.GenerateAccessToken(principalRefreshToken.Identity as ClaimsIdentity);
                    Response.Cookies.Append("accessToken", accessToken, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None,
                        Expires = DateTime.UtcNow.AddMinutes(10)
                    });
                    Response.HttpContext.User = principalRefreshToken;
                }
            }

            var principal = handler.ValidateToken(accessToken, validateParams, out SecurityToken validatedToken);
            if (validatedToken != null)
            {
                Response.HttpContext.User = principal;
            }

            string claimEmail = Response.HttpContext.User.FindFirst(ClaimTypes.Email).Value ?? "";
            User userCurrent = await _userRepository.GetUserByEmailAsync(claimEmail);

            var result = await _mediator.Send(new GetUserPlantByUserQuery(userCurrent.Id));
            return Ok(result);
        }
    }
}

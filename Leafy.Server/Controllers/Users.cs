using Leafy.Application.Features.Commands.UserCommands;
using Leafy.Application.Features.Queries.UserQueries;
using Leafy.Application.Interfaces;
using Leafy.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Leafy.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Users : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private readonly IToken _token;

        public Users(IMediator mediator, IConfiguration configuration, IToken token)
        {
            _token = token;
            _mediator = mediator;
            _configuration = configuration;
        }

        [HttpPost("GetUsers")]
        public async Task<IActionResult> UserList()
        {
            try {
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
                if(accessToken == null)
                {
                    var principal = handler.ValidateToken(accessToken, validateParams, out SecurityToken validatedToken);
                    var refreshToken = Request.Cookies["refreshToken"];
                    if (validatedToken != null)
                    {
                        Response.HttpContext.User = principal;
                    }
                    else
                    {
                        return Unauthorized("Bu istek için yetkili değilsiniz!!");
                    }
                    if(refreshToken == null)
                    {
                        return Ok("Bu istek için yetkili değilsiniz!");
                    }
                    var principalRefreshToken = handler.ValidateToken(refreshToken, validateParams, out SecurityToken validatedRefreshToken);
                    if (validatedRefreshToken == null)
                    {
                        return Ok("Tekrardan giriş yapın!");
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

                var users = await _mediator.Send(new GetUserQuery());
                if (users is null)
                    return NotFound(JsonSerializer.Serialize(new
                    {
                        Title = "Hata!",
                        Message = "Kullanıcılar getirilemedi!"
                    }));
                return Ok(users);
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

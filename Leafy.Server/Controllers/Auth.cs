using Leafy.Application.DTOs;
using Leafy.Application.Features.Commands.UserCommands;
using Leafy.Application.Features.Queries.UserQueries;
using Leafy.Application.Interfaces;
using Leafy.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;

namespace Leafy.Server.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class Auth : ControllerBase
    {
        private readonly IAuthRepository _repository;
        private readonly IMediator _mediator;

        public Auth(IAuthRepository authRepository, IMediator mediator)
        {
            _repository = authRepository;
            _mediator = mediator;
        }

        [HttpPost("loginCookie")]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                int stat =  await _repository.LoginUser(email, password);
                User user = await _repository.GetUserByEmail(email);
                
                if (stat == -1) return BadRequest("Wrong email or password!");
                if (stat == 1) return BadRequest("Wrong password!");
                
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role),
                };

                var identity = new ClaimsIdentity(claims, "Cookie-0");
                var pricipal = new ClaimsPrincipal(identity);

                return SignIn(pricipal);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("loginJWT")]
        public async Task<IActionResult> LoginJWT([FromBody] LoginModel login)
        {
            try { 
                var response = await _mediator.Send(new LoginUserQuery(login.Email, login.Password));
                
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, response.UserName),
                    new Claim(ClaimTypes.Email, response.Email),
                    new Claim(ClaimTypes.Role, response.Role),
                    new Claim("token", response.JWT)
                };

                var identity = new ClaimsIdentity(claims, "token");
                var principal = new ClaimsPrincipal(identity);


                return SignIn(principal);
            }
            catch (Exception ex)
            {
                return BadRequest(JsonSerializer.Serialize(new
                {
                    Title = "Hata!",
                    ex.Message,
                }));
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            return SignOut("Cookie-0");
        }

        [Authorize(Policy = "adminOnly")]
        [HttpPost("test")]
        public async Task<IActionResult> Test()
        {
            string token = Request.Headers.Authorization.ToString();

            if (token.StartsWith("Bearer"))
            {
                token = token.Substring("Bearer ".Length).Trim();
            }
            var handler = new JwtSecurityTokenHandler();

            JwtSecurityToken jwt = handler.ReadJwtToken(token);

            var claims = new Dictionary<string, string>();

            foreach (var claim in jwt.Claims)
            {
                claims.Add(claim.Type, claim.Value);
            }
            

            return Ok(claims);
        }
    }
}

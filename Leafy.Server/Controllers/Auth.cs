using Leafy.Application.Features.Commands.UserCommands;
using Leafy.Application.Interfaces;
using Leafy.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Leafy.Server.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class Auth : ControllerBase
    {
        private readonly IAuthRepository _repository;

        public Auth(IAuthRepository authRepository)
        {
            _repository = authRepository;
        }

        [HttpPost("login")]
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
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            return SignOut("Cookie-0");
        }
    }
}

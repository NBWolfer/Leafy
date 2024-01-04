using Leafy.Application.DTOs;
using Leafy.Application.Features.Commands.UserCommands;
using Leafy.Application.Features.Queries.UserQueries;
using Leafy.Application.Interfaces;
using Leafy.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
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
        private readonly IAuthService _authService;
        private readonly IToken _token;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public Auth(IAuthRepository authRepository, IMediator mediator, IAuthService authService, IToken token, IUserRepository userRepository, IConfiguration configuration)
        {
            _configuration = configuration;
            _repository = authRepository;
            _mediator = mediator;
            _authService = authService;
            _token = token;
            _userRepository = userRepository;
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

                return Ok(response);
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

        [HttpPost("loginJWTwithCookie")]
        public async Task<IActionResult> LoginJWTwithCookie([FromBody] LoginModel login)
        {
            try
            {
                var principal = await _authService.AuthenticateUserAsync(login.Email, login.Password);
                var user = await _userRepository.GetUserByEmailAsync(login.Email);

                if (principal == null)
                {
                    return Unauthorized("Wrong email or password!");
                }

                var accessToken = _token.GenerateAccessToken(principal.Identity as ClaimsIdentity);
                var refreshToken = _token.GenerateRefreshToken(principal.Identity as ClaimsIdentity);

                Response.Cookies.Append("refreshToken", refreshToken.Result, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddDays(7)
                });

                Response.Cookies.Append("accessToken", accessToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddMinutes(10)
                });

                Response.HttpContext.User = principal;

                return Ok(new {user.Name, user.Email, user.Id, user.RegisteredDate});
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
            var accessToken = Request.Cookies["accessToken"];

            var handler = new JwtSecurityTokenHandler();

            var validateParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetValue<string>("secretKey")??"")),
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuer = false,
            };

            var principal = handler.ValidateToken(accessToken, validateParams, out SecurityToken validatedToken);
            if (validatedToken != null)
            {
                Response.HttpContext.User = principal;
            }
            return Ok("Hello from test-route");
        }
    }
}

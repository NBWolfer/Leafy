using Leafy.Application.DTOs;
using Leafy.Application.Features.Commands.UserCommands;
using Leafy.Application.Features.Queries.UserQueries;
using Leafy.Application.Interfaces;
using Leafy.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
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
        private readonly IMediator _mediator;
        private readonly IAuthService _authService;
        private readonly IToken _token;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public Auth(IAuthRepository authRepository, IMediator mediator, IAuthService authService, IToken token, IUserRepository userRepository, IConfiguration configuration, IEmailService emailService)
        {
            _configuration = configuration;
            _mediator = mediator;
            _authService = authService;
            _token = token;
            _userRepository = userRepository;
            _emailService = emailService;
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

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(SignUpCommand command)
        {
            try
            {
                var userExisted = await _userRepository.GetUserByEmailAsync(command.Email);
                if (userExisted != null)
                {
                    return Ok(new { message = "Bu email ile daha önce kayıt yapılmış!", status = 409});
                }

                await _mediator.Send(command);

                var principal = await _authService.AuthenticateUserAsync(command.Email, command.Password);
                var user = await _userRepository.GetUserByEmailAsync(command.Email);

                if (principal == null)
                {
                    return Ok(new { message = "Wrong email or password!", status = 401 });
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

                await _emailService.SendEmailAsync(user.Email, "Hoşgeldiniz!", "Leafy'ye kaydolduğunuz için teşekkürler!");

                return Ok(new { user.Name, user.Email, user.Id, user.RegisteredDate });

            }
            catch(Exception ex)
            {
                return BadRequest(JsonSerializer.Serialize(new
                {
                    message = "Hata!" + ex.Message,
                    status = 500
                }));
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("accessToken");
            Response.Cookies.Delete("refreshToken");
            return Ok("Çıkış yapıldı!");
        }

        [HttpPost("getStatus")]
        public async Task<IActionResult> GetStatus()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if(refreshToken == null)
            {
                return Ok(new
                {
                    status = false,
                    info = ""
                });
            }
            
            var handler = new JwtSecurityTokenHandler();
            var validateParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetValue<string>("secretKey") ?? "")),
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuer = false,
            };

            var principal = handler.ValidateToken(refreshToken, validateParams, out SecurityToken validatedToken);
            if (validatedToken != null)
            {
                Response.HttpContext.User = principal;
            }
            else
            {
                return Ok(new
                {
                    status = false,
                    info = ""
                });
            }
            // null gelme artık
            Claim claim = Response.HttpContext.User.FindFirst(ClaimTypes.Role);
            if(claim == null)
            {
                   return Ok(new
                   {
                    status = false,
                    info = ""
                });
            }
            if (claim.Value != "admin")
                return Ok(new
                {
                    status = true,
                    info = "user"
                });
            return Ok(new
            {
                status = true,
                info = "admin"
            });

        }


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

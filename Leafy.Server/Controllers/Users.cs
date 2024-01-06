using Leafy.Application.Features.Commands.UserCommands;
using Leafy.Application.Features.Queries.UserQueries;
using Leafy.Application.Interfaces;
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
                    var refreshToken = Request.Cookies["refreshToken"];
                    if(refreshToken == null)
                    {
                        return Ok(new { message = "Tekrar giriş yapın!", status= 401 });
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
                
                var principal = handler.ValidateToken(accessToken, validateParams, out SecurityToken validatedToken);
                if (validatedToken != null)
                {
                    Response.HttpContext.User = principal;
                }
                Claim claim = Response.HttpContext.User.FindFirst(ClaimTypes.Role);
                if(claim == null)
                {
                    return Ok("Bu istek için yetkili değilsiniz!");
                }
                if (claim.Value != "admin")
                    return Ok("Bu istek için yetkili değilsiniz!");
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

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
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
                        return Ok(new { message = "Geçersiz token!", status = 403});
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
                Claim claim = Response.HttpContext.User.FindFirst(ClaimTypes.Role);
                if (claim == null)
                {
                    return Ok("Bu istek için yetkili değilsiniz!");
                }
                if (claim.Value != "admin")
                    return Ok("Bu istek için yetkili değilsiniz!");

                await _mediator.Send(command);
                return Ok("User created!");

            }
            catch (Exception ex)
            {
                return BadRequest("Hata !"+ex.Message);
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserCommand command)
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

                var principal = handler.ValidateToken(accessToken, validateParams, out SecurityToken validatedToken);
                if (validatedToken != null)
                {
                    Response.HttpContext.User = principal;
                }
                Claim claim = Response.HttpContext.User.FindFirst(ClaimTypes.Role);
                if (claim == null)
                {
                    return Ok("Bu istek için yetkili değilsiniz!");
                }
                if (claim.Value != "admin")
                    return Ok("Bu istek için yetkili değilsiniz!");

                await _mediator.Send(command);
                return Ok("User updated!");
            }
            catch (Exception ex)
            {
                return Ok(new {message = "Hata !" + ex.Message});
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveUser(int id)
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

                var principal = handler.ValidateToken(accessToken, validateParams, out SecurityToken validatedToken);
                if (validatedToken != null)
                {
                    Response.HttpContext.User = principal;
                }
                Claim claim = Response.HttpContext.User.FindFirst(ClaimTypes.Role);
                if (claim == null)
                {
                    return Ok("Bu istek için yetkili değilsiniz!");
                }
                if (claim.Value != "admin")
                    return Ok("Bu istek için yetkili değilsiniz!");

                await _mediator.Send(new RemoveUserCommand(id));
                return Ok("User removed!");
            } 
            catch (Exception ex)
            {
                return BadRequest(new { message = "Hata !" + ex.Message} );
            }
        }
    }
}

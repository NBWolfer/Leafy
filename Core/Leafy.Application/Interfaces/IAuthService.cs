using Leafy.Application.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Leafy.Application.Interfaces
{
    public interface IAuthService
    {
        Task<ClaimsPrincipal> AuthenticateUserAsync(string username, string password);
    }
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public async Task<ClaimsPrincipal> AuthenticateUserAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(username);
            if (user == null)
            {
                return null;
            }
            var hashedPassword = _userRepository.HashPassword(password, user.Salt, _configuration.GetValue<string>("secretKey")??"", IUserRepository.Iteration);
            if (hashedPassword != user.Password)
            {
                return null;
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var identity = new ClaimsIdentity(claims, "accessToken");
            return new ClaimsPrincipal(identity);
        }
    }
}

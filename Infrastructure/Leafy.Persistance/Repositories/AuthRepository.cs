using Leafy.Application.Interfaces;
using Leafy.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leafy.Persistance.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;

        public AuthRepository(IUserRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async Task<int> LoginUser(string email, string password)
        {
           var user = await _repository.GetUserByEmailAsync(email);
           if(user == null) return -1;
           var hashedPassword = _repository.HashPassword(password, user.Salt, _configuration.GetValue<string>("secretKey")??"", IUserRepository.Iteration);
           if(hashedPassword != user.Password) return 1;
           return 0;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            User user = await _repository.GetUserByEmailAsync(email);
            return user;
        }

    }
}

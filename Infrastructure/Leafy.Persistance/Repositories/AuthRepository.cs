using Leafy.Application.Interfaces;
using Leafy.Domain.Entities;
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

        public AuthRepository(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> LoginUser(string email, string password)
        {
           var user = await _repository.GetUserByEmailAsync(email);
           if(user == null) return -1;
           var hashedPassword = _repository.HashPassword(password, user.Salt, IUserRepository.Pepper, IUserRepository.Iteration);
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

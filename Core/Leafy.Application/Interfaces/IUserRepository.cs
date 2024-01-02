using Leafy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leafy.Application.Interfaces
{
    public interface IUserRepository: IRepository<User>
    {
        public static int Iteration = 10;
        public string HashPassword(string password, string salt, string pepper, int iteration);
        public string GenerateSalt();
        public Task<User> GetUserByEmailAsync(string email);
    }
}

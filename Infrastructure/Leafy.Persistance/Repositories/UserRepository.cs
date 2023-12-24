using Leafy.Application.Interfaces;
using Leafy.Domain.Entities;
using Leafy.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Leafy.Persistance.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LeafyContext _context;

        public UserRepository(LeafyContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(User user)
        {
            _context.Set<User>().Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Set<User>().ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Set<User>().FindAsync(id);
        }

        public async Task RemoveAsync(User entity)
        {
            _context.Set<User>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User entity)
        {
            _context.Set<User>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public string HashPassword(string password, string salt, string pepper, int iteration)
        {
            if (iteration <= 0) return password;
            var passwordWithSalt = password + salt + pepper;
            var bytehash = SHA256.HashData(Encoding.UTF8.GetBytes(passwordWithSalt));
            var hash = Convert.ToBase64String(bytehash);
            return HashPassword(hash, salt, pepper, iteration - 1);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
           var user = await _context.Set<User>()
                .Where(x => x.Email == email)
                .FirstOrDefaultAsync();
           return user;
        }

        public string GenerateSalt()
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] salt = new byte[32];
            rng.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }
    }
}

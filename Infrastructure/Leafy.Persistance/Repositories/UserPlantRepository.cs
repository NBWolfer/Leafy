using Leafy.Application.Interfaces;
using Leafy.Domain.Entities;
using Leafy.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leafy.Persistance.Repositories
{
    public class UserPlantRepository : IUserPlantRepository
    {
        private readonly LeafyContext _context;

        public UserPlantRepository(LeafyContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(UserPlant entity)
        {
            _context.Set<UserPlant>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserPlant>> GetAllAsync()
        {
             return await _context.Set<UserPlant>().ToListAsync();
        }

        public async Task<UserPlant> GetByIdAsync(int id)
        {
            return await _context.Set<UserPlant>().FindAsync(id);
        }

        public async Task<UserPlant> GetUserPlantExpanded(int id)
        {
            return await _context.UserPlants.Include(p => p.plant).Include(p => p.user).FirstOrDefaultAsync(p => p.UserPlantId == id);
        }

        public async Task<List<UserPlant>> GetUserPlantsExpanded()
        {
            return await _context.UserPlants.Include(p => p.plant).Include(p => p.user).ToListAsync();
        }

        public async Task RemoveAsync(UserPlant entity)
        {
            _context.Set<UserPlant>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserPlant entity)
        {
            _context.Set<UserPlant>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}

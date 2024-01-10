using Leafy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leafy.Application.Interfaces
{
    public interface IUserPlantRepository : IRepository<UserPlant>
    {
        public Task<List<UserPlant>> GetUserPlantsExpanded();
        public Task<UserPlant> GetUserPlantExpanded(int id);
    }
}

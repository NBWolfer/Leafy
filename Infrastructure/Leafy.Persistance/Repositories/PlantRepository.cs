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
    public class PlantRepository : IPlantRepository
    {
        private readonly LeafyContext _leafyContext;

        public PlantRepository(LeafyContext leafyContext)
        {
            _leafyContext = leafyContext;
        }

        public async Task<List<Plant>> GetPlantWithDisease()
        {
            var plants = _leafyContext.Plants.Include(x => x.Disease).ToList();
            return plants;
        }
    }
}

using Leafy.Application.Interfaces;
using Leafy.Domain.Entities;
using Leafy.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Leafy.Persistance.Repositories
{
    public class DiseaseRepository : IDiseaseRepository
    {
        private readonly LeafyContext _context;

        public DiseaseRepository(LeafyContext context)
        {
            _context = context;
        }

        public Task<Disease> GetDiseaseByNameAsync(string name)
        {
            var disease = _context.Diseases.Where(d => d.Name == name).FirstOrDefaultAsync();
            return disease;
        }
    }
}

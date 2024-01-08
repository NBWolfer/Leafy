using Leafy.Application.Interfaces;
using Leafy.Domain.Entities;
using Leafy.Persistance.Context;
using Microsoft.EntityFrameworkCore;


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

        public string ScanPlantDisase(string base64Image)
        {
            

            // Base64 string'i byte dizisine çevir
            byte[] imageBytes = Convert.FromBase64String(base64Image);
            try
            {
                
            }
            catch (Exception ex) { return ex.Message; }
            return "";
        }
    }
}

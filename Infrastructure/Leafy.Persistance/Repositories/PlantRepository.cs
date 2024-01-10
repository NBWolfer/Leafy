using Leafy.Application.Interfaces;
using Leafy.Domain.Entities;
using Leafy.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using CliWrap;
using CliWrap.Buffered;
using System.Text;

namespace Leafy.Persistance.Repositories
{
    public class PlantRepository : IPlantRepository
    {
        private readonly LeafyContext _leafyContext;

        public PlantRepository(LeafyContext leafyContext)
        {
            _leafyContext = leafyContext;
        }

        public Task<Plant> GetPlantByName(string name)
        {
            var plant = _leafyContext.Plants.FirstOrDefaultAsync(x => x.Name == name);
            return plant;
        }

        public async Task<List<Plant>> GetPlantWithDisease()
        {
            var plants = _leafyContext.Plants.Include(x => x.Disease).ToList();
            return plants;
        }

        public async Task<string> ScanPlantDisase(string base64Image)
        {
            
            string dirPath = "C:\\Users\\Mahmut Enes\\Desktop\\Leafy-New\\FrontendImages\\";
            string modelPath = "C:\\Users\\Mahmut Enes\\Desktop\\Leafy-New\\Model\\model.py";
            // Base64 string'i byte dizisine çevir
            byte[] imageBytes = Convert.FromBase64String(base64Image);
            try
            {
                string path = dirPath + "image" + string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now) + ".jpg";
                await File.WriteAllBytesAsync(path, imageBytes);

                var result = await Cli.Wrap("python").WithArguments(new[] {modelPath, path}).ExecuteBufferedAsync(Encoding.UTF8);

                string output = result.StandardOutput;
                return output;
            }
            catch (Exception ex) { return ex.Message; }
        }
    }
}

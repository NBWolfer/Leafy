using Leafy.Application.Interfaces;
using Leafy.Domain.Entities;
using Leafy.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;


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

                using (Process process = new Process())
                {
                    process.StartInfo.FileName = "python";
                    process.StartInfo.Arguments = $"{modelPath} \"{path}\"";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;

                    // Start the process asynchronously
                    process.Start();

                    // Read the standard output asynchronously
                    string output = await process.StandardOutput.ReadToEndAsync();

                    // Wait for the process to exit
                    await process.WaitForExitAsync();

                    return output;
                }
            }
            catch (Exception ex) { return ex.Message; }
        }
    }
}

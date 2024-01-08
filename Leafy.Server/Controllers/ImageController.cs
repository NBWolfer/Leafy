using Leafy.Application.DTOs;
using Leafy.Persistance.Context;
using Microsoft.AspNetCore.Mvc;

namespace Leafy.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly LeafyContext _context;

        public ImageController(LeafyContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult UploadImage([FromBody] AddPlantModel file)
        {
            if (file == null || file.Image == null || file.Image.Length == 0)
                return BadRequest("Invalid file");

            string imageData = file.Image;

            // Eğitilmiş modeli çağır ve sonuçları al
            var aiModelResults = InvokeAIModel(imageData);

            // AI model sonuçlarını istemciye döndür
            return Ok(new { results = aiModelResults });
        }

        private object InvokeAIModel(string imageData)
        {
            // AI modeli çağırma işlemleri burada gerçekleştirilir
            // ...
            // Örnek: Sadece resim verisinin uzunluğunu döndürüyoruz
            return imageData.Length;
        }
    }
}


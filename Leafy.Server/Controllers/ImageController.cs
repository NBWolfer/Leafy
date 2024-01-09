using Leafy.Application.DTOs;
using Leafy.Application.Interfaces;
using Leafy.Persistance.Context;
using Microsoft.AspNetCore.Mvc;

namespace Leafy.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly LeafyContext _context;
        private readonly IPlantRepository _plantRepository;

        public ImageController(LeafyContext context, IPlantRepository plantRepository)
        {
            _plantRepository = plantRepository;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage([FromBody] AddPlantModel file)
        {
            if(file.Image == null || file.Image == "")
            {
                return Ok("Image is null");
            }
            string result = await _plantRepository.ScanPlantDisase(file.Image);
            return Ok(result);
        }
    }
}


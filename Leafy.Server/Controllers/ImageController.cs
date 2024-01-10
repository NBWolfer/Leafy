using Leafy.Application.DTOs;
using Leafy.Application.Interfaces;
using Leafy.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Leafy.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IPlantRepository _plantRepository;
        private readonly IConfiguration _configuration;
        private readonly IToken _token;
        private readonly IUserRepository _repository;
        private readonly IDiseaseRepository _diseaseRepository;
        private readonly IRepository<Plant> _repositoryPlant;
        private readonly IRepository<Disease> _repositoryDisease;
        private readonly IRepository<UserPlants> _repositoryUserPlant;
        private readonly IUserPlantRepository _userPlantRepository;

        public ImageController(IPlantRepository plantRepository, IConfiguration configuration, IToken token, IUserRepository repository, IDiseaseRepository diseaseRepository, IRepository<Plant> repositoryPlant, IRepository<Disease> repositoryDisease, IRepository<UserPlants> repositoryUserPlant, IUserPlantRepository userPlantRepository)
        {
            _repositoryPlant = repositoryPlant;
            _repositoryDisease = repositoryDisease;
            _repository = repository;
            _configuration = configuration;
            _plantRepository = plantRepository;
            _token = token;
            _diseaseRepository = diseaseRepository;
            _repositoryUserPlant = repositoryUserPlant;
            _userPlantRepository = userPlantRepository;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage([FromBody] AddPlantModel file)
        {
            var handler = new JwtSecurityTokenHandler();

            var validateParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetValue<string>("secretKey") ?? "")),
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuer = false,
            };
            var accessToken = Request.Cookies["accessToken"];
            if (accessToken == null)
            {
                var refreshToken = Request.Cookies["refreshToken"];
                if (refreshToken == null)
                {
                    return Ok(new { message = "Tekrar giriş yapın!", status = 401 });
                }
                var principalRefreshToken = handler.ValidateToken(refreshToken, validateParams, out SecurityToken validatedRefreshToken);
                if (validatedRefreshToken == null)
                {
                    return Ok(new { message = "Tekrardan giriş yapın!", status = 401 });
                }
                else
                {
                    accessToken = _token.GenerateAccessToken(principalRefreshToken.Identity as ClaimsIdentity);
                    Response.Cookies.Append("accessToken", accessToken, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None,
                        Expires = DateTime.UtcNow.AddMinutes(10)
                    });
                    Response.HttpContext.User = principalRefreshToken;
                }
            }

            var principal = handler.ValidateToken(accessToken, validateParams, out SecurityToken validatedToken);
            if (validatedToken != null)
            {
                Response.HttpContext.User = principal;
            }
            Claim claim = Response.HttpContext.User.FindFirst(ClaimTypes.Role);
            if (claim == null)
            {
                return Ok(new { message = "Bu istek için yetkili değilsiniz!", status = 403 });
            }

            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            User user = await _repository.GetUserByEmailAsync(userEmail);


            if (file.Image == null || file.Image == "")
            {
                return Ok(new { message = "Image is null", status = 400 });
            }
            string result = await _plantRepository.ScanPlantDisase(file.Image);

            result = result[result.IndexOf("Resim")..];

            int startIndex = result.IndexOf("Resim ") + "Resim ".Length;
            int endIndex = result.IndexOf("sinifina");
            string plantDiseaseName = result.Substring(startIndex, endIndex - startIndex).Trim();

            string plantName = plantDiseaseName.Split('_')[0];
            string[] strings = plantDiseaseName.Split('_');
            string diseaseName = string.Join("_", strings, 1, strings.Length-1);




            var disease = await _diseaseRepository.GetDiseaseByNameAsync(diseaseName);
            if(disease == null)
            {
                await _repositoryDisease.CreateAsync(new Disease
                {
                    Name = diseaseName,
                    Description = "Description",
                    ImageUrl = file.Image,
                });
                disease = await _diseaseRepository.GetDiseaseByNameAsync(diseaseName);
            }

            var plant = await _plantRepository.GetPlantByName(plantName);

            if(plant == null)
            {
                await _repositoryPlant.CreateAsync(plant = new Plant
                {
                    Name = plantName,
                    Description = "Description",
                    ImageUrl = file.Image,
                    DiseaseId = disease.Id,
                    LatinName = "LatinName",
                });
                plant = await _plantRepository.GetPlantByName(plantName);
            }

            await _userPlantRepository.CreateAsync(new UserPlant
            {
                PlantId = plant.Id,
                UserId = user.Id,
            });

            return Ok(plantName);
        }
    }
}


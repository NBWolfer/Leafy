using Leafy.Application.Features.Queries.PlantQueries;
using Leafy.Application.Features.Results.PlantResults;
using Leafy.Application.Interfaces;
using Leafy.Domain.Entities;
using MediatR;

namespace Leafy.Application.Features.Handlers.PlantHandlers
{
    public class GetPlantQueryHandler : IRequestHandler<GetPlantQuery, List<GetPlantQueryResult>>
    {
        private readonly IRepository<Plant> _repository;

        public GetPlantQueryHandler(IRepository<Plant> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetPlantQueryResult>> Handle(GetPlantQuery request, CancellationToken cancellationToken)
        {
            var plants = await _repository.GetAllAsync();
            return plants.Select(x => new GetPlantQueryResult
            {
                Id = x.Id,
                Name = x.Name,
                LatinName = x.LatinName,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                DiseaseId = x.DiseaseId
            }).ToList();
        }
    }
}

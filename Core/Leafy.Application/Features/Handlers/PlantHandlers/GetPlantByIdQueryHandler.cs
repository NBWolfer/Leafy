using Leafy.Application.Features.Queries.PlantQueries;
using Leafy.Application.Features.Results.PlantResults;
using Leafy.Application.Interfaces;
using Leafy.Domain.Entities;
using MediatR;

namespace Leafy.Application.Features.Handlers.PlantHandlers
{
    public class GetPlantByIdQueryHandler : IRequestHandler<GetPlantByIdQuery, GetPlantByIdQueryResult>
    {
        private readonly IRepository<Plant> _repository;

        public GetPlantByIdQueryHandler(IRepository<Plant> repository)
        {
            _repository = repository;
        }

        public async Task<GetPlantByIdQueryResult> Handle(GetPlantByIdQuery request, CancellationToken cancellationToken)
        {
            var plant = await _repository.GetByIdAsync(request.Id);
            return new GetPlantByIdQueryResult
            {
                Id = plant.Id,
                Name = plant.Name,
                LatinName = plant.LatinName,
                Description = plant.Description,
                ImageUrl = plant.ImageUrl,
                DiseaseId = plant.DiseaseId
            };
        }
    }
}

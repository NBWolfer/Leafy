using Leafy.Application.Features.Queries.PlantQueries;
using Leafy.Application.Features.Results.PlantResults;
using Leafy.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leafy.Application.Features.Handlers.PlantHandlers
{
    public class GetPlantWithDiseaseQueryHandler : IRequestHandler<GetPlantWithDiseaseQuery, List<GetPlantWithDiseaseQueryResult>>
    {
        private readonly IPlantRepository _plantRepository;

        public GetPlantWithDiseaseQueryHandler(IPlantRepository plantRepository)
        {
            _plantRepository = plantRepository;
        }

        public async Task<List<GetPlantWithDiseaseQueryResult>> Handle(GetPlantWithDiseaseQuery request, CancellationToken cancellationToken)
        {
            var values = await _plantRepository.GetPlantWithDisease();
            return values.Select(value =>
            new GetPlantWithDiseaseQueryResult
            {
                Id = value.Id,
                Description = value.Description,
                DiseaseId = value.DiseaseId,
                DiseaseName = value.Disease.Name,
                Name = value.Name,
            }).ToList();
            
        }
    }
}

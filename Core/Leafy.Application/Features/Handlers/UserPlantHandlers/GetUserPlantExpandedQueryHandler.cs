using Leafy.Application.Features.Queries.UserPlantQueries;
using Leafy.Application.Features.Results.UserPlantResults;
using Leafy.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leafy.Application.Features.Handlers.UserPlantHandlers
{
    public class GetUserPlantExpandedQueryHandler : IRequestHandler<GetUserPlantExpandedQuery, List<GetUserPlantExpandedQueryResult>>
    {
        private readonly IUserPlantRepository _userPlantRepository;

        public GetUserPlantExpandedQueryHandler(IUserPlantRepository userPlantRepository)
        {
            _userPlantRepository = userPlantRepository;
        }

        public async Task<List<GetUserPlantExpandedQueryResult>> Handle(GetUserPlantExpandedQuery request, CancellationToken cancellationToken)
        {
            var userPlants = await _userPlantRepository.GetUserPlantsExpanded();

            return userPlants.Select(userPlant => new GetUserPlantExpandedQueryResult
            {
                Id = userPlant.UserPlantId,
                PlantName = userPlant.plant.Name,
                UserName = userPlant.user.Name,
                ImageUrl = userPlant.plant.ImageUrl
            }).ToList();
        }
    }
}

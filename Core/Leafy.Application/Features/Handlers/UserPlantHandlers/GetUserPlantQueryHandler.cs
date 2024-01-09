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
    public class GetUserPlantQueryHandler : IRequestHandler<GetUserPlantQuery, List<GetUserPlantQueryResult>>
    {
        private readonly IUserPlantRepository _userPlantRepository;

        public GetUserPlantQueryHandler(IUserPlantRepository userPlantRepository)
        {
            _userPlantRepository = userPlantRepository;
        }

        public async Task<List<GetUserPlantQueryResult>> Handle(GetUserPlantQuery request, CancellationToken cancellationToken)
        {
            var userPlants = await _userPlantRepository.GetAllAsync();
            return userPlants.Select(x => new GetUserPlantQueryResult
            {
                Id = x.UserPlantId,
                PlantId = x.PlantId,
                UserId = x.UserId
            }).ToList();
        }
    }
}

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
    public class GetUserPlantByIdQueryHandler : IRequestHandler<GetUserPlantByIdQuery, GetUserPlantByIdQueryResult>
    {
        private readonly IUserPlantRepository _userPlantRepository;

        public GetUserPlantByIdQueryHandler(IUserPlantRepository userPlantRepository)
        {
            _userPlantRepository = userPlantRepository;
        }

        public async Task<GetUserPlantByIdQueryResult> Handle(GetUserPlantByIdQuery request, CancellationToken cancellationToken)
        {
            var userPlant = await _userPlantRepository.GetByIdAsync(request.Id);
            return new GetUserPlantByIdQueryResult
            {
                Id = userPlant.UserPlantId,
                PlantId = userPlant.PlantId,
                UserId = userPlant.UserId
            };
        }
    }
}

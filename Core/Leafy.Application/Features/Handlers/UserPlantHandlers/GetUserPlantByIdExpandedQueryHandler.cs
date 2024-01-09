using Leafy.Application.Features.Queries.UserPlantQueries;
using Leafy.Application.Features.Results.UserPlantResults;
using Leafy.Application.Interfaces;
using MediatR;

namespace Leafy.Application.Features.Handlers.UserPlantHandlers
{
    public class GetUserPlantByIdExpandedQueryHandler : IRequestHandler<GetUserPlantByIdExpandedQuery, GetUserPlantByIdExpandedQueryResult>
    {
        private readonly IUserPlantRepository _userPlantRepository;

        public GetUserPlantByIdExpandedQueryHandler(IUserPlantRepository userPlantRepository)
        {
            _userPlantRepository = userPlantRepository;
        }

        public async Task<GetUserPlantByIdExpandedQueryResult> Handle(GetUserPlantByIdExpandedQuery request, CancellationToken cancellationToken)
        {
            var userPlant = await _userPlantRepository.GetUserPlantExpanded(request.Id);
            

            return new GetUserPlantByIdExpandedQueryResult
            {
                Id = userPlant.UserPlantId,
                PlantName = userPlant.plant.Name,
                UserName = userPlant.user.Name,
            };
        }
    }
}

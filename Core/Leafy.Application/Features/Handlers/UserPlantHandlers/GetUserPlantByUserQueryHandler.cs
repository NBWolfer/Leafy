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
    public class GetUserPlantByUserQueryHandler : IRequestHandler<GetUserPlantByUserQuery, List<GetUserPlantByUserQueryResult>>
    {
        private readonly IUserPlantRepository _repository;

        public GetUserPlantByUserQueryHandler(IUserPlantRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetUserPlantByUserQueryResult>> Handle(GetUserPlantByUserQuery request, CancellationToken cancellationToken)
        {
            var userPlants = await _repository.GetUserPlantsByUserExpanded(request.UserId);

            return userPlants.Select(x => new GetUserPlantByUserQueryResult {
                Id = x.UserPlantId,
                PlantName = x.plant.Name,
                ImageUrl = x.Image
            }).ToList();
        }
    }
}

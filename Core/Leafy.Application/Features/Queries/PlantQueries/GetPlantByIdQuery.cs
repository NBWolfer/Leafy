using Leafy.Application.Features.Results.PlantResults;
using MediatR;

namespace Leafy.Application.Features.Queries.PlantQueries
{
    public class GetPlantByIdQuery : IRequest<GetPlantByIdQueryResult>
    {
        public int Id { get; set; }

        public GetPlantByIdQuery(int id)
        {
            Id = id;
        }
    }
}

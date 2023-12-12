using Leafy.Application.Features.Results.PlantResults;
using MediatR;

namespace Leafy.Application.Features.Queries.PlantQueries
{
    public class GetPlantQuery : IRequest<List<GetPlantQueryResult>>
    {
    }
}

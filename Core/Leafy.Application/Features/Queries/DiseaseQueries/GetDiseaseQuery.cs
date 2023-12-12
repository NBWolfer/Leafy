using Leafy.Application.Features.Results.DiseaseResults;
using MediatR;

namespace Leafy.Application.Features.Queries.DiseaseQueries
{
    public class GetDiseaseQuery : IRequest<List<GetDiseaseQueryResult>>
    {
    }
}

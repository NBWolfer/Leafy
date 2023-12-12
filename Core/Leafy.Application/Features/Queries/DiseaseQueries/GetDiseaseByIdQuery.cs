using Leafy.Application.Features.Results.DiseaseResults;
using MediatR;

namespace Leafy.Application.Features.Queries.DiseaseQueries
{
    public class GetDiseaseByIdQuery : IRequest<GetDiseaseByIdQueryResult>
    {
        public int Id { get; set; }

        public GetDiseaseByIdQuery(int id)
        {
            Id = id;
        }
    }
}

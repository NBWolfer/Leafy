using Leafy.Application.Features.Queries.DiseaseQueries;
using Leafy.Application.Features.Results.DiseaseResults;
using Leafy.Application.Interfaces;
using Leafy.Domain.Entities;
using MediatR;

namespace Leafy.Application.Features.Handlers.DiseaseHandlers
{
    public class GetDiseaseQueryHandler : IRequestHandler<GetDiseaseQuery, List<GetDiseaseQueryResult>>
    {
        private readonly IRepository<Disease> _repository;

        public GetDiseaseQueryHandler(IRepository<Disease> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetDiseaseQueryResult>> Handle(GetDiseaseQuery request, CancellationToken cancellationToken)
        {
            var diseases = await _repository.GetAllAsync();
            return diseases.Select(disease => new GetDiseaseQueryResult
            {
                Id = disease.Id,
                Name = disease.Name,
                Description = disease.Description
            }).ToList();
        }
    }
}

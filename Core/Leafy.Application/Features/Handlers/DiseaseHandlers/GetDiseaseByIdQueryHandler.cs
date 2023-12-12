using Leafy.Application.Features.Queries.DiseaseQueries;
using Leafy.Application.Features.Results.DiseaseResults;
using Leafy.Application.Interfaces;
using Leafy.Domain.Entities;
using MediatR;

namespace Leafy.Application.Features.Handlers.DiseaseHandlers
{
    public class GetDiseaseByIdQueryHandler : IRequestHandler<GetDiseaseByIdQuery, GetDiseaseByIdQueryResult>
    {
        private readonly IRepository<Disease> _repository;

        public GetDiseaseByIdQueryHandler(IRepository<Disease> repository)
        {
            _repository = repository;
        }

        public async Task<GetDiseaseByIdQueryResult> Handle(GetDiseaseByIdQuery request, CancellationToken cancellationToken)
        {
            var disease = await _repository.GetByIdAsync(request.Id);
            return new GetDiseaseByIdQueryResult
            {
                Id = disease.Id,
                Name = disease.Name,
                Description = disease.Description
            };
        }
    }
}

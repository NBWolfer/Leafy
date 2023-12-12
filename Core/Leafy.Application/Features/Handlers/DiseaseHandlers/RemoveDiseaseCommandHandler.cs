using Leafy.Application.Features.Commands.DiseaseCommands;
using Leafy.Application.Interfaces;
using Leafy.Domain.Entities;
using MediatR;

namespace Leafy.Application.Features.Handlers.DiseaseHandlers
{
    public class RemoveDiseaseCommandHandler : IRequestHandler<RemoveDiseaseCommand>
    {
        private readonly IRepository<Disease> _repository;

        public RemoveDiseaseCommandHandler(IRepository<Disease> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveDiseaseCommand request, CancellationToken cancellationToken)
        {
            var disease = await _repository.GetByIdAsync(request.Id);
            await _repository.RemoveAsync(disease);
        }
    }
}

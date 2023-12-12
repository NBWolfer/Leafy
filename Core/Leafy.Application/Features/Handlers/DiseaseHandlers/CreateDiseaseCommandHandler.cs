using Leafy.Application.Features.Commands.DiseaseCommands;
using Leafy.Application.Interfaces;
using Leafy.Domain.Entities;
using MediatR;

namespace Leafy.Application.Features.Handlers.DiseaseHandlers
{
    public class CreateDiseaseCommandHandler : IRequestHandler<CreateDiseaseCommand>
    {
        private readonly IRepository<Disease> _repository;

        public CreateDiseaseCommandHandler(IRepository<Disease> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateDiseaseCommand request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new Disease
            {
                Name = request.Name,
                Description = request.Description
            });
        }
    }
}

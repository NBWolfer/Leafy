using Leafy.Application.Features.Commands.DiseaseCommands;
using Leafy.Application.Interfaces;
using Leafy.Domain.Entities;
using MediatR;

namespace Leafy.Application.Features.Handlers.DiseaseHandlers
{
    public class UpdateDiseaseCommandHandler : IRequestHandler<UpdateDiseaseCommand>
    {
        private readonly IRepository<Disease> _repository;

        public UpdateDiseaseCommandHandler(IRepository<Disease> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateDiseaseCommand request, CancellationToken cancellationToken)
        {
            var disease = await _repository.GetByIdAsync(request.Id);
            disease.Name = request.Name;
            disease.Description = request.Description;
            await _repository.UpdateAsync(disease);
        }
    }
}

using Leafy.Application.Features.Commands.PlantCommands;
using Leafy.Application.Interfaces;
using Leafy.Domain.Entities;
using MediatR;

namespace Leafy.Application.Features.Handlers.PlantHandlers
{
    public class CreatePlantCommandHandler : IRequestHandler<CreatePlantCommand>
    {
        private readonly IRepository<Plant> _repository;

        public CreatePlantCommandHandler(IRepository<Plant> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreatePlantCommand request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new Plant
            {
                Name = request.Name,
                LatinName = request.LatinName,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                DiseaseId = request.DiseaseId
            });
        }
    }
}

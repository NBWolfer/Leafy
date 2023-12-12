using Leafy.Application.Features.Commands.PlantCommands;
using Leafy.Application.Interfaces;
using Leafy.Domain.Entities;
using MediatR;

namespace Leafy.Application.Features.Handlers.PlantHandlers
{
    public class UpdatePlantCommandHandler : IRequestHandler<UpdatePlantCommand>
    {
        private readonly IRepository<Plant> _repository;

        public UpdatePlantCommandHandler(IRepository<Plant> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdatePlantCommand request, CancellationToken cancellationToken)
        {
            var plant = await _repository.GetByIdAsync(request.Id);
            plant.Name = request.Name;
            plant.LatinName = request.LatinName;
            plant.Description = request.Description;
            plant.ImageUrl = request.ImageUrl;
            plant.DiseaseId = request.DiseaseId;
            await _repository.UpdateAsync(plant);
        }
    }
}

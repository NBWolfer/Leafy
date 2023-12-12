using Leafy.Application.Features.Commands.PlantCommands;
using Leafy.Application.Interfaces;
using Leafy.Domain.Entities;
using MediatR;

namespace Leafy.Application.Features.Handlers.PlantHandlers
{
    public class RemovePlantCommandHandler : IRequestHandler<RemovePlantCommand>
    {
        private readonly IRepository<Plant> _repository;

        public RemovePlantCommandHandler(IRepository<Plant> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemovePlantCommand request, CancellationToken cancellationToken)
        {
            var plant = await _repository.GetByIdAsync(request.Id);
            await _repository.RemoveAsync(plant);
        }
    }
}

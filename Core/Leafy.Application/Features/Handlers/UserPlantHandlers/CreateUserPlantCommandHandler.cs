using Leafy.Application.Features.Commands.UserPlantCommands;
using Leafy.Application.Interfaces;
using Leafy.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leafy.Application.Features.Handlers.UserPlantHandlers
{
    public class CreateUserPlantCommandHandler : IRequestHandler<CreateUserPlantCommand>
    {
        private readonly IUserPlantRepository _userPlantRepository;

        public CreateUserPlantCommandHandler(IUserPlantRepository userPlantRepository)
        {
            _userPlantRepository = userPlantRepository;
        }

        public async Task Handle(CreateUserPlantCommand request, CancellationToken cancellationToken)
        {
            await _userPlantRepository.CreateAsync(new UserPlant { PlantId = request.PlantId, UserId = request.UserId });
        }
    }
}

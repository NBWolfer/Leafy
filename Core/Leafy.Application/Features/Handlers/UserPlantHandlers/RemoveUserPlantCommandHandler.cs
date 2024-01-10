using Leafy.Application.Features.Commands.UserPlantCommands;
using Leafy.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leafy.Application.Features.Handlers.UserPlantHandlers
{
    public class RemoveUserPlantCommandHandler : IRequestHandler<RemoveUserPlantCommand>
    {
        private readonly IUserPlantRepository _userPlantRepository;

        public RemoveUserPlantCommandHandler(IUserPlantRepository userPlantRepository)
        {
            _userPlantRepository = userPlantRepository;
        }

        public async Task Handle(RemoveUserPlantCommand request, CancellationToken cancellationToken)
        {
            var userPlant = await _userPlantRepository.GetByIdAsync(request.Id);
            await _userPlantRepository.RemoveAsync(userPlant);
        }
    }
}

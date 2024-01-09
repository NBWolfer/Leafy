using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leafy.Application.Features.Commands.UserPlantCommands
{
    public class CreateUserPlantCommand : IRequest
    {
        public int PlantId { get; set; }
        public int UserId { get; set; }
    }
}

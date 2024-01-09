using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leafy.Application.Features.Commands
{
    public class RemoveUserPlantCommand : IRequest
    {
        public int Id { get; set; }

        public RemoveUserPlantCommand(int id)
        {
            Id = id;
        }
    }
}

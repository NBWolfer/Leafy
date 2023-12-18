using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leafy.Application.Features.Commands.UserCommands
{
    public class RemoveUserCommand: IRequest
    {
        public int Id { get; set; }

        public RemoveUserCommand(int id)
        {
            Id = id;
        }
    }
}

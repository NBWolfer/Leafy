using Leafy.Application.Features.Commands.UserCommands;
using Leafy.Application.Interfaces;
using Leafy.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leafy.Application.Features.Handlers.UserHandlers
{
    public class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommand>
    {
        private readonly IRepository<User> _repository;

        public RemoveUserCommandHandler(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.Id);
            await _repository.RemoveAsync(user);
        }
    }
}

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
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IRepository<User> _repository;

        public UpdateUserCommandHandler(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.Id);
            user.Name = request.Name;
            user.Email = request.Email;
            user.Password = request.Password;
            await _repository.UpdateAsync(user);
        }
    }
}

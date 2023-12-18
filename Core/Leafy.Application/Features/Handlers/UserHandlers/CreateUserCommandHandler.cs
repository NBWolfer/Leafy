using Leafy.Application.Features.Commands.DiseaseCommands;
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
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IRepository<User> _repository;

        public CreateUserCommandHandler(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password
            });
        }
    }
}

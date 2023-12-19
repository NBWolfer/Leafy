using Leafy.Application.Features.Commands.DiseaseCommands;
using Leafy.Application.Features.Commands.UserCommands;
using Leafy.Application.Interfaces;
using Leafy.Domain.Entities;
using MediatR;
using System.Security.Cryptography;

namespace Leafy.Application.Features.Handlers.UserHandlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IUserRepository _repository;

        public CreateUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            string hashedPassword = _repository.HashPassword(request.Password, IUserRepository.SecretKey, IUserRepository.Pepper, IUserRepository.Iteration);

            await _repository.CreateAsync(new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = hashedPassword
            });
        }
    }
}

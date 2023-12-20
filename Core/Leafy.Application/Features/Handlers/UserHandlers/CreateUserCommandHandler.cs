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
            string salt = _repository.GenerateSalt();
            string hashedPassword = _repository.HashPassword(request.Password, salt, IUserRepository.Pepper, IUserRepository.Iteration);
            string role = "user";
            await _repository.CreateAsync(new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = hashedPassword,
                Salt = salt,
                Role = role,
                RegisteredDate = DateTime.Now
            });
        }
    }
}

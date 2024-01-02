using Leafy.Application.Features.Commands.DiseaseCommands;
using Leafy.Application.Features.Commands.UserCommands;
using Leafy.Application.Interfaces;
using Leafy.Application.Services;
using Leafy.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace Leafy.Application.Features.Handlers.UserHandlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;
        

        public CreateUserCommandHandler(IUserRepository repository, IConfiguration configuration)
        {
            _configuration = configuration;
            _repository = repository;
        }

        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            string salt = _repository.GenerateSalt();
            string hashedPassword = _repository.HashPassword(request.Password, salt, _configuration.GetValue<string>("secretKey") ?? "", IUserRepository.Iteration);
            User user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = hashedPassword,
                Salt = salt,
                Role = request.Role,
                RegisteredDate = DateTime.Now,
            };
            
            await _repository.CreateAsync(user);
        }
    }
}

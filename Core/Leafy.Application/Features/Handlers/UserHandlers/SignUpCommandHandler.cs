using Leafy.Application.Features.Commands.UserCommands;
using Leafy.Application.Interfaces;
using Leafy.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leafy.Application.Features.Handlers.UserHandlers
{
    public class SignUpCommandHandler:IRequestHandler<SignUpCommand>
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;

        public SignUpCommandHandler(IConfiguration configuration, IUserRepository repository)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async Task Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            string salt = _repository.GenerateSalt();
            string hashedPassword = _repository.HashPassword(request.Password, salt, _configuration.GetValue<string>("secretKey") ?? "", IUserRepository.Iteration);
            User user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = hashedPassword,
                Salt = salt,
                Role = "user",
                RegisteredDate = DateTime.Now,
            };

            await _repository.CreateAsync(user);
        }
    }
}

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
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;

        public UpdateUserCommandHandler(IUserRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.Id);
            user.Name = request.Name;
            user.Email = request.Email;
            var hashedpassword = _repository.HashPassword(request.Password, user.Salt, _configuration.GetValue<string>("secretKey") ?? "", IUserRepository.Iteration);
            user.Password = hashedpassword;
            await _repository.UpdateAsync(user);
        }
    }
}

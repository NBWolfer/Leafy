using Leafy.Application.Features.Commands.UserCommands;
using Leafy.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leafy.Application.Features.Handlers.UserHandlers
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand>
    {
        private readonly IAuthRepository _repository;

        public LoginUserHandler(IAuthRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            int status = await _repository.LoginUser(request.Email, request.Password);
            return status;
        }
    }
}

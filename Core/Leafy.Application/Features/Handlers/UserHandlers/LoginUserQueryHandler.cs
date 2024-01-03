﻿using Leafy.Application.Features.Queries.UserQueries;
using Leafy.Application.Features.Results.UserResults;
using Leafy.Application.Interfaces;
using Leafy.Application.Services;
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
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, LoginUserQueryResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public LoginUserQueryHandler(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<LoginUserQueryResult> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user == null)
            {
                return new LoginUserQueryResult
                {
                    UserId = -1,
                    JWT = string.Empty,
                    Email = request.Email,
                    UserName = string.Empty,
                    Message = "Email kayıtlı değil!",
                    Role = string.Empty
                };
            }
            else
            {
                if (user.Password == _userRepository.HashPassword(request.Password, user.Salt, _configuration.GetValue<string>("secretKey") ?? "", _configuration.GetValue<int>("iteration")))
                {
                    return new LoginUserQueryResult
                    {
                        Email = request.Email,
                        UserName = user.Name,
                        JWT = TokenService.CreateToken(user, _configuration),
                        Message = "Giriş başarılı",
                        UserId = user.Id,
                        Role = user.Role,
                    };
                }
                else
                {
                    return new LoginUserQueryResult
                    {
                        Email = request.Email,
                        UserName = string.Empty,
                        JWT = string.Empty,
                        Role = string.Empty,
                        UserId = -1,
                        Message = "Şifre Hatalı"
                    };
                }
            }
        }
    }
}
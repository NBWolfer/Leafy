using Leafy.Application.Features.Queries.DiseaseQueries;
using Leafy.Application.Features.Queries.UserQueries;
using Leafy.Application.Features.Results.UserResults;
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
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, List<GetUserQueryResult>>
    {
        private readonly IRepository<User> _repository;

        public GetUserQueryHandler(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetUserQueryResult>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var users = await _repository.GetAllAsync();
            return users.Select(x => new GetUserQueryResult
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Password = x.Password,
                Role = x.Role,
                RegisteredDate = x.RegisteredDate
                
            }).ToList();
        }
    }
}

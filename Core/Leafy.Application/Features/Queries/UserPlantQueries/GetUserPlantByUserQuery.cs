using Leafy.Application.Features.Results.UserPlantResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leafy.Application.Features.Queries.UserPlantQueries
{
    public class GetUserPlantByUserQuery: IRequest<List<GetUserPlantByUserQueryResult>>
    {
        public int UserId { get; set; }

        public GetUserPlantByUserQuery(int userId)
        {
            UserId = userId;
        }
    }
}

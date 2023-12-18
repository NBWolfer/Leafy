using Leafy.Application.Features.Results.UserResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leafy.Application.Features.Queries.UserQueries
{
    public class GetUserQuery: IRequest<List<GetUserQueryResult>>
    {

    }
}

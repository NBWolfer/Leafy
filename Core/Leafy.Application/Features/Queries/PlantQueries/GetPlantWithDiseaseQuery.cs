using Leafy.Application.Features.Results.PlantResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leafy.Application.Features.Queries.PlantQueries
{
    public class GetPlantWithDiseaseQuery:IRequest<List<GetPlantWithDiseaseQueryResult>>
    {

    }
}

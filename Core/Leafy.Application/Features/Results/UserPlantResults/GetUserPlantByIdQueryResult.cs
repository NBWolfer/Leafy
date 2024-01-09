using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leafy.Application.Features.Results.UserPlantResults
{
    public class GetUserPlantByIdQueryResult
    {
        public int Id { get; set; }
        public int PlantId { get; set; }
        public int UserId { get; set; }
    }
}

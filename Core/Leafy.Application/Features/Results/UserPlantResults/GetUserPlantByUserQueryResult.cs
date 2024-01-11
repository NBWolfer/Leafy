using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leafy.Application.Features.Results.UserPlantResults
{
    public class GetUserPlantByUserQueryResult
    {
        public int Id { get; set; }
        public string PlantName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
}

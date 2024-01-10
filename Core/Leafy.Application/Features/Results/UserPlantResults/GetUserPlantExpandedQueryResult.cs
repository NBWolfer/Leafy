using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leafy.Application.Features.Results.UserPlantResults
{
    public class GetUserPlantExpandedQueryResult
    {
        public int Id { get; set; }
        public string PlantName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
}

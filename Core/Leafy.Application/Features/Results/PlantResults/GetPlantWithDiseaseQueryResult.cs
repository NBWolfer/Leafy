using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leafy.Application.Features.Results.PlantResults
{
    public class GetPlantWithDiseaseQueryResult
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DiseaseName { get; set; } = string.Empty;
        public int DiseaseId { get; set; }
    }
}

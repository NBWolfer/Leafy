using MediatR;

namespace Leafy.Application.Features.Commands.PlantCommands
{
    public class CreatePlantCommand : IRequest
    {
        public string Name { get; set; }
        public string LatinName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int DiseaseId { get; set; }
    }
}

using MediatR;

namespace Leafy.Application.Features.Commands.DiseaseCommands
{
    public class UpdateDiseaseCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

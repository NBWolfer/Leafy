using MediatR;

namespace Leafy.Application.Features.Commands.DiseaseCommands
{
    public class CreateDiseaseCommand : IRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

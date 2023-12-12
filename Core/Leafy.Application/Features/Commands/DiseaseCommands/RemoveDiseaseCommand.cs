using MediatR;

namespace Leafy.Application.Features.Commands.DiseaseCommands
{
    public class RemoveDiseaseCommand : IRequest
    {
        public int Id { get; set; }

        public RemoveDiseaseCommand(int id)
        {
            Id = id;
        }
    }
}

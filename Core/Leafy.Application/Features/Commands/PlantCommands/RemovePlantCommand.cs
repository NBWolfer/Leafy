using MediatR;

namespace Leafy.Application.Features.Commands.PlantCommands
{
    public class RemovePlantCommand : IRequest
    {
        public int Id { get; set; }

        public RemovePlantCommand(int id)
        {
            Id = id;
        }
    }
}

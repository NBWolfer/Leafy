using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leafy.Domain.Entities
{
    public class UserPlant
    {
        public int UserPlantId { get; set; }
        public int PlantId { get; set; }
        public int UserId { get; set; }
        public User user { get; set; }
        public Plant plant { get; set; }
        public string Image { get; set; } = string.Empty;
    }
}

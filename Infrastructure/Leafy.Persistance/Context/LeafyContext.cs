using Leafy.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Leafy.Persistance.Context
{
    public class LeafyContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-98J14D0V;initial Catalog=Leafy;integrated Security=true;TrustServerCertificate= true");
        }

        public DbSet<Plant> Plants { get; set; }
        public DbSet<Disease> Diseases { get; set; }
    }
}

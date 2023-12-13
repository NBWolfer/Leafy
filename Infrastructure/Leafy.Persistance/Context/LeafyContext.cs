using Leafy.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Leafy.Persistance.Context
{
    public class LeafyContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-F6VTMJ0;initial Catalog=Leafy;integrated Security=true;TrustServerCertificate= true");
        }

        public DbSet<Plant> Plants { get; set; }
        public DbSet<Disease> Diseases { get; set; }
    }
}

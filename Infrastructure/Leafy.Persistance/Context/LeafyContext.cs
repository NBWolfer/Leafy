using Leafy.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Leafy.Persistance.Context
{
    public class LeafyContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=MAHMUT-ENES\MSSQL22_S1;initial Catalog=Leafy;integrated Security=true;TrustServerCertificate= true");
        }

        public DbSet<Plant> Plants { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<UserPlant> UserPlants { get; set; }
    }
}

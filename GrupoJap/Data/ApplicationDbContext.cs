using GrupoJap.Models;
using Microsoft.EntityFrameworkCore;

namespace GrupoJap.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>()
                .Property(e => e.FuelType)
                .HasConversion<string>();

            modelBuilder.Entity<Vehicle>()
                .HasIndex(v => v.LicensePlate)
                .IsUnique();

            modelBuilder.Entity<Client>()
                .HasIndex(c => c.Email)
                .IsUnique();
        }

        public DbSet<Vehicle> Vehicles { get; set; } 
        public DbSet<Client> Clients { get; set; } 
        public DbSet<RentalContract> RentalContracts { get; set; } 

    }
    
}

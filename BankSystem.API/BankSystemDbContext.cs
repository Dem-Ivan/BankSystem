using System;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.API
{
    public class BankSystemDbContext : DbContext
    {        
        public BankSystemDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port = 5433; Database = bankSystemDb; Username = postgres; Password = postgres");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null) throw new ArgumentNullException(nameof(modelBuilder));

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}

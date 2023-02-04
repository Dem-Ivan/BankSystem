using System;
using BankSystem.API.EntityConfigurations;
using BankSystem.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.API
{
    public class BankSystemDbContext : DbContext
    {
        public DbSet<Employee> Employee => Set<Employee>();
        public DbSet<Client> Client => Set<Client>();
        public DbSet<Contract> Contract => Set<Contract>();
        public DbSet<ContractHistoryElement> ContractHistory => Set<ContractHistoryElement>();

        public BankSystemDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=bankSystemDb2023;Username=postgres;Password=postgres");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null) throw new ArgumentNullException(nameof(modelBuilder));
            
            modelBuilder.ApplyConfiguration(new EmployeeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ClientEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ContractEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ContractHistoryEntityTypeConfiguration());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}

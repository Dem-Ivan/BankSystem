using System;
using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.API.EntityConfigurations
{
    public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.ToTable(nameof(Employee));

            builder.Property(e => e.Id);
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.Age).IsRequired();
            builder.Property(e => e.Role);

            builder.HasKey(e => e.Id);
            builder.HasMany(x => x.Contracts);
        }
    }
}

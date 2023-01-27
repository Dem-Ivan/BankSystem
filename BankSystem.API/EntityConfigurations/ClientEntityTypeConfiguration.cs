using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.API.EntityConfigurations
{
    public class ClientEntityTypeConfiguration : IEntityTypeConfiguration<Client>
    {      
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.ToTable(nameof(Employee));

            builder.Property(e => e.Id);
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.Age).IsRequired();
           

            builder.HasKey(e => e.Id);
            builder.HasOne(cl => cl.Contract).WithOne(co => co.Counteragent).HasForeignKey<Client>(cl => cl.Id);
        }
    }
}

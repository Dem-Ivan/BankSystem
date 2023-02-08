using System;
using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.API.EntityConfigurations
{
    public class ContractEntityTypeConfiguration : IEntityTypeConfiguration<Contract>
    {      
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.ToTable(nameof(Contract));

            builder.Property(e => e.Id);
            builder.Property(e => e.Status).IsRequired();
            builder.Property(e => e.Body);
            builder.Property(e => e.SignerRole);

            builder.HasKey(e => e.Id);

            builder.HasMany(co => co.History);
            builder.HasOne(co => co.Author).WithMany(em => em.Contracts).HasForeignKey(co => co.AuthorId);            
            builder.HasOne(co => co.Counteragent).WithOne(cl => cl.Contract).HasForeignKey<Contract>(cl => cl.Id);
        }        
    }
}

using System;
using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.API.EntityConfigurations
{
    public class ContractHistoryEntityTypeConfiguration : IEntityTypeConfiguration<ContractHistory>
    {    
        public void Configure(EntityTypeBuilder<ContractHistory> builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.ToTable(nameof(ContractHistory));

            builder.Property(e => e.Id);            
            builder.Property(e => e.ChangeDate);
            builder.Property(e => e.NewStatus);

            builder.HasKey(e => e.Id);
            builder.HasOne(ch => ch.Contract).WithMany(co => co.History).HasForeignKey(ch => ch.ContractId);
        }
    }
}

using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.API.EntityConfigurations;

public class ContractHistoryEntityTypeConfiguration : IEntityTypeConfiguration<ContractHistoryElement>
{
    public void Configure(EntityTypeBuilder<ContractHistoryElement> builder)
    {
        if (builder == null) throw new ArgumentNullException(nameof(builder));

        builder.ToTable(nameof(ContractHistoryElement));

        builder.Property(e => e.Id);            
        builder.Property(e => e.ChangeDate);
        builder.Property(e => e.NewStatus);

        builder.HasKey(e => e.Id);
        builder.HasOne(ch => ch.Contract).WithMany(co => co.History).HasForeignKey(ch => ch.ContractId);
    }
}
﻿using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.API.EntityConfigurations;

public class ClientEntityTypeConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        if (builder == null) throw new ArgumentNullException(nameof(builder));

        builder.ToTable(nameof(Client));

        builder.Property(e => e.Id);
        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.Age).IsRequired();
        builder.Property(e => e.Email).IsRequired();
        builder.Property(e => e.CreationDate).IsRequired();
        builder.Property(e => e.DeletedDate);

        builder.HasKey(e => e.Id);
        builder.HasOne(cl => cl.Contract).WithOne(co => co.Counteragent).HasForeignKey<Client>(cl => cl.Id);
    }
}
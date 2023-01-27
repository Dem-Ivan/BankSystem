using System;
using System.ComponentModel.DataAnnotations.Schema;
using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.API.EntityConfigurations
{
    public class ContractEntityTypeConfiguration : IEntityTypeConfiguration<Contract>
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("status")]
        public Status Status { get; set; }

        [Column("body")]
        public string Body { get; set; }

        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.ToTable(nameof(Contract));

            builder.Property(e => e.Id);
            builder.Property(e => e.Status).IsRequired();
            builder.Property(e => e.Body);

            builder.HasKey(e => e.Id);

            builder.HasOne(co => co.Author).WithMany(em => em.Contracts).HasForeignKey(co => co.CounteragentId);            
            builder.HasOne(co => co.Counteragent).WithOne(cl => cl.Contract).HasForeignKey<Client>(co => co.Id);
        }        
    }
}

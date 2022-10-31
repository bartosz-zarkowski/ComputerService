﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerService.Entities.Configuration;
public class AddressEntityConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Country).IsRequired()
            .HasMaxLength(90);
        builder.Property(x => x.State).IsRequired()
            .HasMaxLength(90);
        builder.Property(x => x.City).IsRequired()
            .HasMaxLength(90);
        builder.Property(x => x.PostalCode).IsRequired()
            .HasMaxLength(18);
        builder.Property(x => x.Street).IsRequired();
        builder.Property(x => x.StreetNumber).IsRequired()
            .HasMaxLength(90);
        builder.Property(x => x.Apartment).HasMaxLength(90);

        builder.HasOne(x => x.Client)
            .WithOne(x => x.Address)
            .HasForeignKey<Address>(x => x.Id);
    }
}

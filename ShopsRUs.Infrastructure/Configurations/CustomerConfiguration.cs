using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopsRUs.Domain.Entity;

namespace ShopsRUs.Infrastructure.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(a => a.Name).HasMaxLength(150).IsRequired();
            builder.HasIndex(a => new {a.PhoneNumber, a.Name}).IsUnique();
        }
    }
}

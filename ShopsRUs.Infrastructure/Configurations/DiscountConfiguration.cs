using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopsRUs.Domain.Entity;

namespace ShopsRUs.Infrastructure.Configurations
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.Property(a => a.Name).HasMaxLength(150).IsRequired();
            builder.HasIndex(a => new {a.Name, a.DiscountType}).IsUnique();
            builder.Ignore(x => x.DiscountTypes);
        }
    }
}
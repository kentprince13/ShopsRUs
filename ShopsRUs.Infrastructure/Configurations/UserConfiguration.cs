using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopsRUs.Domain.Entity;

namespace ShopsRUs.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(a => a.Name).HasMaxLength(150).IsRequired();
            builder.HasIndex(a => new {a.PhoneNumber, a.Name}).IsUnique();
            builder.Ignore(x => x.UsersType);
        }
    }
}
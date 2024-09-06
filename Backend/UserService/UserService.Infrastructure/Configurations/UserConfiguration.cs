using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Host.Models;

namespace UserService.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.UserId);
        builder.HasIndex(x => x.UserId).IsUnique();
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x=> x.Password).IsRequired().HasMaxLength(255);
        builder.Property(x=> x.Username).IsRequired().HasMaxLength(255);
    }
}
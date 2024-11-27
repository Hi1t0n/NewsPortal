using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Models;

namespace UserService.Infrastructure.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.Id);
        builder.HasIndex(r => r.Id).IsUnique();
        builder.Property(r => r.Id).IsRequired();
        builder.HasIndex(r => r.Name).IsUnique();
        builder.Property(r => r.Name).IsRequired();
    }
}
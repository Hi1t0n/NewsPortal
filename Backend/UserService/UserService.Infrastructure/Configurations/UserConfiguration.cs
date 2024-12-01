using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Models;

namespace UserService.Infrastructure.Configurations;

/// <summary>
/// Конфигурация модели <see cref="User"/>
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.UserId);
        builder.HasIndex(x => x.UserId).IsUnique();
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x=> x.Password).IsRequired().HasMaxLength(255);
        builder.Property(x=> x.Username).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Email).IsRequired();
        builder.HasIndex(x => x.Email).IsUnique();
        builder.Property(x => x.EmailConfirmed).IsRequired();
        builder.Property(x => x.PhoneNumber).IsRequired();
        builder.HasIndex(x=> x.PhoneNumber).IsUnique();

        builder.HasOne(x => x.Role)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.RoleId);
    }
}
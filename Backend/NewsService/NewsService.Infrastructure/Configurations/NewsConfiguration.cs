using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsService.Domain.Models;

namespace NewsService.Infrastructure.Configurations;

public class NewsConfiguration : IEntityTypeConfiguration<News>
{
    public void Configure(EntityTypeBuilder<News> builder)
    {
        builder.Property(x => x.Id).IsRequired();
        builder.HasIndex(x => x.Id).IsUnique();
        builder.Property(x => x.Content).IsRequired();
        builder.Property(x => x.DateTime).IsRequired();
        builder.Property(x => x.Title).IsRequired().HasMaxLength(255);
    }
}
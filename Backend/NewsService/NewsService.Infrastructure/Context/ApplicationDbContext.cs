using Microsoft.EntityFrameworkCore;
using NewsService.Domain.Models;
using NewsService.Infrastructure.Configurations;

namespace NewsService.Infrastructure.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new NewsConfiguration());
    }
    
    public DbSet<News> News => Set<News>();
}
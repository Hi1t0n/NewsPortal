using Microsoft.EntityFrameworkCore;
using UserService.Host.Models;
using UserService.Infrastructure.Configurations;

namespace UserService.Infrastructure.Context;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }

    public DbSet<User> Users => Set<User>();
}
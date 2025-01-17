using Microsoft.EntityFrameworkCore;
using UserService.Domain.Models;
using UserService.Infrastructure.Configurations;

namespace UserService.Infrastructure.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }

    public DbSet<User> Users => Set<User>();
}
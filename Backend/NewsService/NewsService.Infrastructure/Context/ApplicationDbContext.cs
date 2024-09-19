using Microsoft.EntityFrameworkCore;
using NewsService.Domain.Models;

namespace NewsService.Infrastructure.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
    
    public DbSet<News> News => Set<News>();
}
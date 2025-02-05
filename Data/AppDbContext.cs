using Microsoft.EntityFrameworkCore;
using TestingBackend.Models;

namespace TestingBackend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    
    public DbSet<Product> Products { get; set; }
}
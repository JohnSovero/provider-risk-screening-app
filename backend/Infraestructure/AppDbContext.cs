using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Infraestructure
{    
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Provider> Providers { get; set; }
    }
}
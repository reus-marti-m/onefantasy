using Microsoft.EntityFrameworkCore;

namespace OneFantasy.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Aquí afegiràs els DbSet<T> quan tinguis entitats,
        // per exemple:
        // public DbSet<User> Users { get; set; }
    }
}

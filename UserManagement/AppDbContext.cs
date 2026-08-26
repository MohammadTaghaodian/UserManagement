using Microsoft.EntityFrameworkCore;
using UserManagement.entities;

namespace UserManagement
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<UserEntities> Users { get; set; } = null!;
    }
}

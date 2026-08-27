using Microsoft.EntityFrameworkCore;
using UserManagement.entities;

namespace UserManagement
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<UserEntities> Users { get; set; } = null!;
        public DbSet<ClassEntity> Class { get; set; } = null!;
        public DbSet<SchoolEntity> School { get; set; } = null!;
    }
}

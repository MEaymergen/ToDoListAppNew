using Microsoft.EntityFrameworkCore;
using ToDoAppWebApi.Models;

namespace ToDoAppWebApi.Data
{
    public class AuthDbContext : DbContext
    {
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

            public DbSet<User> Users { get; set; } // Kullanıcılar tablosu
            public DbSet<TaskItem> Tasks { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // Email'in benzersiz olmasını sağla
                modelBuilder.Entity<User>()
                    .HasIndex(u => u.Email)
                    .IsUnique();
                // Username'in benzersiz olmasını sağla
                modelBuilder.Entity<User>()
                    .HasIndex(u => u.UserName)
                    .IsUnique();
            }
        }
    }
}

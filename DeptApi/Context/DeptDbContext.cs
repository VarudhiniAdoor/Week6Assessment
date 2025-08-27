using Microsoft.EntityFrameworkCore;

namespace DeptApi.Models
{
    public class DeptDbContext : DbContext
    {
        public DeptDbContext(DbContextOptions<DeptDbContext> options) : base(options) { }

        public DbSet<Dept> Departments { get; set; }
        public DbSet<Manager> Managers { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role() { RoleId = 1, RoleName = "Admin" },
                new Role() { RoleId = 2, RoleName = "Manager" },
                new Role() { RoleId = 3, RoleName = "User" }
            );

            modelBuilder.Entity<User>().HasData(
                new User() { UserId = 1, UserName = "admin", Password = "Admin@1234", RoleId = 1 },
                new User() { UserId = 2, UserName = "manager", Password = "Manager@123", RoleId = 2 },
                new User() { UserId = 3, UserName = "user", Password = "User@123", RoleId = 3 }
            );
        }
    }
}

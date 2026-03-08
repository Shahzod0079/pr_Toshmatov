using Microsoft.EntityFrameworkCore;
using pr_26_Toshmatov.Models;
using pr_26_Toshmatov.Classes.Common;

namespace pr_26_Toshmatov.Classes
{
    public class AuthContext : DbContext
    {
        public DbSet<SystemUser> Users { get; set; }

        public AuthContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Common.Config.ConnectionConfig, Common.Config.Version);
        }

        // Явно указываем имя таблицы
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SystemUser>().ToTable("SystemUsers");
        }
    }
}
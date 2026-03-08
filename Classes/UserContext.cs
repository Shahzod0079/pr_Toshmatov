using Microsoft.EntityFrameworkCore;
using pr_26_Toshmatov.Models;
using pr_26_Toshmatov.Classes.Common;


namespace pr_26_Toshmatov.Classes
{
    public class UserContext : DbContext
    {
        public DbSet<Users> Users { get; set; }

        public UserContext() =>
            Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseMySql(Config.ConnectionConfig, Config.Version);
    }
}
using Microsoft.EntityFrameworkCore;
using BanksDB.DataBase.Entities;
using System.Reflection.Emit;


namespace BanksDB.DataBase
{
    public class DataBaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Credit> Credits { get; set; }
        public DbSet<CreditType> CreditTypes { get; set; }

        public DataBaseContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=pass123;Database=HWBankSystem;Port=5432;");
        }

    }
}
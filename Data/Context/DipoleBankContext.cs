using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.Enitities;

namespace Data.Context
{
    public class DipoleBankContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountHistory> AccountHistories { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankUser> BankUsers { get; set; }
        public DipoleBankContext( DbContextOptions<DipoleBankContext> options) :base(options) { }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            SeedRoles(builder);
            base.OnModelCreating(builder);
        }
        public static void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Name = "ADMIN", ConcurrencyStamp = "1", NormalizedName = "ADMIN" },
                new IdentityRole() { Name = "USER", ConcurrencyStamp = "2", NormalizedName = "USER" },
            new IdentityRole() { Name = "CASHIER", ConcurrencyStamp = "3", NormalizedName = "CASHIER" });
        }
    }
}

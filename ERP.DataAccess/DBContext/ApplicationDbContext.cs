using ERP.DataAccess.Domains;
using Microsoft.EntityFrameworkCore;

namespace ERP.DataAccess.DBContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public DbSet<OrganizationBankAccount> OrganizationBankAccount { get; set; }
        public DbSet<Bank> Bank { get; set; }
        public DbSet<CNFCompnay> CNFCompnay { get; set; }
        public DbSet<Border> Border { get; set; }
        public DbSet<BankBranch> BankBranch { get; set; }
        public DbSet<LCTransaction> LCTransaction { get; set; }
        public DbSet<CollectionMode> CollectionMode { get; set; }
    }
}
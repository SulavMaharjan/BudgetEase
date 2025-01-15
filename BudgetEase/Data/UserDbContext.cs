using Microsoft.EntityFrameworkCore;
using BudgetEasee.Models;


namespace BudgetEasee.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            // Delete and recreate the database during initialization (for development/testing purposes)
            //this.Database.EnsureDeleted(); // Use cautiously, mostly for development or testing
            this.Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Debt> Debts { get; set; }



    }
}


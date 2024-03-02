using Microsoft.EntityFrameworkCore;
using Models;

namespace AccountSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> other) : base(other) 
        {
        }

        public DbSet<Account> Accounts { get; set; }
    }
}

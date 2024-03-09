using Assignment.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Core
{
    public class AssignmentContext : DbContext
    {
        public AssignmentContext(DbContextOptions<AssignmentContext> options)
        : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasKey(c => c.Id);
        }
    }
}

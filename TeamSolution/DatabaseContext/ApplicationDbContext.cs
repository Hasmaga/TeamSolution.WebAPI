using Microsoft.EntityFrameworkCore;
using TeamSolution.Model;

namespace TeamSolution.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Configure the relationship between each Table
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

    }
}

using entity_framework_aws_deployment.Models;
using Microsoft.EntityFrameworkCore;

namespace entity_framework_aws_deployment.Services
{
    public class UserDetailsDbContext : DbContext
    {
        public UserDetailsDbContext(DbContextOptions<UserDetailsDbContext> option) : base(option)
        {

        }

        public DbSet<UserDetails> UserDetails { get; set; }
    }
}

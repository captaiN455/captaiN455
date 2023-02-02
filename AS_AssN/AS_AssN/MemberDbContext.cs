using AS_AssN.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace AS_AssN
{
    public class MemberDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public MemberDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _configuration.GetConnectionString("MyConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<AppUser> AppUsers { get; set; }
    }
}

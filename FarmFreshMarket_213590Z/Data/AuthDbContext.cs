    using FarmFreshMarket_213590Z.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FarmFreshMarket_213590Z.Data
{
    public class AuthDbContext : IdentityDbContext<AuthUser>
    {
        private readonly IConfiguration _configuration;
        public AuthDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _configuration.GetConnectionString("AuthConnectionString"); 
            optionsBuilder.UseSqlServer(connectionString);
        }

        
    }
}

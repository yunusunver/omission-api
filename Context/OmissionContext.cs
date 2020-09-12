using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using omission.api.Models;

namespace omission.api.Context
{
    public class OmissionContext : DbContext
    {

        private IConfiguration _configuration;
        public OmissionContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("omissionDB");

            optionsBuilder.UseNpgsql(connectionString);
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Lookup> Lookups { get; set; }
        public DbSet<Code> Codes { get; set; }
        public DbSet<Hashtag> Hashtags { get; set; }

    }
}
using Microsoft.EntityFrameworkCore;

namespace MovieFinder.Dal
{
    public class MovieFinderDbContext : DbContext
    {
        public MovieFinderDbContext(DbContextOptions<MovieFinderDbContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}

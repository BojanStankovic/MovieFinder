using Microsoft.EntityFrameworkCore;
using MovieFinder.Dal.Configurations;
using MovieFinder.Dal.Models;

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
            //Movie property configurations
            MoviesConfiguration.Configure(modelBuilder);

            //ImdbData property configurations
            ImdbDataConfiguration.Configure(modelBuilder);

            //VideoData property configurations
            VideoDataConfiguration.Configure(modelBuilder);
        }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<ImdbData> ImdbData { get; set; }

        public DbSet<VideoData> VideoData { get; set; }
    }
}

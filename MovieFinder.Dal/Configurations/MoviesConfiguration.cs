using Microsoft.EntityFrameworkCore;
using MovieFinder.Dal.Models;

namespace MovieFinder.Dal.Configurations
{
    public static class MoviesConfiguration
    {
        public static void Configure(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .Property(m => m.Id)
                .HasColumnName("Id")
                .IsRequired();

            modelBuilder.Entity<Movie>()
                .Property(m => m.Name)
                .HasColumnName("Name")
                .IsRequired();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using MovieFinder.Dal.Models;

namespace MovieFinder.Dal.Configurations
{
    public static class ImdbDataConfiguration
    {
        public static void Configure(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImdbData>()
                .Property(m => m.Id)
                .HasColumnName("Id")
                .IsRequired();

            modelBuilder.Entity<ImdbData>()
                .Property(m => m.ImdbId)
                .HasColumnName("ImdbId")
                .IsRequired();

            modelBuilder.Entity<ImdbData>()
                .Property(m => m.MovieName)
                .HasColumnName("MovieName")
                .IsRequired();

            modelBuilder.Entity<ImdbData>()
                .Property(m => m.ReleaseYear)
                .HasColumnName("ReleaseYear")
                .IsRequired();
        }
    }
}

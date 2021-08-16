using Microsoft.EntityFrameworkCore;
using MovieFinder.Dal.Models;

namespace MovieFinder.Dal.Configurations
{
    public static class ImdbDataConfiguration
    {
        public static void Configure(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImdbData>()
                .Property(d => d.Id)
                .HasColumnName("Id")
                .IsRequired();

            modelBuilder.Entity<ImdbData>()
                .Property(d => d.ImdbId)
                .HasColumnName("ImdbId")
                .IsRequired();

            modelBuilder.Entity<ImdbData>()
                .Property(d => d.MovieName)
                .HasColumnName("MovieName")
                .IsRequired();

            modelBuilder.Entity<ImdbData>()
                .Property(d => d.ReleaseYear)
                .HasColumnName("ReleaseYear")
                .IsRequired();

            modelBuilder.Entity<ImdbData>()
                .HasOne(d => d.Movie)
                .WithOne(m => m.ImdbData)
                .HasPrincipalKey<ImdbData>(d => d.ImdbId)
                .HasForeignKey<Movie>(m => m.ImdbDataId);

            // Indexes
            modelBuilder.Entity<ImdbData>()
                .HasIndex(d => d.ImdbId)
                .IsUnique();
        }
    }
}

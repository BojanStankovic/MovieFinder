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

            modelBuilder.Entity<Movie>()
                .HasOne(m => m.ImdbData)
                .WithOne(i => i.Movie)
                .HasForeignKey<ImdbData>(m => m.Id);

            modelBuilder.Entity<Movie>()
                .HasMany(m => m.VideoData)
                .WithOne(i => i.Movie)
                .HasForeignKey(vd => vd.Id);
        }
    }
}

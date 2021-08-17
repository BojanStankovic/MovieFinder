using Microsoft.EntityFrameworkCore;
using MovieFinder.Dal.Models;

namespace MovieFinder.Dal.Configurations
{
    public static class TrailerRequestDetailsConfiguration
    {
        public static void Configure(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrailerRequestDetails>()
                .Property(cd => cd.Id)
                .HasColumnName("Id")
                .IsRequired();

            modelBuilder.Entity<TrailerRequestDetails>()
                .Property(cd => cd.RequestedMovieTrailerImdbId)
                .HasColumnName("RequestedMovieTrailerImdbId")
                .IsRequired();

            modelBuilder.Entity<TrailerRequestDetails>()
                .Property(cd => cd.EmailAddress)
                .HasColumnName("EmailAddress")
                .IsRequired();

            modelBuilder.Entity<TrailerRequestDetails>()
                .Property(cd => cd.FirstName)
                .HasColumnName("FirstName")
                .IsRequired();

            modelBuilder.Entity<TrailerRequestDetails>()
                .Property(cd => cd.LastName)
                .HasColumnName("LastName");

            modelBuilder.Entity<TrailerRequestDetails>()
                .Property(d => d.Created)
                .HasColumnName("Created")
                .IsRequired();

            modelBuilder.Entity<TrailerRequestDetails>()
                .Property(d => d.IsFirstRequest)
                .HasColumnName("IsFirstRequest")
                .IsRequired();

            // Indexes
            modelBuilder.Entity<TrailerRequestDetails>()
                .HasIndex(d => d.RequestedMovieTrailerImdbId);

            modelBuilder.Entity<TrailerRequestDetails>()
                .HasIndex(d => d.EmailAddress);
        }
    }
}

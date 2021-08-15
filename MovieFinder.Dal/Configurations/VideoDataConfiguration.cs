using Microsoft.EntityFrameworkCore;
using MovieFinder.Dal.Models;

namespace MovieFinder.Dal.Configurations
{
    public static class VideoDataConfiguration
    {
        public static void Configure(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VideoData>()
                .Property(m => m.Id)
                .HasColumnName("Id")
                .IsRequired();

            modelBuilder.Entity<VideoData>()
                .Property(m => m.VideoUrl)
                .HasColumnName("VideoUrl")
                .IsRequired();

            modelBuilder.Entity<VideoData>()
                .Property(m => m.VideoSourceEnum)
                .HasColumnName("VideoSourceEnum")
                .IsRequired();

            modelBuilder.Entity<VideoData>()
                .HasOne(vd => vd.Movie)
                .WithMany(m => m.VideoData)
                .HasForeignKey(vd => vd.MovieId);
        }
    }
}

﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieFinder.Dal;

namespace MovieFinder.Dal.Migrations
{
    [DbContext(typeof(MovieFinderDbContext))]
    partial class MovieFinderDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MovieFinder.Dal.Models.ImdbData", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<string>("ImdbId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ImdbId");

                    b.Property<string>("MovieName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("MovieName");

                    b.Property<int>("ReleaseYear")
                        .HasColumnType("int")
                        .HasColumnName("ReleaseYear");

                    b.HasKey("Id");

                    b.ToTable("ImdbData");
                });

            modelBuilder.Entity("MovieFinder.Dal.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("MovieFinder.Dal.Models.VideoData", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<int>("VideoSourceEnum")
                        .HasColumnType("int")
                        .HasColumnName("VideoSourceEnum");

                    b.Property<string>("VideoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("VideoUrl");

                    b.HasKey("Id");

                    b.ToTable("VideoData");
                });

            modelBuilder.Entity("MovieFinder.Dal.Models.ImdbData", b =>
                {
                    b.HasOne("MovieFinder.Dal.Models.Movie", "Movie")
                        .WithOne("ImdbData")
                        .HasForeignKey("MovieFinder.Dal.Models.ImdbData", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MovieFinder.Dal.Models.VideoData", b =>
                {
                    b.HasOne("MovieFinder.Dal.Models.Movie", "Movie")
                        .WithMany("VideoData")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MovieFinder.Dal.Models.Movie", b =>
                {
                    b.Navigation("ImdbData");

                    b.Navigation("VideoData");
                });
#pragma warning restore 612, 618
        }
    }
}

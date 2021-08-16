﻿// <auto-generated />
using System;
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
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImdbId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ImdbId");

                    b.Property<string>("MovieName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("MovieName");

                    b.Property<int>("ReleaseYear")
                        .HasColumnType("int")
                        .HasColumnName("ReleaseYear");

                    b.HasKey("Id");

                    b.HasIndex("ImdbId")
                        .IsUnique();

                    b.ToTable("ImdbData");
                });

            modelBuilder.Entity("MovieFinder.Dal.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2")
                        .HasColumnName("Created");

                    b.Property<string>("ImdbDataId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2")
                        .HasColumnName("Modified");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.HasIndex("ImdbDataId")
                        .IsUnique()
                        .HasFilter("[ImdbDataId] IS NOT NULL");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("MovieFinder.Dal.Models.VideoData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThumbnailUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VideoSourceEnum")
                        .HasColumnType("int")
                        .HasColumnName("VideoSourceEnum");

                    b.Property<string>("VideoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("VideoUrl");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.ToTable("VideoData");
                });

            modelBuilder.Entity("MovieFinder.Dal.Models.Movie", b =>
                {
                    b.HasOne("MovieFinder.Dal.Models.ImdbData", "ImdbData")
                        .WithOne("Movie")
                        .HasForeignKey("MovieFinder.Dal.Models.Movie", "ImdbDataId")
                        .HasPrincipalKey("MovieFinder.Dal.Models.ImdbData", "ImdbId");

                    b.Navigation("ImdbData");
                });

            modelBuilder.Entity("MovieFinder.Dal.Models.VideoData", b =>
                {
                    b.HasOne("MovieFinder.Dal.Models.Movie", "Movie")
                        .WithMany("VideoData")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MovieFinder.Dal.Models.ImdbData", b =>
                {
                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MovieFinder.Dal.Models.Movie", b =>
                {
                    b.Navigation("VideoData");
                });
#pragma warning restore 612, 618
        }
    }
}

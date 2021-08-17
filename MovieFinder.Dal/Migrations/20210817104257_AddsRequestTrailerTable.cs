using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieFinder.Dal.Migrations
{
    public partial class AddsRequestTrailerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrailerRequestDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestedMovieTrailerImdbId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsFirstRequest = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrailerRequestDetails", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrailerRequestDetails_EmailAddress",
                table: "TrailerRequestDetails",
                column: "EmailAddress");

            migrationBuilder.CreateIndex(
                name: "IX_TrailerRequestDetails_RequestedMovieTrailerImdbId",
                table: "TrailerRequestDetails",
                column: "RequestedMovieTrailerImdbId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrailerRequestDetails");
        }
    }
}

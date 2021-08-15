using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieFinder.Dal.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImdbData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ImdbId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MovieName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImdbData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImdbData_Movies_Id",
                        column: x => x.Id,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoSourceEnum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoData_Movies_Id",
                        column: x => x.Id,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImdbData");

            migrationBuilder.DropTable(
                name: "VideoData");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}

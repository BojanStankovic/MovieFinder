using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieFinder.Dal.Migrations
{
    public partial class UpdateImdbDataTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullTitle",
                table: "ImdbData",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullTitle",
                table: "ImdbData");
        }
    }
}

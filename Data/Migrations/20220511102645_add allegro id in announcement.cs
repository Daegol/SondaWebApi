using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addallegroidinannouncement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AllegroId",
                table: "Announcements",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllegroId",
                table: "Announcements");
        }
    }
}

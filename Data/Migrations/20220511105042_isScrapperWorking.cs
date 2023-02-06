using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class isScrapperWorking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsScrapperWorking",
                table: "AnnouncementCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsScrapperWorking",
                table: "AnnouncementCategories");
        }
    }
}

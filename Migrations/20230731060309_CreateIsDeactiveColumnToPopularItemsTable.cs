using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeZoneBack.Migrations
{
    public partial class CreateIsDeactiveColumnToPopularItemsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeactive",
                table: "PopularItems",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeactive",
                table: "PopularItems");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PriceConsoleWithPostgreSQL.Migrations
{
    public partial class AddIsDeletedFtl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FullTruckLoads",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FullTruckLoads");
        }
    }
}

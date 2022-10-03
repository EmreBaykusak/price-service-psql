using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PriceConsoleWithPostgreSQL.Migrations
{
    public partial class AddTotalWeightColumnForFTLWithFeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalWeight",
                table: "FullTruckLoadWithFeatures",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalWeight",
                table: "FullTruckLoadWithFeatures");
        }
    }
}

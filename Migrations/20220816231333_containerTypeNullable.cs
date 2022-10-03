using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PriceConsoleWithPostgreSQL.Migrations
{
    public partial class containerTypeNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ContainerType",
                table: "FullTruckLoadWithFeatures",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ContainerType",
                table: "FullTruckLoadWithFeatures",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}

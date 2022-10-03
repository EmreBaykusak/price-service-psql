using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PriceConsoleWithPostgreSQL.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FullTruckLoads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PriceValue = table.Column<double>(type: "double precision", nullable: false),
                    TransportationType = table.Column<string>(type: "text", nullable: false),
                    OriginCountry = table.Column<string>(type: "text", nullable: false),
                    DestinationCountry = table.Column<string>(type: "text", nullable: false),
                    TruckId = table.Column<int>(type: "integer", nullable: false),
                    TotalWeight = table.Column<double>(type: "double precision", nullable: false),
                    Distance = table.Column<double>(type: "double precision", nullable: false),
                    PricePerKm = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FullTruckLoads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LessThanTruckLoads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Width = table.Column<double>(type: "double precision", nullable: false),
                    Height = table.Column<double>(type: "double precision", nullable: false),
                    Length = table.Column<double>(type: "double precision", nullable: false),
                    PriceValue = table.Column<double>(type: "double precision", nullable: false),
                    TransportationType = table.Column<string>(type: "text", nullable: false),
                    OriginCountry = table.Column<string>(type: "text", nullable: false),
                    DestinationCountry = table.Column<string>(type: "text", nullable: false),
                    TruckId = table.Column<int>(type: "integer", nullable: false),
                    TotalWeight = table.Column<double>(type: "double precision", nullable: false),
                    Distance = table.Column<double>(type: "double precision", nullable: false),
                    PricePerKm = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessThanTruckLoads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DriverFullTruckLoad",
                columns: table => new
                {
                    DriversId = table.Column<int>(type: "integer", nullable: false),
                    FullTruckLoadsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverFullTruckLoad", x => new { x.DriversId, x.FullTruckLoadsId });
                    table.ForeignKey(
                        name: "FK_DriverFullTruckLoad_Drivers_DriversId",
                        column: x => x.DriversId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverFullTruckLoad_FullTruckLoads_FullTruckLoadsId",
                        column: x => x.FullTruckLoadsId,
                        principalTable: "FullTruckLoads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FullTruckLoadFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullTruckLoad_Id = table.Column<int>(type: "integer", nullable: false),
                    ContainerType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FullTruckLoadFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FullTruckLoads_FullTruckLoadFeatures_FullTruckLoad_Id",
                        column: x => x.FullTruckLoad_Id,
                        principalTable: "FullTruckLoads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DriverLessThanTruckLoad",
                columns: table => new
                {
                    DriversId = table.Column<int>(type: "integer", nullable: false),
                    LessThanTruckLoadsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverLessThanTruckLoad", x => new { x.DriversId, x.LessThanTruckLoadsId });
                    table.ForeignKey(
                        name: "FK_DriverLessThanTruckLoad_Drivers_DriversId",
                        column: x => x.DriversId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverLessThanTruckLoad_LessThanTruckLoads_LessThanTruckLoa~",
                        column: x => x.LessThanTruckLoadsId,
                        principalTable: "LessThanTruckLoads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriverFullTruckLoad_FullTruckLoadsId",
                table: "DriverFullTruckLoad",
                column: "FullTruckLoadsId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverLessThanTruckLoad_LessThanTruckLoadsId",
                table: "DriverLessThanTruckLoad",
                column: "LessThanTruckLoadsId");

            migrationBuilder.CreateIndex(
                name: "IX_FullTruckLoadFeatures_FullTruckLoad_Id",
                table: "FullTruckLoadFeatures",
                column: "FullTruckLoad_Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DriverFullTruckLoad");

            migrationBuilder.DropTable(
                name: "DriverLessThanTruckLoad");

            migrationBuilder.DropTable(
                name: "FullTruckLoadFeatures");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "LessThanTruckLoads");

            migrationBuilder.DropTable(
                name: "FullTruckLoads");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CommandLine.Migrations
{
    public partial class create_citygeocoding_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CityGeocoding",
                schema: "Generic",
                columns: table => new
                {
                    CityGeocodingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Lat = table.Column<double>(type: "float", nullable: false),
                    Lng = table.Column<double>(type: "float", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityGeocoding", x => x.CityGeocodingId);
                    table.ForeignKey(
                        name: "FK_CityGeocoding_City_CityId",
                        column: x => x.CityId,
                        principalSchema: "Generic",
                        principalTable: "City",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CityGeocoding_CityId",
                schema: "Generic",
                table: "CityGeocoding",
                column: "CityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CityGeocoding",
                schema: "Generic");
        }
    }
}

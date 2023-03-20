using Microsoft.EntityFrameworkCore.Migrations;

namespace CommandLine.Migrations
{
    public partial class CreateSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Generic");

            migrationBuilder.RenameTable(
                name: "Province",
                newName: "Province",
                newSchema: "Generic");

            migrationBuilder.RenameTable(
                name: "GeoArea",
                newName: "GeoArea",
                newSchema: "Generic");

            migrationBuilder.RenameTable(
                name: "Country",
                newName: "Country",
                newSchema: "Generic");

            migrationBuilder.RenameTable(
                name: "City",
                newName: "City",
                newSchema: "Generic");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Province",
                schema: "Generic",
                newName: "Province");

            migrationBuilder.RenameTable(
                name: "GeoArea",
                schema: "Generic",
                newName: "GeoArea");

            migrationBuilder.RenameTable(
                name: "Country",
                schema: "Generic",
                newName: "Country");

            migrationBuilder.RenameTable(
                name: "City",
                schema: "Generic",
                newName: "City");
        }
    }
}

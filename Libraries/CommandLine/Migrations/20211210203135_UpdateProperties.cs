using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CommandLine.Migrations
{
    public partial class UpdateProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Generic",
                table: "Province",
                newName: "ProvinceId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Generic",
                table: "GeoArea",
                newName: "GeoAreaId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Generic",
                table: "Country",
                newName: "CountryId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Generic",
                table: "City",
                newName: "CityId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "Generic",
                table: "Province",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DPA_PPGA",
                schema: "Generic",
                table: "Province",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IssuerId",
                schema: "Generic",
                table: "Province",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "Generic",
                table: "GeoArea",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "Generic",
                table: "Country",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DPA_PPGA",
                schema: "Generic",
                table: "City",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "Generic",
                table: "Province");

            migrationBuilder.DropColumn(
                name: "DPA_PPGA",
                schema: "Generic",
                table: "Province");

            migrationBuilder.DropColumn(
                name: "IssuerId",
                schema: "Generic",
                table: "Province");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "Generic",
                table: "GeoArea");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "Generic",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "DPA_PPGA",
                schema: "Generic",
                table: "City");

            migrationBuilder.RenameColumn(
                name: "ProvinceId",
                schema: "Generic",
                table: "Province",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "GeoAreaId",
                schema: "Generic",
                table: "GeoArea",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                schema: "Generic",
                table: "Country",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CityId",
                schema: "Generic",
                table: "City",
                newName: "Id");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Adion.FA.Infrastructure.Core.Data.Migrations.AdionFADb
{
    public partial class _renameColAbsolutePathToWorkspacePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AbsoluteWorkspacePath",
                table: "ProjectConfiguration",
                newName: "WorkspacePath");

            /*
            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 232, DateTimeKind.Utc).AddTicks(9723));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 233, DateTimeKind.Utc).AddTicks(1806));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 233, DateTimeKind.Utc).AddTicks(3698));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 233, DateTimeKind.Utc).AddTicks(5515));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 233, DateTimeKind.Utc).AddTicks(7332));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 233, DateTimeKind.Utc).AddTicks(9114));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 234, DateTimeKind.Utc).AddTicks(889));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 234, DateTimeKind.Utc).AddTicks(2694));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 234, DateTimeKind.Utc).AddTicks(4466));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 10,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 234, DateTimeKind.Utc).AddTicks(6232));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 11,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 234, DateTimeKind.Utc).AddTicks(8006));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 12,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 234, DateTimeKind.Utc).AddTicks(9786));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 13,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 235, DateTimeKind.Utc).AddTicks(1574));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 14,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 235, DateTimeKind.Utc).AddTicks(3336));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 15,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 235, DateTimeKind.Utc).AddTicks(5110));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 235, DateTimeKind.Utc).AddTicks(6873));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 17,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 235, DateTimeKind.Utc).AddTicks(8644));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 18,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 236, DateTimeKind.Utc).AddTicks(416));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 19,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 236, DateTimeKind.Utc).AddTicks(2209));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 20,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 236, DateTimeKind.Utc).AddTicks(3976));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 21,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 236, DateTimeKind.Utc).AddTicks(5750));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 22,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 236, DateTimeKind.Utc).AddTicks(7505));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 23,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 236, DateTimeKind.Utc).AddTicks(9262));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 24,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 237, DateTimeKind.Utc).AddTicks(1042));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 25,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 237, DateTimeKind.Utc).AddTicks(2869));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 26,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 237, DateTimeKind.Utc).AddTicks(4642));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 27,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 237, DateTimeKind.Utc).AddTicks(6403));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 28,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 237, DateTimeKind.Utc).AddTicks(8164));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 29,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 237, DateTimeKind.Utc).AddTicks(9936));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 30,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 238, DateTimeKind.Utc).AddTicks(1726));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 31,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 238, DateTimeKind.Utc).AddTicks(3505));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 32,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 238, DateTimeKind.Utc).AddTicks(5276));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 33,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 238, DateTimeKind.Utc).AddTicks(7051));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 34,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 238, DateTimeKind.Utc).AddTicks(8820));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 35,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 239, DateTimeKind.Utc).AddTicks(603));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 36,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 239, DateTimeKind.Utc).AddTicks(2685));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 37,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 239, DateTimeKind.Utc).AddTicks(4479));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 38,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 239, DateTimeKind.Utc).AddTicks(6270));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 39,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 239, DateTimeKind.Utc).AddTicks(8057));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 40,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 239, DateTimeKind.Utc).AddTicks(9817));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 41,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 240, DateTimeKind.Utc).AddTicks(1587));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 42,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 240, DateTimeKind.Utc).AddTicks(3355));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 43,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 240, DateTimeKind.Utc).AddTicks(5122));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 44,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 240, DateTimeKind.Utc).AddTicks(6887));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 45,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 240, DateTimeKind.Utc).AddTicks(8689));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 46,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 241, DateTimeKind.Utc).AddTicks(455));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 47,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 241, DateTimeKind.Utc).AddTicks(2287));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 48,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 241, DateTimeKind.Utc).AddTicks(4052));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 49,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 241, DateTimeKind.Utc).AddTicks(5824));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 50,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 241, DateTimeKind.Utc).AddTicks(7592));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 51,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 241, DateTimeKind.Utc).AddTicks(9350));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 52,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 242, DateTimeKind.Utc).AddTicks(1114));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 53,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 242, DateTimeKind.Utc).AddTicks(2909));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 54,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 242, DateTimeKind.Utc).AddTicks(4707));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 55,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 242, DateTimeKind.Utc).AddTicks(6597));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 56,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 242, DateTimeKind.Utc).AddTicks(8363));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 57,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 243, DateTimeKind.Utc).AddTicks(114));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 58,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 243, DateTimeKind.Utc).AddTicks(1903));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 59,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 243, DateTimeKind.Utc).AddTicks(3671));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 60,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 243, DateTimeKind.Utc).AddTicks(5433));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 61,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 243, DateTimeKind.Utc).AddTicks(7220));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 62,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 243, DateTimeKind.Utc).AddTicks(8984));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 63,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 244, DateTimeKind.Utc).AddTicks(748));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 64,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 244, DateTimeKind.Utc).AddTicks(2536));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 65,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 244, DateTimeKind.Utc).AddTicks(4309));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 66,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 244, DateTimeKind.Utc).AddTicks(6116));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 67,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 244, DateTimeKind.Utc).AddTicks(7877));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 68,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 244, DateTimeKind.Utc).AddTicks(9681));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 69,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 245, DateTimeKind.Utc).AddTicks(1469));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 70,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 245, DateTimeKind.Utc).AddTicks(3244));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 71,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 245, DateTimeKind.Utc).AddTicks(5014));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 72,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 245, DateTimeKind.Utc).AddTicks(6784));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 73,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 245, DateTimeKind.Utc).AddTicks(8553));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 74,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 246, DateTimeKind.Utc).AddTicks(320));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 75,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 246, DateTimeKind.Utc).AddTicks(2095));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 76,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 246, DateTimeKind.Utc).AddTicks(3858));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 77,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 246, DateTimeKind.Utc).AddTicks(5635));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 78,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 246, DateTimeKind.Utc).AddTicks(7416));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 79,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 246, DateTimeKind.Utc).AddTicks(9190));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 80,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 247, DateTimeKind.Utc).AddTicks(964));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 81,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 247, DateTimeKind.Utc).AddTicks(2759));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 82,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 247, DateTimeKind.Utc).AddTicks(4574));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 83,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 247, DateTimeKind.Utc).AddTicks(6338));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 84,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 247, DateTimeKind.Utc).AddTicks(8102));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 85,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 247, DateTimeKind.Utc).AddTicks(9894));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 86,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 248, DateTimeKind.Utc).AddTicks(1678));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 87,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 248, DateTimeKind.Utc).AddTicks(3447));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 88,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 248, DateTimeKind.Utc).AddTicks(5225));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 89,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 248, DateTimeKind.Utc).AddTicks(6991));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 90,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 248, DateTimeKind.Utc).AddTicks(8809));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 91,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 249, DateTimeKind.Utc).AddTicks(583));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 92,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 249, DateTimeKind.Utc).AddTicks(2360));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 93,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 249, DateTimeKind.Utc).AddTicks(4126));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 94,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 249, DateTimeKind.Utc).AddTicks(5901));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 95,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 249, DateTimeKind.Utc).AddTicks(7675));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 96,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 249, DateTimeKind.Utc).AddTicks(9439));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 97,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 250, DateTimeKind.Utc).AddTicks(1212));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 98,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 250, DateTimeKind.Utc).AddTicks(2982));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 99,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 250, DateTimeKind.Utc).AddTicks(4796));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 100,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 250, DateTimeKind.Utc).AddTicks(6570));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 101,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 250, DateTimeKind.Utc).AddTicks(8338));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 102,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 251, DateTimeKind.Utc).AddTicks(100));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 103,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 251, DateTimeKind.Utc).AddTicks(1888));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 104,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 251, DateTimeKind.Utc).AddTicks(3658));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 105,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 251, DateTimeKind.Utc).AddTicks(5426));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 106,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 251, DateTimeKind.Utc).AddTicks(7193));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 107,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 251, DateTimeKind.Utc).AddTicks(8962));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 108,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 252, DateTimeKind.Utc).AddTicks(724));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 109,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 252, DateTimeKind.Utc).AddTicks(2510));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 110,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 252, DateTimeKind.Utc).AddTicks(4279));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 111,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 252, DateTimeKind.Utc).AddTicks(6037));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 112,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 252, DateTimeKind.Utc).AddTicks(7805));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 113,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 252, DateTimeKind.Utc).AddTicks(9622));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 114,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 253, DateTimeKind.Utc).AddTicks(1406));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 115,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 253, DateTimeKind.Utc).AddTicks(3263));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 116,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 253, DateTimeKind.Utc).AddTicks(5059));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 117,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 253, DateTimeKind.Utc).AddTicks(6836));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 118,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 253, DateTimeKind.Utc).AddTicks(8611));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 119,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 254, DateTimeKind.Utc).AddTicks(385));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 120,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 254, DateTimeKind.Utc).AddTicks(2241));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 121,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 254, DateTimeKind.Utc).AddTicks(4017));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 122,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 254, DateTimeKind.Utc).AddTicks(5791));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 123,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 254, DateTimeKind.Utc).AddTicks(7561));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 124,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 254, DateTimeKind.Utc).AddTicks(9331));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 125,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 255, DateTimeKind.Utc).AddTicks(1116));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 126,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 255, DateTimeKind.Utc).AddTicks(2962));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 127,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 255, DateTimeKind.Utc).AddTicks(4762));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 128,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 255, DateTimeKind.Utc).AddTicks(6555));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 129,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 255, DateTimeKind.Utc).AddTicks(8362));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 130,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 256, DateTimeKind.Utc).AddTicks(142));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 131,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 256, DateTimeKind.Utc).AddTicks(1933));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 132,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 256, DateTimeKind.Utc).AddTicks(3721));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 133,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 256, DateTimeKind.Utc).AddTicks(5496));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 134,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 256, DateTimeKind.Utc).AddTicks(7283));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 135,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 256, DateTimeKind.Utc).AddTicks(9051));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 136,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 257, DateTimeKind.Utc).AddTicks(894));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 137,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 257, DateTimeKind.Utc).AddTicks(2693));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 138,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 257, DateTimeKind.Utc).AddTicks(4470));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 139,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 257, DateTimeKind.Utc).AddTicks(6243));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 140,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 257, DateTimeKind.Utc).AddTicks(8016));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 141,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 257, DateTimeKind.Utc).AddTicks(9796));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 142,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 258, DateTimeKind.Utc).AddTicks(1626));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 143,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 258, DateTimeKind.Utc).AddTicks(3403));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 144,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 258, DateTimeKind.Utc).AddTicks(5179));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 145,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 258, DateTimeKind.Utc).AddTicks(6956));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 146,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 258, DateTimeKind.Utc).AddTicks(8727));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 147,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 259, DateTimeKind.Utc).AddTicks(593));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 148,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 259, DateTimeKind.Utc).AddTicks(2389));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 149,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 259, DateTimeKind.Utc).AddTicks(4163));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 150,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 259, DateTimeKind.Utc).AddTicks(5937));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 151,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 259, DateTimeKind.Utc).AddTicks(7710));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 152,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 259, DateTimeKind.Utc).AddTicks(9487));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 153,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 260, DateTimeKind.Utc).AddTicks(1276));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 154,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 260, DateTimeKind.Utc).AddTicks(3053));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 155,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 260, DateTimeKind.Utc).AddTicks(4842));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 156,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 260, DateTimeKind.Utc).AddTicks(6610));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 157,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 260, DateTimeKind.Utc).AddTicks(8388));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 158,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 261, DateTimeKind.Utc).AddTicks(163));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 159,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 261, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 160,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 261, DateTimeKind.Utc).AddTicks(3759));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 161,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 261, DateTimeKind.Utc).AddTicks(5548));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 162,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 261, DateTimeKind.Utc).AddTicks(7328));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 163,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 261, DateTimeKind.Utc).AddTicks(9104));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 164,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 262, DateTimeKind.Utc).AddTicks(890));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 165,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 262, DateTimeKind.Utc).AddTicks(2673));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 166,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 262, DateTimeKind.Utc).AddTicks(4439));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 167,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 262, DateTimeKind.Utc).AddTicks(6231));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 168,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 262, DateTimeKind.Utc).AddTicks(8028));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 169,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 262, DateTimeKind.Utc).AddTicks(9816));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 170,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 263, DateTimeKind.Utc).AddTicks(1617));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 171,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 263, DateTimeKind.Utc).AddTicks(3400));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 172,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 263, DateTimeKind.Utc).AddTicks(5172));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 173,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 263, DateTimeKind.Utc).AddTicks(6950));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 174,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 263, DateTimeKind.Utc).AddTicks(8727));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 175,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 264, DateTimeKind.Utc).AddTicks(505));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 176,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 264, DateTimeKind.Utc).AddTicks(2315));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 177,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 264, DateTimeKind.Utc).AddTicks(4184));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 178,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 264, DateTimeKind.Utc).AddTicks(5973));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 179,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 264, DateTimeKind.Utc).AddTicks(7751));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 180,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 264, DateTimeKind.Utc).AddTicks(9523));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 181,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 265, DateTimeKind.Utc).AddTicks(1375));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 182,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 265, DateTimeKind.Utc).AddTicks(3150));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 183,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 265, DateTimeKind.Utc).AddTicks(4933));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 184,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 265, DateTimeKind.Utc).AddTicks(6711));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 185,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 265, DateTimeKind.Utc).AddTicks(8488));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 186,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 266, DateTimeKind.Utc).AddTicks(275));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 187,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 266, DateTimeKind.Utc).AddTicks(2078));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 188,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 266, DateTimeKind.Utc).AddTicks(3846));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 189,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 266, DateTimeKind.Utc).AddTicks(5621));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 190,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 266, DateTimeKind.Utc).AddTicks(7398));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 191,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 266, DateTimeKind.Utc).AddTicks(9176));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 192,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 267, DateTimeKind.Utc).AddTicks(961));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 193,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 267, DateTimeKind.Utc).AddTicks(2740));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 194,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 267, DateTimeKind.Utc).AddTicks(4516));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 195,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 267, DateTimeKind.Utc).AddTicks(6289));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 196,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 267, DateTimeKind.Utc).AddTicks(8066));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 197,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 267, DateTimeKind.Utc).AddTicks(9849));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 198,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 268, DateTimeKind.Utc).AddTicks(1638));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 199,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 268, DateTimeKind.Utc).AddTicks(3430));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 200,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 268, DateTimeKind.Utc).AddTicks(5214));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 201,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 268, DateTimeKind.Utc).AddTicks(6995));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 202,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 268, DateTimeKind.Utc).AddTicks(8774));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 203,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 269, DateTimeKind.Utc).AddTicks(567));

            migrationBuilder.UpdateData(
                table: "CurrencyPair",
                keyColumn: "CurrencyPairId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 212, DateTimeKind.Utc).AddTicks(3387));

            migrationBuilder.UpdateData(
                table: "CurrencyPair",
                keyColumn: "CurrencyPairId",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 212, DateTimeKind.Utc).AddTicks(5650));

            migrationBuilder.UpdateData(
                table: "CurrencyPeriod",
                keyColumn: "CurrencyPeriodId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 212, DateTimeKind.Utc).AddTicks(9913));

            migrationBuilder.UpdateData(
                table: "CurrencyPeriod",
                keyColumn: "CurrencyPeriodId",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 213, DateTimeKind.Utc).AddTicks(2139));

            migrationBuilder.UpdateData(
                table: "CurrencyPeriod",
                keyColumn: "CurrencyPeriodId",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 213, DateTimeKind.Utc).AddTicks(4091));

            migrationBuilder.UpdateData(
                table: "CurrencyPeriod",
                keyColumn: "CurrencyPeriodId",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 213, DateTimeKind.Utc).AddTicks(7643));

            migrationBuilder.UpdateData(
                table: "CurrencyPeriod",
                keyColumn: "CurrencyPeriodId",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 213, DateTimeKind.Utc).AddTicks(9610));

            migrationBuilder.UpdateData(
                table: "CurrencyPeriod",
                keyColumn: "CurrencyPeriodId",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 214, DateTimeKind.Utc).AddTicks(1478));

            migrationBuilder.UpdateData(
                table: "CurrencyPeriod",
                keyColumn: "CurrencyPeriodId",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 214, DateTimeKind.Utc).AddTicks(3261));

            migrationBuilder.UpdateData(
                table: "CurrencyPeriod",
                keyColumn: "CurrencyPeriodId",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 214, DateTimeKind.Utc).AddTicks(5069));

            migrationBuilder.UpdateData(
                table: "CurrencyPeriod",
                keyColumn: "CurrencyPeriodId",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 214, DateTimeKind.Utc).AddTicks(6839));

            migrationBuilder.UpdateData(
                table: "CurrencySpread",
                keyColumn: "CurrencySpreadId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 215, DateTimeKind.Utc).AddTicks(92));

            migrationBuilder.UpdateData(
                table: "CurrencySpread",
                keyColumn: "CurrencySpreadId",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 215, DateTimeKind.Utc).AddTicks(2213));

            migrationBuilder.UpdateData(
                table: "CurrencySpread",
                keyColumn: "CurrencySpreadId",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 215, DateTimeKind.Utc).AddTicks(4029));

            migrationBuilder.UpdateData(
                table: "CurrencySpread",
                keyColumn: "CurrencySpreadId",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 215, DateTimeKind.Utc).AddTicks(5890));

            migrationBuilder.UpdateData(
                table: "CurrencySpread",
                keyColumn: "CurrencySpreadId",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 215, DateTimeKind.Utc).AddTicks(7795));

            migrationBuilder.UpdateData(
                table: "CurrencySpread",
                keyColumn: "CurrencySpreadId",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 215, DateTimeKind.Utc).AddTicks(9584));

            migrationBuilder.UpdateData(
                table: "CurrencySpread",
                keyColumn: "CurrencySpreadId",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 216, DateTimeKind.Utc).AddTicks(1478));

            migrationBuilder.UpdateData(
                table: "CurrencySpread",
                keyColumn: "CurrencySpreadId",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 216, DateTimeKind.Utc).AddTicks(3335));

            migrationBuilder.UpdateData(
                table: "CurrencySpread",
                keyColumn: "CurrencySpreadId",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 219, DateTimeKind.Utc).AddTicks(5757));

            migrationBuilder.UpdateData(
                table: "CurrencySpread",
                keyColumn: "CurrencySpreadId",
                keyValue: 10,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 219, DateTimeKind.Utc).AddTicks(7867));

            migrationBuilder.UpdateData(
                table: "CurrencySpread",
                keyColumn: "CurrencySpreadId",
                keyValue: 11,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 219, DateTimeKind.Utc).AddTicks(9743));

            migrationBuilder.UpdateData(
                table: "CurrencySpread",
                keyColumn: "CurrencySpreadId",
                keyValue: 12,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 220, DateTimeKind.Utc).AddTicks(1515));

            migrationBuilder.UpdateData(
                table: "CurrencySpread",
                keyColumn: "CurrencySpreadId",
                keyValue: 13,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 220, DateTimeKind.Utc).AddTicks(3288));

            migrationBuilder.UpdateData(
                table: "CurrencySpread",
                keyColumn: "CurrencySpreadId",
                keyValue: 14,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 220, DateTimeKind.Utc).AddTicks(5017));

            migrationBuilder.UpdateData(
                table: "CurrencySpread",
                keyColumn: "CurrencySpreadId",
                keyValue: 15,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 220, DateTimeKind.Utc).AddTicks(6750));

            migrationBuilder.UpdateData(
                table: "EntityType",
                keyColumn: "EntityTypeId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 208, DateTimeKind.Utc).AddTicks(6362));

            migrationBuilder.UpdateData(
                table: "EntityType",
                keyColumn: "EntityTypeId",
                keyValue: 400,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 209, DateTimeKind.Utc).AddTicks(7400));

            migrationBuilder.UpdateData(
                table: "EntityType",
                keyColumn: "EntityTypeId",
                keyValue: 500,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 209, DateTimeKind.Utc).AddTicks(9472));

            migrationBuilder.UpdateData(
                table: "EntityType",
                keyColumn: "EntityTypeId",
                keyValue: 501,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 210, DateTimeKind.Utc).AddTicks(1373));

            migrationBuilder.UpdateData(
                table: "EntityType",
                keyColumn: "EntityTypeId",
                keyValue: 502,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 210, DateTimeKind.Utc).AddTicks(3460));

            migrationBuilder.UpdateData(
                table: "EntityType",
                keyColumn: "EntityTypeId",
                keyValue: 503,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 210, DateTimeKind.Utc).AddTicks(5401));

            migrationBuilder.UpdateData(
                table: "EntityType",
                keyColumn: "EntityTypeId",
                keyValue: 504,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 210, DateTimeKind.Utc).AddTicks(7259));

            migrationBuilder.UpdateData(
                table: "EntityType",
                keyColumn: "EntityTypeId",
                keyValue: 505,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 210, DateTimeKind.Utc).AddTicks(9101));

            migrationBuilder.UpdateData(
                table: "Market",
                keyColumn: "MarketId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 220, DateTimeKind.Utc).AddTicks(9984));

            migrationBuilder.UpdateData(
                table: "MarketRegion",
                keyColumn: "MarketRegionId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 221, DateTimeKind.Utc).AddTicks(2887));

            migrationBuilder.UpdateData(
                table: "MarketRegion",
                keyColumn: "MarketRegionId",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 221, DateTimeKind.Utc).AddTicks(4852));

            migrationBuilder.UpdateData(
                table: "MarketRegion",
                keyColumn: "MarketRegionId",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 221, DateTimeKind.Utc).AddTicks(6607));

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "OrganizationId",
                keyValue: "22222222-2222-2222-2222-222222222222",
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 230, DateTimeKind.Utc).AddTicks(3736));

            migrationBuilder.UpdateData(
                table: "ProjectGlobalConfiguration",
                keyColumn: "ProjectGlobalConfigurationId",
                keyValue: 1,
                columns: new[] { "CreatedOn", "StartDate" },
                values: new object[] { new DateTime(2023, 1, 5, 13, 23, 7, 231, DateTimeKind.Utc).AddTicks(4566), new DateTime(2023, 1, 5, 13, 23, 7, 231, DateTimeKind.Utc).AddTicks(4089) });

            migrationBuilder.UpdateData(
                table: "ProjectGlobalScheduleConfiguration",
                keyColumn: "ProjectGlobalScheduleConfigurationId",
                keyValue: 1,
                columns: new[] { "CreatedOn", "StartDate" },
                values: new object[] { new DateTime(2023, 1, 5, 13, 23, 7, 231, DateTimeKind.Utc).AddTicks(6154), new DateTime(2023, 1, 5, 13, 23, 7, 231, DateTimeKind.Utc).AddTicks(6152) });

            migrationBuilder.UpdateData(
                table: "ProjectGlobalScheduleConfiguration",
                keyColumn: "ProjectGlobalScheduleConfigurationId",
                keyValue: 2,
                columns: new[] { "CreatedOn", "StartDate" },
                values: new object[] { new DateTime(2023, 1, 5, 13, 23, 7, 231, DateTimeKind.Utc).AddTicks(6159), new DateTime(2023, 1, 5, 13, 23, 7, 231, DateTimeKind.Utc).AddTicks(6158) });

            migrationBuilder.UpdateData(
                table: "ProjectGlobalScheduleConfiguration",
                keyColumn: "ProjectGlobalScheduleConfigurationId",
                keyValue: 3,
                columns: new[] { "CreatedOn", "StartDate" },
                values: new object[] { new DateTime(2023, 1, 5, 13, 23, 7, 231, DateTimeKind.Utc).AddTicks(6161), new DateTime(2023, 1, 5, 13, 23, 7, 231, DateTimeKind.Utc).AddTicks(6161) });

            migrationBuilder.UpdateData(
                table: "ProjectStep",
                keyColumn: "ProjectStepId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 232, DateTimeKind.Utc).AddTicks(390));

            migrationBuilder.UpdateData(
                table: "ProjectStep",
                keyColumn: "ProjectStepId",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 232, DateTimeKind.Utc).AddTicks(2495));

            migrationBuilder.UpdateData(
                table: "ProjectStep",
                keyColumn: "ProjectStepId",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 232, DateTimeKind.Utc).AddTicks(4299));

            migrationBuilder.UpdateData(
                table: "ProjectStep",
                keyColumn: "ProjectStepId",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 232, DateTimeKind.Utc).AddTicks(6076));

            migrationBuilder.UpdateData(
                table: "Setting",
                keyColumn: "SettingId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 211, DateTimeKind.Utc).AddTicks(2613));

            migrationBuilder.UpdateData(
                table: "Setting",
                keyColumn: "SettingId",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 211, DateTimeKind.Utc).AddTicks(4743));

            migrationBuilder.UpdateData(
                table: "Setting",
                keyColumn: "SettingId",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 211, DateTimeKind.Utc).AddTicks(6638));

            migrationBuilder.UpdateData(
                table: "Setting",
                keyColumn: "SettingId",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 23, 7, 211, DateTimeKind.Utc).AddTicks(8452));
            */
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkspacePath",
                table: "ProjectConfiguration",
                newName: "AbsoluteWorkspacePath");

            /*
            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 35, DateTimeKind.Utc).AddTicks(4897));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 35, DateTimeKind.Utc).AddTicks(6806));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 35, DateTimeKind.Utc).AddTicks(8560));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 36, DateTimeKind.Utc).AddTicks(240));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 36, DateTimeKind.Utc).AddTicks(1896));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 36, DateTimeKind.Utc).AddTicks(3548));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 36, DateTimeKind.Utc).AddTicks(5197));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 36, DateTimeKind.Utc).AddTicks(6835));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 36, DateTimeKind.Utc).AddTicks(8490));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 10,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 37, DateTimeKind.Utc).AddTicks(142));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 11,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 37, DateTimeKind.Utc).AddTicks(1772));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 12,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 37, DateTimeKind.Utc).AddTicks(3412));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 13,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 37, DateTimeKind.Utc).AddTicks(5047));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 14,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 37, DateTimeKind.Utc).AddTicks(6683));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 15,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 37, DateTimeKind.Utc).AddTicks(8319));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 37, DateTimeKind.Utc).AddTicks(9960));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 17,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 38, DateTimeKind.Utc).AddTicks(1597));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 18,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 38, DateTimeKind.Utc).AddTicks(3224));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 19,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 38, DateTimeKind.Utc).AddTicks(4860));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 20,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 38, DateTimeKind.Utc).AddTicks(6483));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 21,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 38, DateTimeKind.Utc).AddTicks(8109));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 22,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 38, DateTimeKind.Utc).AddTicks(9754));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 23,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 39, DateTimeKind.Utc).AddTicks(1374));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 24,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 39, DateTimeKind.Utc).AddTicks(3018));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 25,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 39, DateTimeKind.Utc).AddTicks(4689));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 26,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 39, DateTimeKind.Utc).AddTicks(6314));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 27,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 39, DateTimeKind.Utc).AddTicks(7935));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 28,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 39, DateTimeKind.Utc).AddTicks(9575));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 29,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 40, DateTimeKind.Utc).AddTicks(1198));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 30,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 40, DateTimeKind.Utc).AddTicks(2840));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 31,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 40, DateTimeKind.Utc).AddTicks(4468));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 32,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 40, DateTimeKind.Utc).AddTicks(6087));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 33,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 40, DateTimeKind.Utc).AddTicks(7728));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 34,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 40, DateTimeKind.Utc).AddTicks(9379));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 35,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 41, DateTimeKind.Utc).AddTicks(1013));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 36,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 41, DateTimeKind.Utc).AddTicks(2659));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 37,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 41, DateTimeKind.Utc).AddTicks(4290));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 38,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 41, DateTimeKind.Utc).AddTicks(5914));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 39,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 41, DateTimeKind.Utc).AddTicks(7536));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 40,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 41, DateTimeKind.Utc).AddTicks(9178));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 41,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 42, DateTimeKind.Utc).AddTicks(812));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 42,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 42, DateTimeKind.Utc).AddTicks(2434));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 43,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 42, DateTimeKind.Utc).AddTicks(4072));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 44,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 42, DateTimeKind.Utc).AddTicks(5695));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 45,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 42, DateTimeKind.Utc).AddTicks(7347));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 46,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 42, DateTimeKind.Utc).AddTicks(8998));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 47,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 43, DateTimeKind.Utc).AddTicks(673));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 48,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 43, DateTimeKind.Utc).AddTicks(2311));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 49,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 43, DateTimeKind.Utc).AddTicks(3949));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 50,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 43, DateTimeKind.Utc).AddTicks(5585));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 51,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 43, DateTimeKind.Utc).AddTicks(7214));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 52,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 43, DateTimeKind.Utc).AddTicks(8847));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 53,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 44, DateTimeKind.Utc).AddTicks(509));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 54,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 44, DateTimeKind.Utc).AddTicks(2176));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 55,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 44, DateTimeKind.Utc).AddTicks(3843));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 56,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 44, DateTimeKind.Utc).AddTicks(5477));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 57,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 44, DateTimeKind.Utc).AddTicks(7108));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 58,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 44, DateTimeKind.Utc).AddTicks(8734));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 59,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 45, DateTimeKind.Utc).AddTicks(390));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 60,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 45, DateTimeKind.Utc).AddTicks(2032));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 61,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 45, DateTimeKind.Utc).AddTicks(3691));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 62,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 45, DateTimeKind.Utc).AddTicks(5326));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 63,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 45, DateTimeKind.Utc).AddTicks(6971));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 64,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 45, DateTimeKind.Utc).AddTicks(8602));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 65,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 46, DateTimeKind.Utc).AddTicks(266));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 66,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 46, DateTimeKind.Utc).AddTicks(2332));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 67,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 46, DateTimeKind.Utc).AddTicks(3976));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 68,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 46, DateTimeKind.Utc).AddTicks(5654));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 69,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 46, DateTimeKind.Utc).AddTicks(7301));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 70,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 46, DateTimeKind.Utc).AddTicks(8956));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 71,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 47, DateTimeKind.Utc).AddTicks(592));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 72,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 47, DateTimeKind.Utc).AddTicks(2229));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 73,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 47, DateTimeKind.Utc).AddTicks(3866));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 74,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 47, DateTimeKind.Utc).AddTicks(5493));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 75,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 47, DateTimeKind.Utc).AddTicks(7126));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 76,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 47, DateTimeKind.Utc).AddTicks(8760));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 77,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 48, DateTimeKind.Utc).AddTicks(408));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 78,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 48, DateTimeKind.Utc).AddTicks(2041));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 79,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 48, DateTimeKind.Utc).AddTicks(3676));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 80,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 48, DateTimeKind.Utc).AddTicks(5303));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 81,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 48, DateTimeKind.Utc).AddTicks(6944));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 82,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 48, DateTimeKind.Utc).AddTicks(8617));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 83,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 49, DateTimeKind.Utc).AddTicks(277));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 84,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 49, DateTimeKind.Utc).AddTicks(1907));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 85,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 49, DateTimeKind.Utc).AddTicks(3541));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 86,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 49, DateTimeKind.Utc).AddTicks(5174));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 87,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 49, DateTimeKind.Utc).AddTicks(6800));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 88,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 49, DateTimeKind.Utc).AddTicks(8440));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 89,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 50, DateTimeKind.Utc).AddTicks(94));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 90,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 50, DateTimeKind.Utc).AddTicks(1774));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 91,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 50, DateTimeKind.Utc).AddTicks(3414));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 92,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 50, DateTimeKind.Utc).AddTicks(5054));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 93,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 50, DateTimeKind.Utc).AddTicks(6689));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 94,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 50, DateTimeKind.Utc).AddTicks(8321));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 95,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 50, DateTimeKind.Utc).AddTicks(9974));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 96,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 51, DateTimeKind.Utc).AddTicks(1607));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 97,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 51, DateTimeKind.Utc).AddTicks(3239));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 98,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 51, DateTimeKind.Utc).AddTicks(4875));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 99,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 51, DateTimeKind.Utc).AddTicks(6553));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 100,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 51, DateTimeKind.Utc).AddTicks(8191));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 101,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 51, DateTimeKind.Utc).AddTicks(9843));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 102,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 52, DateTimeKind.Utc).AddTicks(1482));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 103,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 52, DateTimeKind.Utc).AddTicks(3124));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 104,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 52, DateTimeKind.Utc).AddTicks(4787));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 105,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 52, DateTimeKind.Utc).AddTicks(6435));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 106,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 52, DateTimeKind.Utc).AddTicks(8067));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 107,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 52, DateTimeKind.Utc).AddTicks(9724));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 108,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 53, DateTimeKind.Utc).AddTicks(1360));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 109,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 53, DateTimeKind.Utc).AddTicks(2993));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 110,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 53, DateTimeKind.Utc).AddTicks(4632));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 111,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 53, DateTimeKind.Utc).AddTicks(6261));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 112,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 53, DateTimeKind.Utc).AddTicks(7898));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 113,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 53, DateTimeKind.Utc).AddTicks(9598));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 114,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 54, DateTimeKind.Utc).AddTicks(1250));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 115,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 54, DateTimeKind.Utc).AddTicks(2886));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 116,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 54, DateTimeKind.Utc).AddTicks(4525));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 117,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 54, DateTimeKind.Utc).AddTicks(6162));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 118,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 54, DateTimeKind.Utc).AddTicks(7800));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 119,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 54, DateTimeKind.Utc).AddTicks(9454));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 120,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 55, DateTimeKind.Utc).AddTicks(1146));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 121,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 55, DateTimeKind.Utc).AddTicks(2798));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 122,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 55, DateTimeKind.Utc).AddTicks(4435));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 123,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 55, DateTimeKind.Utc).AddTicks(6067));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 124,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 55, DateTimeKind.Utc).AddTicks(7703));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 125,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 55, DateTimeKind.Utc).AddTicks(9357));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 126,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 56, DateTimeKind.Utc).AddTicks(996));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 127,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 56, DateTimeKind.Utc).AddTicks(2634));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 128,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 56, DateTimeKind.Utc).AddTicks(4264));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 129,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 56, DateTimeKind.Utc).AddTicks(5910));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 130,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 56, DateTimeKind.Utc).AddTicks(7554));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 131,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 56, DateTimeKind.Utc).AddTicks(9211));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 132,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 57, DateTimeKind.Utc).AddTicks(845));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 133,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 57, DateTimeKind.Utc).AddTicks(2485));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 134,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 57, DateTimeKind.Utc).AddTicks(4136));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 135,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 57, DateTimeKind.Utc).AddTicks(5770));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 136,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 57, DateTimeKind.Utc).AddTicks(7449));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 137,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 57, DateTimeKind.Utc).AddTicks(9112));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 138,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 58, DateTimeKind.Utc).AddTicks(755));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 139,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 58, DateTimeKind.Utc).AddTicks(2479));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 140,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 58, DateTimeKind.Utc).AddTicks(4129));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 141,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 58, DateTimeKind.Utc).AddTicks(5767));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 142,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 58, DateTimeKind.Utc).AddTicks(7406));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 143,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 58, DateTimeKind.Utc).AddTicks(9165));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 144,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 59, DateTimeKind.Utc).AddTicks(808));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 145,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 59, DateTimeKind.Utc).AddTicks(2464));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 146,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 59, DateTimeKind.Utc).AddTicks(4105));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 147,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 59, DateTimeKind.Utc).AddTicks(5823));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 148,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 59, DateTimeKind.Utc).AddTicks(7473));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 149,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 59, DateTimeKind.Utc).AddTicks(9292));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 150,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 60, DateTimeKind.Utc).AddTicks(937));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 151,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 60, DateTimeKind.Utc).AddTicks(2582));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 152,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 60, DateTimeKind.Utc).AddTicks(4222));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 153,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 60, DateTimeKind.Utc).AddTicks(5878));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 154,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 60, DateTimeKind.Utc).AddTicks(7530));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 155,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 60, DateTimeKind.Utc).AddTicks(9190));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 156,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 61, DateTimeKind.Utc).AddTicks(845));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 157,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 61, DateTimeKind.Utc).AddTicks(2487));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 158,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 61, DateTimeKind.Utc).AddTicks(4125));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 159,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 61, DateTimeKind.Utc).AddTicks(5820));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 160,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 61, DateTimeKind.Utc).AddTicks(7459));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 161,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 61, DateTimeKind.Utc).AddTicks(9109));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 162,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 62, DateTimeKind.Utc).AddTicks(750));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 163,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 62, DateTimeKind.Utc).AddTicks(2406));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 164,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 62, DateTimeKind.Utc).AddTicks(4071));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 165,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 62, DateTimeKind.Utc).AddTicks(6520));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 166,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 62, DateTimeKind.Utc).AddTicks(8175));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 167,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 62, DateTimeKind.Utc).AddTicks(9827));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 168,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 63, DateTimeKind.Utc).AddTicks(1475));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 169,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 63, DateTimeKind.Utc).AddTicks(3134));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 170,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 63, DateTimeKind.Utc).AddTicks(5163));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 171,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 63, DateTimeKind.Utc).AddTicks(6812));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 172,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 63, DateTimeKind.Utc).AddTicks(8447));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 173,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 64, DateTimeKind.Utc).AddTicks(100));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 174,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 64, DateTimeKind.Utc).AddTicks(1744));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 175,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 64, DateTimeKind.Utc).AddTicks(3377));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 176,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 64, DateTimeKind.Utc).AddTicks(5046));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 177,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 64, DateTimeKind.Utc).AddTicks(6769));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 178,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 64, DateTimeKind.Utc).AddTicks(8406));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 179,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 65, DateTimeKind.Utc).AddTicks(41));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 180,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 65, DateTimeKind.Utc).AddTicks(1690));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 181,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 65, DateTimeKind.Utc).AddTicks(3374));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 182,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 65, DateTimeKind.Utc).AddTicks(5033));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 183,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 65, DateTimeKind.Utc).AddTicks(6694));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 184,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 65, DateTimeKind.Utc).AddTicks(8337));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 185,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 65, DateTimeKind.Utc).AddTicks(9984));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 186,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 66, DateTimeKind.Utc).AddTicks(1625));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 187,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 66, DateTimeKind.Utc).AddTicks(3271));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 188,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 66, DateTimeKind.Utc).AddTicks(4929));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 189,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 66, DateTimeKind.Utc).AddTicks(6587));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 190,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 66, DateTimeKind.Utc).AddTicks(8243));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 191,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 66, DateTimeKind.Utc).AddTicks(9896));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 192,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 67, DateTimeKind.Utc).AddTicks(1539));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 193,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 67, DateTimeKind.Utc).AddTicks(3186));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 194,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 67, DateTimeKind.Utc).AddTicks(4855));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 195,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 67, DateTimeKind.Utc).AddTicks(6501));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 196,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 67, DateTimeKind.Utc).AddTicks(8153));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 197,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 67, DateTimeKind.Utc).AddTicks(9806));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 198,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 68, DateTimeKind.Utc).AddTicks(1448));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 199,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 68, DateTimeKind.Utc).AddTicks(3103));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 200,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 68, DateTimeKind.Utc).AddTicks(4779));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 201,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 68, DateTimeKind.Utc).AddTicks(6428));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 202,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 68, DateTimeKind.Utc).AddTicks(8082));

            migrationBuilder.UpdateData(
                table: "Currency",
                keyColumn: "CurrencyId",
                keyValue: 203,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 68, DateTimeKind.Utc).AddTicks(9733));

            migrationBuilder.UpdateData(
                table: "CurrencyPair",
                keyColumn: "CurrencyPairId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 16, DateTimeKind.Utc).AddTicks(4751));

            migrationBuilder.UpdateData(
                table: "CurrencyPair",
                keyColumn: "CurrencyPairId",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 16, DateTimeKind.Utc).AddTicks(6750));

            migrationBuilder.UpdateData(
                table: "CurrencyPeriod",
                keyColumn: "CurrencyPeriodId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 17, DateTimeKind.Utc).AddTicks(564));

            migrationBuilder.UpdateData(
                table: "CurrencyPeriod",
                keyColumn: "CurrencyPeriodId",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 17, DateTimeKind.Utc).AddTicks(2581));

            migrationBuilder.UpdateData(
                table: "CurrencyPeriod",
                keyColumn: "CurrencyPeriodId",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 17, DateTimeKind.Utc).AddTicks(4331));

            migrationBuilder.UpdateData(
                table: "CurrencyPeriod",
                keyColumn: "CurrencyPeriodId",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 17, DateTimeKind.Utc).AddTicks(6030));

            migrationBuilder.UpdateData(
                table: "CurrencyPeriod",
                keyColumn: "CurrencyPeriodId",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 17, DateTimeKind.Utc).AddTicks(7707));

            migrationBuilder.UpdateData(
                table: "CurrencyPeriod",
                keyColumn: "CurrencyPeriodId",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 17, DateTimeKind.Utc).AddTicks(9386));

            migrationBuilder.UpdateData(
                table: "CurrencyPeriod",
                keyColumn: "CurrencyPeriodId",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 18, DateTimeKind.Utc).AddTicks(1074));

            migrationBuilder.UpdateData(
                table: "CurrencyPeriod",
                keyColumn: "CurrencyPeriodId",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 18, DateTimeKind.Utc).AddTicks(2748));

            migrationBuilder.UpdateData(
                table: "CurrencyPeriod",
                keyColumn: "CurrencyPeriodId",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 18, DateTimeKind.Utc).AddTicks(4423));

            migrationBuilder.UpdateData(
                table: "CurrencySpread",
                keyColumn: "CurrencySpreadId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 18, DateTimeKind.Utc).AddTicks(7072));

            migrationBuilder.UpdateData(
                table: "CurrencySpread",
                keyColumn: "CurrencySpreadId",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 18, DateTimeKind.Utc).AddTicks(8908));

            migrationBuilder.UpdateData(
                table: "CurrencySpread",
                keyColumn: "CurrencySpreadId",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 19, DateTimeKind.Utc).AddTicks(649));

            migrationBuilder.UpdateData(
                table: "CurrencySpread",
                keyColumn: "CurrencySpreadId",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 19, DateTimeKind.Utc).AddTicks(2296));

            migrationBuilder.UpdateData(
                table: "CurrencySpread",
                keyColumn: "CurrencySpreadId",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 19, DateTimeKind.Utc).AddTicks(3968));

            migrationBuilder.UpdateData(
                table: "CurrencySpread",
                keyColumn: "CurrencySpreadId",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 19, DateTimeKind.Utc).AddTicks(5639));

            migrationBuilder.UpdateData(
                table: "CurrencySpread",
                keyColumn: "CurrencySpreadId",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 19, DateTimeKind.Utc).AddTicks(7262));

            migrationBuilder.UpdateData(
                table: "CurrencySpread",
                keyColumn: "CurrencySpreadId",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 19, DateTimeKind.Utc).AddTicks(8950));

            migrationBuilder.UpdateData(
                table: "CurrencySpread",
                keyColumn: "CurrencySpreadId",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 20, DateTimeKind.Utc).AddTicks(609));

            migrationBuilder.UpdateData(
                table: "CurrencySpread",
                keyColumn: "CurrencySpreadId",
                keyValue: 10,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 23, DateTimeKind.Utc).AddTicks(1112));

            migrationBuilder.UpdateData(
                table: "CurrencySpread",
                keyColumn: "CurrencySpreadId",
                keyValue: 11,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 23, DateTimeKind.Utc).AddTicks(2913));

            migrationBuilder.UpdateData(
                table: "CurrencySpread",
                keyColumn: "CurrencySpreadId",
                keyValue: 12,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 23, DateTimeKind.Utc).AddTicks(4571));

            migrationBuilder.UpdateData(
                table: "CurrencySpread",
                keyColumn: "CurrencySpreadId",
                keyValue: 13,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 23, DateTimeKind.Utc).AddTicks(6181));

            migrationBuilder.UpdateData(
                table: "CurrencySpread",
                keyColumn: "CurrencySpreadId",
                keyValue: 14,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 23, DateTimeKind.Utc).AddTicks(7787));

            migrationBuilder.UpdateData(
                table: "CurrencySpread",
                keyColumn: "CurrencySpreadId",
                keyValue: 15,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 23, DateTimeKind.Utc).AddTicks(9393));

            migrationBuilder.UpdateData(
                table: "EntityType",
                keyColumn: "EntityTypeId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 13, DateTimeKind.Utc).AddTicks(1029));

            migrationBuilder.UpdateData(
                table: "EntityType",
                keyColumn: "EntityTypeId",
                keyValue: 400,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 14, DateTimeKind.Utc).AddTicks(1231));

            migrationBuilder.UpdateData(
                table: "EntityType",
                keyColumn: "EntityTypeId",
                keyValue: 500,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 14, DateTimeKind.Utc).AddTicks(3079));

            migrationBuilder.UpdateData(
                table: "EntityType",
                keyColumn: "EntityTypeId",
                keyValue: 501,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 14, DateTimeKind.Utc).AddTicks(4788));

            migrationBuilder.UpdateData(
                table: "EntityType",
                keyColumn: "EntityTypeId",
                keyValue: 502,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 14, DateTimeKind.Utc).AddTicks(6443));

            migrationBuilder.UpdateData(
                table: "EntityType",
                keyColumn: "EntityTypeId",
                keyValue: 503,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 14, DateTimeKind.Utc).AddTicks(8132));

            migrationBuilder.UpdateData(
                table: "EntityType",
                keyColumn: "EntityTypeId",
                keyValue: 504,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 14, DateTimeKind.Utc).AddTicks(9879));

            migrationBuilder.UpdateData(
                table: "EntityType",
                keyColumn: "EntityTypeId",
                keyValue: 505,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 15, DateTimeKind.Utc).AddTicks(1567));

            migrationBuilder.UpdateData(
                table: "Market",
                keyColumn: "MarketId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 24, DateTimeKind.Utc).AddTicks(2100));

            migrationBuilder.UpdateData(
                table: "MarketRegion",
                keyColumn: "MarketRegionId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 24, DateTimeKind.Utc).AddTicks(4810));

            migrationBuilder.UpdateData(
                table: "MarketRegion",
                keyColumn: "MarketRegionId",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 24, DateTimeKind.Utc).AddTicks(6656));

            migrationBuilder.UpdateData(
                table: "MarketRegion",
                keyColumn: "MarketRegionId",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 24, DateTimeKind.Utc).AddTicks(8277));

            migrationBuilder.UpdateData(
                table: "Organization",
                keyColumn: "OrganizationId",
                keyValue: "22222222-2222-2222-2222-222222222222",
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 33, DateTimeKind.Utc).AddTicks(751));

            migrationBuilder.UpdateData(
                table: "ProjectGlobalConfiguration",
                keyColumn: "ProjectGlobalConfigurationId",
                keyValue: 1,
                columns: new[] { "CreatedOn", "StartDate" },
                values: new object[] { new DateTime(2023, 1, 5, 13, 18, 49, 34, DateTimeKind.Utc).AddTicks(1193), new DateTime(2023, 1, 5, 13, 18, 49, 34, DateTimeKind.Utc).AddTicks(713) });

            migrationBuilder.UpdateData(
                table: "ProjectGlobalScheduleConfiguration",
                keyColumn: "ProjectGlobalScheduleConfigurationId",
                keyValue: 1,
                columns: new[] { "CreatedOn", "StartDate" },
                values: new object[] { new DateTime(2023, 1, 5, 13, 18, 49, 34, DateTimeKind.Utc).AddTicks(2700), new DateTime(2023, 1, 5, 13, 18, 49, 34, DateTimeKind.Utc).AddTicks(2697) });

            migrationBuilder.UpdateData(
                table: "ProjectGlobalScheduleConfiguration",
                keyColumn: "ProjectGlobalScheduleConfigurationId",
                keyValue: 2,
                columns: new[] { "CreatedOn", "StartDate" },
                values: new object[] { new DateTime(2023, 1, 5, 13, 18, 49, 34, DateTimeKind.Utc).AddTicks(2705), new DateTime(2023, 1, 5, 13, 18, 49, 34, DateTimeKind.Utc).AddTicks(2704) });

            migrationBuilder.UpdateData(
                table: "ProjectGlobalScheduleConfiguration",
                keyColumn: "ProjectGlobalScheduleConfigurationId",
                keyValue: 3,
                columns: new[] { "CreatedOn", "StartDate" },
                values: new object[] { new DateTime(2023, 1, 5, 13, 18, 49, 34, DateTimeKind.Utc).AddTicks(2707), new DateTime(2023, 1, 5, 13, 18, 49, 34, DateTimeKind.Utc).AddTicks(2706) });

            migrationBuilder.UpdateData(
                table: "ProjectStep",
                keyColumn: "ProjectStepId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 34, DateTimeKind.Utc).AddTicks(6311));

            migrationBuilder.UpdateData(
                table: "ProjectStep",
                keyColumn: "ProjectStepId",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 34, DateTimeKind.Utc).AddTicks(8223));

            migrationBuilder.UpdateData(
                table: "ProjectStep",
                keyColumn: "ProjectStepId",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 34, DateTimeKind.Utc).AddTicks(9922));

            migrationBuilder.UpdateData(
                table: "ProjectStep",
                keyColumn: "ProjectStepId",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 35, DateTimeKind.Utc).AddTicks(1557));

            migrationBuilder.UpdateData(
                table: "Setting",
                keyColumn: "SettingId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 15, DateTimeKind.Utc).AddTicks(4818));

            migrationBuilder.UpdateData(
                table: "Setting",
                keyColumn: "SettingId",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 15, DateTimeKind.Utc).AddTicks(6694));

            migrationBuilder.UpdateData(
                table: "Setting",
                keyColumn: "SettingId",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 15, DateTimeKind.Utc).AddTicks(8412));

            migrationBuilder.UpdateData(
                table: "Setting",
                keyColumn: "SettingId",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 1, 5, 13, 18, 49, 16, DateTimeKind.Utc).AddTicks(175));
            */
        }
    }
}

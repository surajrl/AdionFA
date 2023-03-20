using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Adion.FA.Infrastructure.Core.Data.Migrations.AdionSecurityDb
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoreUserType",
                columns: table => new
                {
                    UserTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    Inaccesible = table.Column<bool>(type: "INTEGER", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedById = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedById = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Code = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoreUserType", x => x.UserTypeId);
                });

            migrationBuilder.CreateTable(
                name: "CoreUser",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    UserTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    AccessDisabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false),
                    LastAccessFailed = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastAccess = table.Column<DateTime>(type: "TEXT", nullable: true),
                    HashCodeResetPassword = table.Column<string>(type: "TEXT", nullable: true),
                    ConfirmedEmail = table.Column<bool>(type: "INTEGER", nullable: false),
                    ConfirmationToken = table.Column<string>(type: "TEXT", nullable: true),
                    CodeOfConductRead = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    Inaccesible = table.Column<bool>(type: "INTEGER", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedById = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedById = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoreUser", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_CoreUser_CoreUserType_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "CoreUserType",
                        principalColumn: "UserTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CoreUserType",
                columns: new[] { "UserTypeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 1, "Employee", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2022, 9, 29, 19, 17, 37, 95, DateTimeKind.Utc).AddTicks(554), "Employee", false, false, "Employee", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "CoreUser",
                columns: new[] { "UserId", "AccessDisabled", "AccessFailedCount", "CodeOfConductRead", "ConfirmationToken", "ConfirmedEmail", "CreatedById", "CreatedByUserName", "CreatedOn", "Email", "HashCodeResetPassword", "Inaccesible", "IsDeleted", "LastAccess", "LastAccessFailed", "Password", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "UserName", "UserTypeId" },
                values: new object[] { "11111111-1111-1111-11111111111111111", false, 0, false, null, false, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2022, 9, 29, 19, 17, 37, 96, DateTimeKind.Utc).AddTicks(8002), "admin@adionteam.com", null, false, false, null, null, "Pa$$W0rd", "22222222-2222-2222-2222-222222222222", null, null, null, "sysadmin", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_CoreUser_UserName",
                table: "CoreUser",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoreUser_UserTypeId",
                table: "CoreUser",
                column: "UserTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoreUser");

            migrationBuilder.DropTable(
                name: "CoreUserType");
        }
    }
}
